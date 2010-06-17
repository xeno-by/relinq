@echo off

echo Cleaning up the environment
echo ============================
echo.
del EcmaScriptV3Lexer.cs
del EcmaScriptV3Parser.cs
del EcmaScriptV3.tokens

echo.
echo Compiling lexer and parser
echo ============================
echo.
java org.antlr.Tool EcmaScriptV3.g

echo.
pause