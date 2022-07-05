using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lox
{
    public static class Lox
    {
        private static bool hadError = false;
        public static void runFile(string path)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            run(System.Text.Encoding.UTF8.GetString(bytes));
            if (hadError)
            {
                Environment.Exit(65);
            }
        }

        public static void runPrompt()
        {
            for (; ; )
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

        private static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            IList<Token>? tokens = scanner.ScanTokens();
            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        public static void error(int line, string message)
        {
            report(line, "", message);
        }

        private static void report(int line, string where, string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("[line {line}] Error {where}: {message}", line, where, message);
            Console.ForegroundColor = originalColor;
            hadError = true;
        }
    }
}
