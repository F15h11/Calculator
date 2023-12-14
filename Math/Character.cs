using System;

//TODO: Add modulo (%) to operators

namespace Calculator.Math {
    public static class Character {
        public static bool IsDigit(char c) {
            if(c == '0' | c == '1' | c == '2' | c == '3' | c == '4' | c == '5' | c == '6' | c == '7' | c == '8' | c == '9')
                return true;
            else
                return false;
        }
        public static bool IsLetter(char c) {
            if(c == 'a' | c == 'b' | c == 'c' | c == 'd' | c == 'e' | c == 'f' | c == 'g' | c == 'h' | c == 'i' | c == 'j' | c == 'k' | c == 'l' | c == 'm' | c == 'n' | c == 'o' | c == 'p' | c == 'q' | c == 'r' | c == 's' | c == 't' | c == 'u' | c == 'v' | c == 'w' | c == 'x' | c == 'y' | c == 'z')
                return true;
            else if(c == 'A' | c == 'B' | c == 'C' | c == 'D' | c == 'E' | c == 'F' | c == 'G' | c == 'H' | c == 'I' | c == 'J' | c == 'K' | c == 'L' | c == 'M' | c == 'N' | c == 'O' | c == 'P' | c == 'Q' | c == 'R' | c == 'S' | c == 'T' | c == 'U' | c == 'V' | c == 'W' | c == 'X' | c == 'Y' | c == 'Z')
                return true;
            else 
                return false;
        }
        public static bool IsBracket(char c) {
            if(c == '(' | c == ')')
                return true;
            else
                return false;
        }
        public static bool IsOperator(char c) {
            if(c == '+' | c == '-' | c == '*' | c == '/')
                return true;
            else
                return false;
        }
        public static bool IsPlus(char c) {
            if(c == '+') 
                return true;
            else
                return false;
        }
        public static bool IsMinus(char c) {
            if(c == '-')
                return true;
            else
                return false;
        }
        public static bool IsMultiply(char c) {
            if(c == '*')
                return true;
            else 
                return false;
        }
        public static bool IsDivide(char c) {
            if(c == '/')
                return true;
            else
                return false;
        }
        public static bool IsMultiplicative(char c) {
            if(c == '*' | c == '/')
                return true;
            else
                return false;
        }
        public static bool IsAdditive(char c) {
            if(c == '+' | c == '-')
                return true;
            else
                return false;
        }
        public static bool IsComma(char c) {
            if(c == ',')
                return true;
            else
                return false;
        }
        public static bool IsSpace(char c) {
            if(c == ' ')
                return true;
            else
                return false;
        }
    }
}
