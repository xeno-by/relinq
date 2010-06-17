using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Relinq.Exceptions.Core;
using Relinq.Exceptions.JSToCSharp;
using Relinq.Exceptions.JSToCSharp.TypeInference;
using Relinq.Exceptions.JSToCSharp.TypeInference.MethodResolution;
using Relinq.Helpers.Collections;
using Relinq.Helpers.Reflection;
using Relinq.Script.Compilation;
using Relinq.Script.Semantics.Casts;
using Relinq.Script.Semantics.TypeSystem;
using Relinq.Script.Compilation.SignatureValidation.Core;
using Relinq.Script.Syntax.Expressions;

namespace Relinq.Script.Semantics.TypeInference
{
    // http://code.google.com/p/relinq/wiki/TypeInferenceMethods
    public static class TypeInferenceMethods
    {
        // todo. resolve the method group -> delegate cast
        // and also provide generic arg inference altogether

        public static ResolvedInvocation Resolve(this MethodGroup mg, TypeInferenceContext ctx)
        {
            var failboats = new Dictionary<MethodInfo, MethodResolutionException>();
            var infToOrig = new Dictionary<MethodInfo, MethodInfo>();
            var origToInf = new Dictionary<MethodInfo, MethodInfo>();

            try
            {
                // Step 1. Get rid of alternatives that don't have a form that suits arg count.
                var suitableAlts = new List<MethodInfo>();
                foreach (var alt in mg.Alts)
                {
                    if (alt.XArgsCount().Contains(ctx.Root.XArgsCount()))
                    {
                        suitableAlts.Add(alt);
                    }
                    else
                    {
                        failboats.Add(alt, new MethodMatchingException(
                            MethodMatchingExceptionType.ArgcMismatch, alt, null, ctx));
                    }
                }

                // Step 2. Infer generic arguments of all open generic alternatives and get rid of those who fail to be inferred.
                var inferredAlts = new List<MethodInfo>();
                foreach (var salt in suitableAlts)
                {
                    try
                    {
                        var infAlt = salt.InferGenericTypeParams(ctx);

                        inferredAlts.Add(infAlt);
                        origToInf.Add(salt, infAlt);
                        infToOrig.Add(infAlt, salt);
                    }
                    catch (GenericArgumentsInferenceException gex)
                    {
                        if (gex.IsUnexpected)
                        {
                            throw;
                        }
                        else
                        {
                            failboats.Add(salt, new MethodMatchingException(
                                MethodMatchingExceptionType.GenericArgInferenceFailed, salt, null, ctx, gex));
                        }
                    }
                }

                // Step 3. Store type inference context of all alternatives that had made it so far
                var sigs = new List<ResolvedSignature>();
                foreach (var infAlt in inferredAlts)
                {
                    var infAlt1 = infAlt;
                    var resolvedCtx = new TypeInferenceEngine(ctx);

                    ctx.Root.CallArgs().ForEach((callArg, i) =>
                    {
                        if (callArg is LambdaExpression)
                        {
                            var inferredFormal = infAlt1.IsExtension()
                                ? infAlt1.GetParameters()[i + 1].ParameterType
                                : infAlt1.GetParameters()[i].ParameterType;

                            var inferredLambda = new Lambda(
                                (LambdaExpression)callArg, inferredFormal.NormalizeFunctionType());
                            resolvedCtx.Ctx.Inferences.Add(callArg, inferredLambda);
                        }

                        resolvedCtx.InferTypes(callArg);
                    });

                    sigs.Add(new ResolvedSignature(resolvedCtx.Ctx, infAlt));
                }

                // Step 4. Now get rid of alternatives that do not match Relinq-specific validation
                var validSigs = new List<ResolvedSignature>();
                foreach (var sig in sigs)
                {
                    var args = ctx.Root.ToXArgs(sig.Inferences);
                    if (sig.Signature.IsValid(sig.Signature.IsExtension() ? args : args.Skip(1)))
                    {
                        validSigs.Add(sig);
                    }
                    else
                    {
                        failboats.Add(infToOrig[sig.Signature], new MethodMatchingException(
                            MethodMatchingExceptionType.SignatureFailsMetaConstrains, infToOrig[sig.Signature], sig.Signature, ctx));
                    }
                }

                // Step 5. Eliminate signatures that do not satisfy actual argument conversions
                var invs = new List<ResolvedInvocation>();
                foreach (var validSig in validSigs)
                {
                    var args = ctx.Root.ToXArgs(validSig.Inferences);
                    var @params = validSig.Signature.ToXArgs(ctx.Root.XArgsCount());
                    var inv = new ResolvedInvocation(validSig, args.Zip(@params, (arg, param) => Cast.Lookup(arg, param)));

                    var fail = Array.FindIndex(inv.Casts.ToArray(), c => c == null);
                    if (fail == -1)
                    {
                        invs.Add(inv);
                    }
                    else
                    {
                        failboats.Add(infToOrig[validSig.Signature], new MethodMatchingException(
                            MethodMatchingExceptionType.ActualArgCannotBeCastToFormalArg, infToOrig[validSig.Signature], validSig.Signature, ctx, fail));
                    }
                }

                // Step 6. Remove sub-par signatures from the all-satisfying survivors
                var res = new List<ResolvedInvocation>();
                foreach (var inv in invs)
                {
                    var subpar = false;
                    foreach (var anotherInv in invs)
                    {
                        subpar |= anotherInv > inv;
                    }

                    if (!subpar)
                    {
                        res.Add(inv);
                    }
                    else
                    {
                        failboats.Add(infToOrig[inv.Signature], new MethodMatchingException(
                            MethodMatchingExceptionType.SignatureIsSubPar, infToOrig[inv.Signature], inv.Signature, ctx));
                    }
                }

                // Step 7. Finalize the death race (and evade the final trap)
                switch (res.Count())
                {
                    case 0:
                        throw new MethodGroupResolutionFailedException(
                            JSToCSharpExceptionType.FruitlessMethodGroupResolution, mg, failboats, ctx);

                    case 1:
                        var winrar = res.Single();
                        if (winrar.Signature.IsTrap())
                        {
                            failboats.Add(infToOrig[winrar.Signature], new MethodMatchingException(
                                MethodMatchingExceptionType.SignatureIsEntrapped, infToOrig[winrar.Signature], winrar.Signature, ctx));

                            throw new MethodGroupResolutionFailedException(
                                JSToCSharpExceptionType.FruitlessMethodGroupResolution, mg, failboats, ctx);
                        }

                        // my sincere congratulations
                        return winrar;

                    default:
                        foreach (var winrar2 in res)
                        {
                            failboats.Add(infToOrig[winrar2.Signature], new MethodMatchingException(
                                MethodMatchingExceptionType.SignatureIsOkButAmbiguous, infToOrig[winrar2.Signature], winrar2.Signature, ctx));
                        }

                        throw new MethodGroupResolutionFailedException(
                            JSToCSharpExceptionType.AmbiguousMethodGroupResolution, mg, failboats, ctx);
                }
            }
            catch (MethodGroupResolutionFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MethodGroupResolutionFailedException(
                    JSToCSharpExceptionType.Unexpected, mg, failboats, ctx, ex);
            }
        }
    }
}
