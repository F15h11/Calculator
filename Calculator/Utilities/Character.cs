namespace Calculator.Utilities {
    public static class Character {
        public static bool IsDigit(char c) {
            if(c == '0' | c == '1' | c == '2' | c == '3' | c == '4' | c == '5' | c == '6' | c == '7' | c == '8' | c == '9') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsLetter(char c) {
            if(c == 'a' | c == 'b' | c == 'c' | c == 'd' | c == 'e' | c == 'f' | c == 'g' | c == 'h' | c == 'i' | c == 'j' | c == 'k' | c == 'l' | c == 'm' | c == 'n' | c == 'o' | c == 'p' | c == 'q' | c == 'r' | c == 's' | c == 't' | c == 'u' | c == 'v' | c == 'w' | c == 'x' | c == 'y' | c == 'z') {
                return true;
            }
            else if(c == 'A' | c == 'B' | c == 'C' | c == 'D' | c == 'E' | c == 'F' | c == 'G' | c == 'H' | c == 'I' | c == 'J' | c == 'K' | c == 'L' | c == 'M' | c == 'N' | c == 'O' | c == 'P' | c == 'Q' | c == 'R' | c == 'S' | c == 'T' | c == 'U' | c == 'V' | c == 'W' | c == 'X' | c == 'Y' | c == 'Z') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsUmlaut(char c) {
            if(c == 'Ä' | c == 'ä' | c == 'Ö' | c == 'ö' | c == 'Ü' | c == 'ü') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsOperator(char c) {
            if(c == '+' | c == '-' | c == '*' | c == '/') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsPlus(char c) {
            if(c == '+') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsMinus(char c) {
            if(c == '-') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsMultiply(char c) {
            if(c == '*') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsDivide(char c) {
            if(c == '/') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsModulo(char c) {
            if(c == '%') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsMultiplicative(char c) {
            if(c == '*' | c == '/') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsAdditive(char c) {
            if(c == '+' | c == '-') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsComma(char c) {
            if(c == ',') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsDot(char c) {
            if(c == '.') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsSpace(char c) {
            if(c == ' ') {
                return true;
            } else {
                return false;
            }
        }
        public static bool IsBlacklisted(char[] cBlacklist, char c) {
            for(int i = 0; i < cBlacklist.Length; i++) {
                if(c == cBlacklist[i]) {
                    return true;
                }
            }
            return false;
        }
        public static char[] Swap(char[] cArr, char newChar, int indexStart, int indexStop) {
            if(indexStart >= 0 && indexStart <= cArr.Length - 1 && indexStop >= 0 && indexStop <= cArr.Length - 1) {
                for(int i = indexStart; i <= indexStop; i++) {
                    cArr[i] = newChar;
                }
                return cArr;
            } else {
                return cArr;
            }
        }
        public static bool IsBlacklisted(char[][] blacklist, char[]? whitelist, char c) {
            foreach(char[] item in blacklist) {
                for(int i = 0; i < item.Length; i++) {
                    if(c == item[i]) {
                        if(whitelist is not null) {
                            for(int j = 0; j < whitelist.Length; j++) {
                                if(c == whitelist[j]) {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
