﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lox
{
    public class Token
    {
        private readonly TokenType _type;
        private readonly string _lexeme;
        private readonly Object? _literal;
        private readonly int _line;

        public Token(TokenType type, string lexeme, Object? literal, int line)
        {
            _type = type;
            _lexeme = lexeme;
            _literal = literal;
            _line = line;
        }

        public override string ToString()
        {
            return _type + " " + _lexeme + " " + _literal;
        }
    }
}
