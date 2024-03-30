using Calculator;
using Calculator.Utilities;

public sealed class Converter {
    private readonly string? input;
    private char[]? expression;
    private List<Operand> operands;
    private List<char> operators;
    private char[][] blacklist;
    private char[]? blacklistException;
    private bool isSubTerm;
    private readonly char[] LETTERSLC = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
    private readonly char[] LETTERSUC = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
    private readonly char[] UMLAUTE = {'Ä', 'ä', 'Ö', 'ö', 'Ü', 'ü'};
    private readonly char[] SYMBOLS = {'°', '"', '§' , '$', '%', '&', '{', '}', '[', ']', '?', 'ß', '´', '`', '~', '\'', '#', ';', '<', '>', '|', '²', '³', '@', '€', 'µ'}; //'_' is used for parseString() to ignore the current char

    public Converter(string input) {
        this.input = input + ' ';
        this.expression = this.input.ToCharArray();
        this.operands = new List<Operand>();
        this.operators = new List<char>();
        this.blacklist = [LETTERSLC, LETTERSUC, UMLAUTE, SYMBOLS];
        this.isSubTerm = false;
    }
    public Converter(string input, char[] allowedVariableNames) {
        this.input = input + ' ';
        this.expression = this.input.ToCharArray();
        this.operands = new List<Operand>();
        this.operators = new List<char>();
        this.blacklistException = allowedVariableNames;
        this.blacklist = [LETTERSLC, LETTERSUC, UMLAUTE, SYMBOLS];
        this.isSubTerm = false;
    }
    public Converter(string input, bool isSubTerm) {
        this.input = input + ' ';
        this.expression = this.input.ToCharArray();
        this.operands = new List<Operand>();
        this.operators = new List<char>();
        this.blacklist = [LETTERSLC, LETTERSUC, UMLAUTE, SYMBOLS];
        this.blacklistException = null;
        this.isSubTerm = isSubTerm;
    }
    public Converter(string input, char[] allowedVariableNames, bool isSubTerm) {
        this.input = input + ' ';
        this.expression = this.input.ToCharArray();
        this.operands = new List<Operand>();
        this.operators = new List<char>();
        this.blacklistException = allowedVariableNames;
        this.blacklist = [LETTERSLC, LETTERSUC, UMLAUTE, SYMBOLS];
        this.isSubTerm = isSubTerm;
    }
    public (MathTerm? MathTerm, bool Success, string? ErrorMessage) GetTerm() {
        var parseString = ParseString();
        if(!parseString.Success) {
            return (null, false, parseString.ErrorMessage);
        }
        return (new MathTerm(operands.ToArray(), operators.ToArray()), true, null);
    }
    //Never forget to reset da goddamn varables!!!
    private (bool Success, string ErrorMessage) ParseString() {
        string currentOperand = "";
        bool currentOperandIsVariable = false;
        Operand? tempOperand = null;
        bool getOperand = true;           //True = program searches operants //False = program searches operators
        bool atFirstChar = true;          //True = index i is at first char of possible operant or less //False = index is behind the first char of a possible operant
                                          //Switches when index i is at a char thats either a sign (-) or a number
        bool operandRequired = true;      //True = operant is required //False = ..its obvious
                                          //Switches from true to false when a operant is added
                                          //If its true after the for loop this method will return null
        bool digitRequired = false;      //if next char must be a digit
        bool containsDotYet = false;    //Switches true when a dot is added to currentOperant and false when currentOperant is beeing resetted
        bool nextNumberIsExponent = false; 

        if(expression.Length == 1) {
            return (false, "Input string was empty");
        }
        for(int i = 0; i < expression.Length; i++) {
            if(Character.IsBlacklisted(this.blacklist,this.blacklistException, expression[i])) {
                return (false, "Invalid character found");
            }
            if(getOperand) {
                if(atFirstChar) {
                    if(Character.IsDigit(this.expression[i])) {     //If current char is a digit
                        currentOperand += this.expression[i];
                        atFirstChar = false;
                    } else if(Character.IsMinus(this.expression[i])) {       //Minus will be used as sign if at first char of a operant
                        currentOperand += this.expression[i];
                        atFirstChar = false;
                        digitRequired = true;
                    } else if(this.expression[i] == '(') {      //If current char is '(' the program searches for a subterm
                        if(nextNumberIsExponent) {
                            return (false, "Exponent can't be a subterm");
                        } else {
                            var seperateSubTerm = Seperate.GetNearestBracket(input, i);
                            if(!seperateSubTerm.ClosedBracketFound) {
                                return (false, "Expected bracket not found");
                            }
                            var converter = new Converter(seperateSubTerm.Output, this.blacklistException, true).GetTerm();
                            if(converter.Success) {
                                tempOperand = new Operand((MathTerm)converter.MathTerm, false);
                                this.expression = Character.Swap(this.expression, '_', seperateSubTerm.IndexStart, seperateSubTerm.IndexStop);
                            } else {
                                return (false, converter.ErrorMessage);
                            }
                            currentOperand = "";
                            atFirstChar = false;
                            operandRequired = false;
                        }
                    } else if(Character.IsOperator(this.expression[i])) {     //If current char is an operator
                        return (false, "Unexpected operator found");
                    } else if(Character.IsDot(this.expression[i])) {    //If current char is dot
                        return (false, "Unexpected dot found");
                    } else if(Character.IsComma(this.expression[i])) {  //If current char is comma
                        return (false, "Unexpected comma found");
                    } else if(this.expression[i] == ')') {      //Closing bracket will be deletet if subterm detected so it is invalid in every case if found here
                        return (false, "Unexpected bracket found");
                    } else if(this.expression[i] == '^') {      //If current char is exponent operator
                        return (false, "Operator '^' isn't valid without a base value");
                    } else if(this.expression[i] != ' ' && expression[i] != '_') {  //If current char is ignored/break
                        if(nextNumberIsExponent) {
                            return (false, "Exponent cant be a variable");
                        } else {
                            tempOperand = new Operand(expression[i], false);
                            currentOperand = "";
                            atFirstChar = false;
                        }
                    }
                } else if(!atFirstChar) {      //Checks for things that shouldnt be in the middle of a number and dots
                    if(Character.IsDigit(this.expression[i])) {   //Add number if char is digit
                        if(digitRequired) {
                            digitRequired = false;
                        }
                        currentOperand += this.expression[i];
                    } else if(Character.IsOperator(this.expression[i])) {
                        if(digitRequired) {
                            return (false, "Unexpected Operator found");
                        } else {
                            if(tempOperand is not null) {
                                Operand temp = (Operand)tempOperand;
                                if(nextNumberIsExponent) {
                                    try {
                                        temp.Exponent = Convert.ToInt32(currentOperand);
                                    } catch {
                                        return (false, "Exponent must be an integer");
                                    }
                                    this.operands.Add(temp);
                                    nextNumberIsExponent = false;
                                } else {
                                    this.operands.Add(temp);
                                }
                                tempOperand = null;
                            } else {
                                try {
                                    this.operands.Add(new Operand(Convert.ToDouble(currentOperand)));
                                } catch {
                                    return (false, "Current operand was invalid");
                                }
                            }
                            currentOperand = "";
                            getOperand = false;
                            atFirstChar = true;
                            operandRequired = false;
                            containsDotYet = false;
                        }
                    } else if(this.expression[i] == '.') {
                        if(containsDotYet) {
                            return (false, "Operand has to many dots/commas");
                        } else if(digitRequired) {
                            return (false, "Required digit not found");
                        } else {
                            containsDotYet = true;
                            digitRequired = true;
                            currentOperand += this.expression[i];
                        }
                    } else if(this.expression[i] == ',') {
                        if(containsDotYet) {
                            return (false, "Operand has to many dots/commas");
                        } else if(digitRequired) {
                            return (false, "Required digit not found");
                        } else {
                            containsDotYet = true;
                            digitRequired = true;
                            currentOperand += '.';
                        }
                    } else if(this.expression[i] == ' ') {
                        if(nextNumberIsExponent) {
                            if(tempOperand is null) {
                                return (false, "Exponent had no Base");
                            } else {
                                Operand temp = (Operand)tempOperand;
                                try {
                                    temp.Exponent = Convert.ToInt32(currentOperand);
                                } catch {
                                    return (false, "Exponent must be an integer");
                                }
                                this.operands.Add(temp);
                                tempOperand = null;
                            }
                            currentOperand = "";
                            nextNumberIsExponent = false;
                        } else {
                            if(digitRequired) {
                                return (false, "Required digit not found");
                            } else if(tempOperand is not null) {
                                this.operands.Add((Operand)tempOperand);
                                tempOperand = null;
                            } else {
                                try {
                                    this.operands.Add(new Operand(Convert.ToDouble(currentOperand)));
                                } catch {
                                    return (false, "Current operant was invalid");
                                }
                            }
                        }
                        //Resetting Variables
                        currentOperand = "";
                        getOperand = false;
                        atFirstChar = true;
                        operandRequired = false;
                        containsDotYet = false;
                    } else if(this.expression[i] == '(') {
                        bool isNegative;
                        if(i > 0) {
                            isNegative = (expression[i - 1] == '-') ? true : false;
                        } else {
                            isNegative = false;
                        }
                        double? factor = null;
                        if(currentOperand.Length > 0) {
                            try {
                                factor = Convert.ToDouble(currentOperand);
                            } catch {
                                return (false, "Factor wasn't valid");
                            }
                        }
                        var seperateSubTerm = Seperate.GetNearestBracket(input, i);
                        if(!seperateSubTerm.ClosedBracketFound) {
                            return (false, "Expected bracket not found");
                        }
                        var converter = new Converter(seperateSubTerm.Output, this.blacklistException, true).GetTerm();
                        if(converter.Success) {
                            tempOperand = new Operand((MathTerm)converter.MathTerm, isNegative, factor);
                            this.expression = Character.Swap(this.expression, '_', seperateSubTerm.IndexStart, seperateSubTerm.IndexStop);
                        } else {
                            return (false, converter.ErrorMessage);
                        }
                        //Resetting variables
                        currentOperand = "";
                        operandRequired = false;
                        digitRequired = false;
                        containsDotYet = false;
                    } else if(expression[i] == '^') {
                        if(tempOperand is null) {
                            try {
                                tempOperand = new Operand(Convert.ToDouble(currentOperand));
                            } catch {
                                return (false, "Current operand was invalid");
                            }
                            //Resetting variables
                            currentOperand = "";
                            atFirstChar = true;
                            operandRequired = false;
                            digitRequired = true;
                            containsDotYet = false;
                        }
                        nextNumberIsExponent = true;
                    } else if (expression[i] != ' ' && expression[i] != '_') {
                        if(currentOperand.Length == 1 && currentOperand[0] == '-') {
                            tempOperand = new Operand(this.expression[i], true);
                        } else {
                            try {
                                tempOperand = new Operand(this.expression[i], Convert.ToDouble(currentOperand));
                            } catch {
                                return (false, "Factor was invalid");
                            }
                        }
                        currentOperand = "";
                        operandRequired = false;
                        containsDotYet = false;
                    }
                }
            }  
            if(!getOperand) {
                if(Character.IsOperator(this.expression[i])) {
                    operators.Add(this.expression[i]);
                    //Resetting Variables
                    getOperand = true;
                    atFirstChar = true;
                    operandRequired = true;
                } else if(Character.IsDigit(this.expression[i])) {
                    return (false, $"Unexpected digit found ({i})");
                } else if(Character.IsBlacklisted(this.blacklist, null, this.expression[i]) || this.expression[i] == '^') {
                    return (false, "Invalid token found");
                }
            }
        }
        if(operands.Count() == 0) {
            return (false, "Input didnt contain any operands");
        } else if(operandRequired) {
            return (false, "Required operant not found");
        } else {
            return (true, null);
        }
    }
    public override string ToString() {
        return "";
    }
}
