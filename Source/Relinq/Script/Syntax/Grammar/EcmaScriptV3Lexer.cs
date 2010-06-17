// $ANTLR 3.1.1 EcmaScriptV3.g 2009-04-28 11:08:38
// The variable 'variable' is assigned but its value is never used.
using Relinq.Exceptions.JSToCSharp.Parser;

#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162
namespace  Relinq.Script.Syntax.Grammar 
{

using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class EcmaScriptV3Lexer : Lexer {
    public const int PACKAGE = 52;
    public const int FUNCTION = 17;
    public const int LOR = 95;
    public const int VT = 134;
    public const int SHR = 87;
    public const int RegularExpressionChar = 171;
    public const int LT = 72;
    public const int WHILE = 30;
    public const int MOD = 83;
    public const int SHL = 86;
    public const int CONST = 37;
    public const int BackslashSequence = 169;
    public const int LS = 142;
    public const int CASE = 8;
    public const int CHAR = 35;
    public const int NEW = 21;
    public const int DO = 13;
    public const int DQUOTE = 131;
    public const int NOT = 92;
    public const int DecimalDigit = 152;
    public const int BYFIELD = 114;
    public const int EOF = -1;
    public const int BREAK = 7;
    public const int CEXPR = 117;
    public const int DIVASS = 110;
    public const int Identifier = 148;
    public const int BYINDEX = 115;
    public const int INC = 84;
    public const int RPAREN = 66;
    public const int FINAL = 43;
    public const int FORSTEP = 120;
    public const int IMPORT = 47;
    public const int EOL = 145;
    public const int POS = 129;
    public const int OctalDigit = 156;
    public const int RETURN = 22;
    public const int THIS = 24;
    public const int DOUBLE = 39;
    public const int ARGS = 111;
    public const int ExponentPart = 157;
    public const int WhiteSpace = 139;
    public const int VAR = 28;
    public const int EXPORT = 41;
    public const int VOID = 29;
    public const int LABELLED = 122;
    public const int SUPER = 58;
    public const int GOTO = 45;
    public const int EQ = 76;
    public const int XORASS = 108;
    public const int ADDASS = 99;
    public const int ARRAY = 112;
    public const int SHU = 88;
    public const int RBRACK = 68;
    public const int RBRACE = 64;
    public const int PRIVATE = 53;
    public const int STATIC = 57;
    public const int INV = 93;
    public const int SWITCH = 23;
    public const int NULL = 4;
    public const int ELSE = 14;
    public const int NATIVE = 51;
    public const int THROWS = 60;
    public const int INT = 48;
    public const int DELETE = 12;
    public const int MUL = 82;
    public const int IdentifierStartASCII = 151;
    public const int TRY = 26;
    public const int FF = 135;
    public const int SHLASS = 103;
    public const int OctalEscapeSequence = 165;
    public const int USP = 138;
    public const int RegularExpressionFirstChar = 170;
    public const int ANDASS = 106;
    public const int TYPEOF = 27;
    public const int IdentifierNameASCIIStart = 154;
    public const int QUE = 96;
    public const int OR = 90;
    public const int DEBUGGER = 38;
    public const int GT = 73;
    public const int PDEC = 127;
    public const int CALL = 116;
    public const int CharacterEscapeSequence = 163;
    public const int CATCH = 9;
    public const int FALSE = 6;
    public const int EscapeSequence = 168;
    public const int LAND = 94;
    public const int MULASS = 101;
    public const int THROW = 25;
    public const int PINC = 128;
    public const int DEC = 85;
    public const int PROTECTED = 54;
    public const int OctalIntegerLiteral = 161;
    public const int CLASS = 36;
    public const int LBRACK = 67;
    public const int ORASS = 107;
    public const int HexEscapeSequence = 166;
    public const int NAMEDVALUE = 123;
    public const int SingleLineComment = 147;
    public const int GTE = 75;
    public const int LBRACE = 63;
    public const int FOR = 16;
    public const int SUB = 81;
    public const int RegularExpressionLiteral = 155;
    public const int FLOAT = 44;
    public const int ABSTRACT = 32;
    public const int AND = 89;
    public const int DecimalIntegerLiteral = 159;
    public const int LTE = 74;
    public const int HexDigit = 150;
    public const int LPAREN = 65;
    public const int IF = 18;
    public const int DecimalIntegerPart = 158;
    public const int SUBASS = 100;
    public const int SYNCHRONIZED = 59;
    public const int BOOLEAN = 33;
    public const int EXPR = 118;
    public const int IN = 19;
    public const int IMPLEMENTS = 46;
    public const int CONTINUE = 10;
    public const int OBJECT = 125;
    public const int COMMA = 71;
    public const int TRANSIENT = 61;
    public const int FORITER = 119;
    public const int DecimalFloatingPointLiteral = 160;
    public const int MODASS = 102;
    public const int SHRASS = 104;
    public const int PS = 143;
    public const int DOT = 69;
    public const int MultiLineComment = 146;
    public const int IdentifierPart = 153;
    public const int WITH = 31;
    public const int ADD = 80;
    public const int BYTE = 34;
    public const int XOR = 91;
    public const int ZeroToThree = 164;
    public const int VOLATILE = 62;
    public const int ITEM = 121;
    public const int UnicodeEscapeSequence = 167;
    public const int NSAME = 79;
    public const int DEFAULT = 11;
    public const int SHUASS = 105;
    public const int TAB = 133;
    public const int SHORT = 56;
    public const int INSTANCEOF = 20;
    public const int SQUOTE = 132;
    public const int TRUE = 5;
    public const int SAME = 78;
    public const int COLON = 97;
    public const int StringLiteral = 149;
    public const int NEQ = 77;
    public const int PAREXPR = 126;
    public const int ENUM = 40;
    public const int FINALLY = 15;
    public const int NBSP = 137;
    public const int HexIntegerLiteral = 162;
    public const int SP = 136;
    public const int BLOCK = 113;
    public const int NEG = 124;
    public const int LineTerminator = 144;
    public const int ASSIGN = 98;
    public const int INTERFACE = 49;
    public const int DIV = 109;
    public const int SEMIC = 70;
    public const int LONG = 50;
    public const int CR = 141;
    public const int PUBLIC = 55;
    public const int EXTENDS = 42;
    public const int BSLASH = 130;
    public const int LF = 140;

    private IToken last;

    private bool areRegularExpressionsEnabled()
    {
    	if (last == null)
    	{
    		return true;
    	}
    	switch (last.Type)
    	{
    	// identifier
    		case Identifier:
    	// literals
    		case NULL:
    		case TRUE:
    		case FALSE:
    		case THIS:
    		case OctalIntegerLiteral:
    		case DecimalIntegerLiteral:
    		case DecimalFloatingPointLiteral:
    		case HexIntegerLiteral:
    		case StringLiteral:
    	// member access ending 
    		case RBRACK:
    	// function call or nested expression ending
    		case RPAREN:
    			return false;
    	// otherwise OK
    		default:
    			return true;
    	}
    }
    	
    private void consumeIdentifierUnicodeStart()
    {
    	int ch = input.LA(1);
    	if (isIdentifierStartUnicode(ch))
    	{
    		MatchAny();
    		do
    		{
    			ch = input.LA(1);
    			if (ch == '$' || (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'Z') || ch == '\\' || ch == '_' || (ch >= 'a' && ch <= 'z') || isIdentifierPartUnicode(ch))
    			{
    				mIdentifierPart();
    			}
    			else
    			{
    				return;
    			}
    		}
    		while (true);
    	}
    	else
    	{
    		throw new NoViableAltException();
    	}
    }
    	
    private bool isIdentifierPartUnicode(int ch)
    {
    	return System.Text.RegularExpressions.Regex.IsMatch(new String((char)ch, 1), @"[0-9a-zA-Z_$]");
    }
    	
    private bool isIdentifierStartUnicode(int ch)
    {
    	return System.Text.RegularExpressions.Regex.IsMatch(new String((char)ch, 1), @"[a-zA-Z_$]");
    }

    public override IToken NextToken()
    {
    	IToken result = base.NextToken();
    	if (result.Channel == Token.DEFAULT_CHANNEL)
    	{
    		last = result;
    	}
    	return result;		
    }

    public override void ReportError(RecognitionException e)
    {
    	throw RecognitionExceptionHelper.Report(this, e);
    }


    // delegates
    // delegators

    public EcmaScriptV3Lexer() 
    {
		InitializeCyclicDFAs();
    }
    public EcmaScriptV3Lexer(ICharStream input)
		: this(input, null) {
    }
    public EcmaScriptV3Lexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "EcmaScriptV3.g";} 
    }

    // $ANTLR start "NULL"
    public void mNULL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NULL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:94:6: ( 'null' )
            // EcmaScriptV3.g:94:8: 'null'
            {
            	Match("null"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NULL"

    // $ANTLR start "TRUE"
    public void mTRUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:95:6: ( 'true' )
            // EcmaScriptV3.g:95:8: 'true'
            {
            	Match("true"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TRUE"

    // $ANTLR start "FALSE"
    public void mFALSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FALSE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:96:7: ( 'false' )
            // EcmaScriptV3.g:96:9: 'false'
            {
            	Match("false"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FALSE"

    // $ANTLR start "BREAK"
    public void mBREAK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BREAK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:97:7: ( 'break' )
            // EcmaScriptV3.g:97:9: 'break'
            {
            	Match("break"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BREAK"

    // $ANTLR start "CASE"
    public void mCASE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CASE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:98:6: ( 'case' )
            // EcmaScriptV3.g:98:8: 'case'
            {
            	Match("case"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CASE"

    // $ANTLR start "CATCH"
    public void mCATCH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CATCH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:99:7: ( 'catch' )
            // EcmaScriptV3.g:99:9: 'catch'
            {
            	Match("catch"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CATCH"

    // $ANTLR start "CONTINUE"
    public void mCONTINUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONTINUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:100:10: ( 'continue' )
            // EcmaScriptV3.g:100:12: 'continue'
            {
            	Match("continue"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONTINUE"

    // $ANTLR start "DEFAULT"
    public void mDEFAULT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEFAULT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:101:9: ( 'default' )
            // EcmaScriptV3.g:101:11: 'default'
            {
            	Match("default"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEFAULT"

    // $ANTLR start "DELETE"
    public void mDELETE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DELETE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:102:8: ( 'delete' )
            // EcmaScriptV3.g:102:10: 'delete'
            {
            	Match("delete"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DELETE"

    // $ANTLR start "DO"
    public void mDO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:103:4: ( 'do' )
            // EcmaScriptV3.g:103:6: 'do'
            {
            	Match("do"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DO"

    // $ANTLR start "ELSE"
    public void mELSE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ELSE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:104:6: ( 'else' )
            // EcmaScriptV3.g:104:8: 'else'
            {
            	Match("else"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ELSE"

    // $ANTLR start "FINALLY"
    public void mFINALLY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FINALLY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:105:9: ( 'finally' )
            // EcmaScriptV3.g:105:11: 'finally'
            {
            	Match("finally"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FINALLY"

    // $ANTLR start "FOR"
    public void mFOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:106:5: ( 'for' )
            // EcmaScriptV3.g:106:7: 'for'
            {
            	Match("for"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FOR"

    // $ANTLR start "FUNCTION"
    public void mFUNCTION() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FUNCTION;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:107:10: ( 'function' )
            // EcmaScriptV3.g:107:12: 'function'
            {
            	Match("function"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FUNCTION"

    // $ANTLR start "IF"
    public void mIF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:108:4: ( 'if' )
            // EcmaScriptV3.g:108:6: 'if'
            {
            	Match("if"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IF"

    // $ANTLR start "IN"
    public void mIN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:109:4: ( 'in' )
            // EcmaScriptV3.g:109:6: 'in'
            {
            	Match("in"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IN"

    // $ANTLR start "INSTANCEOF"
    public void mINSTANCEOF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INSTANCEOF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:110:12: ( 'instanceof' )
            // EcmaScriptV3.g:110:14: 'instanceof'
            {
            	Match("instanceof"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INSTANCEOF"

    // $ANTLR start "NEW"
    public void mNEW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NEW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:111:5: ( 'new' )
            // EcmaScriptV3.g:111:7: 'new'
            {
            	Match("new"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NEW"

    // $ANTLR start "RETURN"
    public void mRETURN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RETURN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:112:8: ( 'return' )
            // EcmaScriptV3.g:112:10: 'return'
            {
            	Match("return"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RETURN"

    // $ANTLR start "SWITCH"
    public void mSWITCH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SWITCH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:113:8: ( 'switch' )
            // EcmaScriptV3.g:113:10: 'switch'
            {
            	Match("switch"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SWITCH"

    // $ANTLR start "THIS"
    public void mTHIS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = THIS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:114:6: ( 'this' )
            // EcmaScriptV3.g:114:8: 'this'
            {
            	Match("this"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "THIS"

    // $ANTLR start "THROW"
    public void mTHROW() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = THROW;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:115:7: ( 'throw' )
            // EcmaScriptV3.g:115:9: 'throw'
            {
            	Match("throw"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "THROW"

    // $ANTLR start "TRY"
    public void mTRY() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRY;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:116:5: ( 'try' )
            // EcmaScriptV3.g:116:7: 'try'
            {
            	Match("try"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TRY"

    // $ANTLR start "TYPEOF"
    public void mTYPEOF() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TYPEOF;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:117:8: ( 'typeof' )
            // EcmaScriptV3.g:117:10: 'typeof'
            {
            	Match("typeof"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TYPEOF"

    // $ANTLR start "VAR"
    public void mVAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:118:5: ( 'var' )
            // EcmaScriptV3.g:118:7: 'var'
            {
            	Match("var"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VAR"

    // $ANTLR start "VOID"
    public void mVOID() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VOID;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:119:6: ( 'void' )
            // EcmaScriptV3.g:119:8: 'void'
            {
            	Match("void"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VOID"

    // $ANTLR start "WHILE"
    public void mWHILE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WHILE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:120:7: ( 'while' )
            // EcmaScriptV3.g:120:9: 'while'
            {
            	Match("while"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WHILE"

    // $ANTLR start "WITH"
    public void mWITH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WITH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:121:6: ( 'with' )
            // EcmaScriptV3.g:121:8: 'with'
            {
            	Match("with"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WITH"

    // $ANTLR start "ABSTRACT"
    public void mABSTRACT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ABSTRACT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:122:10: ( 'abstract' )
            // EcmaScriptV3.g:122:12: 'abstract'
            {
            	Match("abstract"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ABSTRACT"

    // $ANTLR start "BOOLEAN"
    public void mBOOLEAN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BOOLEAN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:123:9: ( 'boolean' )
            // EcmaScriptV3.g:123:11: 'boolean'
            {
            	Match("boolean"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BOOLEAN"

    // $ANTLR start "BYTE"
    public void mBYTE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BYTE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:124:6: ( 'byte' )
            // EcmaScriptV3.g:124:8: 'byte'
            {
            	Match("byte"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BYTE"

    // $ANTLR start "CHAR"
    public void mCHAR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHAR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:125:6: ( 'char' )
            // EcmaScriptV3.g:125:8: 'char'
            {
            	Match("char"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHAR"

    // $ANTLR start "CLASS"
    public void mCLASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CLASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:126:7: ( 'class' )
            // EcmaScriptV3.g:126:9: 'class'
            {
            	Match("class"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CLASS"

    // $ANTLR start "CONST"
    public void mCONST() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CONST;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:127:7: ( 'const' )
            // EcmaScriptV3.g:127:9: 'const'
            {
            	Match("const"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CONST"

    // $ANTLR start "DEBUGGER"
    public void mDEBUGGER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEBUGGER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:128:10: ( 'debugger' )
            // EcmaScriptV3.g:128:12: 'debugger'
            {
            	Match("debugger"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEBUGGER"

    // $ANTLR start "DOUBLE"
    public void mDOUBLE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOUBLE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:129:8: ( 'double' )
            // EcmaScriptV3.g:129:10: 'double'
            {
            	Match("double"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOUBLE"

    // $ANTLR start "ENUM"
    public void mENUM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ENUM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:130:6: ( 'enum' )
            // EcmaScriptV3.g:130:8: 'enum'
            {
            	Match("enum"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ENUM"

    // $ANTLR start "EXPORT"
    public void mEXPORT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXPORT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:131:8: ( 'export' )
            // EcmaScriptV3.g:131:10: 'export'
            {
            	Match("export"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXPORT"

    // $ANTLR start "EXTENDS"
    public void mEXTENDS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EXTENDS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:132:9: ( 'extends' )
            // EcmaScriptV3.g:132:11: 'extends'
            {
            	Match("extends"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EXTENDS"

    // $ANTLR start "FINAL"
    public void mFINAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FINAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:133:7: ( 'final' )
            // EcmaScriptV3.g:133:9: 'final'
            {
            	Match("final"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FINAL"

    // $ANTLR start "FLOAT"
    public void mFLOAT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = FLOAT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:134:7: ( 'float' )
            // EcmaScriptV3.g:134:9: 'float'
            {
            	Match("float"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "FLOAT"

    // $ANTLR start "GOTO"
    public void mGOTO() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GOTO;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:135:6: ( 'goto' )
            // EcmaScriptV3.g:135:8: 'goto'
            {
            	Match("goto"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GOTO"

    // $ANTLR start "IMPLEMENTS"
    public void mIMPLEMENTS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IMPLEMENTS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:136:12: ( 'implements' )
            // EcmaScriptV3.g:136:14: 'implements'
            {
            	Match("implements"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IMPLEMENTS"

    // $ANTLR start "IMPORT"
    public void mIMPORT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = IMPORT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:137:8: ( 'import' )
            // EcmaScriptV3.g:137:10: 'import'
            {
            	Match("import"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IMPORT"

    // $ANTLR start "INT"
    public void mINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:138:5: ( 'int' )
            // EcmaScriptV3.g:138:7: 'int'
            {
            	Match("int"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INT"

    // $ANTLR start "INTERFACE"
    public void mINTERFACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INTERFACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:139:11: ( 'interface' )
            // EcmaScriptV3.g:139:13: 'interface'
            {
            	Match("interface"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INTERFACE"

    // $ANTLR start "LONG"
    public void mLONG() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LONG;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:140:6: ( 'long' )
            // EcmaScriptV3.g:140:8: 'long'
            {
            	Match("long"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LONG"

    // $ANTLR start "NATIVE"
    public void mNATIVE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NATIVE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:141:8: ( 'native' )
            // EcmaScriptV3.g:141:10: 'native'
            {
            	Match("native"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NATIVE"

    // $ANTLR start "PACKAGE"
    public void mPACKAGE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PACKAGE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:142:9: ( 'package' )
            // EcmaScriptV3.g:142:11: 'package'
            {
            	Match("package"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PACKAGE"

    // $ANTLR start "PRIVATE"
    public void mPRIVATE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PRIVATE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:143:9: ( 'private' )
            // EcmaScriptV3.g:143:11: 'private'
            {
            	Match("private"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PRIVATE"

    // $ANTLR start "PROTECTED"
    public void mPROTECTED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PROTECTED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:144:11: ( 'protected' )
            // EcmaScriptV3.g:144:13: 'protected'
            {
            	Match("protected"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PROTECTED"

    // $ANTLR start "PUBLIC"
    public void mPUBLIC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = PUBLIC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:145:8: ( 'public' )
            // EcmaScriptV3.g:145:10: 'public'
            {
            	Match("public"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "PUBLIC"

    // $ANTLR start "SHORT"
    public void mSHORT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHORT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:146:7: ( 'short' )
            // EcmaScriptV3.g:146:9: 'short'
            {
            	Match("short"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHORT"

    // $ANTLR start "STATIC"
    public void mSTATIC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STATIC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:147:8: ( 'static' )
            // EcmaScriptV3.g:147:10: 'static'
            {
            	Match("static"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STATIC"

    // $ANTLR start "SUPER"
    public void mSUPER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SUPER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:148:7: ( 'super' )
            // EcmaScriptV3.g:148:9: 'super'
            {
            	Match("super"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SUPER"

    // $ANTLR start "SYNCHRONIZED"
    public void mSYNCHRONIZED() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SYNCHRONIZED;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:149:14: ( 'synchronized' )
            // EcmaScriptV3.g:149:16: 'synchronized'
            {
            	Match("synchronized"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SYNCHRONIZED"

    // $ANTLR start "THROWS"
    public void mTHROWS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = THROWS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:150:8: ( 'throws' )
            // EcmaScriptV3.g:150:10: 'throws'
            {
            	Match("throws"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "THROWS"

    // $ANTLR start "TRANSIENT"
    public void mTRANSIENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = TRANSIENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:151:11: ( 'transient' )
            // EcmaScriptV3.g:151:13: 'transient'
            {
            	Match("transient"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "TRANSIENT"

    // $ANTLR start "VOLATILE"
    public void mVOLATILE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = VOLATILE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:152:10: ( 'volatile' )
            // EcmaScriptV3.g:152:12: 'volatile'
            {
            	Match("volatile"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "VOLATILE"

    // $ANTLR start "LBRACE"
    public void mLBRACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LBRACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:153:8: ( '{' )
            // EcmaScriptV3.g:153:10: '{'
            {
            	Match('{'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LBRACE"

    // $ANTLR start "RBRACE"
    public void mRBRACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RBRACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:154:8: ( '}' )
            // EcmaScriptV3.g:154:10: '}'
            {
            	Match('}'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RBRACE"

    // $ANTLR start "LPAREN"
    public void mLPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:155:8: ( '(' )
            // EcmaScriptV3.g:155:10: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LPAREN"

    // $ANTLR start "RPAREN"
    public void mRPAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RPAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:156:8: ( ')' )
            // EcmaScriptV3.g:156:10: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RPAREN"

    // $ANTLR start "LBRACK"
    public void mLBRACK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LBRACK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:157:8: ( '[' )
            // EcmaScriptV3.g:157:10: '['
            {
            	Match('['); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LBRACK"

    // $ANTLR start "RBRACK"
    public void mRBRACK() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RBRACK;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:158:8: ( ']' )
            // EcmaScriptV3.g:158:10: ']'
            {
            	Match(']'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RBRACK"

    // $ANTLR start "DOT"
    public void mDOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:159:5: ( '.' )
            // EcmaScriptV3.g:159:7: '.'
            {
            	Match('.'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DOT"

    // $ANTLR start "SEMIC"
    public void mSEMIC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SEMIC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:160:7: ( ';' )
            // EcmaScriptV3.g:160:9: ';'
            {
            	Match(';'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SEMIC"

    // $ANTLR start "COMMA"
    public void mCOMMA() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMA;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:161:7: ( ',' )
            // EcmaScriptV3.g:161:9: ','
            {
            	Match(','); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMA"

    // $ANTLR start "LT"
    public void mLT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:162:4: ( '<' )
            // EcmaScriptV3.g:162:6: '<'
            {
            	Match('<'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LT"

    // $ANTLR start "GT"
    public void mGT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:163:4: ( '>' )
            // EcmaScriptV3.g:163:6: '>'
            {
            	Match('>'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GT"

    // $ANTLR start "LTE"
    public void mLTE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LTE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:164:5: ( '<=' )
            // EcmaScriptV3.g:164:7: '<='
            {
            	Match("<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LTE"

    // $ANTLR start "GTE"
    public void mGTE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = GTE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:165:5: ( '>=' )
            // EcmaScriptV3.g:165:7: '>='
            {
            	Match(">="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "GTE"

    // $ANTLR start "EQ"
    public void mEQ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EQ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:166:4: ( '==' )
            // EcmaScriptV3.g:166:6: '=='
            {
            	Match("=="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EQ"

    // $ANTLR start "NEQ"
    public void mNEQ() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NEQ;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:167:5: ( '!=' )
            // EcmaScriptV3.g:167:7: '!='
            {
            	Match("!="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NEQ"

    // $ANTLR start "SAME"
    public void mSAME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SAME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:168:6: ( '===' )
            // EcmaScriptV3.g:168:8: '==='
            {
            	Match("==="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SAME"

    // $ANTLR start "NSAME"
    public void mNSAME() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NSAME;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:169:7: ( '!==' )
            // EcmaScriptV3.g:169:9: '!=='
            {
            	Match("!=="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NSAME"

    // $ANTLR start "ADD"
    public void mADD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ADD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:170:5: ( '+' )
            // EcmaScriptV3.g:170:7: '+'
            {
            	Match('+'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ADD"

    // $ANTLR start "SUB"
    public void mSUB() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SUB;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:171:5: ( '-' )
            // EcmaScriptV3.g:171:7: '-'
            {
            	Match('-'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SUB"

    // $ANTLR start "MUL"
    public void mMUL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MUL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:172:5: ( '*' )
            // EcmaScriptV3.g:172:7: '*'
            {
            	Match('*'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MUL"

    // $ANTLR start "MOD"
    public void mMOD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MOD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:173:5: ( '%' )
            // EcmaScriptV3.g:173:7: '%'
            {
            	Match('%'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MOD"

    // $ANTLR start "INC"
    public void mINC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:174:5: ( '++' )
            // EcmaScriptV3.g:174:7: '++'
            {
            	Match("++"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INC"

    // $ANTLR start "DEC"
    public void mDEC() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DEC;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:175:5: ( '--' )
            // EcmaScriptV3.g:175:7: '--'
            {
            	Match("--"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DEC"

    // $ANTLR start "SHL"
    public void mSHL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:176:5: ( '<<' )
            // EcmaScriptV3.g:176:7: '<<'
            {
            	Match("<<"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHL"

    // $ANTLR start "SHR"
    public void mSHR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:177:5: ( '>>' )
            // EcmaScriptV3.g:177:7: '>>'
            {
            	Match(">>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHR"

    // $ANTLR start "SHU"
    public void mSHU() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHU;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:178:5: ( '>>>' )
            // EcmaScriptV3.g:178:7: '>>>'
            {
            	Match(">>>"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHU"

    // $ANTLR start "AND"
    public void mAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:179:5: ( '&' )
            // EcmaScriptV3.g:179:7: '&'
            {
            	Match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AND"

    // $ANTLR start "OR"
    public void mOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:180:4: ( '|' )
            // EcmaScriptV3.g:180:6: '|'
            {
            	Match('|'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OR"

    // $ANTLR start "XOR"
    public void mXOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = XOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:181:5: ( '^' )
            // EcmaScriptV3.g:181:7: '^'
            {
            	Match('^'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "XOR"

    // $ANTLR start "NOT"
    public void mNOT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NOT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:182:5: ( '!' )
            // EcmaScriptV3.g:182:7: '!'
            {
            	Match('!'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NOT"

    // $ANTLR start "INV"
    public void mINV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = INV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:183:5: ( '~' )
            // EcmaScriptV3.g:183:7: '~'
            {
            	Match('~'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "INV"

    // $ANTLR start "LAND"
    public void mLAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LAND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:184:6: ( '&&' )
            // EcmaScriptV3.g:184:8: '&&'
            {
            	Match("&&"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LAND"

    // $ANTLR start "LOR"
    public void mLOR() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LOR;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:185:5: ( '||' )
            // EcmaScriptV3.g:185:7: '||'
            {
            	Match("||"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LOR"

    // $ANTLR start "QUE"
    public void mQUE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = QUE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:186:5: ( '?' )
            // EcmaScriptV3.g:186:7: '?'
            {
            	Match('?'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "QUE"

    // $ANTLR start "COLON"
    public void mCOLON() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COLON;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:187:7: ( ':' )
            // EcmaScriptV3.g:187:9: ':'
            {
            	Match(':'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COLON"

    // $ANTLR start "ASSIGN"
    public void mASSIGN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ASSIGN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:188:8: ( '=' )
            // EcmaScriptV3.g:188:10: '='
            {
            	Match('='); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ASSIGN"

    // $ANTLR start "ADDASS"
    public void mADDASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ADDASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:189:8: ( '+=' )
            // EcmaScriptV3.g:189:10: '+='
            {
            	Match("+="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ADDASS"

    // $ANTLR start "SUBASS"
    public void mSUBASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SUBASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:190:8: ( '-=' )
            // EcmaScriptV3.g:190:10: '-='
            {
            	Match("-="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SUBASS"

    // $ANTLR start "MULASS"
    public void mMULASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MULASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:191:8: ( '*=' )
            // EcmaScriptV3.g:191:10: '*='
            {
            	Match("*="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MULASS"

    // $ANTLR start "MODASS"
    public void mMODASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MODASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:192:8: ( '%=' )
            // EcmaScriptV3.g:192:10: '%='
            {
            	Match("%="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MODASS"

    // $ANTLR start "SHLASS"
    public void mSHLASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHLASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:193:8: ( '<<=' )
            // EcmaScriptV3.g:193:10: '<<='
            {
            	Match("<<="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHLASS"

    // $ANTLR start "SHRASS"
    public void mSHRASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHRASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:194:8: ( '>>=' )
            // EcmaScriptV3.g:194:10: '>>='
            {
            	Match(">>="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHRASS"

    // $ANTLR start "SHUASS"
    public void mSHUASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SHUASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:195:8: ( '>>>=' )
            // EcmaScriptV3.g:195:10: '>>>='
            {
            	Match(">>>="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SHUASS"

    // $ANTLR start "ANDASS"
    public void mANDASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ANDASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:196:8: ( '&=' )
            // EcmaScriptV3.g:196:10: '&='
            {
            	Match("&="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ANDASS"

    // $ANTLR start "ORASS"
    public void mORASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = ORASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:197:7: ( '|=' )
            // EcmaScriptV3.g:197:9: '|='
            {
            	Match("|="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "ORASS"

    // $ANTLR start "XORASS"
    public void mXORASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = XORASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:198:8: ( '^=' )
            // EcmaScriptV3.g:198:10: '^='
            {
            	Match("^="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "XORASS"

    // $ANTLR start "DIV"
    public void mDIV() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DIV;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:199:5: ( '/' )
            // EcmaScriptV3.g:199:7: '/'
            {
            	Match('/'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIV"

    // $ANTLR start "DIVASS"
    public void mDIVASS() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DIVASS;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:200:8: ( '/=' )
            // EcmaScriptV3.g:200:10: '/='
            {
            	Match("/="); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DIVASS"

    // $ANTLR start "BSLASH"
    public void mBSLASH() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:422:2: ( '\\\\' )
            // EcmaScriptV3.g:422:4: '\\\\'
            {
            	Match('\\'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "BSLASH"

    // $ANTLR start "DQUOTE"
    public void mDQUOTE() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:426:2: ( '\"' )
            // EcmaScriptV3.g:426:4: '\"'
            {
            	Match('\"'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "DQUOTE"

    // $ANTLR start "SQUOTE"
    public void mSQUOTE() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:430:2: ( '\\'' )
            // EcmaScriptV3.g:430:4: '\\''
            {
            	Match('\''); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "SQUOTE"

    // $ANTLR start "TAB"
    public void mTAB() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:436:2: ( '\\u0009' )
            // EcmaScriptV3.g:436:4: '\\u0009'
            {
            	Match('\t'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "TAB"

    // $ANTLR start "VT"
    public void mVT() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:440:2: ( '\\u000b' )
            // EcmaScriptV3.g:440:4: '\\u000b'
            {
            	Match('\u000B'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "VT"

    // $ANTLR start "FF"
    public void mFF() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:444:2: ( '\\u000c' )
            // EcmaScriptV3.g:444:4: '\\u000c'
            {
            	Match('\f'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "FF"

    // $ANTLR start "SP"
    public void mSP() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:448:2: ( '\\u0020' )
            // EcmaScriptV3.g:448:4: '\\u0020'
            {
            	Match(' '); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "SP"

    // $ANTLR start "NBSP"
    public void mNBSP() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:452:2: ( '\\u00a0' )
            // EcmaScriptV3.g:452:4: '\\u00a0'
            {
            	Match('\u00A0'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "NBSP"

    // $ANTLR start "USP"
    public void mUSP() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:456:2: ( '\\u1680' | '\\u180E' | '\\u2000' | '\\u2001' | '\\u2002' | '\\u2003' | '\\u2004' | '\\u2005' | '\\u2006' | '\\u2007' | '\\u2008' | '\\u2009' | '\\u200A' | '\\u202F' | '\\u205F' | '\\u3000' )
            // EcmaScriptV3.g:
            {
            	if ( input.LA(1) == '\u1680' || input.LA(1) == '\u180E' || (input.LA(1) >= '\u2000' && input.LA(1) <= '\u200A') || input.LA(1) == '\u202F' || input.LA(1) == '\u205F' || input.LA(1) == '\u3000' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "USP"

    // $ANTLR start "WhiteSpace"
    public void mWhiteSpace() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = WhiteSpace;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:475:2: ( ( TAB | VT | FF | SP | NBSP | USP )+ )
            // EcmaScriptV3.g:475:4: ( TAB | VT | FF | SP | NBSP | USP )+
            {
            	// EcmaScriptV3.g:475:4: ( TAB | VT | FF | SP | NBSP | USP )+
            	int cnt1 = 0;
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == '\t' || (LA1_0 >= '\u000B' && LA1_0 <= '\f') || LA1_0 == ' ' || LA1_0 == '\u00A0' || LA1_0 == '\u1680' || LA1_0 == '\u180E' || (LA1_0 >= '\u2000' && LA1_0 <= '\u200A') || LA1_0 == '\u202F' || LA1_0 == '\u205F' || LA1_0 == '\u3000') )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:
            			    {
            			    	if ( input.LA(1) == '\t' || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || input.LA(1) == ' ' || input.LA(1) == '\u00A0' || input.LA(1) == '\u1680' || input.LA(1) == '\u180E' || (input.LA(1) >= '\u2000' && input.LA(1) <= '\u200A') || input.LA(1) == '\u202F' || input.LA(1) == '\u205F' || input.LA(1) == '\u3000' ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt1 >= 1 ) goto loop1;
            		            EarlyExitException eee =
            		                new EarlyExitException(1, input);
            		            throw eee;
            	    }
            	    cnt1++;
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whinging that label 'loop1' has no statements

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "WhiteSpace"

    // $ANTLR start "LF"
    public void mLF() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:483:2: ( '\\n' )
            // EcmaScriptV3.g:483:4: '\\n'
            {
            	Match('\n'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LF"

    // $ANTLR start "CR"
    public void mCR() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:487:2: ( '\\r' )
            // EcmaScriptV3.g:487:4: '\\r'
            {
            	Match('\r'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "CR"

    // $ANTLR start "LS"
    public void mLS() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:491:2: ( '\\u2028' )
            // EcmaScriptV3.g:491:4: '\\u2028'
            {
            	Match('\u2028'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LS"

    // $ANTLR start "PS"
    public void mPS() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:495:2: ( '\\u2029' )
            // EcmaScriptV3.g:495:4: '\\u2029'
            {
            	Match('\u2029'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "PS"

    // $ANTLR start "LineTerminator"
    public void mLineTerminator() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:499:2: ( CR | LF | LS | PS )
            // EcmaScriptV3.g:
            {
            	if ( input.LA(1) == '\n' || input.LA(1) == '\r' || (input.LA(1) >= '\u2028' && input.LA(1) <= '\u2029') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "LineTerminator"

    // $ANTLR start "EOL"
    public void mEOL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = EOL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:503:2: ( ( ( CR ( LF )? ) | LF | LS | PS ) )
            // EcmaScriptV3.g:503:4: ( ( CR ( LF )? ) | LF | LS | PS )
            {
            	// EcmaScriptV3.g:503:4: ( ( CR ( LF )? ) | LF | LS | PS )
            	int alt3 = 4;
            	switch ( input.LA(1) ) 
            	{
            	case '\r':
            		{
            	    alt3 = 1;
            	    }
            	    break;
            	case '\n':
            		{
            	    alt3 = 2;
            	    }
            	    break;
            	case '\u2028':
            		{
            	    alt3 = 3;
            	    }
            	    break;
            	case '\u2029':
            		{
            	    alt3 = 4;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d3s0 =
            		        new NoViableAltException("", 3, 0, input);

            		    throw nvae_d3s0;
            	}

            	switch (alt3) 
            	{
            	    case 1 :
            	        // EcmaScriptV3.g:503:6: ( CR ( LF )? )
            	        {
            	        	// EcmaScriptV3.g:503:6: ( CR ( LF )? )
            	        	// EcmaScriptV3.g:503:8: CR ( LF )?
            	        	{
            	        		mCR(); 
            	        		// EcmaScriptV3.g:503:11: ( LF )?
            	        		int alt2 = 2;
            	        		int LA2_0 = input.LA(1);

            	        		if ( (LA2_0 == '\n') )
            	        		{
            	        		    alt2 = 1;
            	        		}
            	        		switch (alt2) 
            	        		{
            	        		    case 1 :
            	        		        // EcmaScriptV3.g:503:11: LF
            	        		        {
            	        		        	mLF(); 

            	        		        }
            	        		        break;

            	        		}


            	        	}


            	        }
            	        break;
            	    case 2 :
            	        // EcmaScriptV3.g:503:19: LF
            	        {
            	        	mLF(); 

            	        }
            	        break;
            	    case 3 :
            	        // EcmaScriptV3.g:503:24: LS
            	        {
            	        	mLS(); 

            	        }
            	        break;
            	    case 4 :
            	        // EcmaScriptV3.g:503:29: PS
            	        {
            	        	mPS(); 

            	        }
            	        break;

            	}

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EOL"

    // $ANTLR start "MultiLineComment"
    public void mMultiLineComment() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = MultiLineComment;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:510:2: ( '/*' ( options {greedy=false; } : . )* '*/' )
            // EcmaScriptV3.g:510:4: '/*' ( options {greedy=false; } : . )* '*/'
            {
            	Match("/*"); 

            	// EcmaScriptV3.g:510:9: ( options {greedy=false; } : . )*
            	do 
            	{
            	    int alt4 = 2;
            	    int LA4_0 = input.LA(1);

            	    if ( (LA4_0 == '*') )
            	    {
            	        int LA4_1 = input.LA(2);

            	        if ( (LA4_1 == '/') )
            	        {
            	            alt4 = 2;
            	        }
            	        else if ( ((LA4_1 >= '\u0000' && LA4_1 <= '.') || (LA4_1 >= '0' && LA4_1 <= '\uFFFF')) )
            	        {
            	            alt4 = 1;
            	        }


            	    }
            	    else if ( ((LA4_0 >= '\u0000' && LA4_0 <= ')') || (LA4_0 >= '+' && LA4_0 <= '\uFFFF')) )
            	    {
            	        alt4 = 1;
            	    }


            	    switch (alt4) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:510:41: .
            			    {
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop4;
            	    }
            	} while (true);

            	loop4:
            		;	// Stops C# compiler whining that label 'loop4' has no statements

            	Match("*/"); 

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "MultiLineComment"

    // $ANTLR start "SingleLineComment"
    public void mSingleLineComment() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SingleLineComment;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:514:2: ( '//' (~ ( LineTerminator ) )* )
            // EcmaScriptV3.g:514:4: '//' (~ ( LineTerminator ) )*
            {
            	Match("//"); 

            	// EcmaScriptV3.g:514:9: (~ ( LineTerminator ) )*
            	do 
            	{
            	    int alt5 = 2;
            	    int LA5_0 = input.LA(1);

            	    if ( ((LA5_0 >= '\u0000' && LA5_0 <= '\t') || (LA5_0 >= '\u000B' && LA5_0 <= '\f') || (LA5_0 >= '\u000E' && LA5_0 <= '\u2027') || (LA5_0 >= '\u202A' && LA5_0 <= '\uFFFF')) )
            	    {
            	        alt5 = 1;
            	    }


            	    switch (alt5) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:514:11: ~ ( LineTerminator )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop5;
            	    }
            	} while (true);

            	loop5:
            		;	// Stops C# compiler whining that label 'loop5' has no statements

            	 _channel = HIDDEN; 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SingleLineComment"

    // $ANTLR start "IdentifierStartASCII"
    public void mIdentifierStartASCII() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:615:2: ( 'a' .. 'z' | 'A' .. 'Z' | '$' | '_' | BSLASH 'u' HexDigit HexDigit HexDigit HexDigit )
            int alt6 = 5;
            switch ( input.LA(1) ) 
            {
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            case 'm':
            case 'n':
            case 'o':
            case 'p':
            case 'q':
            case 'r':
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            case 'y':
            case 'z':
            	{
                alt6 = 1;
                }
                break;
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
            case 'G':
            case 'H':
            case 'I':
            case 'J':
            case 'K':
            case 'L':
            case 'M':
            case 'N':
            case 'O':
            case 'P':
            case 'Q':
            case 'R':
            case 'S':
            case 'T':
            case 'U':
            case 'V':
            case 'W':
            case 'X':
            case 'Y':
            case 'Z':
            	{
                alt6 = 2;
                }
                break;
            case '$':
            	{
                alt6 = 3;
                }
                break;
            case '_':
            	{
                alt6 = 4;
                }
                break;
            case '\\':
            	{
                alt6 = 5;
                }
                break;
            	default:
            	    NoViableAltException nvae_d6s0 =
            	        new NoViableAltException("", 6, 0, input);

            	    throw nvae_d6s0;
            }

            switch (alt6) 
            {
                case 1 :
                    // EcmaScriptV3.g:615:4: 'a' .. 'z'
                    {
                    	MatchRange('a','z'); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:615:15: 'A' .. 'Z'
                    {
                    	MatchRange('A','Z'); 

                    }
                    break;
                case 3 :
                    // EcmaScriptV3.g:616:4: '$'
                    {
                    	Match('$'); 

                    }
                    break;
                case 4 :
                    // EcmaScriptV3.g:617:4: '_'
                    {
                    	Match('_'); 

                    }
                    break;
                case 5 :
                    // EcmaScriptV3.g:618:4: BSLASH 'u' HexDigit HexDigit HexDigit HexDigit
                    {
                    	mBSLASH(); 
                    	Match('u'); 
                    	mHexDigit(); 
                    	mHexDigit(); 
                    	mHexDigit(); 
                    	mHexDigit(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IdentifierStartASCII"

    // $ANTLR start "IdentifierPart"
    public void mIdentifierPart() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:626:2: ( DecimalDigit | IdentifierStartASCII | {...}?)
            int alt7 = 3;
            switch ( input.LA(1) ) 
            {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            	{
                alt7 = 1;
                }
                break;
            case '$':
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
            case 'G':
            case 'H':
            case 'I':
            case 'J':
            case 'K':
            case 'L':
            case 'M':
            case 'N':
            case 'O':
            case 'P':
            case 'Q':
            case 'R':
            case 'S':
            case 'T':
            case 'U':
            case 'V':
            case 'W':
            case 'X':
            case 'Y':
            case 'Z':
            case '\\':
            case '_':
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            case 'm':
            case 'n':
            case 'o':
            case 'p':
            case 'q':
            case 'r':
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            case 'y':
            case 'z':
            	{
                alt7 = 2;
                }
                break;
            	default:
                	alt7 = 3;
                	break;}

            switch (alt7) 
            {
                case 1 :
                    // EcmaScriptV3.g:626:4: DecimalDigit
                    {
                    	mDecimalDigit(); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:627:4: IdentifierStartASCII
                    {
                    	mIdentifierStartASCII(); 

                    }
                    break;
                case 3 :
                    // EcmaScriptV3.g:628:4: {...}?
                    {
                    	if ( !(( isIdentifierPartUnicode(input.LA(1)) )) ) 
                    	{
                    	    throw new FailedPredicateException(input, "IdentifierPart", " isIdentifierPartUnicode(input.LA(1)) ");
                    	}
                    	 MatchAny(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "IdentifierPart"

    // $ANTLR start "IdentifierNameASCIIStart"
    public void mIdentifierNameASCIIStart() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:632:2: ( IdentifierStartASCII ( IdentifierPart )* )
            // EcmaScriptV3.g:632:4: IdentifierStartASCII ( IdentifierPart )*
            {
            	mIdentifierStartASCII(); 
            	// EcmaScriptV3.g:632:25: ( IdentifierPart )*
            	do 
            	{
            	    int alt8 = 2;
            	    int LA8_0 = input.LA(1);

            	    if ( (LA8_0 == '$' || (LA8_0 >= '0' && LA8_0 <= '9') || (LA8_0 >= 'A' && LA8_0 <= 'Z') || LA8_0 == '\\' || LA8_0 == '_' || (LA8_0 >= 'a' && LA8_0 <= 'z')) )
            	    {
            	        alt8 = 1;
            	    }
            	    else if ( (( isIdentifierPartUnicode(input.LA(1)) )) )
            	    {
            	        alt8 = 1;
            	    }


            	    switch (alt8) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:632:25: IdentifierPart
            			    {
            			    	mIdentifierPart(); 

            			    }
            			    break;

            			default:
            			    goto loop8;
            	    }
            	} while (true);

            	loop8:
            		;	// Stops C# compiler whining that label 'loop8' has no statements


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "IdentifierNameASCIIStart"

    // $ANTLR start "Identifier"
    public void mIdentifier() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = Identifier;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:644:2: ( IdentifierNameASCIIStart | )
            int alt9 = 2;
            int LA9_0 = input.LA(1);

            if ( (LA9_0 == '$' || (LA9_0 >= 'A' && LA9_0 <= 'Z') || LA9_0 == '\\' || LA9_0 == '_' || (LA9_0 >= 'a' && LA9_0 <= 'z')) )
            {
                alt9 = 1;
            }
            else 
            {
                alt9 = 2;}
            switch (alt9) 
            {
                case 1 :
                    // EcmaScriptV3.g:644:4: IdentifierNameASCIIStart
                    {
                    	mIdentifierNameASCIIStart(); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:645:4: 
                    {
                    	 consumeIdentifierUnicodeStart(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "Identifier"

    // $ANTLR start "DecimalDigit"
    public void mDecimalDigit() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:728:2: ( '0' .. '9' )
            // EcmaScriptV3.g:728:4: '0' .. '9'
            {
            	MatchRange('0','9'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "DecimalDigit"

    // $ANTLR start "HexDigit"
    public void mHexDigit() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:732:2: ( DecimalDigit | 'a' .. 'f' | 'A' .. 'F' )
            // EcmaScriptV3.g:
            {
            	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "HexDigit"

    // $ANTLR start "OctalDigit"
    public void mOctalDigit() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:736:2: ( '0' .. '7' )
            // EcmaScriptV3.g:736:4: '0' .. '7'
            {
            	MatchRange('0','7'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "OctalDigit"

    // $ANTLR start "ExponentPart"
    public void mExponentPart() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:740:2: ( ( 'e' | 'E' ) ( '+' | '-' )? ( DecimalDigit )+ )
            // EcmaScriptV3.g:740:4: ( 'e' | 'E' ) ( '+' | '-' )? ( DecimalDigit )+
            {
            	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}

            	// EcmaScriptV3.g:740:18: ( '+' | '-' )?
            	int alt10 = 2;
            	int LA10_0 = input.LA(1);

            	if ( (LA10_0 == '+' || LA10_0 == '-') )
            	{
            	    alt10 = 1;
            	}
            	switch (alt10) 
            	{
            	    case 1 :
            	        // EcmaScriptV3.g:
            	        {
            	        	if ( input.LA(1) == '+' || input.LA(1) == '-' ) 
            	        	{
            	        	    input.Consume();

            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    Recover(mse);
            	        	    throw mse;}


            	        }
            	        break;

            	}

            	// EcmaScriptV3.g:740:33: ( DecimalDigit )+
            	int cnt11 = 0;
            	do 
            	{
            	    int alt11 = 2;
            	    int LA11_0 = input.LA(1);

            	    if ( ((LA11_0 >= '0' && LA11_0 <= '9')) )
            	    {
            	        alt11 = 1;
            	    }


            	    switch (alt11) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:740:33: DecimalDigit
            			    {
            			    	mDecimalDigit(); 

            			    }
            			    break;

            			default:
            			    if ( cnt11 >= 1 ) goto loop11;
            		            EarlyExitException eee =
            		                new EarlyExitException(11, input);
            		            throw eee;
            	    }
            	    cnt11++;
            	} while (true);

            	loop11:
            		;	// Stops C# compiler whinging that label 'loop11' has no statements


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "ExponentPart"

    // $ANTLR start "DecimalIntegerPart"
    public void mDecimalIntegerPart() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:744:2: ( '0' | '1' .. '9' ( DecimalDigit )* )
            int alt13 = 2;
            int LA13_0 = input.LA(1);

            if ( (LA13_0 == '0') )
            {
                alt13 = 1;
            }
            else if ( ((LA13_0 >= '1' && LA13_0 <= '9')) )
            {
                alt13 = 2;
            }
            else 
            {
                NoViableAltException nvae_d13s0 =
                    new NoViableAltException("", 13, 0, input);

                throw nvae_d13s0;
            }
            switch (alt13) 
            {
                case 1 :
                    // EcmaScriptV3.g:744:4: '0'
                    {
                    	Match('0'); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:745:4: '1' .. '9' ( DecimalDigit )*
                    {
                    	MatchRange('1','9'); 
                    	// EcmaScriptV3.g:745:13: ( DecimalDigit )*
                    	do 
                    	{
                    	    int alt12 = 2;
                    	    int LA12_0 = input.LA(1);

                    	    if ( ((LA12_0 >= '0' && LA12_0 <= '9')) )
                    	    {
                    	        alt12 = 1;
                    	    }


                    	    switch (alt12) 
                    		{
                    			case 1 :
                    			    // EcmaScriptV3.g:745:13: DecimalDigit
                    			    {
                    			    	mDecimalDigit(); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop12;
                    	    }
                    	} while (true);

                    	loop12:
                    		;	// Stops C# compiler whining that label 'loop12' has no statements


                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DecimalIntegerPart"

    // $ANTLR start "DecimalIntegerLiteral"
    public void mDecimalIntegerLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DecimalIntegerLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:749:2: ( DecimalIntegerPart )
            // EcmaScriptV3.g:749:4: DecimalIntegerPart
            {
            	mDecimalIntegerPart(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DecimalIntegerLiteral"

    // $ANTLR start "DecimalFloatingPointLiteral"
    public void mDecimalFloatingPointLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = DecimalFloatingPointLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:753:2: ( DecimalIntegerPart '.' ( DecimalDigit )* ( ExponentPart )? | '.' ( DecimalDigit )+ ( ExponentPart )? | DecimalIntegerPart ExponentPart )
            int alt18 = 3;
            alt18 = dfa18.Predict(input);
            switch (alt18) 
            {
                case 1 :
                    // EcmaScriptV3.g:753:4: DecimalIntegerPart '.' ( DecimalDigit )* ( ExponentPart )?
                    {
                    	mDecimalIntegerPart(); 
                    	Match('.'); 
                    	// EcmaScriptV3.g:753:27: ( DecimalDigit )*
                    	do 
                    	{
                    	    int alt14 = 2;
                    	    int LA14_0 = input.LA(1);

                    	    if ( ((LA14_0 >= '0' && LA14_0 <= '9')) )
                    	    {
                    	        alt14 = 1;
                    	    }


                    	    switch (alt14) 
                    		{
                    			case 1 :
                    			    // EcmaScriptV3.g:753:27: DecimalDigit
                    			    {
                    			    	mDecimalDigit(); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop14;
                    	    }
                    	} while (true);

                    	loop14:
                    		;	// Stops C# compiler whining that label 'loop14' has no statements

                    	// EcmaScriptV3.g:753:41: ( ExponentPart )?
                    	int alt15 = 2;
                    	int LA15_0 = input.LA(1);

                    	if ( (LA15_0 == 'E' || LA15_0 == 'e') )
                    	{
                    	    alt15 = 1;
                    	}
                    	switch (alt15) 
                    	{
                    	    case 1 :
                    	        // EcmaScriptV3.g:753:41: ExponentPart
                    	        {
                    	        	mExponentPart(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:754:4: '.' ( DecimalDigit )+ ( ExponentPart )?
                    {
                    	Match('.'); 
                    	// EcmaScriptV3.g:754:8: ( DecimalDigit )+
                    	int cnt16 = 0;
                    	do 
                    	{
                    	    int alt16 = 2;
                    	    int LA16_0 = input.LA(1);

                    	    if ( ((LA16_0 >= '0' && LA16_0 <= '9')) )
                    	    {
                    	        alt16 = 1;
                    	    }


                    	    switch (alt16) 
                    		{
                    			case 1 :
                    			    // EcmaScriptV3.g:754:8: DecimalDigit
                    			    {
                    			    	mDecimalDigit(); 

                    			    }
                    			    break;

                    			default:
                    			    if ( cnt16 >= 1 ) goto loop16;
                    		            EarlyExitException eee =
                    		                new EarlyExitException(16, input);
                    		            throw eee;
                    	    }
                    	    cnt16++;
                    	} while (true);

                    	loop16:
                    		;	// Stops C# compiler whinging that label 'loop16' has no statements

                    	// EcmaScriptV3.g:754:22: ( ExponentPart )?
                    	int alt17 = 2;
                    	int LA17_0 = input.LA(1);

                    	if ( (LA17_0 == 'E' || LA17_0 == 'e') )
                    	{
                    	    alt17 = 1;
                    	}
                    	switch (alt17) 
                    	{
                    	    case 1 :
                    	        // EcmaScriptV3.g:754:22: ExponentPart
                    	        {
                    	        	mExponentPart(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 3 :
                    // EcmaScriptV3.g:755:4: DecimalIntegerPart ExponentPart
                    {
                    	mDecimalIntegerPart(); 
                    	mExponentPart(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "DecimalFloatingPointLiteral"

    // $ANTLR start "OctalIntegerLiteral"
    public void mOctalIntegerLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OctalIntegerLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:759:2: ( '0' ( OctalDigit )+ )
            // EcmaScriptV3.g:759:4: '0' ( OctalDigit )+
            {
            	Match('0'); 
            	// EcmaScriptV3.g:759:8: ( OctalDigit )+
            	int cnt19 = 0;
            	do 
            	{
            	    int alt19 = 2;
            	    int LA19_0 = input.LA(1);

            	    if ( ((LA19_0 >= '0' && LA19_0 <= '7')) )
            	    {
            	        alt19 = 1;
            	    }


            	    switch (alt19) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:759:8: OctalDigit
            			    {
            			    	mOctalDigit(); 

            			    }
            			    break;

            			default:
            			    if ( cnt19 >= 1 ) goto loop19;
            		            EarlyExitException eee =
            		                new EarlyExitException(19, input);
            		            throw eee;
            	    }
            	    cnt19++;
            	} while (true);

            	loop19:
            		;	// Stops C# compiler whinging that label 'loop19' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OctalIntegerLiteral"

    // $ANTLR start "HexIntegerLiteral"
    public void mHexIntegerLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HexIntegerLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:763:2: ( ( '0x' | '0X' ) ( HexDigit )+ )
            // EcmaScriptV3.g:763:4: ( '0x' | '0X' ) ( HexDigit )+
            {
            	// EcmaScriptV3.g:763:4: ( '0x' | '0X' )
            	int alt20 = 2;
            	int LA20_0 = input.LA(1);

            	if ( (LA20_0 == '0') )
            	{
            	    int LA20_1 = input.LA(2);

            	    if ( (LA20_1 == 'x') )
            	    {
            	        alt20 = 1;
            	    }
            	    else if ( (LA20_1 == 'X') )
            	    {
            	        alt20 = 2;
            	    }
            	    else 
            	    {
            	        NoViableAltException nvae_d20s1 =
            	            new NoViableAltException("", 20, 1, input);

            	        throw nvae_d20s1;
            	    }
            	}
            	else 
            	{
            	    NoViableAltException nvae_d20s0 =
            	        new NoViableAltException("", 20, 0, input);

            	    throw nvae_d20s0;
            	}
            	switch (alt20) 
            	{
            	    case 1 :
            	        // EcmaScriptV3.g:763:6: '0x'
            	        {
            	        	Match("0x"); 


            	        }
            	        break;
            	    case 2 :
            	        // EcmaScriptV3.g:763:13: '0X'
            	        {
            	        	Match("0X"); 


            	        }
            	        break;

            	}

            	// EcmaScriptV3.g:763:20: ( HexDigit )+
            	int cnt21 = 0;
            	do 
            	{
            	    int alt21 = 2;
            	    int LA21_0 = input.LA(1);

            	    if ( ((LA21_0 >= '0' && LA21_0 <= '9') || (LA21_0 >= 'A' && LA21_0 <= 'F') || (LA21_0 >= 'a' && LA21_0 <= 'f')) )
            	    {
            	        alt21 = 1;
            	    }


            	    switch (alt21) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:763:20: HexDigit
            			    {
            			    	mHexDigit(); 

            			    }
            			    break;

            			default:
            			    if ( cnt21 >= 1 ) goto loop21;
            		            EarlyExitException eee =
            		                new EarlyExitException(21, input);
            		            throw eee;
            	    }
            	    cnt21++;
            	} while (true);

            	loop21:
            		;	// Stops C# compiler whinging that label 'loop21' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HexIntegerLiteral"

    // $ANTLR start "CharacterEscapeSequence"
    public void mCharacterEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:783:2: (~ ( DecimalDigit | 'x' | 'u' | LineTerminator ) )
            // EcmaScriptV3.g:783:4: ~ ( DecimalDigit | 'x' | 'u' | LineTerminator )
            {
            	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '/') || (input.LA(1) >= ':' && input.LA(1) <= 't') || (input.LA(1) >= 'v' && input.LA(1) <= 'w') || (input.LA(1) >= 'y' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "CharacterEscapeSequence"

    // $ANTLR start "ZeroToThree"
    public void mZeroToThree() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:787:2: ( '0' .. '3' )
            // EcmaScriptV3.g:787:4: '0' .. '3'
            {
            	MatchRange('0','3'); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "ZeroToThree"

    // $ANTLR start "OctalEscapeSequence"
    public void mOctalEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:791:2: ( OctalDigit | ZeroToThree OctalDigit | '4' .. '7' OctalDigit | ZeroToThree OctalDigit OctalDigit )
            int alt22 = 4;
            int LA22_0 = input.LA(1);

            if ( ((LA22_0 >= '0' && LA22_0 <= '3')) )
            {
                int LA22_1 = input.LA(2);

                if ( ((LA22_1 >= '0' && LA22_1 <= '7')) )
                {
                    int LA22_4 = input.LA(3);

                    if ( ((LA22_4 >= '0' && LA22_4 <= '7')) )
                    {
                        alt22 = 4;
                    }
                    else 
                    {
                        alt22 = 2;}
                }
                else 
                {
                    alt22 = 1;}
            }
            else if ( ((LA22_0 >= '4' && LA22_0 <= '7')) )
            {
                int LA22_2 = input.LA(2);

                if ( ((LA22_2 >= '0' && LA22_2 <= '7')) )
                {
                    alt22 = 3;
                }
                else 
                {
                    alt22 = 1;}
            }
            else 
            {
                NoViableAltException nvae_d22s0 =
                    new NoViableAltException("", 22, 0, input);

                throw nvae_d22s0;
            }
            switch (alt22) 
            {
                case 1 :
                    // EcmaScriptV3.g:791:4: OctalDigit
                    {
                    	mOctalDigit(); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:792:4: ZeroToThree OctalDigit
                    {
                    	mZeroToThree(); 
                    	mOctalDigit(); 

                    }
                    break;
                case 3 :
                    // EcmaScriptV3.g:793:4: '4' .. '7' OctalDigit
                    {
                    	MatchRange('4','7'); 
                    	mOctalDigit(); 

                    }
                    break;
                case 4 :
                    // EcmaScriptV3.g:794:4: ZeroToThree OctalDigit OctalDigit
                    {
                    	mZeroToThree(); 
                    	mOctalDigit(); 
                    	mOctalDigit(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OctalEscapeSequence"

    // $ANTLR start "HexEscapeSequence"
    public void mHexEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:798:2: ( 'x' HexDigit HexDigit )
            // EcmaScriptV3.g:798:4: 'x' HexDigit HexDigit
            {
            	Match('x'); 
            	mHexDigit(); 
            	mHexDigit(); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "HexEscapeSequence"

    // $ANTLR start "UnicodeEscapeSequence"
    public void mUnicodeEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:802:2: ( 'u' HexDigit HexDigit HexDigit HexDigit )
            // EcmaScriptV3.g:802:4: 'u' HexDigit HexDigit HexDigit HexDigit
            {
            	Match('u'); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 
            	mHexDigit(); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "UnicodeEscapeSequence"

    // $ANTLR start "EscapeSequence"
    public void mEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:806:2: ( BSLASH ( CharacterEscapeSequence | OctalEscapeSequence | HexEscapeSequence | UnicodeEscapeSequence ) )
            // EcmaScriptV3.g:807:2: BSLASH ( CharacterEscapeSequence | OctalEscapeSequence | HexEscapeSequence | UnicodeEscapeSequence )
            {
            	mBSLASH(); 
            	// EcmaScriptV3.g:808:2: ( CharacterEscapeSequence | OctalEscapeSequence | HexEscapeSequence | UnicodeEscapeSequence )
            	int alt23 = 4;
            	int LA23_0 = input.LA(1);

            	if ( ((LA23_0 >= '\u0000' && LA23_0 <= '\t') || (LA23_0 >= '\u000B' && LA23_0 <= '\f') || (LA23_0 >= '\u000E' && LA23_0 <= '/') || (LA23_0 >= ':' && LA23_0 <= 't') || (LA23_0 >= 'v' && LA23_0 <= 'w') || (LA23_0 >= 'y' && LA23_0 <= '\u2027') || (LA23_0 >= '\u202A' && LA23_0 <= '\uFFFF')) )
            	{
            	    alt23 = 1;
            	}
            	else if ( ((LA23_0 >= '0' && LA23_0 <= '7')) )
            	{
            	    alt23 = 2;
            	}
            	else if ( (LA23_0 == 'x') )
            	{
            	    alt23 = 3;
            	}
            	else if ( (LA23_0 == 'u') )
            	{
            	    alt23 = 4;
            	}
            	else 
            	{
            	    NoViableAltException nvae_d23s0 =
            	        new NoViableAltException("", 23, 0, input);

            	    throw nvae_d23s0;
            	}
            	switch (alt23) 
            	{
            	    case 1 :
            	        // EcmaScriptV3.g:809:3: CharacterEscapeSequence
            	        {
            	        	mCharacterEscapeSequence(); 

            	        }
            	        break;
            	    case 2 :
            	        // EcmaScriptV3.g:810:5: OctalEscapeSequence
            	        {
            	        	mOctalEscapeSequence(); 

            	        }
            	        break;
            	    case 3 :
            	        // EcmaScriptV3.g:811:5: HexEscapeSequence
            	        {
            	        	mHexEscapeSequence(); 

            	        }
            	        break;
            	    case 4 :
            	        // EcmaScriptV3.g:812:5: UnicodeEscapeSequence
            	        {
            	        	mUnicodeEscapeSequence(); 

            	        }
            	        break;

            	}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "EscapeSequence"

    // $ANTLR start "StringLiteral"
    public void mStringLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = StringLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:817:2: ( SQUOTE (~ ( SQUOTE | BSLASH | LineTerminator ) | EscapeSequence )* SQUOTE | DQUOTE (~ ( DQUOTE | BSLASH | LineTerminator ) | EscapeSequence )* DQUOTE )
            int alt26 = 2;
            int LA26_0 = input.LA(1);

            if ( (LA26_0 == '\'') )
            {
                alt26 = 1;
            }
            else if ( (LA26_0 == '\"') )
            {
                alt26 = 2;
            }
            else 
            {
                NoViableAltException nvae_d26s0 =
                    new NoViableAltException("", 26, 0, input);

                throw nvae_d26s0;
            }
            switch (alt26) 
            {
                case 1 :
                    // EcmaScriptV3.g:817:4: SQUOTE (~ ( SQUOTE | BSLASH | LineTerminator ) | EscapeSequence )* SQUOTE
                    {
                    	mSQUOTE(); 
                    	// EcmaScriptV3.g:817:11: (~ ( SQUOTE | BSLASH | LineTerminator ) | EscapeSequence )*
                    	do 
                    	{
                    	    int alt24 = 3;
                    	    int LA24_0 = input.LA(1);

                    	    if ( ((LA24_0 >= '\u0000' && LA24_0 <= '\t') || (LA24_0 >= '\u000B' && LA24_0 <= '\f') || (LA24_0 >= '\u000E' && LA24_0 <= '&') || (LA24_0 >= '(' && LA24_0 <= '[') || (LA24_0 >= ']' && LA24_0 <= '\u2027') || (LA24_0 >= '\u202A' && LA24_0 <= '\uFFFF')) )
                    	    {
                    	        alt24 = 1;
                    	    }
                    	    else if ( (LA24_0 == '\\') )
                    	    {
                    	        alt24 = 2;
                    	    }


                    	    switch (alt24) 
                    		{
                    			case 1 :
                    			    // EcmaScriptV3.g:817:13: ~ ( SQUOTE | BSLASH | LineTerminator )
                    			    {
                    			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '&') || (input.LA(1) >= '(' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
                    			    	{
                    			    	    input.Consume();

                    			    	}
                    			    	else 
                    			    	{
                    			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    	    Recover(mse);
                    			    	    throw mse;}


                    			    }
                    			    break;
                    			case 2 :
                    			    // EcmaScriptV3.g:817:53: EscapeSequence
                    			    {
                    			    	mEscapeSequence(); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop24;
                    	    }
                    	} while (true);

                    	loop24:
                    		;	// Stops C# compiler whining that label 'loop24' has no statements

                    	mSQUOTE(); 

                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:818:4: DQUOTE (~ ( DQUOTE | BSLASH | LineTerminator ) | EscapeSequence )* DQUOTE
                    {
                    	mDQUOTE(); 
                    	// EcmaScriptV3.g:818:11: (~ ( DQUOTE | BSLASH | LineTerminator ) | EscapeSequence )*
                    	do 
                    	{
                    	    int alt25 = 3;
                    	    int LA25_0 = input.LA(1);

                    	    if ( ((LA25_0 >= '\u0000' && LA25_0 <= '\t') || (LA25_0 >= '\u000B' && LA25_0 <= '\f') || (LA25_0 >= '\u000E' && LA25_0 <= '!') || (LA25_0 >= '#' && LA25_0 <= '[') || (LA25_0 >= ']' && LA25_0 <= '\u2027') || (LA25_0 >= '\u202A' && LA25_0 <= '\uFFFF')) )
                    	    {
                    	        alt25 = 1;
                    	    }
                    	    else if ( (LA25_0 == '\\') )
                    	    {
                    	        alt25 = 2;
                    	    }


                    	    switch (alt25) 
                    		{
                    			case 1 :
                    			    // EcmaScriptV3.g:818:13: ~ ( DQUOTE | BSLASH | LineTerminator )
                    			    {
                    			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
                    			    	{
                    			    	    input.Consume();

                    			    	}
                    			    	else 
                    			    	{
                    			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    			    	    Recover(mse);
                    			    	    throw mse;}


                    			    }
                    			    break;
                    			case 2 :
                    			    // EcmaScriptV3.g:818:53: EscapeSequence
                    			    {
                    			    	mEscapeSequence(); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop25;
                    	    }
                    	} while (true);

                    	loop25:
                    		;	// Stops C# compiler whining that label 'loop25' has no statements

                    	mDQUOTE(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "StringLiteral"

    // $ANTLR start "BackslashSequence"
    public void mBackslashSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:826:2: ( BSLASH ~ ( LineTerminator ) )
            // EcmaScriptV3.g:826:4: BSLASH ~ ( LineTerminator )
            {
            	mBSLASH(); 
            	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "BackslashSequence"

    // $ANTLR start "RegularExpressionFirstChar"
    public void mRegularExpressionFirstChar() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:830:2: (~ ( LineTerminator | MUL | BSLASH | DIV ) | BackslashSequence )
            int alt27 = 2;
            int LA27_0 = input.LA(1);

            if ( ((LA27_0 >= '\u0000' && LA27_0 <= '\t') || (LA27_0 >= '\u000B' && LA27_0 <= '\f') || (LA27_0 >= '\u000E' && LA27_0 <= ')') || (LA27_0 >= '+' && LA27_0 <= '.') || (LA27_0 >= '0' && LA27_0 <= '[') || (LA27_0 >= ']' && LA27_0 <= '\u2027') || (LA27_0 >= '\u202A' && LA27_0 <= '\uFFFF')) )
            {
                alt27 = 1;
            }
            else if ( (LA27_0 == '\\') )
            {
                alt27 = 2;
            }
            else 
            {
                NoViableAltException nvae_d27s0 =
                    new NoViableAltException("", 27, 0, input);

                throw nvae_d27s0;
            }
            switch (alt27) 
            {
                case 1 :
                    // EcmaScriptV3.g:830:4: ~ ( LineTerminator | MUL | BSLASH | DIV )
                    {
                    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= ')') || (input.LA(1) >= '+' && input.LA(1) <= '.') || (input.LA(1) >= '0' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
                    	{
                    	    input.Consume();

                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    Recover(mse);
                    	    throw mse;}


                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:831:4: BackslashSequence
                    {
                    	mBackslashSequence(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RegularExpressionFirstChar"

    // $ANTLR start "RegularExpressionChar"
    public void mRegularExpressionChar() // throws RecognitionException [2]
    {
    		try
    		{
            // EcmaScriptV3.g:835:2: (~ ( LineTerminator | BSLASH | DIV ) | BackslashSequence )
            int alt28 = 2;
            int LA28_0 = input.LA(1);

            if ( ((LA28_0 >= '\u0000' && LA28_0 <= '\t') || (LA28_0 >= '\u000B' && LA28_0 <= '\f') || (LA28_0 >= '\u000E' && LA28_0 <= '.') || (LA28_0 >= '0' && LA28_0 <= '[') || (LA28_0 >= ']' && LA28_0 <= '\u2027') || (LA28_0 >= '\u202A' && LA28_0 <= '\uFFFF')) )
            {
                alt28 = 1;
            }
            else if ( (LA28_0 == '\\') )
            {
                alt28 = 2;
            }
            else 
            {
                NoViableAltException nvae_d28s0 =
                    new NoViableAltException("", 28, 0, input);

                throw nvae_d28s0;
            }
            switch (alt28) 
            {
                case 1 :
                    // EcmaScriptV3.g:835:4: ~ ( LineTerminator | BSLASH | DIV )
                    {
                    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '.') || (input.LA(1) >= '0' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\u2027') || (input.LA(1) >= '\u202A' && input.LA(1) <= '\uFFFF') ) 
                    	{
                    	    input.Consume();

                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    Recover(mse);
                    	    throw mse;}


                    }
                    break;
                case 2 :
                    // EcmaScriptV3.g:836:4: BackslashSequence
                    {
                    	mBackslashSequence(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RegularExpressionChar"

    // $ANTLR start "RegularExpressionLiteral"
    public void mRegularExpressionLiteral() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RegularExpressionLiteral;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // EcmaScriptV3.g:840:2: ({...}? => DIV RegularExpressionFirstChar ( RegularExpressionChar )* DIV ( IdentifierPart )* )
            // EcmaScriptV3.g:840:4: {...}? => DIV RegularExpressionFirstChar ( RegularExpressionChar )* DIV ( IdentifierPart )*
            {
            	if ( !(( areRegularExpressionsEnabled() )) ) 
            	{
            	    throw new FailedPredicateException(input, "RegularExpressionLiteral", " areRegularExpressionsEnabled() ");
            	}
            	mDIV(); 
            	mRegularExpressionFirstChar(); 
            	// EcmaScriptV3.g:840:73: ( RegularExpressionChar )*
            	do 
            	{
            	    int alt29 = 2;
            	    int LA29_0 = input.LA(1);

            	    if ( ((LA29_0 >= '\u0000' && LA29_0 <= '\t') || (LA29_0 >= '\u000B' && LA29_0 <= '\f') || (LA29_0 >= '\u000E' && LA29_0 <= '.') || (LA29_0 >= '0' && LA29_0 <= '\u2027') || (LA29_0 >= '\u202A' && LA29_0 <= '\uFFFF')) )
            	    {
            	        alt29 = 1;
            	    }


            	    switch (alt29) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:840:73: RegularExpressionChar
            			    {
            			    	mRegularExpressionChar(); 

            			    }
            			    break;

            			default:
            			    goto loop29;
            	    }
            	} while (true);

            	loop29:
            		;	// Stops C# compiler whining that label 'loop29' has no statements

            	mDIV(); 
            	// EcmaScriptV3.g:840:100: ( IdentifierPart )*
            	do 
            	{
            	    int alt30 = 2;
            	    int LA30_0 = input.LA(1);

            	    if ( (LA30_0 == '$' || (LA30_0 >= '0' && LA30_0 <= '9') || (LA30_0 >= 'A' && LA30_0 <= 'Z') || LA30_0 == '\\' || LA30_0 == '_' || (LA30_0 >= 'a' && LA30_0 <= 'z')) )
            	    {
            	        alt30 = 1;
            	    }
            	    else if ( (( isIdentifierPartUnicode(input.LA(1)) )) )
            	    {
            	        alt30 = 1;
            	    }


            	    switch (alt30) 
            		{
            			case 1 :
            			    // EcmaScriptV3.g:840:100: IdentifierPart
            			    {
            			    	mIdentifierPart(); 

            			    }
            			    break;

            			default:
            			    goto loop30;
            	    }
            	} while (true);

            	loop30:
            		;	// Stops C# compiler whining that label 'loop30' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RegularExpressionLiteral"

    override public void mTokens() // throws RecognitionException 
    {
        // EcmaScriptV3.g:1:8: ( NULL | TRUE | FALSE | BREAK | CASE | CATCH | CONTINUE | DEFAULT | DELETE | DO | ELSE | FINALLY | FOR | FUNCTION | IF | IN | INSTANCEOF | NEW | RETURN | SWITCH | THIS | THROW | TRY | TYPEOF | VAR | VOID | WHILE | WITH | ABSTRACT | BOOLEAN | BYTE | CHAR | CLASS | CONST | DEBUGGER | DOUBLE | ENUM | EXPORT | EXTENDS | FINAL | FLOAT | GOTO | IMPLEMENTS | IMPORT | INT | INTERFACE | LONG | NATIVE | PACKAGE | PRIVATE | PROTECTED | PUBLIC | SHORT | STATIC | SUPER | SYNCHRONIZED | THROWS | TRANSIENT | VOLATILE | LBRACE | RBRACE | LPAREN | RPAREN | LBRACK | RBRACK | DOT | SEMIC | COMMA | LT | GT | LTE | GTE | EQ | NEQ | SAME | NSAME | ADD | SUB | MUL | MOD | INC | DEC | SHL | SHR | SHU | AND | OR | XOR | NOT | INV | LAND | LOR | QUE | COLON | ASSIGN | ADDASS | SUBASS | MULASS | MODASS | SHLASS | SHRASS | SHUASS | ANDASS | ORASS | XORASS | DIV | DIVASS | WhiteSpace | EOL | MultiLineComment | SingleLineComment | Identifier | DecimalIntegerLiteral | DecimalFloatingPointLiteral | OctalIntegerLiteral | HexIntegerLiteral | StringLiteral | RegularExpressionLiteral )
        int alt31 = 118;
        alt31 = dfa31.Predict(input);
        switch (alt31) 
        {
            case 1 :
                // EcmaScriptV3.g:1:10: NULL
                {
                	mNULL(); 

                }
                break;
            case 2 :
                // EcmaScriptV3.g:1:15: TRUE
                {
                	mTRUE(); 

                }
                break;
            case 3 :
                // EcmaScriptV3.g:1:20: FALSE
                {
                	mFALSE(); 

                }
                break;
            case 4 :
                // EcmaScriptV3.g:1:26: BREAK
                {
                	mBREAK(); 

                }
                break;
            case 5 :
                // EcmaScriptV3.g:1:32: CASE
                {
                	mCASE(); 

                }
                break;
            case 6 :
                // EcmaScriptV3.g:1:37: CATCH
                {
                	mCATCH(); 

                }
                break;
            case 7 :
                // EcmaScriptV3.g:1:43: CONTINUE
                {
                	mCONTINUE(); 

                }
                break;
            case 8 :
                // EcmaScriptV3.g:1:52: DEFAULT
                {
                	mDEFAULT(); 

                }
                break;
            case 9 :
                // EcmaScriptV3.g:1:60: DELETE
                {
                	mDELETE(); 

                }
                break;
            case 10 :
                // EcmaScriptV3.g:1:67: DO
                {
                	mDO(); 

                }
                break;
            case 11 :
                // EcmaScriptV3.g:1:70: ELSE
                {
                	mELSE(); 

                }
                break;
            case 12 :
                // EcmaScriptV3.g:1:75: FINALLY
                {
                	mFINALLY(); 

                }
                break;
            case 13 :
                // EcmaScriptV3.g:1:83: FOR
                {
                	mFOR(); 

                }
                break;
            case 14 :
                // EcmaScriptV3.g:1:87: FUNCTION
                {
                	mFUNCTION(); 

                }
                break;
            case 15 :
                // EcmaScriptV3.g:1:96: IF
                {
                	mIF(); 

                }
                break;
            case 16 :
                // EcmaScriptV3.g:1:99: IN
                {
                	mIN(); 

                }
                break;
            case 17 :
                // EcmaScriptV3.g:1:102: INSTANCEOF
                {
                	mINSTANCEOF(); 

                }
                break;
            case 18 :
                // EcmaScriptV3.g:1:113: NEW
                {
                	mNEW(); 

                }
                break;
            case 19 :
                // EcmaScriptV3.g:1:117: RETURN
                {
                	mRETURN(); 

                }
                break;
            case 20 :
                // EcmaScriptV3.g:1:124: SWITCH
                {
                	mSWITCH(); 

                }
                break;
            case 21 :
                // EcmaScriptV3.g:1:131: THIS
                {
                	mTHIS(); 

                }
                break;
            case 22 :
                // EcmaScriptV3.g:1:136: THROW
                {
                	mTHROW(); 

                }
                break;
            case 23 :
                // EcmaScriptV3.g:1:142: TRY
                {
                	mTRY(); 

                }
                break;
            case 24 :
                // EcmaScriptV3.g:1:146: TYPEOF
                {
                	mTYPEOF(); 

                }
                break;
            case 25 :
                // EcmaScriptV3.g:1:153: VAR
                {
                	mVAR(); 

                }
                break;
            case 26 :
                // EcmaScriptV3.g:1:157: VOID
                {
                	mVOID(); 

                }
                break;
            case 27 :
                // EcmaScriptV3.g:1:162: WHILE
                {
                	mWHILE(); 

                }
                break;
            case 28 :
                // EcmaScriptV3.g:1:168: WITH
                {
                	mWITH(); 

                }
                break;
            case 29 :
                // EcmaScriptV3.g:1:173: ABSTRACT
                {
                	mABSTRACT(); 

                }
                break;
            case 30 :
                // EcmaScriptV3.g:1:182: BOOLEAN
                {
                	mBOOLEAN(); 

                }
                break;
            case 31 :
                // EcmaScriptV3.g:1:190: BYTE
                {
                	mBYTE(); 

                }
                break;
            case 32 :
                // EcmaScriptV3.g:1:195: CHAR
                {
                	mCHAR(); 

                }
                break;
            case 33 :
                // EcmaScriptV3.g:1:200: CLASS
                {
                	mCLASS(); 

                }
                break;
            case 34 :
                // EcmaScriptV3.g:1:206: CONST
                {
                	mCONST(); 

                }
                break;
            case 35 :
                // EcmaScriptV3.g:1:212: DEBUGGER
                {
                	mDEBUGGER(); 

                }
                break;
            case 36 :
                // EcmaScriptV3.g:1:221: DOUBLE
                {
                	mDOUBLE(); 

                }
                break;
            case 37 :
                // EcmaScriptV3.g:1:228: ENUM
                {
                	mENUM(); 

                }
                break;
            case 38 :
                // EcmaScriptV3.g:1:233: EXPORT
                {
                	mEXPORT(); 

                }
                break;
            case 39 :
                // EcmaScriptV3.g:1:240: EXTENDS
                {
                	mEXTENDS(); 

                }
                break;
            case 40 :
                // EcmaScriptV3.g:1:248: FINAL
                {
                	mFINAL(); 

                }
                break;
            case 41 :
                // EcmaScriptV3.g:1:254: FLOAT
                {
                	mFLOAT(); 

                }
                break;
            case 42 :
                // EcmaScriptV3.g:1:260: GOTO
                {
                	mGOTO(); 

                }
                break;
            case 43 :
                // EcmaScriptV3.g:1:265: IMPLEMENTS
                {
                	mIMPLEMENTS(); 

                }
                break;
            case 44 :
                // EcmaScriptV3.g:1:276: IMPORT
                {
                	mIMPORT(); 

                }
                break;
            case 45 :
                // EcmaScriptV3.g:1:283: INT
                {
                	mINT(); 

                }
                break;
            case 46 :
                // EcmaScriptV3.g:1:287: INTERFACE
                {
                	mINTERFACE(); 

                }
                break;
            case 47 :
                // EcmaScriptV3.g:1:297: LONG
                {
                	mLONG(); 

                }
                break;
            case 48 :
                // EcmaScriptV3.g:1:302: NATIVE
                {
                	mNATIVE(); 

                }
                break;
            case 49 :
                // EcmaScriptV3.g:1:309: PACKAGE
                {
                	mPACKAGE(); 

                }
                break;
            case 50 :
                // EcmaScriptV3.g:1:317: PRIVATE
                {
                	mPRIVATE(); 

                }
                break;
            case 51 :
                // EcmaScriptV3.g:1:325: PROTECTED
                {
                	mPROTECTED(); 

                }
                break;
            case 52 :
                // EcmaScriptV3.g:1:335: PUBLIC
                {
                	mPUBLIC(); 

                }
                break;
            case 53 :
                // EcmaScriptV3.g:1:342: SHORT
                {
                	mSHORT(); 

                }
                break;
            case 54 :
                // EcmaScriptV3.g:1:348: STATIC
                {
                	mSTATIC(); 

                }
                break;
            case 55 :
                // EcmaScriptV3.g:1:355: SUPER
                {
                	mSUPER(); 

                }
                break;
            case 56 :
                // EcmaScriptV3.g:1:361: SYNCHRONIZED
                {
                	mSYNCHRONIZED(); 

                }
                break;
            case 57 :
                // EcmaScriptV3.g:1:374: THROWS
                {
                	mTHROWS(); 

                }
                break;
            case 58 :
                // EcmaScriptV3.g:1:381: TRANSIENT
                {
                	mTRANSIENT(); 

                }
                break;
            case 59 :
                // EcmaScriptV3.g:1:391: VOLATILE
                {
                	mVOLATILE(); 

                }
                break;
            case 60 :
                // EcmaScriptV3.g:1:400: LBRACE
                {
                	mLBRACE(); 

                }
                break;
            case 61 :
                // EcmaScriptV3.g:1:407: RBRACE
                {
                	mRBRACE(); 

                }
                break;
            case 62 :
                // EcmaScriptV3.g:1:414: LPAREN
                {
                	mLPAREN(); 

                }
                break;
            case 63 :
                // EcmaScriptV3.g:1:421: RPAREN
                {
                	mRPAREN(); 

                }
                break;
            case 64 :
                // EcmaScriptV3.g:1:428: LBRACK
                {
                	mLBRACK(); 

                }
                break;
            case 65 :
                // EcmaScriptV3.g:1:435: RBRACK
                {
                	mRBRACK(); 

                }
                break;
            case 66 :
                // EcmaScriptV3.g:1:442: DOT
                {
                	mDOT(); 

                }
                break;
            case 67 :
                // EcmaScriptV3.g:1:446: SEMIC
                {
                	mSEMIC(); 

                }
                break;
            case 68 :
                // EcmaScriptV3.g:1:452: COMMA
                {
                	mCOMMA(); 

                }
                break;
            case 69 :
                // EcmaScriptV3.g:1:458: LT
                {
                	mLT(); 

                }
                break;
            case 70 :
                // EcmaScriptV3.g:1:461: GT
                {
                	mGT(); 

                }
                break;
            case 71 :
                // EcmaScriptV3.g:1:464: LTE
                {
                	mLTE(); 

                }
                break;
            case 72 :
                // EcmaScriptV3.g:1:468: GTE
                {
                	mGTE(); 

                }
                break;
            case 73 :
                // EcmaScriptV3.g:1:472: EQ
                {
                	mEQ(); 

                }
                break;
            case 74 :
                // EcmaScriptV3.g:1:475: NEQ
                {
                	mNEQ(); 

                }
                break;
            case 75 :
                // EcmaScriptV3.g:1:479: SAME
                {
                	mSAME(); 

                }
                break;
            case 76 :
                // EcmaScriptV3.g:1:484: NSAME
                {
                	mNSAME(); 

                }
                break;
            case 77 :
                // EcmaScriptV3.g:1:490: ADD
                {
                	mADD(); 

                }
                break;
            case 78 :
                // EcmaScriptV3.g:1:494: SUB
                {
                	mSUB(); 

                }
                break;
            case 79 :
                // EcmaScriptV3.g:1:498: MUL
                {
                	mMUL(); 

                }
                break;
            case 80 :
                // EcmaScriptV3.g:1:502: MOD
                {
                	mMOD(); 

                }
                break;
            case 81 :
                // EcmaScriptV3.g:1:506: INC
                {
                	mINC(); 

                }
                break;
            case 82 :
                // EcmaScriptV3.g:1:510: DEC
                {
                	mDEC(); 

                }
                break;
            case 83 :
                // EcmaScriptV3.g:1:514: SHL
                {
                	mSHL(); 

                }
                break;
            case 84 :
                // EcmaScriptV3.g:1:518: SHR
                {
                	mSHR(); 

                }
                break;
            case 85 :
                // EcmaScriptV3.g:1:522: SHU
                {
                	mSHU(); 

                }
                break;
            case 86 :
                // EcmaScriptV3.g:1:526: AND
                {
                	mAND(); 

                }
                break;
            case 87 :
                // EcmaScriptV3.g:1:530: OR
                {
                	mOR(); 

                }
                break;
            case 88 :
                // EcmaScriptV3.g:1:533: XOR
                {
                	mXOR(); 

                }
                break;
            case 89 :
                // EcmaScriptV3.g:1:537: NOT
                {
                	mNOT(); 

                }
                break;
            case 90 :
                // EcmaScriptV3.g:1:541: INV
                {
                	mINV(); 

                }
                break;
            case 91 :
                // EcmaScriptV3.g:1:545: LAND
                {
                	mLAND(); 

                }
                break;
            case 92 :
                // EcmaScriptV3.g:1:550: LOR
                {
                	mLOR(); 

                }
                break;
            case 93 :
                // EcmaScriptV3.g:1:554: QUE
                {
                	mQUE(); 

                }
                break;
            case 94 :
                // EcmaScriptV3.g:1:558: COLON
                {
                	mCOLON(); 

                }
                break;
            case 95 :
                // EcmaScriptV3.g:1:564: ASSIGN
                {
                	mASSIGN(); 

                }
                break;
            case 96 :
                // EcmaScriptV3.g:1:571: ADDASS
                {
                	mADDASS(); 

                }
                break;
            case 97 :
                // EcmaScriptV3.g:1:578: SUBASS
                {
                	mSUBASS(); 

                }
                break;
            case 98 :
                // EcmaScriptV3.g:1:585: MULASS
                {
                	mMULASS(); 

                }
                break;
            case 99 :
                // EcmaScriptV3.g:1:592: MODASS
                {
                	mMODASS(); 

                }
                break;
            case 100 :
                // EcmaScriptV3.g:1:599: SHLASS
                {
                	mSHLASS(); 

                }
                break;
            case 101 :
                // EcmaScriptV3.g:1:606: SHRASS
                {
                	mSHRASS(); 

                }
                break;
            case 102 :
                // EcmaScriptV3.g:1:613: SHUASS
                {
                	mSHUASS(); 

                }
                break;
            case 103 :
                // EcmaScriptV3.g:1:620: ANDASS
                {
                	mANDASS(); 

                }
                break;
            case 104 :
                // EcmaScriptV3.g:1:627: ORASS
                {
                	mORASS(); 

                }
                break;
            case 105 :
                // EcmaScriptV3.g:1:633: XORASS
                {
                	mXORASS(); 

                }
                break;
            case 106 :
                // EcmaScriptV3.g:1:640: DIV
                {
                	mDIV(); 

                }
                break;
            case 107 :
                // EcmaScriptV3.g:1:644: DIVASS
                {
                	mDIVASS(); 

                }
                break;
            case 108 :
                // EcmaScriptV3.g:1:651: WhiteSpace
                {
                	mWhiteSpace(); 

                }
                break;
            case 109 :
                // EcmaScriptV3.g:1:662: EOL
                {
                	mEOL(); 

                }
                break;
            case 110 :
                // EcmaScriptV3.g:1:666: MultiLineComment
                {
                	mMultiLineComment(); 

                }
                break;
            case 111 :
                // EcmaScriptV3.g:1:683: SingleLineComment
                {
                	mSingleLineComment(); 

                }
                break;
            case 112 :
                // EcmaScriptV3.g:1:701: Identifier
                {
                	mIdentifier(); 

                }
                break;
            case 113 :
                // EcmaScriptV3.g:1:712: DecimalIntegerLiteral
                {
                	mDecimalIntegerLiteral(); 

                }
                break;
            case 114 :
                // EcmaScriptV3.g:1:734: DecimalFloatingPointLiteral
                {
                	mDecimalFloatingPointLiteral(); 

                }
                break;
            case 115 :
                // EcmaScriptV3.g:1:762: OctalIntegerLiteral
                {
                	mOctalIntegerLiteral(); 

                }
                break;
            case 116 :
                // EcmaScriptV3.g:1:782: HexIntegerLiteral
                {
                	mHexIntegerLiteral(); 

                }
                break;
            case 117 :
                // EcmaScriptV3.g:1:800: StringLiteral
                {
                	mStringLiteral(); 

                }
                break;
            case 118 :
                // EcmaScriptV3.g:1:814: RegularExpressionLiteral
                {
                	mRegularExpressionLiteral(); 

                }
                break;

        }

    }


    protected DFA18 dfa18;
    protected DFA31 dfa31;
	private void InitializeCyclicDFAs()
	{
	    this.dfa18 = new DFA18(this);
	    this.dfa31 = new DFA31(this);

	    this.dfa31.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA31_SpecialStateTransition);
	}

    const string DFA18_eotS =
        "\x07\uffff";
    const string DFA18_eofS =
        "\x07\uffff";
    const string DFA18_minS =
        "\x03\x2e\x03\uffff\x01\x2e";
    const string DFA18_maxS =
        "\x01\x39\x02\x65\x03\uffff\x01\x65";
    const string DFA18_acceptS =
        "\x03\uffff\x01\x02\x01\x03\x01\x01\x01\uffff";
    const string DFA18_specialS =
        "\x07\uffff}>";
    static readonly string[] DFA18_transitionS = {
            "\x01\x03\x01\uffff\x01\x01\x09\x02",
            "\x01\x05\x16\uffff\x01\x04\x1f\uffff\x01\x04",
            "\x01\x05\x01\uffff\x0a\x06\x0b\uffff\x01\x04\x1f\uffff\x01"+
            "\x04",
            "",
            "",
            "",
            "\x01\x05\x01\uffff\x0a\x06\x0b\uffff\x01\x04\x1f\uffff\x01"+
            "\x04"
    };

    static readonly short[] DFA18_eot = DFA.UnpackEncodedString(DFA18_eotS);
    static readonly short[] DFA18_eof = DFA.UnpackEncodedString(DFA18_eofS);
    static readonly char[] DFA18_min = DFA.UnpackEncodedStringToUnsignedChars(DFA18_minS);
    static readonly char[] DFA18_max = DFA.UnpackEncodedStringToUnsignedChars(DFA18_maxS);
    static readonly short[] DFA18_accept = DFA.UnpackEncodedString(DFA18_acceptS);
    static readonly short[] DFA18_special = DFA.UnpackEncodedString(DFA18_specialS);
    static readonly short[][] DFA18_transition = DFA.UnpackEncodedStringArray(DFA18_transitionS);

    protected class DFA18 : DFA
    {
        public DFA18(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 18;
            this.eot = DFA18_eot;
            this.eof = DFA18_eof;
            this.min = DFA18_min;
            this.max = DFA18_max;
            this.accept = DFA18_accept;
            this.special = DFA18_special;
            this.transition = DFA18_transition;

        }

        override public string Description
        {
            get { return "752:1: DecimalFloatingPointLiteral : ( DecimalIntegerPart '.' ( DecimalDigit )* ( ExponentPart )? | '.' ( DecimalDigit )+ ( ExponentPart )? | DecimalIntegerPart ExponentPart );"; }
        }

    }

    const string DFA31_eotS =
        "\x11\x2b\x06\uffff\x01\x5a\x02\uffff\x01\x5d\x01\x60\x01\x62\x01"+
        "\x64\x01\x67\x01\x6a\x01\x6c\x01\x6e\x01\x71\x01\x74\x01\x76\x03"+
        "\uffff\x01\x7a\x03\uffff\x02\x7e\x01\uffff\x13\x2b\x01\u009a\x03"+
        "\x2b\x01\u009f\x01\u00a2\x11\x2b\x03\uffff\x01\u00b7\x02\uffff\x01"+
        "\u00ba\x01\uffff\x01\u00bc\x01\uffff\x01\u00be\x13\uffff\x01\u00bf"+
        "\x07\uffff\x01\x7e\x01\x2b\x01\u00c1\x02\x2b\x01\u00c4\x06\x2b\x01"+
        "\u00cb\x0e\x2b\x01\uffff\x04\x2b\x01\uffff\x01\x2b\x01\u00e1\x01"+
        "\uffff\x07\x2b\x01\u00ea\x0b\x2b\x02\uffff\x01\u00f7\x07\uffff\x01"+
        "\u00f8\x01\uffff\x01\x2b\x01\u00fa\x01\uffff\x01\x2b\x01\u00fc\x04"+
        "\x2b\x01\uffff\x04\x2b\x01\u0105\x01\u0106\x03\x2b\x01\u010a\x05"+
        "\x2b\x01\u0110\x01\u0111\x04\x2b\x01\uffff\x08\x2b\x01\uffff\x01"+
        "\u011e\x02\x2b\x01\u0121\x01\x2b\x01\u0123\x01\u0124\x04\x2b\x03"+
        "\uffff\x01\x2b\x01\uffff\x01\x2b\x01\uffff\x01\u012c\x01\x2b\x01"+
        "\u012e\x01\u0130\x01\x2b\x01\u0132\x01\u0133\x01\x2b\x02\uffff\x01"+
        "\u0135\x01\x2b\x01\u0137\x01\uffff\x01\u0138\x04\x2b\x02\uffff\x08"+
        "\x2b\x01\u0145\x01\x2b\x01\u0147\x01\x2b\x01\uffff\x01\x2b\x01\u014a"+
        "\x01\uffff\x01\x2b\x02\uffff\x04\x2b\x01\u0150\x01\x2b\x01\u0152"+
        "\x01\uffff\x01\u0153\x01\uffff\x01\x2b\x01\uffff\x01\x2b\x02\uffff"+
        "\x01\x2b\x01\uffff\x01\x2b\x02\uffff\x01\x2b\x01\u0159\x01\x2b\x01"+
        "\u015b\x01\u015c\x04\x2b\x01\u0161\x01\u0162\x01\u0163\x01\uffff"+
        "\x01\u0164\x01\uffff\x02\x2b\x01\uffff\x04\x2b\x01\u016b\x01\uffff"+
        "\x01\x2b\x02\uffff\x01\u016d\x01\x2b\x01\u016f\x01\x2b\x01\u0171"+
        "\x01\uffff\x01\x2b\x02\uffff\x01\u0173\x03\x2b\x04\uffff\x03\x2b"+
        "\x01\u017a\x01\u017b\x01\x2b\x01\uffff\x01\x2b\x01\uffff\x01\u017e"+
        "\x01\uffff\x01\u017f\x01\uffff\x01\u0180\x01\uffff\x04\x2b\x01\u0185"+
        "\x01\u0186\x02\uffff\x01\x2b\x01\u0188\x03\uffff\x01\x2b\x01\u018a"+
        "\x02\x2b\x02\uffff\x01\u018d\x01\uffff\x01\u018e\x01\uffff\x01\u018f"+
        "\x01\x2b\x03\uffff\x01\x2b\x01\u0192\x01\uffff";
    const string DFA31_eofS =
        "\u0193\uffff";
    const string DFA31_minS =
        "\x01\x09\x01\x61\x01\x68\x01\x61\x01\x6f\x01\x61\x01\x65\x01\x6c"+
        "\x01\x66\x01\x65\x01\x68\x01\x61\x01\x68\x01\x62\x02\x6f\x01\x61"+
        "\x06\uffff\x01\x30\x02\uffff\x01\x3c\x03\x3d\x01\x2b\x01\x2d\x02"+
        "\x3d\x01\x26\x02\x3d\x03\uffff\x01\x00\x03\uffff\x02\x2e\x01\uffff"+
        "\x01\x6c\x01\x77\x01\x74\x01\x61\x01\x69\x01\x70\x01\x6c\x01\x6e"+
        "\x01\x72\x01\x6e\x01\x6f\x01\x65\x01\x6f\x01\x74\x01\x73\x01\x6e"+
        "\x02\x61\x01\x62\x01\x24\x01\x73\x01\x75\x01\x70\x02\x24\x01\x70"+
        "\x01\x74\x01\x69\x01\x6f\x01\x61\x01\x70\x01\x6e\x01\x72\x02\x69"+
        "\x01\x74\x01\x73\x01\x74\x01\x6e\x01\x63\x01\x69\x01\x62\x03\uffff"+
        "\x01\x3d\x02\uffff\x01\x3d\x01\uffff\x01\x3d\x01\uffff\x01\x3d\x13"+
        "\uffff\x01\x00\x07\uffff\x01\x2e\x01\x6c\x01\x24\x01\x69\x01\x65"+
        "\x01\x24\x01\x6e\x01\x73\x01\x6f\x01\x65\x01\x73\x01\x61\x01\x24"+
        "\x01\x63\x02\x61\x01\x6c\x02\x65\x01\x63\x01\x73\x01\x72\x01\x73"+
        "\x01\x61\x01\x65\x01\x75\x01\x62\x01\uffff\x01\x65\x01\x6d\x01\x6f"+
        "\x01\x65\x01\uffff\x01\x74\x01\x24\x01\uffff\x01\x6c\x01\x75\x01"+
        "\x74\x01\x72\x01\x74\x01\x65\x01\x63\x01\x24\x01\x64\x01\x61\x01"+
        "\x6c\x01\x68\x01\x74\x01\x6f\x01\x67\x01\x6b\x01\x76\x01\x74\x01"+
        "\x6c\x02\uffff\x01\x3d\x07\uffff\x01\x24\x01\uffff\x01\x76\x01\x24"+
        "\x01\uffff\x01\x73\x01\x24\x01\x77\x01\x6f\x01\x65\x01\x6c\x01\uffff"+
        "\x02\x74\x01\x6b\x01\x65\x02\x24\x01\x68\x01\x69\x01\x74\x01\x24"+
        "\x01\x73\x01\x75\x01\x74\x01\x67\x01\x6c\x02\x24\x01\x72\x01\x6e"+
        "\x01\x61\x01\x72\x01\uffff\x01\x65\x02\x72\x01\x63\x01\x74\x01\x69"+
        "\x01\x72\x01\x68\x01\uffff\x01\x24\x01\x74\x01\x65\x01\x24\x01\x72"+
        "\x02\x24\x02\x61\x01\x65\x01\x69\x03\uffff\x01\x65\x01\uffff\x01"+
        "\x69\x01\uffff\x01\x24\x01\x66\x02\x24\x01\x69\x02\x24\x01\x61\x02"+
        "\uffff\x01\x24\x01\x6e\x01\x24\x01\uffff\x01\x24\x01\x6c\x01\x65"+
        "\x01\x67\x01\x65\x02\uffff\x01\x74\x01\x64\x01\x6e\x01\x66\x01\x6d"+
        "\x01\x74\x01\x6e\x01\x68\x01\x24\x01\x63\x01\x24\x01\x72\x01\uffff"+
        "\x01\x69\x01\x24\x01\uffff\x01\x61\x02\uffff\x01\x67\x01\x74\x02"+
        "\x63\x01\x24\x01\x65\x01\x24\x01\uffff\x01\x24\x01\uffff\x01\x79"+
        "\x01\uffff\x01\x6f\x02\uffff\x01\x6e\x01\uffff\x01\x75\x02\uffff"+
        "\x01\x74\x01\x24\x01\x65\x02\x24\x01\x73\x01\x63\x01\x61\x01\x65"+
        "\x03\x24\x01\uffff\x01\x24\x01\uffff\x01\x6f\x01\x6c\x01\uffff\x01"+
        "\x63\x02\x65\x01\x74\x01\x24\x01\uffff\x01\x6e\x02\uffff\x01\x24"+
        "\x01\x6e\x01\x24\x01\x65\x01\x24\x01\uffff\x01\x72\x02\uffff\x01"+
        "\x24\x01\x65\x01\x63\x01\x6e\x04\uffff\x01\x6e\x01\x65\x01\x74\x02"+
        "\x24\x01\x65\x01\uffff\x01\x74\x01\uffff\x01\x24\x01\uffff\x01\x24"+
        "\x01\uffff\x01\x24\x01\uffff\x01\x6f\x01\x65\x01\x74\x01\x69\x02"+
        "\x24\x02\uffff\x01\x64\x01\x24\x03\uffff\x01\x66\x01\x24\x01\x73"+
        "\x01\x7a\x02\uffff\x01\x24\x01\uffff\x01\x24\x01\uffff\x01\x24\x01"+
        "\x65\x03\uffff\x01\x64\x01\x24\x01\uffff";
    const string DFA31_maxS =
        "\x01\u3000\x01\x75\x01\x79\x01\x75\x01\x79\x02\x6f\x01\x78\x01"+
        "\x6e\x01\x65\x01\x79\x01\x6f\x01\x69\x01\x62\x02\x6f\x01\x75\x06"+
        "\uffff\x01\x39\x02\uffff\x01\x3d\x01\x3e\x07\x3d\x01\x7c\x01\x3d"+
        "\x03\uffff\x01\uffff\x03\uffff\x01\x78\x01\x65\x01\uffff\x01\x6c"+
        "\x01\x77\x01\x74\x01\x79\x01\x72\x01\x70\x01\x6c\x01\x6e\x01\x72"+
        "\x01\x6e\x01\x6f\x01\x65\x01\x6f\x02\x74\x01\x6e\x02\x61\x01\x6c"+
        "\x01\x7a\x01\x73\x01\x75\x01\x74\x02\x7a\x01\x70\x01\x74\x01\x69"+
        "\x01\x6f\x01\x61\x01\x70\x01\x6e\x01\x72\x01\x6c\x01\x69\x01\x74"+
        "\x01\x73\x01\x74\x01\x6e\x01\x63\x01\x6f\x01\x62\x03\uffff\x01\x3d"+
        "\x02\uffff\x01\x3e\x01\uffff\x01\x3d\x01\uffff\x01\x3d\x13\uffff"+
        "\x01\uffff\x07\uffff\x01\x65\x01\x6c\x01\x7a\x01\x69\x01\x65\x01"+
        "\x7a\x01\x6e\x01\x73\x01\x6f\x01\x65\x01\x73\x01\x61\x01\x7a\x01"+
        "\x63\x02\x61\x01\x6c\x02\x65\x01\x63\x01\x74\x01\x72\x01\x73\x01"+
        "\x61\x01\x65\x01\x75\x01\x62\x01\uffff\x01\x65\x01\x6d\x01\x6f\x01"+
        "\x65\x01\uffff\x01\x74\x01\x7a\x01\uffff\x01\x6f\x01\x75\x01\x74"+
        "\x01\x72\x01\x74\x01\x65\x01\x63\x01\x7a\x01\x64\x01\x61\x01\x6c"+
        "\x01\x68\x01\x74\x01\x6f\x01\x67\x01\x6b\x01\x76\x01\x74\x01\x6c"+
        "\x02\uffff\x01\x3d\x07\uffff\x01\x7a\x01\uffff\x01\x76\x01\x7a\x01"+
        "\uffff\x01\x73\x01\x7a\x01\x77\x01\x6f\x01\x65\x01\x6c\x01\uffff"+
        "\x02\x74\x01\x6b\x01\x65\x02\x7a\x01\x68\x01\x69\x01\x74\x01\x7a"+
        "\x01\x73\x01\x75\x01\x74\x01\x67\x01\x6c\x02\x7a\x01\x72\x01\x6e"+
        "\x01\x61\x01\x72\x01\uffff\x01\x65\x02\x72\x01\x63\x01\x74\x01\x69"+
        "\x01\x72\x01\x68\x01\uffff\x01\x7a\x01\x74\x01\x65\x01\x7a\x01\x72"+
        "\x02\x7a\x02\x61\x01\x65\x01\x69\x03\uffff\x01\x65\x01\uffff\x01"+
        "\x69\x01\uffff\x01\x7a\x01\x66\x02\x7a\x01\x69\x02\x7a\x01\x61\x02"+
        "\uffff\x01\x7a\x01\x6e\x01\x7a\x01\uffff\x01\x7a\x01\x6c\x01\x65"+
        "\x01\x67\x01\x65\x02\uffff\x01\x74\x01\x64\x01\x6e\x01\x66\x01\x6d"+
        "\x01\x74\x01\x6e\x01\x68\x01\x7a\x01\x63\x01\x7a\x01\x72\x01\uffff"+
        "\x01\x69\x01\x7a\x01\uffff\x01\x61\x02\uffff\x01\x67\x01\x74\x02"+
        "\x63\x01\x7a\x01\x65\x01\x7a\x01\uffff\x01\x7a\x01\uffff\x01\x79"+
        "\x01\uffff\x01\x6f\x02\uffff\x01\x6e\x01\uffff\x01\x75\x02\uffff"+
        "\x01\x74\x01\x7a\x01\x65\x02\x7a\x01\x73\x01\x63\x01\x61\x01\x65"+
        "\x03\x7a\x01\uffff\x01\x7a\x01\uffff\x01\x6f\x01\x6c\x01\uffff\x01"+
        "\x63\x02\x65\x01\x74\x01\x7a\x01\uffff\x01\x6e\x02\uffff\x01\x7a"+
        "\x01\x6e\x01\x7a\x01\x65\x01\x7a\x01\uffff\x01\x72\x02\uffff\x01"+
        "\x7a\x01\x65\x01\x63\x01\x6e\x04\uffff\x01\x6e\x01\x65\x01\x74\x02"+
        "\x7a\x01\x65\x01\uffff\x01\x74\x01\uffff\x01\x7a\x01\uffff\x01\x7a"+
        "\x01\uffff\x01\x7a\x01\uffff\x01\x6f\x01\x65\x01\x74\x01\x69\x02"+
        "\x7a\x02\uffff\x01\x64\x01\x7a\x03\uffff\x01\x66\x01\x7a\x01\x73"+
        "\x01\x7a\x02\uffff\x01\x7a\x01\uffff\x01\x7a\x01\uffff\x01\x7a\x01"+
        "\x65\x03\uffff\x01\x64\x01\x7a\x01\uffff";
    const string DFA31_acceptS =
        "\x11\uffff\x01\x3c\x01\x3d\x01\x3e\x01\x3f\x01\x40\x01\x41\x01"+
        "\uffff\x01\x43\x01\x44\x0b\uffff\x01\x5a\x01\x5d\x01\x5e\x01\uffff"+
        "\x01\x6c\x01\x6d\x01\x70\x02\uffff\x01\x75\x2a\uffff\x01\x72\x01"+
        "\x42\x01\x47\x01\uffff\x01\x45\x01\x48\x01\uffff\x01\x46\x01\uffff"+
        "\x01\x5f\x01\uffff\x01\x59\x01\x51\x01\x60\x01\x4d\x01\x52\x01\x61"+
        "\x01\x4e\x01\x62\x01\x4f\x01\x63\x01\x50\x01\x5b\x01\x67\x01\x56"+
        "\x01\x5c\x01\x68\x01\x57\x01\x69\x01\x58\x01\uffff\x01\x6e\x01\x6f"+
        "\x01\x6a\x01\x76\x01\x74\x01\x73\x01\x71\x1b\uffff\x01\x0a\x04\uffff"+
        "\x01\x0f\x02\uffff\x01\x10\x13\uffff\x01\x64\x01\x53\x01\uffff\x01"+
        "\x65\x01\x54\x01\x4b\x01\x49\x01\x4c\x01\x4a\x01\x6b\x01\uffff\x01"+
        "\x12\x02\uffff\x01\x17\x06\uffff\x01\x0d\x15\uffff\x01\x2d\x08\uffff"+
        "\x01\x19\x0b\uffff\x01\x66\x01\x55\x01\x01\x01\uffff\x01\x02\x01"+
        "\uffff\x01\x15\x08\uffff\x01\x1f\x01\x05\x03\uffff\x01\x20\x05\uffff"+
        "\x01\x0b\x01\x25\x0c\uffff\x01\x1a\x02\uffff\x01\x1c\x01\uffff\x01"+
        "\x2a\x01\x2f\x07\uffff\x01\x16\x01\uffff\x01\x03\x01\uffff\x01\x28"+
        "\x01\uffff\x01\x29\x01\x04\x01\uffff\x01\x06\x01\uffff\x01\x22\x01"+
        "\x21\x0c\uffff\x01\x35\x01\uffff\x01\x37\x02\uffff\x01\x1b\x05\uffff"+
        "\x01\x30\x01\uffff\x01\x39\x01\x18\x05\uffff\x01\x09\x01\uffff\x01"+
        "\x24\x01\x26\x04\uffff\x01\x2c\x01\x13\x01\x14\x01\x36\x06\uffff"+
        "\x01\x34\x01\uffff\x01\x0c\x01\uffff\x01\x1e\x01\uffff\x01\x08\x01"+
        "\uffff\x01\x27\x06\uffff\x01\x31\x01\x32\x02\uffff\x01\x0e\x01\x07"+
        "\x01\x23\x04\uffff\x01\x3b\x01\x1d\x01\uffff\x01\x3a\x01\uffff\x01"+
        "\x2e\x02\uffff\x01\x33\x01\x11\x01\x2b\x02\uffff\x01\x38";
    const string DFA31_specialS =
        "\x28\uffff\x01\x00\x4e\uffff\x01\x01\u011b\uffff}>";
    static readonly string[] DFA31_transitionS = {
            "\x01\x29\x01\x2a\x02\x29\x01\x2a\x12\uffff\x01\x29\x01\x1d"+
            "\x01\x2e\x02\uffff\x01\x21\x01\x22\x01\x2e\x01\x13\x01\x14\x01"+
            "\x20\x01\x1e\x01\x19\x01\x1f\x01\x17\x01\x28\x01\x2c\x09\x2d"+
            "\x01\x27\x01\x18\x01\x1a\x01\x1c\x01\x1b\x01\x26\x1b\uffff\x01"+
            "\x15\x01\uffff\x01\x16\x01\x24\x02\uffff\x01\x0d\x01\x04\x01"+
            "\x05\x01\x06\x01\x07\x01\x03\x01\x0e\x01\uffff\x01\x08\x02\uffff"+
            "\x01\x0f\x01\uffff\x01\x01\x01\uffff\x01\x10\x01\uffff\x01\x09"+
            "\x01\x0a\x01\x02\x01\uffff\x01\x0b\x01\x0c\x03\uffff\x01\x11"+
            "\x01\x23\x01\x12\x01\x25\x21\uffff\x01\x29\u15df\uffff\x01\x29"+
            "\u018d\uffff\x01\x29\u07f1\uffff\x0b\x29\x1d\uffff\x02\x2a\x05"+
            "\uffff\x01\x29\x2f\uffff\x01\x29\u0fa0\uffff\x01\x29",
            "\x01\x31\x03\uffff\x01\x30\x0f\uffff\x01\x2f",
            "\x01\x33\x09\uffff\x01\x32\x06\uffff\x01\x34",
            "\x01\x35\x07\uffff\x01\x36\x02\uffff\x01\x39\x02\uffff\x01"+
            "\x37\x05\uffff\x01\x38",
            "\x01\x3b\x02\uffff\x01\x3a\x06\uffff\x01\x3c",
            "\x01\x3d\x06\uffff\x01\x3f\x03\uffff\x01\x40\x02\uffff\x01"+
            "\x3e",
            "\x01\x41\x09\uffff\x01\x42",
            "\x01\x43\x01\uffff\x01\x44\x09\uffff\x01\x45",
            "\x01\x46\x06\uffff\x01\x48\x01\x47",
            "\x01\x49",
            "\x01\x4b\x0b\uffff\x01\x4c\x01\x4d\x01\uffff\x01\x4a\x01\uffff"+
            "\x01\x4e",
            "\x01\x4f\x0d\uffff\x01\x50",
            "\x01\x51\x01\x52",
            "\x01\x53",
            "\x01\x54",
            "\x01\x55",
            "\x01\x56\x10\uffff\x01\x57\x02\uffff\x01\x58",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x0a\x59",
            "",
            "",
            "\x01\x5c\x01\x5b",
            "\x01\x5e\x01\x5f",
            "\x01\x61",
            "\x01\x63",
            "\x01\x65\x11\uffff\x01\x66",
            "\x01\x68\x0f\uffff\x01\x69",
            "\x01\x6b",
            "\x01\x6d",
            "\x01\x6f\x16\uffff\x01\x70",
            "\x01\x73\x3e\uffff\x01\x72",
            "\x01\x75",
            "",
            "",
            "",
            "\x0a\x7b\x01\uffff\x02\x7b\x01\uffff\x1c\x7b\x01\x78\x04\x7b"+
            "\x01\x79\x0d\x7b\x01\x77\u1fea\x7b\x02\uffff\udfd6\x7b",
            "",
            "",
            "",
            "\x01\x59\x01\uffff\x08\x7d\x0d\uffff\x01\x59\x12\uffff\x01"+
            "\x7c\x0c\uffff\x01\x59\x12\uffff\x01\x7c",
            "\x01\x59\x01\uffff\x0a\x7f\x0b\uffff\x01\x59\x1f\uffff\x01"+
            "\x59",
            "",
            "\x01\u0080",
            "\x01\u0081",
            "\x01\u0082",
            "\x01\u0085\x13\uffff\x01\u0083\x03\uffff\x01\u0084",
            "\x01\u0086\x08\uffff\x01\u0087",
            "\x01\u0088",
            "\x01\u0089",
            "\x01\u008a",
            "\x01\u008b",
            "\x01\u008c",
            "\x01\u008d",
            "\x01\u008e",
            "\x01\u008f",
            "\x01\u0090",
            "\x01\u0091\x01\u0092",
            "\x01\u0093",
            "\x01\u0094",
            "\x01\u0095",
            "\x01\u0098\x03\uffff\x01\u0096\x05\uffff\x01\u0097",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x14\x2b\x01\u0099\x05\x2b",
            "\x01\u009b",
            "\x01\u009c",
            "\x01\u009d\x03\uffff\x01\u009e",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x12\x2b\x01\u00a0\x01\u00a1"+
            "\x06\x2b",
            "\x01\u00a3",
            "\x01\u00a4",
            "\x01\u00a5",
            "\x01\u00a6",
            "\x01\u00a7",
            "\x01\u00a8",
            "\x01\u00a9",
            "\x01\u00aa",
            "\x01\u00ab\x02\uffff\x01\u00ac",
            "\x01\u00ad",
            "\x01\u00ae",
            "\x01\u00af",
            "\x01\u00b0",
            "\x01\u00b1",
            "\x01\u00b2",
            "\x01\u00b3\x05\uffff\x01\u00b4",
            "\x01\u00b5",
            "",
            "",
            "",
            "\x01\u00b6",
            "",
            "",
            "\x01\u00b9\x01\u00b8",
            "",
            "\x01\u00bb",
            "",
            "\x01\u00bd",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x0a\x7b\x01\uffff\x02\x7b\x01\uffff\u201a\x7b\x02\uffff\udfd6"+
            "\x7b",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x59\x01\uffff\x0a\x7f\x0b\uffff\x01\x59\x1f\uffff\x01"+
            "\x59",
            "\x01\u00c0",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u00c2",
            "\x01\u00c3",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u00c5",
            "\x01\u00c6",
            "\x01\u00c7",
            "\x01\u00c8",
            "\x01\u00c9",
            "\x01\u00ca",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u00cc",
            "\x01\u00cd",
            "\x01\u00ce",
            "\x01\u00cf",
            "\x01\u00d0",
            "\x01\u00d1",
            "\x01\u00d2",
            "\x01\u00d4\x01\u00d3",
            "\x01\u00d5",
            "\x01\u00d6",
            "\x01\u00d7",
            "\x01\u00d8",
            "\x01\u00d9",
            "\x01\u00da",
            "",
            "\x01\u00db",
            "\x01\u00dc",
            "\x01\u00dd",
            "\x01\u00de",
            "",
            "\x01\u00df",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x04\x2b\x01\u00e0\x15\x2b",
            "",
            "\x01\u00e2\x02\uffff\x01\u00e3",
            "\x01\u00e4",
            "\x01\u00e5",
            "\x01\u00e6",
            "\x01\u00e7",
            "\x01\u00e8",
            "\x01\u00e9",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u00eb",
            "\x01\u00ec",
            "\x01\u00ed",
            "\x01\u00ee",
            "\x01\u00ef",
            "\x01\u00f0",
            "\x01\u00f1",
            "\x01\u00f2",
            "\x01\u00f3",
            "\x01\u00f4",
            "\x01\u00f5",
            "",
            "",
            "\x01\u00f6",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u00f9",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u00fb",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u00fd",
            "\x01\u00fe",
            "\x01\u00ff",
            "\x01\u0100",
            "",
            "\x01\u0101",
            "\x01\u0102",
            "\x01\u0103",
            "\x01\u0104",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0107",
            "\x01\u0108",
            "\x01\u0109",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u010b",
            "\x01\u010c",
            "\x01\u010d",
            "\x01\u010e",
            "\x01\u010f",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0112",
            "\x01\u0113",
            "\x01\u0114",
            "\x01\u0115",
            "",
            "\x01\u0116",
            "\x01\u0117",
            "\x01\u0118",
            "\x01\u0119",
            "\x01\u011a",
            "\x01\u011b",
            "\x01\u011c",
            "\x01\u011d",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u011f",
            "\x01\u0120",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0122",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0125",
            "\x01\u0126",
            "\x01\u0127",
            "\x01\u0128",
            "",
            "",
            "",
            "\x01\u0129",
            "",
            "\x01\u012a",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x12\x2b\x01\u012b\x07\x2b",
            "\x01\u012d",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x0b\x2b\x01\u012f\x0e\x2b",
            "\x01\u0131",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0134",
            "",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0136",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0139",
            "\x01\u013a",
            "\x01\u013b",
            "\x01\u013c",
            "",
            "",
            "\x01\u013d",
            "\x01\u013e",
            "\x01\u013f",
            "\x01\u0140",
            "\x01\u0141",
            "\x01\u0142",
            "\x01\u0143",
            "\x01\u0144",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0146",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0148",
            "",
            "\x01\u0149",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u014b",
            "",
            "",
            "\x01\u014c",
            "\x01\u014d",
            "\x01\u014e",
            "\x01\u014f",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0151",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u0154",
            "",
            "\x01\u0155",
            "",
            "",
            "\x01\u0156",
            "",
            "\x01\u0157",
            "",
            "",
            "\x01\u0158",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u015a",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u015d",
            "\x01\u015e",
            "\x01\u015f",
            "\x01\u0160",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u0165",
            "\x01\u0166",
            "",
            "\x01\u0167",
            "\x01\u0168",
            "\x01\u0169",
            "\x01\u016a",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u016c",
            "",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u016e",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0170",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u0172",
            "",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0174",
            "\x01\u0175",
            "\x01\u0176",
            "",
            "",
            "",
            "",
            "\x01\u0177",
            "\x01\u0178",
            "\x01\u0179",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u017c",
            "",
            "\x01\u017d",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\u0181",
            "\x01\u0182",
            "\x01\u0183",
            "\x01\u0184",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "",
            "\x01\u0187",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "",
            "",
            "\x01\u0189",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u018b",
            "\x01\u018c",
            "",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            "\x01\u0190",
            "",
            "",
            "",
            "\x01\u0191",
            "\x01\x2b\x0b\uffff\x0a\x2b\x07\uffff\x1a\x2b\x01\uffff\x01"+
            "\x2b\x02\uffff\x01\x2b\x01\uffff\x1a\x2b",
            ""
    };

    static readonly short[] DFA31_eot = DFA.UnpackEncodedString(DFA31_eotS);
    static readonly short[] DFA31_eof = DFA.UnpackEncodedString(DFA31_eofS);
    static readonly char[] DFA31_min = DFA.UnpackEncodedStringToUnsignedChars(DFA31_minS);
    static readonly char[] DFA31_max = DFA.UnpackEncodedStringToUnsignedChars(DFA31_maxS);
    static readonly short[] DFA31_accept = DFA.UnpackEncodedString(DFA31_acceptS);
    static readonly short[] DFA31_special = DFA.UnpackEncodedString(DFA31_specialS);
    static readonly short[][] DFA31_transition = DFA.UnpackEncodedStringArray(DFA31_transitionS);

    protected class DFA31 : DFA
    {
        public DFA31(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 31;
            this.eot = DFA31_eot;
            this.eof = DFA31_eof;
            this.min = DFA31_min;
            this.max = DFA31_max;
            this.accept = DFA31_accept;
            this.special = DFA31_special;
            this.transition = DFA31_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( NULL | TRUE | FALSE | BREAK | CASE | CATCH | CONTINUE | DEFAULT | DELETE | DO | ELSE | FINALLY | FOR | FUNCTION | IF | IN | INSTANCEOF | NEW | RETURN | SWITCH | THIS | THROW | TRY | TYPEOF | VAR | VOID | WHILE | WITH | ABSTRACT | BOOLEAN | BYTE | CHAR | CLASS | CONST | DEBUGGER | DOUBLE | ENUM | EXPORT | EXTENDS | FINAL | FLOAT | GOTO | IMPLEMENTS | IMPORT | INT | INTERFACE | LONG | NATIVE | PACKAGE | PRIVATE | PROTECTED | PUBLIC | SHORT | STATIC | SUPER | SYNCHRONIZED | THROWS | TRANSIENT | VOLATILE | LBRACE | RBRACE | LPAREN | RPAREN | LBRACK | RBRACK | DOT | SEMIC | COMMA | LT | GT | LTE | GTE | EQ | NEQ | SAME | NSAME | ADD | SUB | MUL | MOD | INC | DEC | SHL | SHR | SHU | AND | OR | XOR | NOT | INV | LAND | LOR | QUE | COLON | ASSIGN | ADDASS | SUBASS | MULASS | MODASS | SHLASS | SHRASS | SHUASS | ANDASS | ORASS | XORASS | DIV | DIVASS | WhiteSpace | EOL | MultiLineComment | SingleLineComment | Identifier | DecimalIntegerLiteral | DecimalFloatingPointLiteral | OctalIntegerLiteral | HexIntegerLiteral | StringLiteral | RegularExpressionLiteral );"; }
        }

    }


    protected internal int DFA31_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            IIntStream input = _input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA31_40 = input.LA(1);

                   	 
                   	int index31_40 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( (LA31_40 == '=') ) { s = 119; }

                   	else if ( (LA31_40 == '*') ) { s = 120; }

                   	else if ( (LA31_40 == '/') ) { s = 121; }

                   	else if ( ((LA31_40 >= '\u0000' && LA31_40 <= '\t') || (LA31_40 >= '\u000B' && LA31_40 <= '\f') || (LA31_40 >= '\u000E' && LA31_40 <= ')') || (LA31_40 >= '+' && LA31_40 <= '.') || (LA31_40 >= '0' && LA31_40 <= '<') || (LA31_40 >= '>' && LA31_40 <= '\u2027') || (LA31_40 >= '\u202A' && LA31_40 <= '\uFFFF')) && (( areRegularExpressionsEnabled() )) ) { s = 123; }

                   	else s = 122;

                   	 
                   	input.Seek(index31_40);
                   	if ( s >= 0 ) return s;
                   	break;
               	case 1 : 
                   	int LA31_119 = input.LA(1);

                   	 
                   	int index31_119 = input.Index();
                   	input.Rewind();
                   	s = -1;
                   	if ( ((LA31_119 >= '\u0000' && LA31_119 <= '\t') || (LA31_119 >= '\u000B' && LA31_119 <= '\f') || (LA31_119 >= '\u000E' && LA31_119 <= '\u2027') || (LA31_119 >= '\u202A' && LA31_119 <= '\uFFFF')) && (( areRegularExpressionsEnabled() )) ) { s = 123; }

                   	else s = 191;

                   	 
                   	input.Seek(index31_119);
                   	if ( s >= 0 ) return s;
                   	break;
        }
        NoViableAltException nvae =
            new NoViableAltException(dfa.Description, 31, _s, input);
        dfa.Error(nvae);
        throw nvae;
    }
 
    
}
}