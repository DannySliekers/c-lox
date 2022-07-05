using lox;

string[] arguments = Environment.GetCommandLineArgs();
if (arguments.Length > 1)
{
    Console.WriteLine("Usage: clox [script]");
    Environment.Exit(64);
} else if (arguments.Length == 1)
{
    Lox.runFile(arguments[0]);
} else
{
    Lox.runPrompt();
}