namespace Relinq.Exceptions.JSToCSharp
{
    public enum JSToCSharpExceptionType
    {
        Unexpected,

        // SyntaxError
        SyntaxError,

        // SemanticError
        UnexpectedJsConstruct,
        UnsupportedJsConstruct,
        JsShouldBeSingleExpression,
        ArrayLiteralsShouldBeCompileTimeConstants,

        // TypeInference
        Integration,
        UnexpectedNodeType,
        UndeclaredVariable,
        ConstantInferenceFailed,
        CannotForgeAnonymousType,
        RedeclaredVariable, // todo. update spec
        VariableOverridesKeyword, // todo. update the spec
        NoSuchFieldOrProp,
        NoSuchMethod,
        NoSuchIndexer,
        NoSuchOperator,
        CannotBeInvoked,
        FruitlessMethodGroupResolution,
        AmbiguousMethodGroupResolution,
        ConditionalTestInvalidType,
        ConditionalClausesNoCommonTypeWithOnlyOneClauseBeingCast,
        ConditionalClausesAmbiguousCommonType,

        // CSharpBuilder
        CannotResolveQuasiTypeFromContext,
        UnexpectedInferredAst,
    }
}