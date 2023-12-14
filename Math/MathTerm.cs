namespace Calculator.Math {
    public struct MathTerm {
        public char[] Operators;
        public double[] Operants;
        public bool IsValid;

        private string exceptionMessage = "Error: ";
        public MathTerm(char[] operators, double[] operants) {
            if(isValid(operators, operants)) {
                Operators = operators;
                Operants = operants;
                IsValid = true;
            }
            else {
                Operators = new char[] { '+' };
                Operants = new double[] { 1, 1 };
                exceptionMessage += "Invalid values given to the constructor";
                IsValid = false;
            }
        }
        public MathTerm(string str) {
            Operators = new char[] { '+' };
            Operants = new double[] { 1, 1 };
            exceptionMessage += str;
            IsValid = false;
        }
        public override string ToString() {

            if(IsValid) {
                if(Operators.Length > 0 & Operants.Length > 0) {
                    string str = "";
                    str += Operants[0].ToString();
                    for(int i = 0; i < Operators.Length; i++) {
                        str += $" {Operators[i]} {Operants[i + 1]}";
                    }
                    return str;
                }
                else if(Operants.Length == 1) {
                    return $"{Operants[0]}";
                }
                else {
                    return exceptionMessage += "Unknown";
                }
            }       
            else {
                return exceptionMessage;
            }
        }
        static bool isValid(char[] operators, double[] operants) {
            if(operatorsValid(operators)) {
                if(operants.Length > 0) {
                    if(operators.Length == operants.Length - 1) {                       
                        return true;
                    }
                    else
                        return false;
                }
                else {
                    return false;
                }
            }
            else
                return false;
        }
        static bool operatorsValid(char[] operators) {
            foreach(char c in operators) {
                if(!Character.IsOperator(c)) {
                    return false;
                }
            }
            return true;
        }
    }
}
