string[] arguments = Environment.GetCommandLineArgs();
if(arguments.Length > 1)
{
    Console.WriteLine("Usage: clox [script]");
    Environment.Exit(64);
} else if(arguments.Length == 1)
{
    runFile(arguments[0]);
} else
{
    runPrompt();
}

static void runFile(string path)
{
    byte[] bytes = System.IO.File.ReadAllBytes(path);
    run(System.Text.Encoding.UTF8.GetString(bytes));
    if (hadError)
    {
        Environment.Exit(65);
    }
}

static void runPrompt()
{
    for(; ; )
    {
        Console.WriteLine("> ");
        string? line = Console.ReadLine();
        if (String.IsNullOrEmpty(line))
        {
            break;
        }
        run(line);
        hadError = false;
    }
}

static void run(string source)
{
    Scanner scanner = new Scanner(source);
    List<Token> tokens = scanner.scanTokens();
    foreach(Token token in tokens)
    {
        Console.WriteLine(token);
    }
}

static void error(int line, string message)
{
    report(line, "", message);
}

static void report(int line, string where, string message)
{
    ConsoleColor originalColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine("[line {line}] Error {where}: {message}", line, where, message);
    Console.ForegroundColor = originalColor;
    hadError = true;
}

public partial class Program
{
    static bool hadError = false;
}