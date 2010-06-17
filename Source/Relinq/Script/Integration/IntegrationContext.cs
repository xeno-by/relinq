using System;
using System.Collections.Generic;
using System.Linq;
using Relinq.Exceptions.Integration;
using Relinq.Helpers.Assurance;

namespace Relinq.Script.Integration
{
    public class IntegrationContext
    {
        public bool AllowDuplicateRules { get; set; }

        private Dictionary<Func<Object, bool>, Func<Object, String>> _jsFactories =
            new Dictionary<Func<Object, bool>, Func<Object, String>>();
        private Dictionary<Func<String, bool>, Func<String, Object>> _csharpFactories =
            new Dictionary<Func<String, bool>, Func<String, Object>>();

        public IntegrationContext RegisterCSharp(Type csharpType, String jsName)
        {
            return RegisterCSharp(o => o == csharpType, o => jsName);
        }

        public IntegrationContext RegisterCSharp(Func<Object, bool> csharpTest, Func<Object, String> jsFactory)
        {
            _jsFactories.Add(csharpTest, jsFactory);
            return this;
        }

        public IntegrationContext RegisterJS(String jsName, Object csharpObject)
        {
            return RegisterJS(s => s == jsName, s => csharpObject);
        }

        public IntegrationContext RegisterJS(Func<String, bool> jsTest, Func<String, Object> csharpFactory)
        {
            _csharpFactories.Add(jsTest, csharpFactory);
            return this;
        }

        public bool IsRegisteredCSharp(Object csharpObject)
        {
            return _jsFactories.Keys.Any(csharpTest => csharpTest(csharpObject));
        }

        public bool IsRegisteredJS(String jsName)
        {
            return _csharpFactories.Keys.Any(jsTest => jsTest(jsName));
        }

        public String ProduceJS(Object csharpObject)
        {
            try
            {
                if (IsRegisteredCSharp(csharpObject))
                {
                    var applicable = _jsFactories.Where(kvp => kvp.Key(csharpObject));

                    if (!AllowDuplicateRules && applicable.Count() > 1)
                    {
                        throw new CSharpToJSIntegrationException(
                            IntegrationExceptionType.MultipleSuitableRules, this, csharpObject);
                    }
                    else
                    {
                        var factory = applicable.FirstOrDefault().Value.SurelyNotNull();

                        try
                        {
                            return factory(csharpObject);
                        }
                        catch (Exception ex)
                        {
                            throw new CSharpToJSIntegrationException(IntegrationExceptionType.MappingFailed, this, csharpObject, ex);
                        }
                    }
                }
                else
                {
                    throw new CSharpToJSIntegrationException(
                        IntegrationExceptionType.NoSuitableRule, this, csharpObject);
                }
            }
            catch (CSharpToJSIntegrationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CSharpToJSIntegrationException(
                    IntegrationExceptionType.Unexpected, this, csharpObject, ex);
            }
        }

        public Object ProduceCSharp(String jsName)
        {
            try
            {
                if (IsRegisteredJS(jsName))
                {
                    var applicable = _csharpFactories.Where(kvp => kvp.Key(jsName));

                    if (!AllowDuplicateRules && applicable.Count() > 1)
                    {
                        throw new JSToCSharpIntegrationException(
                            IntegrationExceptionType.MultipleSuitableRules, this, jsName);
                    }
                    else
                    {
                        var factory = applicable.FirstOrDefault().Value.SurelyNotNull();

                        try
                        {
                            return factory(jsName);
                        }
                        catch (Exception ex)
                        {
                            throw new JSToCSharpIntegrationException(IntegrationExceptionType.MappingFailed, this, jsName, ex);
                        }
                    }
                }
                else
                {
                    throw new JSToCSharpIntegrationException(
                        IntegrationExceptionType.NoSuitableRule, this, jsName);
                }
            }
            catch (JSToCSharpIntegrationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new JSToCSharpIntegrationException(
                    IntegrationExceptionType.Unexpected, this, jsName, ex);
            }
        }
    }
}
