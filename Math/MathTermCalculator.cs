using System;

namespace Calculator.Math {
    public class MathTermCalculator {
        private char[] _inputOperators;
        private double[] _inputOperants;
        private List<double> operants;
        private List<char> operators;
        private bool isValid;
        private string? exceptionMessage = "Error: ";    //Message to tell why result is null if its null

        public MathTerm Term;
        public double? Result;
        public MathTermCalculator(MathTerm input) {
            Term = input;
            _inputOperants = input.Operants;
            _inputOperators = input.Operators;
            operants = new List<double>();
            operators = new List<char>();

            if(hasMultiplicatives()) {      
                if(solveMultiplicatives()) {
                    Result = solveAdditives();
                }
                if(Result != null) {
                    isValid = true;
                }
                else {
                    isValid = false;
                }
            }
            else {
                operants = _inputOperants.ToList();
                operators = _inputOperators.ToList();
                Result = solveAdditives();
            }
            if(Result != null) {
                isValid = true;
            }
            else {
                isValid = false;
            }
        }
        bool hasMultiplicatives() {
            for(int op = 0; op < _inputOperators.Length; op++) {
                if(Character.IsMultiplicative(_inputOperators[op])) {
                    return true;
                }
            }
            return false;
        }
        bool solveMultiplicatives() {       //TODO add numbers[0]
            double temp = 0;        //Temp operant if multiple multiplicative operators follow each other (if the Term part is 1 * 2 * 3, 1 * 2 -> temp * 3 and so on) 
            for(int i = 0; i < _inputOperators.Length; i++) {
                if(_inputOperators[i] == '+') {
                    operators.Add('+');
                    if(i > 0) {
                        if(Character.IsAdditive(_inputOperators[i - 1]))       //Only adds the left operand if its not part of a multiplicative partial operation
                            operants.Add(_inputOperants[i]);
                    }
                    else {
                        operants.Add(_inputOperants[i]);
                    }

                    if(i == _inputOperators.Length - 1) {
                        operants.Add(_inputOperants[i + 1]);
                    }
                }
                else if(_inputOperators[i] == '-') {
                    operators.Add('-');
                    if(i > 0) {
                        if(Character.IsAdditive(_inputOperators[i - 1]))       //Only adds the left operand if its not part of a multiplicative partial operation
                            operants.Add(_inputOperants[i]);
                    }
                    else {
                        operants.Add(_inputOperants[i]);
                    }
                    if(i == _inputOperators.Length - 1) {
                        operants.Add(_inputOperants[i + 1]);
                    }
                }
                else if(_inputOperators[i] == '*') {
                    if(i > 0) {
                        if(Character.IsMultiplicative(_inputOperators[i - 1])) {
                            operants.Remove(operants[operants.Count() - 1]);
                            operants.Add(temp * _inputOperants[i + 1]);
                            temp *= _inputOperants[i + 1];
                        }
                        else {
                            operants.Add(_inputOperants[i] * _inputOperants[i + 1]);
                            temp = _inputOperants[i] * _inputOperants[i + 1];
                        }
                    }
                    else {
                        operants.Add(_inputOperants[i] * _inputOperants[i + 1]);
                        temp = _inputOperants[i] * _inputOperants[i + 1];
                    }
                }
                else if(_inputOperators[i] == '/') {
                    if(_inputOperants[i + 1] == 0) {
                        exceptionMessage += "Attempt to devide by 0";
                        return false;
                    }
                    if(i > 0) {
                        if(Character.IsMultiplicative(_inputOperators[i - 1])) {
                            operants.Remove(operants[operants.Count() - 1]);
                            operants.Add(temp / _inputOperants[i + 1]);
                            temp /= _inputOperants[i + 1];
                        }
                        else {
                            operants.Add(_inputOperants[i] / _inputOperants[i + 1]);
                            temp = _inputOperants[i] / _inputOperants[i + 1];
                        }
                    }
                    else {
                        operants.Add(_inputOperants[i] / _inputOperants[i + 1]);
                        temp = _inputOperants[i] / _inputOperants[i + 1];
                    }
                }
            }
            return true;
        }
        double? solveAdditives() {
            if(operants.Count() > 1 & operators.Count() > 0) {
                double? result = operants[0];
                for(int i = 0;i < operators.Count();i++) {
                    if(operators[i] == '+') {
                        result += operants[i + 1];
                    }
                    if(operators[i] == '-') {
                        result -= operants[i + 1];
                    }
                }
                return result;
            }
            else if(operants.Count() == 1) {
                return operants[0];
            }
            else {
                exceptionMessage += "No operants found !!Dev needs to fix!!";
                return null;
            }
        }
        public override string ToString() {
            if(!isValid) {
                return exceptionMessage;
            }
            else {
                if(Term.IsValid) {
                    return $"{Term.ToString()} = {Result}";
                }
                else {
                    return Term.ToString();
                }
            }
        }
    }
}
