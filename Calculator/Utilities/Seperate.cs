using System;

static class Seperate {
    //Seperates a string and the part in the first found brackets
    public static (string? Output, int IndexStart, int IndexStop, bool ClosedBracketFound) GetNearestBracket(string str, int index) {
        string output = "";
        bool bracketFound = false;
        bool inSubBracket = false;
        int indexStart = index;
        int indexStop = str.Length - 1;

        int subBracketCounter = 0;
        for(int i = index; i < str.Length; i++) {
            if(str[i] == '(') {
                if(bracketFound) {
                    subBracketCounter++;
                    output += str[i];
                } else {
                    bracketFound = true;
                }
            } else if(str[i] == ')') {
                if(bracketFound) {
                    if(subBracketCounter > 0) {
                        subBracketCounter--;
                        output += str[i];
                    } else {
                        indexStop = i;
                        return (output, indexStart, indexStop, true);
                    }
                } else {
                    return("Unexpected closing bracket found", 0, 0, false);
                }
            } else if(bracketFound) {
                output += str[i];
            }
        }
        Console.WriteLine("-------------------------------------------");

        return (null, 0, 0, false);
    }
    private static int? countBrackets(string str) {
        int openBracketsFound = 0;
        int closedBracketsFound = 0;
        for(int i = 0; i < str.Length; i++) {
            if(str[i] == '(') {
                openBracketsFound++;
            } else if(str[i] == ')') {
                closedBracketsFound++;
            }
        }
        if(openBracketsFound == closedBracketsFound) {
            return openBracketsFound;
        } else {
            return null;
        }
    }
}