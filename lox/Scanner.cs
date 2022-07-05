using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lox
{
    public class Scanner
    {
        private readonly string _source;
        private readonly IList<Token>? _tokens;
        private int start = 0;
        private int current = 0;
        private int line = 1;

        public Scanner(string source)
        {
            _source = source;
        }

        public IList<Token>? ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            _tokens?.Add(new Token(TokenType.EOF, "", null, line));
            return _tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(': AddToken(TokenType.LEFT_PAREN); break;
                case ')': AddToken(TokenType.RIGHT_PAREN); break;
                case '{': AddToken(TokenType.LEFT_BRACE); break;
                case '}': AddToken(TokenType.RIGHT_BRACE); break;
                case ',': AddToken(TokenType.COMMA); break;
                case '.': AddToken(TokenType.DOT); break;
                case '-': AddToken(TokenType.MINUS); break;
                case '+': AddToken(TokenType.PLUS); break;
                case ';': AddToken(TokenType.SEMICOLON); break;
                case '*': AddToken(TokenType.STAR); break;
                case '!':
                    AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                default:
                    Lox.error(line, "Unexpected character");
                    break;
            }
        }

        private bool Match(char expected)
        {
            if (IsAtEnd()) return false;
            if (_source[current] != expected) return false;

            current++;
            return true;
        }

        private bool IsAtEnd()
        {
            return current >= _source.Length;
        }

        private char Advance()
        {
            return _source[current++];
        }
        
        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, Object? literal)
        {
            string text = _source.Substring(start, current);
            _tokens?.Add(new Token(type, text, literal, line));
        }

    }
}
