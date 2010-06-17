// $ANTLR 3.1.1 RelinqScript.g 2008-10-30 21:48:36
// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162
namespace 
	Relinq.DSL

{

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;

using IDictionary	= System.Collections.IDictionary;
using Hashtable 	= System.Collections.Hashtable;


using Antlr.Runtime.Tree;

public partial class RelinqScriptParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"StringLiteral", 
		"NumericLiteral", 
		"Identifier", 
		"DoubleStringCharacter", 
		"SingleStringCharacter", 
		"EscapeSequence", 
		"CharacterEscapeSequence", 
		"HexEscapeSequence", 
		"UnicodeEscapeSequence", 
		"SingleEscapeCharacter", 
		"NonEscapeCharacter", 
		"EscapeCharacter", 
		"DecimalDigit", 
		"HexDigit", 
		"DecimalLiteral", 
		"HexIntegerLiteral", 
		"ExponentPart", 
		"IdentifierStart", 
		"IdentifierPart", 
		"UnicodeLetter", 
		"UnicodeDigit", 
		"UnicodeConnectorPunctuation", 
		"UnicodeCombiningMark", 
		"EndOfLine", 
		"Comment", 
		"LineComment", 
		"WhiteSpace", 
		"';'", 
		"'('", 
		"')'", 
		"'this'", 
		"'null'", 
		"'true'", 
		"'false'", 
		"'['", 
		"','", 
		"']'", 
		"'{'", 
		"':'", 
		"'}'", 
		"'function'", 
		"'return'", 
		"'.'", 
		"'||'", 
		"'&&'", 
		"'|'", 
		"'^'", 
		"'&'", 
		"'=='", 
		"'!='", 
		"'==='", 
		"'!=='", 
		"'<'", 
		"'>'", 
		"'<='", 
		"'>='", 
		"'<<'", 
		"'>>'", 
		"'>>>'", 
		"'+'", 
		"'-'", 
		"'*'", 
		"'/'", 
		"'%'", 
		"'++'", 
		"'--'", 
		"'~'", 
		"'!'"
    };

    public const int T__68 = 68;
    public const int T__69 = 69;
    public const int T__66 = 66;
    public const int T__67 = 67;
    public const int T__64 = 64;
    public const int T__65 = 65;
    public const int T__62 = 62;
    public const int HexEscapeSequence = 11;
    public const int T__63 = 63;
    public const int LineComment = 29;
    public const int EndOfLine = 27;
    public const int DecimalDigit = 16;
    public const int T__61 = 61;
    public const int EOF = -1;
    public const int T__60 = 60;
    public const int HexDigit = 17;
    public const int Identifier = 6;
    public const int SingleStringCharacter = 8;
    public const int T__55 = 55;
    public const int T__56 = 56;
    public const int T__57 = 57;
    public const int T__58 = 58;
    public const int T__51 = 51;
    public const int T__52 = 52;
    public const int T__53 = 53;
    public const int T__54 = 54;
    public const int Comment = 28;
    public const int T__59 = 59;
    public const int SingleEscapeCharacter = 13;
    public const int UnicodeLetter = 23;
    public const int ExponentPart = 20;
    public const int EscapeCharacter = 15;
    public const int WhiteSpace = 30;
    public const int T__50 = 50;
    public const int IdentifierPart = 22;
    public const int UnicodeCombiningMark = 26;
    public const int T__42 = 42;
    public const int T__43 = 43;
    public const int UnicodeDigit = 24;
    public const int T__40 = 40;
    public const int T__41 = 41;
    public const int T__46 = 46;
    public const int NumericLiteral = 5;
    public const int T__47 = 47;
    public const int T__44 = 44;
    public const int T__45 = 45;
    public const int T__48 = 48;
    public const int T__49 = 49;
    public const int UnicodeEscapeSequence = 12;
    public const int IdentifierStart = 21;
    public const int DoubleStringCharacter = 7;
    public const int DecimalLiteral = 18;
    public const int StringLiteral = 4;
    public const int T__31 = 31;
    public const int T__32 = 32;
    public const int T__71 = 71;
    public const int T__33 = 33;
    public const int T__34 = 34;
    public const int T__35 = 35;
    public const int T__36 = 36;
    public const int T__70 = 70;
    public const int T__37 = 37;
    public const int T__38 = 38;
    public const int T__39 = 39;
    public const int HexIntegerLiteral = 19;
    public const int NonEscapeCharacter = 14;
    public const int CharacterEscapeSequence = 10;
    public const int EscapeSequence = 9;
    public const int UnicodeConnectorPunctuation = 25;

    // delegates
    // delegators



        public RelinqScriptParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public RelinqScriptParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();
            this.state.ruleMemo = new Hashtable[73+1];
             
             
        }
        
    protected ITreeAdaptor adaptor = new CommonTreeAdaptor();

    public ITreeAdaptor TreeAdaptor
    {
        get { return this.adaptor; }
        set {
    	this.adaptor = value;
    	}
    }

    override public string[] TokenNames {
		get { return RelinqScriptParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "RelinqScript.g"; }
    }


    public class script_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "script"
    // RelinqScript.g:23:1: script : expression ';' EOF ;
    public RelinqScriptParser.script_return script() // throws RecognitionException [1]
    {   
        RelinqScriptParser.script_return retval = new RelinqScriptParser.script_return();
        retval.Start = input.LT(1);
        int script_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal2 = null;
        IToken EOF3 = null;
        RelinqScriptParser.expression_return expression1 = default(RelinqScriptParser.expression_return);


        object char_literal2_tree=null;
        object EOF3_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 1) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:24:2: ( expression ';' EOF )
            // RelinqScript.g:24:4: expression ';' EOF
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_expression_in_script61);
            	expression1 = expression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression1.Tree);
            	char_literal2=(IToken)Match(input,31,FOLLOW_31_in_script63); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal2_tree = (object)adaptor.Create(char_literal2);
            		adaptor.AddChild(root_0, char_literal2_tree);
            	}
            	EOF3=(IToken)Match(input,EOF,FOLLOW_EOF_in_script65); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{EOF3_tree = (object)adaptor.Create(EOF3);
            		adaptor.AddChild(root_0, EOF3_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 1, script_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "script"

    public class expression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "expression"
    // RelinqScript.g:27:1: expression : ( '(' expression ')' | constantExpression ( operatorExpression | memberExpression )* );
    public RelinqScriptParser.expression_return expression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.expression_return retval = new RelinqScriptParser.expression_return();
        retval.Start = input.LT(1);
        int expression_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal4 = null;
        IToken char_literal6 = null;
        RelinqScriptParser.expression_return expression5 = default(RelinqScriptParser.expression_return);

        RelinqScriptParser.constantExpression_return constantExpression7 = default(RelinqScriptParser.constantExpression_return);

        RelinqScriptParser.operatorExpression_return operatorExpression8 = default(RelinqScriptParser.operatorExpression_return);

        RelinqScriptParser.memberExpression_return memberExpression9 = default(RelinqScriptParser.memberExpression_return);


        object char_literal4_tree=null;
        object char_literal6_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 2) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:28:2: ( '(' expression ')' | constantExpression ( operatorExpression | memberExpression )* )
            int alt2 = 2;
            int LA2_0 = input.LA(1);

            if ( (LA2_0 == 32) )
            {
                alt2 = 1;
            }
            else if ( ((LA2_0 >= StringLiteral && LA2_0 <= Identifier) || (LA2_0 >= 34 && LA2_0 <= 38) || LA2_0 == 41 || LA2_0 == 44) )
            {
                alt2 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d2s0 =
                    new NoViableAltException("", 2, 0, input);

                throw nvae_d2s0;
            }
            switch (alt2) 
            {
                case 1 :
                    // RelinqScript.g:28:4: '(' expression ')'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	char_literal4=(IToken)Match(input,32,FOLLOW_32_in_expression77); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal4_tree = (object)adaptor.Create(char_literal4);
                    		adaptor.AddChild(root_0, char_literal4_tree);
                    	}
                    	PushFollow(FOLLOW_expression_in_expression79);
                    	expression5 = expression();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression5.Tree);
                    	char_literal6=(IToken)Match(input,33,FOLLOW_33_in_expression81); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal6_tree = (object)adaptor.Create(char_literal6);
                    		adaptor.AddChild(root_0, char_literal6_tree);
                    	}

                    }
                    break;
                case 2 :
                    // RelinqScript.g:29:4: constantExpression ( operatorExpression | memberExpression )*
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_constantExpression_in_expression86);
                    	constantExpression7 = constantExpression();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, constantExpression7.Tree);
                    	// RelinqScript.g:29:23: ( operatorExpression | memberExpression )*
                    	do 
                    	{
                    	    int alt1 = 3;
                    	    alt1 = dfa1.Predict(input);
                    	    switch (alt1) 
                    		{
                    			case 1 :
                    			    // RelinqScript.g:29:24: operatorExpression
                    			    {
                    			    	PushFollow(FOLLOW_operatorExpression_in_expression89);
                    			    	operatorExpression8 = operatorExpression();
                    			    	state.followingStackPointer--;
                    			    	if (state.failed) return retval;
                    			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, operatorExpression8.Tree);

                    			    }
                    			    break;
                    			case 2 :
                    			    // RelinqScript.g:29:45: memberExpression
                    			    {
                    			    	PushFollow(FOLLOW_memberExpression_in_expression93);
                    			    	memberExpression9 = memberExpression();
                    			    	state.followingStackPointer--;
                    			    	if (state.failed) return retval;
                    			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, memberExpression9.Tree);

                    			    }
                    			    break;

                    			default:
                    			    goto loop1;
                    	    }
                    	} while (true);

                    	loop1:
                    		;	// Stops C# compiler whining that label 'loop1' has no statements


                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 2, expression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "expression"

    public class constantExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "constantExpression"
    // RelinqScript.g:32:1: constantExpression : ( objectDefinition | objectReference );
    public RelinqScriptParser.constantExpression_return constantExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.constantExpression_return retval = new RelinqScriptParser.constantExpression_return();
        retval.Start = input.LT(1);
        int constantExpression_StartIndex = input.Index();
        object root_0 = null;

        RelinqScriptParser.objectDefinition_return objectDefinition10 = default(RelinqScriptParser.objectDefinition_return);

        RelinqScriptParser.objectReference_return objectReference11 = default(RelinqScriptParser.objectReference_return);



        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 3) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:33:2: ( objectDefinition | objectReference )
            int alt3 = 2;
            int LA3_0 = input.LA(1);

            if ( ((LA3_0 >= StringLiteral && LA3_0 <= NumericLiteral) || (LA3_0 >= 34 && LA3_0 <= 38) || LA3_0 == 41 || LA3_0 == 44) )
            {
                alt3 = 1;
            }
            else if ( (LA3_0 == Identifier) )
            {
                alt3 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d3s0 =
                    new NoViableAltException("", 3, 0, input);

                throw nvae_d3s0;
            }
            switch (alt3) 
            {
                case 1 :
                    // RelinqScript.g:33:4: objectDefinition
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_objectDefinition_in_constantExpression107);
                    	objectDefinition10 = objectDefinition();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, objectDefinition10.Tree);

                    }
                    break;
                case 2 :
                    // RelinqScript.g:34:4: objectReference
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_objectReference_in_constantExpression112);
                    	objectReference11 = objectReference();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, objectReference11.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 3, constantExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "constantExpression"

    public class objectDefinition_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "objectDefinition"
    // RelinqScript.g:39:1: objectDefinition : ( 'this' | 'null' | 'true' | 'false' | StringLiteral | NumericLiteral | arrayConstructor | objectConstructor | lambda );
    public RelinqScriptParser.objectDefinition_return objectDefinition() // throws RecognitionException [1]
    {   
        RelinqScriptParser.objectDefinition_return retval = new RelinqScriptParser.objectDefinition_return();
        retval.Start = input.LT(1);
        int objectDefinition_StartIndex = input.Index();
        object root_0 = null;

        IToken string_literal12 = null;
        IToken string_literal13 = null;
        IToken string_literal14 = null;
        IToken string_literal15 = null;
        IToken StringLiteral16 = null;
        IToken NumericLiteral17 = null;
        RelinqScriptParser.arrayConstructor_return arrayConstructor18 = default(RelinqScriptParser.arrayConstructor_return);

        RelinqScriptParser.objectConstructor_return objectConstructor19 = default(RelinqScriptParser.objectConstructor_return);

        RelinqScriptParser.lambda_return lambda20 = default(RelinqScriptParser.lambda_return);


        object string_literal12_tree=null;
        object string_literal13_tree=null;
        object string_literal14_tree=null;
        object string_literal15_tree=null;
        object StringLiteral16_tree=null;
        object NumericLiteral17_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 4) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:40:2: ( 'this' | 'null' | 'true' | 'false' | StringLiteral | NumericLiteral | arrayConstructor | objectConstructor | lambda )
            int alt4 = 9;
            switch ( input.LA(1) ) 
            {
            case 34:
            	{
                alt4 = 1;
                }
                break;
            case 35:
            	{
                alt4 = 2;
                }
                break;
            case 36:
            	{
                alt4 = 3;
                }
                break;
            case 37:
            	{
                alt4 = 4;
                }
                break;
            case StringLiteral:
            	{
                alt4 = 5;
                }
                break;
            case NumericLiteral:
            	{
                alt4 = 6;
                }
                break;
            case 38:
            	{
                alt4 = 7;
                }
                break;
            case 41:
            	{
                alt4 = 8;
                }
                break;
            case 44:
            	{
                alt4 = 9;
                }
                break;
            	default:
            	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            	    NoViableAltException nvae_d4s0 =
            	        new NoViableAltException("", 4, 0, input);

            	    throw nvae_d4s0;
            }

            switch (alt4) 
            {
                case 1 :
                    // RelinqScript.g:40:4: 'this'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	string_literal12=(IToken)Match(input,34,FOLLOW_34_in_objectDefinition128); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{string_literal12_tree = (object)adaptor.Create(string_literal12);
                    		adaptor.AddChild(root_0, string_literal12_tree);
                    	}

                    }
                    break;
                case 2 :
                    // RelinqScript.g:41:4: 'null'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	string_literal13=(IToken)Match(input,35,FOLLOW_35_in_objectDefinition133); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{string_literal13_tree = (object)adaptor.Create(string_literal13);
                    		adaptor.AddChild(root_0, string_literal13_tree);
                    	}

                    }
                    break;
                case 3 :
                    // RelinqScript.g:42:4: 'true'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	string_literal14=(IToken)Match(input,36,FOLLOW_36_in_objectDefinition138); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{string_literal14_tree = (object)adaptor.Create(string_literal14);
                    		adaptor.AddChild(root_0, string_literal14_tree);
                    	}

                    }
                    break;
                case 4 :
                    // RelinqScript.g:43:4: 'false'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	string_literal15=(IToken)Match(input,37,FOLLOW_37_in_objectDefinition143); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{string_literal15_tree = (object)adaptor.Create(string_literal15);
                    		adaptor.AddChild(root_0, string_literal15_tree);
                    	}

                    }
                    break;
                case 5 :
                    // RelinqScript.g:44:4: StringLiteral
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	StringLiteral16=(IToken)Match(input,StringLiteral,FOLLOW_StringLiteral_in_objectDefinition148); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{StringLiteral16_tree = (object)adaptor.Create(StringLiteral16);
                    		adaptor.AddChild(root_0, StringLiteral16_tree);
                    	}

                    }
                    break;
                case 6 :
                    // RelinqScript.g:45:4: NumericLiteral
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	NumericLiteral17=(IToken)Match(input,NumericLiteral,FOLLOW_NumericLiteral_in_objectDefinition153); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{NumericLiteral17_tree = (object)adaptor.Create(NumericLiteral17);
                    		adaptor.AddChild(root_0, NumericLiteral17_tree);
                    	}

                    }
                    break;
                case 7 :
                    // RelinqScript.g:46:4: arrayConstructor
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_arrayConstructor_in_objectDefinition158);
                    	arrayConstructor18 = arrayConstructor();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, arrayConstructor18.Tree);

                    }
                    break;
                case 8 :
                    // RelinqScript.g:47:4: objectConstructor
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_objectConstructor_in_objectDefinition163);
                    	objectConstructor19 = objectConstructor();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, objectConstructor19.Tree);

                    }
                    break;
                case 9 :
                    // RelinqScript.g:48:4: lambda
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_lambda_in_objectDefinition168);
                    	lambda20 = lambda();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, lambda20.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 4, objectDefinition_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "objectDefinition"

    public class arrayConstructor_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "arrayConstructor"
    // RelinqScript.g:51:1: arrayConstructor : '[' ( expression )? ( ',' ( expression )? )* ']' ;
    public RelinqScriptParser.arrayConstructor_return arrayConstructor() // throws RecognitionException [1]
    {   
        RelinqScriptParser.arrayConstructor_return retval = new RelinqScriptParser.arrayConstructor_return();
        retval.Start = input.LT(1);
        int arrayConstructor_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal21 = null;
        IToken char_literal23 = null;
        IToken char_literal25 = null;
        RelinqScriptParser.expression_return expression22 = default(RelinqScriptParser.expression_return);

        RelinqScriptParser.expression_return expression24 = default(RelinqScriptParser.expression_return);


        object char_literal21_tree=null;
        object char_literal23_tree=null;
        object char_literal25_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 5) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:52:2: ( '[' ( expression )? ( ',' ( expression )? )* ']' )
            // RelinqScript.g:52:4: '[' ( expression )? ( ',' ( expression )? )* ']'
            {
            	root_0 = (object)adaptor.GetNilNode();

            	char_literal21=(IToken)Match(input,38,FOLLOW_38_in_arrayConstructor180); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal21_tree = (object)adaptor.Create(char_literal21);
            		adaptor.AddChild(root_0, char_literal21_tree);
            	}
            	// RelinqScript.g:52:8: ( expression )?
            	int alt5 = 2;
            	int LA5_0 = input.LA(1);

            	if ( ((LA5_0 >= StringLiteral && LA5_0 <= Identifier) || LA5_0 == 32 || (LA5_0 >= 34 && LA5_0 <= 38) || LA5_0 == 41 || LA5_0 == 44) )
            	{
            	    alt5 = 1;
            	}
            	switch (alt5) 
            	{
            	    case 1 :
            	        // RelinqScript.g:0:0: expression
            	        {
            	        	PushFollow(FOLLOW_expression_in_arrayConstructor182);
            	        	expression22 = expression();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return retval;
            	        	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression22.Tree);

            	        }
            	        break;

            	}

            	// RelinqScript.g:52:20: ( ',' ( expression )? )*
            	do 
            	{
            	    int alt7 = 2;
            	    int LA7_0 = input.LA(1);

            	    if ( (LA7_0 == 39) )
            	    {
            	        alt7 = 1;
            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // RelinqScript.g:52:21: ',' ( expression )?
            			    {
            			    	char_literal23=(IToken)Match(input,39,FOLLOW_39_in_arrayConstructor186); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{char_literal23_tree = (object)adaptor.Create(char_literal23);
            			    		adaptor.AddChild(root_0, char_literal23_tree);
            			    	}
            			    	// RelinqScript.g:52:25: ( expression )?
            			    	int alt6 = 2;
            			    	int LA6_0 = input.LA(1);

            			    	if ( ((LA6_0 >= StringLiteral && LA6_0 <= Identifier) || LA6_0 == 32 || (LA6_0 >= 34 && LA6_0 <= 38) || LA6_0 == 41 || LA6_0 == 44) )
            			    	{
            			    	    alt6 = 1;
            			    	}
            			    	switch (alt6) 
            			    	{
            			    	    case 1 :
            			    	        // RelinqScript.g:52:26: expression
            			    	        {
            			    	        	PushFollow(FOLLOW_expression_in_arrayConstructor189);
            			    	        	expression24 = expression();
            			    	        	state.followingStackPointer--;
            			    	        	if (state.failed) return retval;
            			    	        	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression24.Tree);

            			    	        }
            			    	        break;

            			    	}


            			    }
            			    break;

            			default:
            			    goto loop7;
            	    }
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whining that label 'loop7' has no statements

            	char_literal25=(IToken)Match(input,40,FOLLOW_40_in_arrayConstructor195); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal25_tree = (object)adaptor.Create(char_literal25);
            		adaptor.AddChild(root_0, char_literal25_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 5, arrayConstructor_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "arrayConstructor"

    public class objectConstructor_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "objectConstructor"
    // RelinqScript.g:55:1: objectConstructor : '{' ( Identifier ':' expression )? ( ',' ( Identifier ':' expression ) )* '}' ;
    public RelinqScriptParser.objectConstructor_return objectConstructor() // throws RecognitionException [1]
    {   
        RelinqScriptParser.objectConstructor_return retval = new RelinqScriptParser.objectConstructor_return();
        retval.Start = input.LT(1);
        int objectConstructor_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal26 = null;
        IToken Identifier27 = null;
        IToken char_literal28 = null;
        IToken char_literal30 = null;
        IToken Identifier31 = null;
        IToken char_literal32 = null;
        IToken char_literal34 = null;
        RelinqScriptParser.expression_return expression29 = default(RelinqScriptParser.expression_return);

        RelinqScriptParser.expression_return expression33 = default(RelinqScriptParser.expression_return);


        object char_literal26_tree=null;
        object Identifier27_tree=null;
        object char_literal28_tree=null;
        object char_literal30_tree=null;
        object Identifier31_tree=null;
        object char_literal32_tree=null;
        object char_literal34_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 6) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:56:2: ( '{' ( Identifier ':' expression )? ( ',' ( Identifier ':' expression ) )* '}' )
            // RelinqScript.g:56:4: '{' ( Identifier ':' expression )? ( ',' ( Identifier ':' expression ) )* '}'
            {
            	root_0 = (object)adaptor.GetNilNode();

            	char_literal26=(IToken)Match(input,41,FOLLOW_41_in_objectConstructor207); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal26_tree = (object)adaptor.Create(char_literal26);
            		adaptor.AddChild(root_0, char_literal26_tree);
            	}
            	// RelinqScript.g:56:8: ( Identifier ':' expression )?
            	int alt8 = 2;
            	int LA8_0 = input.LA(1);

            	if ( (LA8_0 == Identifier) )
            	{
            	    alt8 = 1;
            	}
            	switch (alt8) 
            	{
            	    case 1 :
            	        // RelinqScript.g:56:9: Identifier ':' expression
            	        {
            	        	Identifier27=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_objectConstructor210); if (state.failed) return retval;
            	        	if ( state.backtracking == 0 )
            	        	{Identifier27_tree = (object)adaptor.Create(Identifier27);
            	        		adaptor.AddChild(root_0, Identifier27_tree);
            	        	}
            	        	char_literal28=(IToken)Match(input,42,FOLLOW_42_in_objectConstructor212); if (state.failed) return retval;
            	        	if ( state.backtracking == 0 )
            	        	{char_literal28_tree = (object)adaptor.Create(char_literal28);
            	        		adaptor.AddChild(root_0, char_literal28_tree);
            	        	}
            	        	PushFollow(FOLLOW_expression_in_objectConstructor214);
            	        	expression29 = expression();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return retval;
            	        	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression29.Tree);

            	        }
            	        break;

            	}

            	// RelinqScript.g:56:37: ( ',' ( Identifier ':' expression ) )*
            	do 
            	{
            	    int alt9 = 2;
            	    int LA9_0 = input.LA(1);

            	    if ( (LA9_0 == 39) )
            	    {
            	        alt9 = 1;
            	    }


            	    switch (alt9) 
            		{
            			case 1 :
            			    // RelinqScript.g:56:38: ',' ( Identifier ':' expression )
            			    {
            			    	char_literal30=(IToken)Match(input,39,FOLLOW_39_in_objectConstructor219); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{char_literal30_tree = (object)adaptor.Create(char_literal30);
            			    		adaptor.AddChild(root_0, char_literal30_tree);
            			    	}
            			    	// RelinqScript.g:56:42: ( Identifier ':' expression )
            			    	// RelinqScript.g:56:43: Identifier ':' expression
            			    	{
            			    		Identifier31=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_objectConstructor222); if (state.failed) return retval;
            			    		if ( state.backtracking == 0 )
            			    		{Identifier31_tree = (object)adaptor.Create(Identifier31);
            			    			adaptor.AddChild(root_0, Identifier31_tree);
            			    		}
            			    		char_literal32=(IToken)Match(input,42,FOLLOW_42_in_objectConstructor224); if (state.failed) return retval;
            			    		if ( state.backtracking == 0 )
            			    		{char_literal32_tree = (object)adaptor.Create(char_literal32);
            			    			adaptor.AddChild(root_0, char_literal32_tree);
            			    		}
            			    		PushFollow(FOLLOW_expression_in_objectConstructor226);
            			    		expression33 = expression();
            			    		state.followingStackPointer--;
            			    		if (state.failed) return retval;
            			    		if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression33.Tree);

            			    	}


            			    }
            			    break;

            			default:
            			    goto loop9;
            	    }
            	} while (true);

            	loop9:
            		;	// Stops C# compiler whining that label 'loop9' has no statements

            	char_literal34=(IToken)Match(input,43,FOLLOW_43_in_objectConstructor231); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal34_tree = (object)adaptor.Create(char_literal34);
            		adaptor.AddChild(root_0, char_literal34_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 6, objectConstructor_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "objectConstructor"

    public class lambda_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "lambda"
    // RelinqScript.g:59:1: lambda : 'function' lambdaParameters lambdaBody ;
    public RelinqScriptParser.lambda_return lambda() // throws RecognitionException [1]
    {   
        RelinqScriptParser.lambda_return retval = new RelinqScriptParser.lambda_return();
        retval.Start = input.LT(1);
        int lambda_StartIndex = input.Index();
        object root_0 = null;

        IToken string_literal35 = null;
        RelinqScriptParser.lambdaParameters_return lambdaParameters36 = default(RelinqScriptParser.lambdaParameters_return);

        RelinqScriptParser.lambdaBody_return lambdaBody37 = default(RelinqScriptParser.lambdaBody_return);


        object string_literal35_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 7) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:60:2: ( 'function' lambdaParameters lambdaBody )
            // RelinqScript.g:60:4: 'function' lambdaParameters lambdaBody
            {
            	root_0 = (object)adaptor.GetNilNode();

            	string_literal35=(IToken)Match(input,44,FOLLOW_44_in_lambda243); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{string_literal35_tree = (object)adaptor.Create(string_literal35);
            		adaptor.AddChild(root_0, string_literal35_tree);
            	}
            	PushFollow(FOLLOW_lambdaParameters_in_lambda245);
            	lambdaParameters36 = lambdaParameters();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, lambdaParameters36.Tree);
            	PushFollow(FOLLOW_lambdaBody_in_lambda247);
            	lambdaBody37 = lambdaBody();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, lambdaBody37.Tree);

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 7, lambda_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "lambda"

    public class lambdaParameters_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "lambdaParameters"
    // RelinqScript.g:63:1: lambdaParameters : '(' ( Identifier ( ',' Identifier )* )? ')' ;
    public RelinqScriptParser.lambdaParameters_return lambdaParameters() // throws RecognitionException [1]
    {   
        RelinqScriptParser.lambdaParameters_return retval = new RelinqScriptParser.lambdaParameters_return();
        retval.Start = input.LT(1);
        int lambdaParameters_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal38 = null;
        IToken Identifier39 = null;
        IToken char_literal40 = null;
        IToken Identifier41 = null;
        IToken char_literal42 = null;

        object char_literal38_tree=null;
        object Identifier39_tree=null;
        object char_literal40_tree=null;
        object Identifier41_tree=null;
        object char_literal42_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 8) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:64:2: ( '(' ( Identifier ( ',' Identifier )* )? ')' )
            // RelinqScript.g:64:4: '(' ( Identifier ( ',' Identifier )* )? ')'
            {
            	root_0 = (object)adaptor.GetNilNode();

            	char_literal38=(IToken)Match(input,32,FOLLOW_32_in_lambdaParameters259); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal38_tree = (object)adaptor.Create(char_literal38);
            		adaptor.AddChild(root_0, char_literal38_tree);
            	}
            	// RelinqScript.g:64:8: ( Identifier ( ',' Identifier )* )?
            	int alt11 = 2;
            	int LA11_0 = input.LA(1);

            	if ( (LA11_0 == Identifier) )
            	{
            	    alt11 = 1;
            	}
            	switch (alt11) 
            	{
            	    case 1 :
            	        // RelinqScript.g:64:9: Identifier ( ',' Identifier )*
            	        {
            	        	Identifier39=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_lambdaParameters262); if (state.failed) return retval;
            	        	if ( state.backtracking == 0 )
            	        	{Identifier39_tree = (object)adaptor.Create(Identifier39);
            	        		adaptor.AddChild(root_0, Identifier39_tree);
            	        	}
            	        	// RelinqScript.g:64:20: ( ',' Identifier )*
            	        	do 
            	        	{
            	        	    int alt10 = 2;
            	        	    int LA10_0 = input.LA(1);

            	        	    if ( (LA10_0 == 39) )
            	        	    {
            	        	        alt10 = 1;
            	        	    }


            	        	    switch (alt10) 
            	        		{
            	        			case 1 :
            	        			    // RelinqScript.g:64:21: ',' Identifier
            	        			    {
            	        			    	char_literal40=(IToken)Match(input,39,FOLLOW_39_in_lambdaParameters265); if (state.failed) return retval;
            	        			    	if ( state.backtracking == 0 )
            	        			    	{char_literal40_tree = (object)adaptor.Create(char_literal40);
            	        			    		adaptor.AddChild(root_0, char_literal40_tree);
            	        			    	}
            	        			    	Identifier41=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_lambdaParameters267); if (state.failed) return retval;
            	        			    	if ( state.backtracking == 0 )
            	        			    	{Identifier41_tree = (object)adaptor.Create(Identifier41);
            	        			    		adaptor.AddChild(root_0, Identifier41_tree);
            	        			    	}

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop10;
            	        	    }
            	        	} while (true);

            	        	loop10:
            	        		;	// Stops C# compiler whining that label 'loop10' has no statements


            	        }
            	        break;

            	}

            	char_literal42=(IToken)Match(input,33,FOLLOW_33_in_lambdaParameters273); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal42_tree = (object)adaptor.Create(char_literal42);
            		adaptor.AddChild(root_0, char_literal42_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 8, lambdaParameters_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "lambdaParameters"

    public class lambdaBody_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "lambdaBody"
    // RelinqScript.g:67:1: lambdaBody : '{' 'return' expression ';' '}' ;
    public RelinqScriptParser.lambdaBody_return lambdaBody() // throws RecognitionException [1]
    {   
        RelinqScriptParser.lambdaBody_return retval = new RelinqScriptParser.lambdaBody_return();
        retval.Start = input.LT(1);
        int lambdaBody_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal43 = null;
        IToken string_literal44 = null;
        IToken char_literal46 = null;
        IToken char_literal47 = null;
        RelinqScriptParser.expression_return expression45 = default(RelinqScriptParser.expression_return);


        object char_literal43_tree=null;
        object string_literal44_tree=null;
        object char_literal46_tree=null;
        object char_literal47_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 9) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:68:2: ( '{' 'return' expression ';' '}' )
            // RelinqScript.g:68:4: '{' 'return' expression ';' '}'
            {
            	root_0 = (object)adaptor.GetNilNode();

            	char_literal43=(IToken)Match(input,41,FOLLOW_41_in_lambdaBody285); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal43_tree = (object)adaptor.Create(char_literal43);
            		adaptor.AddChild(root_0, char_literal43_tree);
            	}
            	string_literal44=(IToken)Match(input,45,FOLLOW_45_in_lambdaBody287); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{string_literal44_tree = (object)adaptor.Create(string_literal44);
            		adaptor.AddChild(root_0, string_literal44_tree);
            	}
            	PushFollow(FOLLOW_expression_in_lambdaBody289);
            	expression45 = expression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression45.Tree);
            	char_literal46=(IToken)Match(input,31,FOLLOW_31_in_lambdaBody291); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal46_tree = (object)adaptor.Create(char_literal46);
            		adaptor.AddChild(root_0, char_literal46_tree);
            	}
            	char_literal47=(IToken)Match(input,43,FOLLOW_43_in_lambdaBody293); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal47_tree = (object)adaptor.Create(char_literal47);
            		adaptor.AddChild(root_0, char_literal47_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 9, lambdaBody_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "lambdaBody"

    public class objectReference_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "objectReference"
    // RelinqScript.g:71:1: objectReference : Identifier ;
    public RelinqScriptParser.objectReference_return objectReference() // throws RecognitionException [1]
    {   
        RelinqScriptParser.objectReference_return retval = new RelinqScriptParser.objectReference_return();
        retval.Start = input.LT(1);
        int objectReference_StartIndex = input.Index();
        object root_0 = null;

        IToken Identifier48 = null;

        object Identifier48_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 10) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:72:2: ( Identifier )
            // RelinqScript.g:72:4: Identifier
            {
            	root_0 = (object)adaptor.GetNilNode();

            	Identifier48=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_objectReference305); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{Identifier48_tree = (object)adaptor.Create(Identifier48);
            		adaptor.AddChild(root_0, Identifier48_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 10, objectReference_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "objectReference"

    public class memberExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "memberExpression"
    // RelinqScript.g:77:1: memberExpression : ( '.' Identifier '(' callParameters ')' | '.' Identifier );
    public RelinqScriptParser.memberExpression_return memberExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.memberExpression_return retval = new RelinqScriptParser.memberExpression_return();
        retval.Start = input.LT(1);
        int memberExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal49 = null;
        IToken Identifier50 = null;
        IToken char_literal51 = null;
        IToken char_literal53 = null;
        IToken char_literal54 = null;
        IToken Identifier55 = null;
        RelinqScriptParser.callParameters_return callParameters52 = default(RelinqScriptParser.callParameters_return);


        object char_literal49_tree=null;
        object Identifier50_tree=null;
        object char_literal51_tree=null;
        object char_literal53_tree=null;
        object char_literal54_tree=null;
        object Identifier55_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 11) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:78:2: ( '.' Identifier '(' callParameters ')' | '.' Identifier )
            int alt12 = 2;
            int LA12_0 = input.LA(1);

            if ( (LA12_0 == 46) )
            {
                int LA12_1 = input.LA(2);

                if ( (synpred20_RelinqScript()) )
                {
                    alt12 = 1;
                }
                else if ( (true) )
                {
                    alt12 = 2;
                }
                else 
                {
                    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    NoViableAltException nvae_d12s1 =
                        new NoViableAltException("", 12, 1, input);

                    throw nvae_d12s1;
                }
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d12s0 =
                    new NoViableAltException("", 12, 0, input);

                throw nvae_d12s0;
            }
            switch (alt12) 
            {
                case 1 :
                    // RelinqScript.g:78:4: '.' Identifier '(' callParameters ')'
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	char_literal49=(IToken)Match(input,46,FOLLOW_46_in_memberExpression320); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal49_tree = (object)adaptor.Create(char_literal49);
                    		adaptor.AddChild(root_0, char_literal49_tree);
                    	}
                    	Identifier50=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_memberExpression322); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{Identifier50_tree = (object)adaptor.Create(Identifier50);
                    		adaptor.AddChild(root_0, Identifier50_tree);
                    	}
                    	char_literal51=(IToken)Match(input,32,FOLLOW_32_in_memberExpression324); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal51_tree = (object)adaptor.Create(char_literal51);
                    		adaptor.AddChild(root_0, char_literal51_tree);
                    	}
                    	PushFollow(FOLLOW_callParameters_in_memberExpression326);
                    	callParameters52 = callParameters();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, callParameters52.Tree);
                    	char_literal53=(IToken)Match(input,33,FOLLOW_33_in_memberExpression328); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal53_tree = (object)adaptor.Create(char_literal53);
                    		adaptor.AddChild(root_0, char_literal53_tree);
                    	}

                    }
                    break;
                case 2 :
                    // RelinqScript.g:79:4: '.' Identifier
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	char_literal54=(IToken)Match(input,46,FOLLOW_46_in_memberExpression333); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{char_literal54_tree = (object)adaptor.Create(char_literal54);
                    		adaptor.AddChild(root_0, char_literal54_tree);
                    	}
                    	Identifier55=(IToken)Match(input,Identifier,FOLLOW_Identifier_in_memberExpression335); if (state.failed) return retval;
                    	if ( state.backtracking == 0 )
                    	{Identifier55_tree = (object)adaptor.Create(Identifier55);
                    		adaptor.AddChild(root_0, Identifier55_tree);
                    	}

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 11, memberExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "memberExpression"

    public class callParameters_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "callParameters"
    // RelinqScript.g:82:1: callParameters : '(' ( expression ( ',' expression )* )? ')' ;
    public RelinqScriptParser.callParameters_return callParameters() // throws RecognitionException [1]
    {   
        RelinqScriptParser.callParameters_return retval = new RelinqScriptParser.callParameters_return();
        retval.Start = input.LT(1);
        int callParameters_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal56 = null;
        IToken char_literal58 = null;
        IToken char_literal60 = null;
        RelinqScriptParser.expression_return expression57 = default(RelinqScriptParser.expression_return);

        RelinqScriptParser.expression_return expression59 = default(RelinqScriptParser.expression_return);


        object char_literal56_tree=null;
        object char_literal58_tree=null;
        object char_literal60_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 12) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:83:2: ( '(' ( expression ( ',' expression )* )? ')' )
            // RelinqScript.g:83:4: '(' ( expression ( ',' expression )* )? ')'
            {
            	root_0 = (object)adaptor.GetNilNode();

            	char_literal56=(IToken)Match(input,32,FOLLOW_32_in_callParameters347); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal56_tree = (object)adaptor.Create(char_literal56);
            		adaptor.AddChild(root_0, char_literal56_tree);
            	}
            	// RelinqScript.g:83:8: ( expression ( ',' expression )* )?
            	int alt14 = 2;
            	int LA14_0 = input.LA(1);

            	if ( ((LA14_0 >= StringLiteral && LA14_0 <= Identifier) || LA14_0 == 32 || (LA14_0 >= 34 && LA14_0 <= 38) || LA14_0 == 41 || LA14_0 == 44) )
            	{
            	    alt14 = 1;
            	}
            	switch (alt14) 
            	{
            	    case 1 :
            	        // RelinqScript.g:83:9: expression ( ',' expression )*
            	        {
            	        	PushFollow(FOLLOW_expression_in_callParameters350);
            	        	expression57 = expression();
            	        	state.followingStackPointer--;
            	        	if (state.failed) return retval;
            	        	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression57.Tree);
            	        	// RelinqScript.g:83:20: ( ',' expression )*
            	        	do 
            	        	{
            	        	    int alt13 = 2;
            	        	    int LA13_0 = input.LA(1);

            	        	    if ( (LA13_0 == 39) )
            	        	    {
            	        	        alt13 = 1;
            	        	    }


            	        	    switch (alt13) 
            	        		{
            	        			case 1 :
            	        			    // RelinqScript.g:83:21: ',' expression
            	        			    {
            	        			    	char_literal58=(IToken)Match(input,39,FOLLOW_39_in_callParameters353); if (state.failed) return retval;
            	        			    	if ( state.backtracking == 0 )
            	        			    	{char_literal58_tree = (object)adaptor.Create(char_literal58);
            	        			    		adaptor.AddChild(root_0, char_literal58_tree);
            	        			    	}
            	        			    	PushFollow(FOLLOW_expression_in_callParameters355);
            	        			    	expression59 = expression();
            	        			    	state.followingStackPointer--;
            	        			    	if (state.failed) return retval;
            	        			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression59.Tree);

            	        			    }
            	        			    break;

            	        			default:
            	        			    goto loop13;
            	        	    }
            	        	} while (true);

            	        	loop13:
            	        		;	// Stops C# compiler whining that label 'loop13' has no statements


            	        }
            	        break;

            	}

            	char_literal60=(IToken)Match(input,33,FOLLOW_33_in_callParameters361); if (state.failed) return retval;
            	if ( state.backtracking == 0 )
            	{char_literal60_tree = (object)adaptor.Create(char_literal60);
            		adaptor.AddChild(root_0, char_literal60_tree);
            	}

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 12, callParameters_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "callParameters"

    public class operatorExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "operatorExpression"
    // RelinqScript.g:88:1: operatorExpression : logicalORExpression ;
    public RelinqScriptParser.operatorExpression_return operatorExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.operatorExpression_return retval = new RelinqScriptParser.operatorExpression_return();
        retval.Start = input.LT(1);
        int operatorExpression_StartIndex = input.Index();
        object root_0 = null;

        RelinqScriptParser.logicalORExpression_return logicalORExpression61 = default(RelinqScriptParser.logicalORExpression_return);



        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 13) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:89:2: ( logicalORExpression )
            // RelinqScript.g:89:4: logicalORExpression
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_logicalORExpression_in_operatorExpression376);
            	logicalORExpression61 = logicalORExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, logicalORExpression61.Tree);

            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 13, operatorExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "operatorExpression"

    public class logicalORExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "logicalORExpression"
    // RelinqScript.g:92:1: logicalORExpression : logicalANDExpression ( '||' logicalANDExpression )* ;
    public RelinqScriptParser.logicalORExpression_return logicalORExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.logicalORExpression_return retval = new RelinqScriptParser.logicalORExpression_return();
        retval.Start = input.LT(1);
        int logicalORExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken string_literal63 = null;
        RelinqScriptParser.logicalANDExpression_return logicalANDExpression62 = default(RelinqScriptParser.logicalANDExpression_return);

        RelinqScriptParser.logicalANDExpression_return logicalANDExpression64 = default(RelinqScriptParser.logicalANDExpression_return);


        object string_literal63_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 14) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:93:2: ( logicalANDExpression ( '||' logicalANDExpression )* )
            // RelinqScript.g:93:4: logicalANDExpression ( '||' logicalANDExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_logicalANDExpression_in_logicalORExpression388);
            	logicalANDExpression62 = logicalANDExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, logicalANDExpression62.Tree);
            	// RelinqScript.g:93:25: ( '||' logicalANDExpression )*
            	do 
            	{
            	    int alt15 = 2;
            	    int LA15_0 = input.LA(1);

            	    if ( (LA15_0 == 47) )
            	    {
            	        int LA15_2 = input.LA(2);

            	        if ( (synpred23_RelinqScript()) )
            	        {
            	            alt15 = 1;
            	        }


            	    }


            	    switch (alt15) 
            		{
            			case 1 :
            			    // RelinqScript.g:93:26: '||' logicalANDExpression
            			    {
            			    	string_literal63=(IToken)Match(input,47,FOLLOW_47_in_logicalORExpression391); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{string_literal63_tree = (object)adaptor.Create(string_literal63);
            			    		adaptor.AddChild(root_0, string_literal63_tree);
            			    	}
            			    	PushFollow(FOLLOW_logicalANDExpression_in_logicalORExpression393);
            			    	logicalANDExpression64 = logicalANDExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, logicalANDExpression64.Tree);

            			    }
            			    break;

            			default:
            			    goto loop15;
            	    }
            	} while (true);

            	loop15:
            		;	// Stops C# compiler whining that label 'loop15' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 14, logicalORExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "logicalORExpression"

    public class logicalANDExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "logicalANDExpression"
    // RelinqScript.g:96:1: logicalANDExpression : bitwiseORExpression ( '&&' bitwiseORExpression )* ;
    public RelinqScriptParser.logicalANDExpression_return logicalANDExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.logicalANDExpression_return retval = new RelinqScriptParser.logicalANDExpression_return();
        retval.Start = input.LT(1);
        int logicalANDExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken string_literal66 = null;
        RelinqScriptParser.bitwiseORExpression_return bitwiseORExpression65 = default(RelinqScriptParser.bitwiseORExpression_return);

        RelinqScriptParser.bitwiseORExpression_return bitwiseORExpression67 = default(RelinqScriptParser.bitwiseORExpression_return);


        object string_literal66_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 15) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:97:2: ( bitwiseORExpression ( '&&' bitwiseORExpression )* )
            // RelinqScript.g:97:4: bitwiseORExpression ( '&&' bitwiseORExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_bitwiseORExpression_in_logicalANDExpression407);
            	bitwiseORExpression65 = bitwiseORExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseORExpression65.Tree);
            	// RelinqScript.g:97:24: ( '&&' bitwiseORExpression )*
            	do 
            	{
            	    int alt16 = 2;
            	    int LA16_0 = input.LA(1);

            	    if ( (LA16_0 == 48) )
            	    {
            	        int LA16_2 = input.LA(2);

            	        if ( (synpred24_RelinqScript()) )
            	        {
            	            alt16 = 1;
            	        }


            	    }


            	    switch (alt16) 
            		{
            			case 1 :
            			    // RelinqScript.g:97:25: '&&' bitwiseORExpression
            			    {
            			    	string_literal66=(IToken)Match(input,48,FOLLOW_48_in_logicalANDExpression410); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{string_literal66_tree = (object)adaptor.Create(string_literal66);
            			    		adaptor.AddChild(root_0, string_literal66_tree);
            			    	}
            			    	PushFollow(FOLLOW_bitwiseORExpression_in_logicalANDExpression412);
            			    	bitwiseORExpression67 = bitwiseORExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseORExpression67.Tree);

            			    }
            			    break;

            			default:
            			    goto loop16;
            	    }
            	} while (true);

            	loop16:
            		;	// Stops C# compiler whining that label 'loop16' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 15, logicalANDExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "logicalANDExpression"

    public class bitwiseORExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "bitwiseORExpression"
    // RelinqScript.g:100:1: bitwiseORExpression : bitwiseXORExpression ( '|' bitwiseXORExpression )* ;
    public RelinqScriptParser.bitwiseORExpression_return bitwiseORExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.bitwiseORExpression_return retval = new RelinqScriptParser.bitwiseORExpression_return();
        retval.Start = input.LT(1);
        int bitwiseORExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal69 = null;
        RelinqScriptParser.bitwiseXORExpression_return bitwiseXORExpression68 = default(RelinqScriptParser.bitwiseXORExpression_return);

        RelinqScriptParser.bitwiseXORExpression_return bitwiseXORExpression70 = default(RelinqScriptParser.bitwiseXORExpression_return);


        object char_literal69_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 16) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:101:2: ( bitwiseXORExpression ( '|' bitwiseXORExpression )* )
            // RelinqScript.g:101:4: bitwiseXORExpression ( '|' bitwiseXORExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_bitwiseXORExpression_in_bitwiseORExpression426);
            	bitwiseXORExpression68 = bitwiseXORExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseXORExpression68.Tree);
            	// RelinqScript.g:101:25: ( '|' bitwiseXORExpression )*
            	do 
            	{
            	    int alt17 = 2;
            	    int LA17_0 = input.LA(1);

            	    if ( (LA17_0 == 49) )
            	    {
            	        int LA17_2 = input.LA(2);

            	        if ( (synpred25_RelinqScript()) )
            	        {
            	            alt17 = 1;
            	        }


            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // RelinqScript.g:101:26: '|' bitwiseXORExpression
            			    {
            			    	char_literal69=(IToken)Match(input,49,FOLLOW_49_in_bitwiseORExpression429); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{char_literal69_tree = (object)adaptor.Create(char_literal69);
            			    		adaptor.AddChild(root_0, char_literal69_tree);
            			    	}
            			    	PushFollow(FOLLOW_bitwiseXORExpression_in_bitwiseORExpression431);
            			    	bitwiseXORExpression70 = bitwiseXORExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseXORExpression70.Tree);

            			    }
            			    break;

            			default:
            			    goto loop17;
            	    }
            	} while (true);

            	loop17:
            		;	// Stops C# compiler whining that label 'loop17' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 16, bitwiseORExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "bitwiseORExpression"

    public class bitwiseXORExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "bitwiseXORExpression"
    // RelinqScript.g:104:1: bitwiseXORExpression : bitwiseANDExpression ( '^' bitwiseANDExpression )* ;
    public RelinqScriptParser.bitwiseXORExpression_return bitwiseXORExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.bitwiseXORExpression_return retval = new RelinqScriptParser.bitwiseXORExpression_return();
        retval.Start = input.LT(1);
        int bitwiseXORExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal72 = null;
        RelinqScriptParser.bitwiseANDExpression_return bitwiseANDExpression71 = default(RelinqScriptParser.bitwiseANDExpression_return);

        RelinqScriptParser.bitwiseANDExpression_return bitwiseANDExpression73 = default(RelinqScriptParser.bitwiseANDExpression_return);


        object char_literal72_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 17) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:105:2: ( bitwiseANDExpression ( '^' bitwiseANDExpression )* )
            // RelinqScript.g:105:4: bitwiseANDExpression ( '^' bitwiseANDExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_bitwiseANDExpression_in_bitwiseXORExpression445);
            	bitwiseANDExpression71 = bitwiseANDExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseANDExpression71.Tree);
            	// RelinqScript.g:105:25: ( '^' bitwiseANDExpression )*
            	do 
            	{
            	    int alt18 = 2;
            	    int LA18_0 = input.LA(1);

            	    if ( (LA18_0 == 50) )
            	    {
            	        int LA18_2 = input.LA(2);

            	        if ( (synpred26_RelinqScript()) )
            	        {
            	            alt18 = 1;
            	        }


            	    }


            	    switch (alt18) 
            		{
            			case 1 :
            			    // RelinqScript.g:105:26: '^' bitwiseANDExpression
            			    {
            			    	char_literal72=(IToken)Match(input,50,FOLLOW_50_in_bitwiseXORExpression448); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{char_literal72_tree = (object)adaptor.Create(char_literal72);
            			    		adaptor.AddChild(root_0, char_literal72_tree);
            			    	}
            			    	PushFollow(FOLLOW_bitwiseANDExpression_in_bitwiseXORExpression450);
            			    	bitwiseANDExpression73 = bitwiseANDExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, bitwiseANDExpression73.Tree);

            			    }
            			    break;

            			default:
            			    goto loop18;
            	    }
            	} while (true);

            	loop18:
            		;	// Stops C# compiler whining that label 'loop18' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 17, bitwiseXORExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "bitwiseXORExpression"

    public class bitwiseANDExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "bitwiseANDExpression"
    // RelinqScript.g:108:1: bitwiseANDExpression : equalityExpression ( '&' equalityExpression )* ;
    public RelinqScriptParser.bitwiseANDExpression_return bitwiseANDExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.bitwiseANDExpression_return retval = new RelinqScriptParser.bitwiseANDExpression_return();
        retval.Start = input.LT(1);
        int bitwiseANDExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken char_literal75 = null;
        RelinqScriptParser.equalityExpression_return equalityExpression74 = default(RelinqScriptParser.equalityExpression_return);

        RelinqScriptParser.equalityExpression_return equalityExpression76 = default(RelinqScriptParser.equalityExpression_return);


        object char_literal75_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 18) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:109:2: ( equalityExpression ( '&' equalityExpression )* )
            // RelinqScript.g:109:4: equalityExpression ( '&' equalityExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_equalityExpression_in_bitwiseANDExpression464);
            	equalityExpression74 = equalityExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, equalityExpression74.Tree);
            	// RelinqScript.g:109:23: ( '&' equalityExpression )*
            	do 
            	{
            	    int alt19 = 2;
            	    int LA19_0 = input.LA(1);

            	    if ( (LA19_0 == 51) )
            	    {
            	        int LA19_2 = input.LA(2);

            	        if ( (synpred27_RelinqScript()) )
            	        {
            	            alt19 = 1;
            	        }


            	    }


            	    switch (alt19) 
            		{
            			case 1 :
            			    // RelinqScript.g:109:24: '&' equalityExpression
            			    {
            			    	char_literal75=(IToken)Match(input,51,FOLLOW_51_in_bitwiseANDExpression467); if (state.failed) return retval;
            			    	if ( state.backtracking == 0 )
            			    	{char_literal75_tree = (object)adaptor.Create(char_literal75);
            			    		adaptor.AddChild(root_0, char_literal75_tree);
            			    	}
            			    	PushFollow(FOLLOW_equalityExpression_in_bitwiseANDExpression469);
            			    	equalityExpression76 = equalityExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, equalityExpression76.Tree);

            			    }
            			    break;

            			default:
            			    goto loop19;
            	    }
            	} while (true);

            	loop19:
            		;	// Stops C# compiler whining that label 'loop19' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 18, bitwiseANDExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "bitwiseANDExpression"

    public class equalityExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "equalityExpression"
    // RelinqScript.g:112:1: equalityExpression : relationalExpression ( ( '==' | '!=' | '===' | '!==' ) relationalExpression )* ;
    public RelinqScriptParser.equalityExpression_return equalityExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.equalityExpression_return retval = new RelinqScriptParser.equalityExpression_return();
        retval.Start = input.LT(1);
        int equalityExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set78 = null;
        RelinqScriptParser.relationalExpression_return relationalExpression77 = default(RelinqScriptParser.relationalExpression_return);

        RelinqScriptParser.relationalExpression_return relationalExpression79 = default(RelinqScriptParser.relationalExpression_return);


        object set78_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 19) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:113:2: ( relationalExpression ( ( '==' | '!=' | '===' | '!==' ) relationalExpression )* )
            // RelinqScript.g:113:4: relationalExpression ( ( '==' | '!=' | '===' | '!==' ) relationalExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_relationalExpression_in_equalityExpression483);
            	relationalExpression77 = relationalExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, relationalExpression77.Tree);
            	// RelinqScript.g:113:25: ( ( '==' | '!=' | '===' | '!==' ) relationalExpression )*
            	do 
            	{
            	    int alt20 = 2;
            	    int LA20_0 = input.LA(1);

            	    if ( ((LA20_0 >= 52 && LA20_0 <= 55)) )
            	    {
            	        int LA20_2 = input.LA(2);

            	        if ( (synpred31_RelinqScript()) )
            	        {
            	            alt20 = 1;
            	        }


            	    }


            	    switch (alt20) 
            		{
            			case 1 :
            			    // RelinqScript.g:113:26: ( '==' | '!=' | '===' | '!==' ) relationalExpression
            			    {
            			    	set78 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= 52 && input.LA(1) <= 55) ) 
            			    	{
            			    	    input.Consume();
            			    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set78));
            			    	    state.errorRecovery = false;state.failed = false;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_relationalExpression_in_equalityExpression502);
            			    	relationalExpression79 = relationalExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, relationalExpression79.Tree);

            			    }
            			    break;

            			default:
            			    goto loop20;
            	    }
            	} while (true);

            	loop20:
            		;	// Stops C# compiler whining that label 'loop20' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 19, equalityExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "equalityExpression"

    public class relationalExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "relationalExpression"
    // RelinqScript.g:116:1: relationalExpression : shiftExpression ( ( '<' | '>' | '<=' | '>=' ) shiftExpression )* ;
    public RelinqScriptParser.relationalExpression_return relationalExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.relationalExpression_return retval = new RelinqScriptParser.relationalExpression_return();
        retval.Start = input.LT(1);
        int relationalExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set81 = null;
        RelinqScriptParser.shiftExpression_return shiftExpression80 = default(RelinqScriptParser.shiftExpression_return);

        RelinqScriptParser.shiftExpression_return shiftExpression82 = default(RelinqScriptParser.shiftExpression_return);


        object set81_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 20) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:117:2: ( shiftExpression ( ( '<' | '>' | '<=' | '>=' ) shiftExpression )* )
            // RelinqScript.g:117:4: shiftExpression ( ( '<' | '>' | '<=' | '>=' ) shiftExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_shiftExpression_in_relationalExpression516);
            	shiftExpression80 = shiftExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, shiftExpression80.Tree);
            	// RelinqScript.g:117:20: ( ( '<' | '>' | '<=' | '>=' ) shiftExpression )*
            	do 
            	{
            	    int alt21 = 2;
            	    int LA21_0 = input.LA(1);

            	    if ( ((LA21_0 >= 56 && LA21_0 <= 59)) )
            	    {
            	        int LA21_2 = input.LA(2);

            	        if ( (synpred35_RelinqScript()) )
            	        {
            	            alt21 = 1;
            	        }


            	    }


            	    switch (alt21) 
            		{
            			case 1 :
            			    // RelinqScript.g:117:21: ( '<' | '>' | '<=' | '>=' ) shiftExpression
            			    {
            			    	set81 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= 56 && input.LA(1) <= 59) ) 
            			    	{
            			    	    input.Consume();
            			    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set81));
            			    	    state.errorRecovery = false;state.failed = false;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_shiftExpression_in_relationalExpression535);
            			    	shiftExpression82 = shiftExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, shiftExpression82.Tree);

            			    }
            			    break;

            			default:
            			    goto loop21;
            	    }
            	} while (true);

            	loop21:
            		;	// Stops C# compiler whining that label 'loop21' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 20, relationalExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "relationalExpression"

    public class shiftExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "shiftExpression"
    // RelinqScript.g:120:1: shiftExpression : additiveExpression ( ( '<<' | '>>' | '>>>' ) additiveExpression )* ;
    public RelinqScriptParser.shiftExpression_return shiftExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.shiftExpression_return retval = new RelinqScriptParser.shiftExpression_return();
        retval.Start = input.LT(1);
        int shiftExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set84 = null;
        RelinqScriptParser.additiveExpression_return additiveExpression83 = default(RelinqScriptParser.additiveExpression_return);

        RelinqScriptParser.additiveExpression_return additiveExpression85 = default(RelinqScriptParser.additiveExpression_return);


        object set84_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 21) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:121:2: ( additiveExpression ( ( '<<' | '>>' | '>>>' ) additiveExpression )* )
            // RelinqScript.g:121:4: additiveExpression ( ( '<<' | '>>' | '>>>' ) additiveExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_additiveExpression_in_shiftExpression549);
            	additiveExpression83 = additiveExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, additiveExpression83.Tree);
            	// RelinqScript.g:121:23: ( ( '<<' | '>>' | '>>>' ) additiveExpression )*
            	do 
            	{
            	    int alt22 = 2;
            	    int LA22_0 = input.LA(1);

            	    if ( ((LA22_0 >= 60 && LA22_0 <= 62)) )
            	    {
            	        int LA22_2 = input.LA(2);

            	        if ( (synpred38_RelinqScript()) )
            	        {
            	            alt22 = 1;
            	        }


            	    }


            	    switch (alt22) 
            		{
            			case 1 :
            			    // RelinqScript.g:121:24: ( '<<' | '>>' | '>>>' ) additiveExpression
            			    {
            			    	set84 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= 60 && input.LA(1) <= 62) ) 
            			    	{
            			    	    input.Consume();
            			    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set84));
            			    	    state.errorRecovery = false;state.failed = false;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_additiveExpression_in_shiftExpression564);
            			    	additiveExpression85 = additiveExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, additiveExpression85.Tree);

            			    }
            			    break;

            			default:
            			    goto loop22;
            	    }
            	} while (true);

            	loop22:
            		;	// Stops C# compiler whining that label 'loop22' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 21, shiftExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "shiftExpression"

    public class additiveExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "additiveExpression"
    // RelinqScript.g:124:1: additiveExpression : multiplicativeExpression ( ( '+' | '-' ) multiplicativeExpression )* ;
    public RelinqScriptParser.additiveExpression_return additiveExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.additiveExpression_return retval = new RelinqScriptParser.additiveExpression_return();
        retval.Start = input.LT(1);
        int additiveExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set87 = null;
        RelinqScriptParser.multiplicativeExpression_return multiplicativeExpression86 = default(RelinqScriptParser.multiplicativeExpression_return);

        RelinqScriptParser.multiplicativeExpression_return multiplicativeExpression88 = default(RelinqScriptParser.multiplicativeExpression_return);


        object set87_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 22) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:125:2: ( multiplicativeExpression ( ( '+' | '-' ) multiplicativeExpression )* )
            // RelinqScript.g:125:4: multiplicativeExpression ( ( '+' | '-' ) multiplicativeExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_multiplicativeExpression_in_additiveExpression578);
            	multiplicativeExpression86 = multiplicativeExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, multiplicativeExpression86.Tree);
            	// RelinqScript.g:125:29: ( ( '+' | '-' ) multiplicativeExpression )*
            	do 
            	{
            	    int alt23 = 2;
            	    int LA23_0 = input.LA(1);

            	    if ( ((LA23_0 >= 63 && LA23_0 <= 64)) )
            	    {
            	        int LA23_2 = input.LA(2);

            	        if ( (synpred40_RelinqScript()) )
            	        {
            	            alt23 = 1;
            	        }


            	    }


            	    switch (alt23) 
            		{
            			case 1 :
            			    // RelinqScript.g:125:30: ( '+' | '-' ) multiplicativeExpression
            			    {
            			    	set87 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= 63 && input.LA(1) <= 64) ) 
            			    	{
            			    	    input.Consume();
            			    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set87));
            			    	    state.errorRecovery = false;state.failed = false;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_multiplicativeExpression_in_additiveExpression589);
            			    	multiplicativeExpression88 = multiplicativeExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, multiplicativeExpression88.Tree);

            			    }
            			    break;

            			default:
            			    goto loop23;
            	    }
            	} while (true);

            	loop23:
            		;	// Stops C# compiler whining that label 'loop23' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 22, additiveExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "additiveExpression"

    public class multiplicativeExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "multiplicativeExpression"
    // RelinqScript.g:128:1: multiplicativeExpression : unaryExpression ( ( '*' | '/' | '%' ) unaryExpression )* ;
    public RelinqScriptParser.multiplicativeExpression_return multiplicativeExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.multiplicativeExpression_return retval = new RelinqScriptParser.multiplicativeExpression_return();
        retval.Start = input.LT(1);
        int multiplicativeExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set90 = null;
        RelinqScriptParser.unaryExpression_return unaryExpression89 = default(RelinqScriptParser.unaryExpression_return);

        RelinqScriptParser.unaryExpression_return unaryExpression91 = default(RelinqScriptParser.unaryExpression_return);


        object set90_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 23) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:129:2: ( unaryExpression ( ( '*' | '/' | '%' ) unaryExpression )* )
            // RelinqScript.g:129:4: unaryExpression ( ( '*' | '/' | '%' ) unaryExpression )*
            {
            	root_0 = (object)adaptor.GetNilNode();

            	PushFollow(FOLLOW_unaryExpression_in_multiplicativeExpression603);
            	unaryExpression89 = unaryExpression();
            	state.followingStackPointer--;
            	if (state.failed) return retval;
            	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, unaryExpression89.Tree);
            	// RelinqScript.g:129:20: ( ( '*' | '/' | '%' ) unaryExpression )*
            	do 
            	{
            	    int alt24 = 2;
            	    int LA24_0 = input.LA(1);

            	    if ( ((LA24_0 >= 65 && LA24_0 <= 67)) )
            	    {
            	        int LA24_2 = input.LA(2);

            	        if ( (synpred43_RelinqScript()) )
            	        {
            	            alt24 = 1;
            	        }


            	    }


            	    switch (alt24) 
            		{
            			case 1 :
            			    // RelinqScript.g:129:21: ( '*' | '/' | '%' ) unaryExpression
            			    {
            			    	set90 = (IToken)input.LT(1);
            			    	if ( (input.LA(1) >= 65 && input.LA(1) <= 67) ) 
            			    	{
            			    	    input.Consume();
            			    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set90));
            			    	    state.errorRecovery = false;state.failed = false;
            			    	}
            			    	else 
            			    	{
            			    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    throw mse;
            			    	}

            			    	PushFollow(FOLLOW_unaryExpression_in_multiplicativeExpression618);
            			    	unaryExpression91 = unaryExpression();
            			    	state.followingStackPointer--;
            			    	if (state.failed) return retval;
            			    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, unaryExpression91.Tree);

            			    }
            			    break;

            			default:
            			    goto loop24;
            	    }
            	} while (true);

            	loop24:
            		;	// Stops C# compiler whining that label 'loop24' has no statements


            }

            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 23, multiplicativeExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "multiplicativeExpression"

    public class unaryExpression_return : ParserRuleReturnScope
    {
        private object tree;
        override public object Tree
        {
        	get { return tree; }
        	set { tree = (object) value; }
        }
    };

    // $ANTLR start "unaryExpression"
    // RelinqScript.g:132:1: unaryExpression : ( expression | ( '++' | '--' | '+' | '-' | '~' | '!' ) unaryExpression );
    public RelinqScriptParser.unaryExpression_return unaryExpression() // throws RecognitionException [1]
    {   
        RelinqScriptParser.unaryExpression_return retval = new RelinqScriptParser.unaryExpression_return();
        retval.Start = input.LT(1);
        int unaryExpression_StartIndex = input.Index();
        object root_0 = null;

        IToken set93 = null;
        RelinqScriptParser.expression_return expression92 = default(RelinqScriptParser.expression_return);

        RelinqScriptParser.unaryExpression_return unaryExpression94 = default(RelinqScriptParser.unaryExpression_return);


        object set93_tree=null;

        try 
    	{
    	    if ( (state.backtracking > 0) && AlreadyParsedRule(input, 24) ) 
    	    {
    	    	return retval; 
    	    }
            // RelinqScript.g:133:2: ( expression | ( '++' | '--' | '+' | '-' | '~' | '!' ) unaryExpression )
            int alt25 = 2;
            int LA25_0 = input.LA(1);

            if ( ((LA25_0 >= StringLiteral && LA25_0 <= Identifier) || LA25_0 == 32 || (LA25_0 >= 34 && LA25_0 <= 38) || LA25_0 == 41 || LA25_0 == 44) )
            {
                alt25 = 1;
            }
            else if ( ((LA25_0 >= 63 && LA25_0 <= 64) || (LA25_0 >= 68 && LA25_0 <= 71)) )
            {
                alt25 = 2;
            }
            else 
            {
                if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                NoViableAltException nvae_d25s0 =
                    new NoViableAltException("", 25, 0, input);

                throw nvae_d25s0;
            }
            switch (alt25) 
            {
                case 1 :
                    // RelinqScript.g:133:4: expression
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	PushFollow(FOLLOW_expression_in_unaryExpression632);
                    	expression92 = expression();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, expression92.Tree);

                    }
                    break;
                case 2 :
                    // RelinqScript.g:134:4: ( '++' | '--' | '+' | '-' | '~' | '!' ) unaryExpression
                    {
                    	root_0 = (object)adaptor.GetNilNode();

                    	set93 = (IToken)input.LT(1);
                    	if ( (input.LA(1) >= 63 && input.LA(1) <= 64) || (input.LA(1) >= 68 && input.LA(1) <= 71) ) 
                    	{
                    	    input.Consume();
                    	    if ( state.backtracking == 0 ) adaptor.AddChild(root_0, (object)adaptor.Create(set93));
                    	    state.errorRecovery = false;state.failed = false;
                    	}
                    	else 
                    	{
                    	    if ( state.backtracking > 0 ) {state.failed = true; return retval;}
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    throw mse;
                    	}

                    	PushFollow(FOLLOW_unaryExpression_in_unaryExpression661);
                    	unaryExpression94 = unaryExpression();
                    	state.followingStackPointer--;
                    	if (state.failed) return retval;
                    	if ( state.backtracking == 0 ) adaptor.AddChild(root_0, unaryExpression94.Tree);

                    }
                    break;

            }
            retval.Stop = input.LT(-1);

            if ( state.backtracking==0 )
            {	retval.Tree = (object)adaptor.RulePostProcessing(root_0);
            	adaptor.SetTokenBoundaries(retval.Tree, (IToken) retval.Start, (IToken) retval.Stop);}
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
    	// Conversion of the second argument necessary, but harmless
    	retval.Tree = (object)adaptor.ErrorNode(input, (IToken) retval.Start, input.LT(-1), re);

        }
        finally 
    	{
            if ( state.backtracking > 0 ) 
            {
            	Memoize(input, 24, unaryExpression_StartIndex); 
            }
        }
        return retval;
    }
    // $ANTLR end "unaryExpression"

    // $ANTLR start "synpred2_RelinqScript"
    public void synpred2_RelinqScript_fragment() {
        // RelinqScript.g:29:24: ( operatorExpression )
        // RelinqScript.g:29:24: operatorExpression
        {
        	PushFollow(FOLLOW_operatorExpression_in_synpred2_RelinqScript89);
        	operatorExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred2_RelinqScript"

    // $ANTLR start "synpred3_RelinqScript"
    public void synpred3_RelinqScript_fragment() {
        // RelinqScript.g:29:45: ( memberExpression )
        // RelinqScript.g:29:45: memberExpression
        {
        	PushFollow(FOLLOW_memberExpression_in_synpred3_RelinqScript93);
        	memberExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred3_RelinqScript"

    // $ANTLR start "synpred20_RelinqScript"
    public void synpred20_RelinqScript_fragment() {
        // RelinqScript.g:78:4: ( '.' Identifier '(' callParameters ')' )
        // RelinqScript.g:78:4: '.' Identifier '(' callParameters ')'
        {
        	Match(input,46,FOLLOW_46_in_synpred20_RelinqScript320); if (state.failed) return ;
        	Match(input,Identifier,FOLLOW_Identifier_in_synpred20_RelinqScript322); if (state.failed) return ;
        	Match(input,32,FOLLOW_32_in_synpred20_RelinqScript324); if (state.failed) return ;
        	PushFollow(FOLLOW_callParameters_in_synpred20_RelinqScript326);
        	callParameters();
        	state.followingStackPointer--;
        	if (state.failed) return ;
        	Match(input,33,FOLLOW_33_in_synpred20_RelinqScript328); if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred20_RelinqScript"

    // $ANTLR start "synpred23_RelinqScript"
    public void synpred23_RelinqScript_fragment() {
        // RelinqScript.g:93:26: ( '||' logicalANDExpression )
        // RelinqScript.g:93:26: '||' logicalANDExpression
        {
        	Match(input,47,FOLLOW_47_in_synpred23_RelinqScript391); if (state.failed) return ;
        	PushFollow(FOLLOW_logicalANDExpression_in_synpred23_RelinqScript393);
        	logicalANDExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred23_RelinqScript"

    // $ANTLR start "synpred24_RelinqScript"
    public void synpred24_RelinqScript_fragment() {
        // RelinqScript.g:97:25: ( '&&' bitwiseORExpression )
        // RelinqScript.g:97:25: '&&' bitwiseORExpression
        {
        	Match(input,48,FOLLOW_48_in_synpred24_RelinqScript410); if (state.failed) return ;
        	PushFollow(FOLLOW_bitwiseORExpression_in_synpred24_RelinqScript412);
        	bitwiseORExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred24_RelinqScript"

    // $ANTLR start "synpred25_RelinqScript"
    public void synpred25_RelinqScript_fragment() {
        // RelinqScript.g:101:26: ( '|' bitwiseXORExpression )
        // RelinqScript.g:101:26: '|' bitwiseXORExpression
        {
        	Match(input,49,FOLLOW_49_in_synpred25_RelinqScript429); if (state.failed) return ;
        	PushFollow(FOLLOW_bitwiseXORExpression_in_synpred25_RelinqScript431);
        	bitwiseXORExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred25_RelinqScript"

    // $ANTLR start "synpred26_RelinqScript"
    public void synpred26_RelinqScript_fragment() {
        // RelinqScript.g:105:26: ( '^' bitwiseANDExpression )
        // RelinqScript.g:105:26: '^' bitwiseANDExpression
        {
        	Match(input,50,FOLLOW_50_in_synpred26_RelinqScript448); if (state.failed) return ;
        	PushFollow(FOLLOW_bitwiseANDExpression_in_synpred26_RelinqScript450);
        	bitwiseANDExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred26_RelinqScript"

    // $ANTLR start "synpred27_RelinqScript"
    public void synpred27_RelinqScript_fragment() {
        // RelinqScript.g:109:24: ( '&' equalityExpression )
        // RelinqScript.g:109:24: '&' equalityExpression
        {
        	Match(input,51,FOLLOW_51_in_synpred27_RelinqScript467); if (state.failed) return ;
        	PushFollow(FOLLOW_equalityExpression_in_synpred27_RelinqScript469);
        	equalityExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred27_RelinqScript"

    // $ANTLR start "synpred31_RelinqScript"
    public void synpred31_RelinqScript_fragment() {
        // RelinqScript.g:113:26: ( ( '==' | '!=' | '===' | '!==' ) relationalExpression )
        // RelinqScript.g:113:26: ( '==' | '!=' | '===' | '!==' ) relationalExpression
        {
        	if ( (input.LA(1) >= 52 && input.LA(1) <= 55) ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}

        	PushFollow(FOLLOW_relationalExpression_in_synpred31_RelinqScript502);
        	relationalExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred31_RelinqScript"

    // $ANTLR start "synpred35_RelinqScript"
    public void synpred35_RelinqScript_fragment() {
        // RelinqScript.g:117:21: ( ( '<' | '>' | '<=' | '>=' ) shiftExpression )
        // RelinqScript.g:117:21: ( '<' | '>' | '<=' | '>=' ) shiftExpression
        {
        	if ( (input.LA(1) >= 56 && input.LA(1) <= 59) ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}

        	PushFollow(FOLLOW_shiftExpression_in_synpred35_RelinqScript535);
        	shiftExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred35_RelinqScript"

    // $ANTLR start "synpred38_RelinqScript"
    public void synpred38_RelinqScript_fragment() {
        // RelinqScript.g:121:24: ( ( '<<' | '>>' | '>>>' ) additiveExpression )
        // RelinqScript.g:121:24: ( '<<' | '>>' | '>>>' ) additiveExpression
        {
        	if ( (input.LA(1) >= 60 && input.LA(1) <= 62) ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}

        	PushFollow(FOLLOW_additiveExpression_in_synpred38_RelinqScript564);
        	additiveExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred38_RelinqScript"

    // $ANTLR start "synpred40_RelinqScript"
    public void synpred40_RelinqScript_fragment() {
        // RelinqScript.g:125:30: ( ( '+' | '-' ) multiplicativeExpression )
        // RelinqScript.g:125:30: ( '+' | '-' ) multiplicativeExpression
        {
        	if ( (input.LA(1) >= 63 && input.LA(1) <= 64) ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}

        	PushFollow(FOLLOW_multiplicativeExpression_in_synpred40_RelinqScript589);
        	multiplicativeExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred40_RelinqScript"

    // $ANTLR start "synpred43_RelinqScript"
    public void synpred43_RelinqScript_fragment() {
        // RelinqScript.g:129:21: ( ( '*' | '/' | '%' ) unaryExpression )
        // RelinqScript.g:129:21: ( '*' | '/' | '%' ) unaryExpression
        {
        	if ( (input.LA(1) >= 65 && input.LA(1) <= 67) ) 
        	{
        	    input.Consume();
        	    state.errorRecovery = false;state.failed = false;
        	}
        	else 
        	{
        	    if ( state.backtracking > 0 ) {state.failed = true; return ;}
        	    MismatchedSetException mse = new MismatchedSetException(null,input);
        	    throw mse;
        	}

        	PushFollow(FOLLOW_unaryExpression_in_synpred43_RelinqScript618);
        	unaryExpression();
        	state.followingStackPointer--;
        	if (state.failed) return ;

        }
    }
    // $ANTLR end "synpred43_RelinqScript"

    // Delegated rules

   	public bool synpred27_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred27_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred38_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred38_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred40_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred40_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred43_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred43_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred23_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred23_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred20_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred20_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred24_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred24_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred2_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred2_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred26_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred26_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred25_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred25_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred31_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred31_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred3_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred3_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}
   	public bool synpred35_RelinqScript() 
   	{
   	    state.backtracking++;
   	    int start = input.Mark();
   	    try 
   	    {
   	        synpred35_RelinqScript_fragment(); // can never throw exception
   	    }
   	    catch (RecognitionException re) 
   	    {
   	        Console.Error.WriteLine("impossible: "+re);
   	    }
   	    bool success = !state.failed;
   	    input.Rewind(start);
   	    state.backtracking--;
   	    state.failed = false;
   	    return success;
   	}


   	protected DFA1 dfa1;
	private void InitializeCyclicDFAs()
	{
    	this.dfa1 = new DFA1(this);
	    this.dfa1.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA1_SpecialStateTransition);
	}

    const string DFA1_eotS =
        "\x20\uffff";
    const string DFA1_eofS =
        "\x01\x01\x1f\uffff";
    const string DFA1_minS =
        "\x01\x04\x06\uffff\x01\x00\x08\uffff\x0d\x00\x03\uffff";
    const string DFA1_maxS =
        "\x01\x47\x06\uffff\x01\x00\x08\uffff\x0d\x00\x03\uffff";
    const string DFA1_acceptS =
        "\x01\uffff\x01\x03\x1c\uffff\x01\x01\x01\x02";
    const string DFA1_specialS =
        "\x07\uffff\x01\x00\x08\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01"+
        "\x05\x01\x06\x01\x07\x01\x08\x01\x09\x01\x0a\x01\x0b\x01\x0c\x01"+
        "\x0d\x03\uffff}>";
    static readonly string[] DFA1_transitionS = {
            "\x01\x15\x01\x16\x01\x1a\x18\uffff\x01\x01\x01\x10\x01\x01"+
            "\x01\x11\x01\x12\x01\x13\x01\x14\x01\x17\x02\x01\x01\x18\x01"+
            "\uffff\x01\x01\x01\x19\x01\uffff\x01\x1c\x10\x01\x02\x07\x03"+
            "\x01\x04\x1b",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\uffff",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "\x01\uffff",
            "",
            "",
            ""
    };

    static readonly short[] DFA1_eot = DFA.UnpackEncodedString(DFA1_eotS);
    static readonly short[] DFA1_eof = DFA.UnpackEncodedString(DFA1_eofS);
    static readonly char[] DFA1_min = DFA.UnpackEncodedStringToUnsignedChars(DFA1_minS);
    static readonly char[] DFA1_max = DFA.UnpackEncodedStringToUnsignedChars(DFA1_maxS);
    static readonly short[] DFA1_accept = DFA.UnpackEncodedString(DFA1_acceptS);
    static readonly short[] DFA1_special = DFA.UnpackEncodedString(DFA1_specialS);
    static readonly short[][] DFA1_transition = DFA.UnpackEncodedStringArray(DFA1_transitionS);

    protected class DFA1 : DFA
    {
        public DFA1(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 1;
            this.eot = DFA1_eot;
            this.eof = DFA1_eof;
            this.min = DFA1_min;
            this.max = DFA1_max;
            this.accept = DFA1_accept;
            this.special = DFA1_special;
            this.transition = DFA1_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 29:23: ( operatorExpression | memberExpression )*"; }
        }

    }


    protected internal int DFA1_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            ITokenStream input = (ITokenStream)_input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA1_7 = input.LA(1);

                   	 
                   	int index1_7 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_7);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 1 : 
                   	int LA1_16 = input.LA(1);

                   	 
                   	int index1_16 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_16);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 2 : 
                   	int LA1_17 = input.LA(1);

                   	 
                   	int index1_17 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_17);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 3 : 
                   	int LA1_18 = input.LA(1);

                   	 
                   	int index1_18 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_18);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 4 : 
                   	int LA1_19 = input.LA(1);

                   	 
                   	int index1_19 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_19);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 5 : 
                   	int LA1_20 = input.LA(1);

                   	 
                   	int index1_20 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_20);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 6 : 
                   	int LA1_21 = input.LA(1);

                   	 
                   	int index1_21 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_21);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 7 : 
                   	int LA1_22 = input.LA(1);

                   	 
                   	int index1_22 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_22);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 8 : 
                   	int LA1_23 = input.LA(1);

                   	 
                   	int index1_23 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_23);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 9 : 
                   	int LA1_24 = input.LA(1);

                   	 
                   	int index1_24 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_24);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 10 : 
                   	int LA1_25 = input.LA(1);

                   	 
                   	int index1_25 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_25);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 11 : 
                   	int LA1_26 = input.LA(1);

                   	 
                   	int index1_26 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_26);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 12 : 
                   	int LA1_27 = input.LA(1);

                   	 
                   	int index1_27 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred2_RelinqScript()) ) { s = 30; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_27);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 13 : 
                   	int LA1_28 = input.LA(1);

                   	 
                   	int index1_28 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (synpred3_RelinqScript()) ) { s = 31; }

                   	else if ( (true) ) { s = 1; }

                   	 
                   	input.Seek(index1_28);
                   	if ( s >= 0 ) return s;
                   	break;
        }
        if (state.backtracking > 0) {state.failed = true; return -1;}
        NoViableAltException nvae =
            new NoViableAltException(dfa.Description, 1, _s, input);
        dfa.Error(nvae);
        throw nvae;
    }
 

    public static readonly BitSet FOLLOW_expression_in_script61 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_31_in_script63 = new BitSet(new ulong[]{0x0000000000000000UL});
    public static readonly BitSet FOLLOW_EOF_in_script65 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_expression77 = new BitSet(new ulong[]{0x0000127D00000070UL});
    public static readonly BitSet FOLLOW_expression_in_expression79 = new BitSet(new ulong[]{0x0000000200000000UL});
    public static readonly BitSet FOLLOW_33_in_expression81 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_constantExpression_in_expression86 = new BitSet(new ulong[]{0x8000527D00000072UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_operatorExpression_in_expression89 = new BitSet(new ulong[]{0x8000527D00000072UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_memberExpression_in_expression93 = new BitSet(new ulong[]{0x8000527D00000072UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_objectDefinition_in_constantExpression107 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_objectReference_in_constantExpression112 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_34_in_objectDefinition128 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_35_in_objectDefinition133 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_36_in_objectDefinition138 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_37_in_objectDefinition143 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_StringLiteral_in_objectDefinition148 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NumericLiteral_in_objectDefinition153 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_arrayConstructor_in_objectDefinition158 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_objectConstructor_in_objectDefinition163 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_lambda_in_objectDefinition168 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_38_in_arrayConstructor180 = new BitSet(new ulong[]{0x000013FD00000070UL});
    public static readonly BitSet FOLLOW_expression_in_arrayConstructor182 = new BitSet(new ulong[]{0x0000018000000000UL});
    public static readonly BitSet FOLLOW_39_in_arrayConstructor186 = new BitSet(new ulong[]{0x000013FD00000070UL});
    public static readonly BitSet FOLLOW_expression_in_arrayConstructor189 = new BitSet(new ulong[]{0x0000018000000000UL});
    public static readonly BitSet FOLLOW_40_in_arrayConstructor195 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_41_in_objectConstructor207 = new BitSet(new ulong[]{0x0000088000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_objectConstructor210 = new BitSet(new ulong[]{0x0000040000000000UL});
    public static readonly BitSet FOLLOW_42_in_objectConstructor212 = new BitSet(new ulong[]{0x0000127D00000070UL});
    public static readonly BitSet FOLLOW_expression_in_objectConstructor214 = new BitSet(new ulong[]{0x0000088000000000UL});
    public static readonly BitSet FOLLOW_39_in_objectConstructor219 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_objectConstructor222 = new BitSet(new ulong[]{0x0000040000000000UL});
    public static readonly BitSet FOLLOW_42_in_objectConstructor224 = new BitSet(new ulong[]{0x0000127D00000070UL});
    public static readonly BitSet FOLLOW_expression_in_objectConstructor226 = new BitSet(new ulong[]{0x0000088000000000UL});
    public static readonly BitSet FOLLOW_43_in_objectConstructor231 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_44_in_lambda243 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_lambdaParameters_in_lambda245 = new BitSet(new ulong[]{0x0000020000000000UL});
    public static readonly BitSet FOLLOW_lambdaBody_in_lambda247 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_lambdaParameters259 = new BitSet(new ulong[]{0x0000000200000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_lambdaParameters262 = new BitSet(new ulong[]{0x0000008200000000UL});
    public static readonly BitSet FOLLOW_39_in_lambdaParameters265 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_lambdaParameters267 = new BitSet(new ulong[]{0x0000008200000000UL});
    public static readonly BitSet FOLLOW_33_in_lambdaParameters273 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_41_in_lambdaBody285 = new BitSet(new ulong[]{0x0000200000000000UL});
    public static readonly BitSet FOLLOW_45_in_lambdaBody287 = new BitSet(new ulong[]{0x0000127D00000070UL});
    public static readonly BitSet FOLLOW_expression_in_lambdaBody289 = new BitSet(new ulong[]{0x0000000080000000UL});
    public static readonly BitSet FOLLOW_31_in_lambdaBody291 = new BitSet(new ulong[]{0x0000080000000000UL});
    public static readonly BitSet FOLLOW_43_in_lambdaBody293 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_Identifier_in_objectReference305 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_46_in_memberExpression320 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_memberExpression322 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_32_in_memberExpression324 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_callParameters_in_memberExpression326 = new BitSet(new ulong[]{0x0000000200000000UL});
    public static readonly BitSet FOLLOW_33_in_memberExpression328 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_46_in_memberExpression333 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_memberExpression335 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_32_in_callParameters347 = new BitSet(new ulong[]{0x0000127F00000070UL});
    public static readonly BitSet FOLLOW_expression_in_callParameters350 = new BitSet(new ulong[]{0x0000008200000000UL});
    public static readonly BitSet FOLLOW_39_in_callParameters353 = new BitSet(new ulong[]{0x0000127D00000070UL});
    public static readonly BitSet FOLLOW_expression_in_callParameters355 = new BitSet(new ulong[]{0x0000008200000000UL});
    public static readonly BitSet FOLLOW_33_in_callParameters361 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_logicalORExpression_in_operatorExpression376 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_logicalANDExpression_in_logicalORExpression388 = new BitSet(new ulong[]{0x0000800000000002UL});
    public static readonly BitSet FOLLOW_47_in_logicalORExpression391 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_logicalANDExpression_in_logicalORExpression393 = new BitSet(new ulong[]{0x0000800000000002UL});
    public static readonly BitSet FOLLOW_bitwiseORExpression_in_logicalANDExpression407 = new BitSet(new ulong[]{0x0001000000000002UL});
    public static readonly BitSet FOLLOW_48_in_logicalANDExpression410 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseORExpression_in_logicalANDExpression412 = new BitSet(new ulong[]{0x0001000000000002UL});
    public static readonly BitSet FOLLOW_bitwiseXORExpression_in_bitwiseORExpression426 = new BitSet(new ulong[]{0x0002000000000002UL});
    public static readonly BitSet FOLLOW_49_in_bitwiseORExpression429 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseXORExpression_in_bitwiseORExpression431 = new BitSet(new ulong[]{0x0002000000000002UL});
    public static readonly BitSet FOLLOW_bitwiseANDExpression_in_bitwiseXORExpression445 = new BitSet(new ulong[]{0x0004000000000002UL});
    public static readonly BitSet FOLLOW_50_in_bitwiseXORExpression448 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseANDExpression_in_bitwiseXORExpression450 = new BitSet(new ulong[]{0x0004000000000002UL});
    public static readonly BitSet FOLLOW_equalityExpression_in_bitwiseANDExpression464 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_51_in_bitwiseANDExpression467 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_equalityExpression_in_bitwiseANDExpression469 = new BitSet(new ulong[]{0x0008000000000002UL});
    public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression483 = new BitSet(new ulong[]{0x00F0000000000002UL});
    public static readonly BitSet FOLLOW_set_in_equalityExpression486 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression502 = new BitSet(new ulong[]{0x00F0000000000002UL});
    public static readonly BitSet FOLLOW_shiftExpression_in_relationalExpression516 = new BitSet(new ulong[]{0x0F00000000000002UL});
    public static readonly BitSet FOLLOW_set_in_relationalExpression519 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_shiftExpression_in_relationalExpression535 = new BitSet(new ulong[]{0x0F00000000000002UL});
    public static readonly BitSet FOLLOW_additiveExpression_in_shiftExpression549 = new BitSet(new ulong[]{0x7000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_shiftExpression552 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_additiveExpression_in_shiftExpression564 = new BitSet(new ulong[]{0x7000000000000002UL});
    public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression578 = new BitSet(new ulong[]{0x8000000000000002UL,0x0000000000000001UL});
    public static readonly BitSet FOLLOW_set_in_additiveExpression581 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression589 = new BitSet(new ulong[]{0x8000000000000002UL,0x0000000000000001UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression603 = new BitSet(new ulong[]{0x0000000000000002UL,0x000000000000000EUL});
    public static readonly BitSet FOLLOW_set_in_multiplicativeExpression606 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression618 = new BitSet(new ulong[]{0x0000000000000002UL,0x000000000000000EUL});
    public static readonly BitSet FOLLOW_expression_in_unaryExpression632 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_unaryExpression637 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_unaryExpression661 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_operatorExpression_in_synpred2_RelinqScript89 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_memberExpression_in_synpred3_RelinqScript93 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_46_in_synpred20_RelinqScript320 = new BitSet(new ulong[]{0x0000000000000040UL});
    public static readonly BitSet FOLLOW_Identifier_in_synpred20_RelinqScript322 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_32_in_synpred20_RelinqScript324 = new BitSet(new ulong[]{0x0000000100000000UL});
    public static readonly BitSet FOLLOW_callParameters_in_synpred20_RelinqScript326 = new BitSet(new ulong[]{0x0000000200000000UL});
    public static readonly BitSet FOLLOW_33_in_synpred20_RelinqScript328 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_47_in_synpred23_RelinqScript391 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_logicalANDExpression_in_synpred23_RelinqScript393 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_48_in_synpred24_RelinqScript410 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseORExpression_in_synpred24_RelinqScript412 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_49_in_synpred25_RelinqScript429 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseXORExpression_in_synpred25_RelinqScript431 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_50_in_synpred26_RelinqScript448 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_bitwiseANDExpression_in_synpred26_RelinqScript450 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_51_in_synpred27_RelinqScript467 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_equalityExpression_in_synpred27_RelinqScript469 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_synpred31_RelinqScript486 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_relationalExpression_in_synpred31_RelinqScript502 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_synpred35_RelinqScript519 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_shiftExpression_in_synpred35_RelinqScript535 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_synpred38_RelinqScript552 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_additiveExpression_in_synpred38_RelinqScript564 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_synpred40_RelinqScript581 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_multiplicativeExpression_in_synpred40_RelinqScript589 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_synpred43_RelinqScript606 = new BitSet(new ulong[]{0x8000127D00000070UL,0x00000000000000F1UL});
    public static readonly BitSet FOLLOW_unaryExpression_in_synpred43_RelinqScript618 = new BitSet(new ulong[]{0x0000000000000002UL});

}
}