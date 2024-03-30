using Calculator;
using Calculator.Utilities;

public sealed class MathTermCalculator {
    public MathTerm MathTerm;
    private Operand[] inputOperands;
    private char[] _inputOperators;
    private List<double> operants;
    private List<char> operators;
    public MathTermCalculator(MathTerm input) {
        this.MathTerm = input;
        inputOperands = input.Operands;
        _inputOperators = input.Operators;
    }
    public (bool Success, double? Result, string? ErrorMessage) Solve() {
        this.operants = new List<double>();
        this.operators = new List<char>();
        if(this.inputOperands is null) {
            return (false, null, "Operand[] inputOperands was null");
        }
        //Solve subterms
        foreach(int index in GetIndexesOfSubTerms(this.MathTerm)) {
            var calculator = new MathTermCalculator((MathTerm)this.inputOperands[index].MathTerm).Solve();
            if(!calculator.Success) {
                return (false, null, calculator.ErrorMessage);
            } else {
                if((bool)this.inputOperands[index].IsNegative) {
                    this.inputOperands[index].Value = -calculator.Result;
                } else {
                    this.inputOperands[index].Value = calculator.Result;
                }
                this.inputOperands[index].IsSolved = true;
            }
        }
        //Solve operand that have a factor
        foreach(int index in GetIndexesOfExponentOperands(this.MathTerm)) {
            this.inputOperands[index].Value = SolveExponents((double)this.inputOperands[index].Value, (int)this.inputOperands[index].Exponent);
            //this.inputOperands[index].Exponent = null;
        }
        foreach(int index in GetIndexesOfFactorOperands(this.MathTerm)) {
            var calculator = new MathTermCalculator(new MathTerm([new Operand((double)this.inputOperands[index].Value), new Operand((double)this.inputOperands[index].Factor)], ['*'])).Solve();
            if(!calculator.Success) {
                return (false, null, calculator.ErrorMessage);
            } else {
                this.inputOperands[index].Value = calculator.Result;
            }
        } 
        if(hasMultiplicatives(_inputOperators)) {
            var solve = solveMultiplicatives();
            if(solve.Success) {
                return (true, SolveAdditives(this.operants, this.operators), null);
            } else {
                return (false, null, solve.ErrorMessage);
            }
        } else {
            return (true, SolveAdditives(inputOperands, _inputOperators), null);
        }
    }
    private (bool Success, string? ErrorMessage) solveMultiplicatives() {       
        double temp = 0;        //Temp operant if multiple multiplicative operators follow each other (if the Term part is 1 * 2 * 3, 1 * 2 -> temp * 3 and so on) 
        for(int i = 0; i < _inputOperators.Length; i++) {
            if(_inputOperators[i] == '+') {
                operators.Add('+');
                if(i > 0) {
                    if(Character.IsAdditive(_inputOperators[i - 1]))       //Only adds the left operand if its not part of a multiplicative partial operation
                        operants.Add((double)inputOperands[i].Value);
                } else {
                    operants.Add((double)inputOperands[i].Value);
                }
                if(i == _inputOperators.Length - 1) {
                    operants.Add((double)inputOperands[i + 1].Value);
                }
            }
            else if(_inputOperators[i] == '-') {
                operators.Add('-');
                if(i > 0) {
                    if(Character.IsAdditive(_inputOperators[i - 1]))       //Only adds the left operand if its not part of a multiplicative partial operation
                        operants.Add((double)inputOperands[i].Value);
                } else {
                    operants.Add((double)inputOperands[i].Value);
                }
                if(i == _inputOperators.Length - 1) {
                    operants.Add((double)inputOperands[i + 1].Value);
                }
            }
            else if(_inputOperators[i] == '*') {
                if(i > 0) {
                    if(Character.IsMultiplicative(_inputOperators[i - 1])) {
                        operants.Remove(operants[operants.Count() - 1]);
                        operants.Add(temp * (double)inputOperands[i + 1].Value);
                        temp *= (double)inputOperands[i + 1].Value;
                    } else {
                        operants.Add((double)inputOperands[i].Value * (double)inputOperands[i + 1].Value);
                        temp = (double)inputOperands[i].Value * (double)inputOperands[i + 1].Value;
                    }
                } else {
                    operants.Add((double)inputOperands[i].Value * (double)inputOperands[i + 1].Value);
                    temp = (double)inputOperands[i].Value * (double)inputOperands[i + 1].Value;
                }
            }
            else if(_inputOperators[i] == '/') {
                if((double)inputOperands[i + 1].Value == 0) {
                    return (false, "Error: Attempt to devide by 0");
                }
                if(i > 0) {
                    if(Character.IsMultiplicative(_inputOperators[i - 1])) {
                        operants.Remove(operants[operants.Count() - 1]);
                        operants.Add(temp / (double)inputOperands[i + 1].Value);
                        temp /= (double)inputOperands[i + 1].Value;
                    } else {
                        operants.Add((double)inputOperands[i].Value / (double)inputOperands[i + 1].Value);
                        temp = (double)inputOperands[i].Value / (double)inputOperands[i + 1].Value;
                    }
                } else {
                    operants.Add((double)inputOperands[i].Value / (double)inputOperands[i + 1].Value);
                    temp = (double)inputOperands[i].Value / (double)inputOperands[i + 1].Value;
                }
            }
        }
        return (true, null);
    }
    public static double SolveAdditives(List<double> operands, List<char> operators) {
        double output = operands[0];
        for(int i = 0; i < operators.Count(); i++) {
            if(operators[i] == '+') {
                output += operands[i + 1];
            }
            if(operators[i] == '-') {
                output -= operands[i + 1];
            }
        }
        return output;
    }
    public static double SolveAdditives(Operand[] operands, char[] operators) {
        double output = (double)operands[0].Value;
        for(int i = 0; i < operators.Count(); i++) {
            if(operators[i] == '+') {
                output += (double)operands[i + 1].Value;
            } else if(operators[i] == '-') {
                output -= (double)operands[i + 1].Value;
            }
        }
        return output;
    }
    public static double SolveExponents(double v, int e) {
        double output = 1;
        if(e > 0) {
            for(int i = 0; i < e; i++) {
                output *= v;
            }
        } else if(e < 0) {
            for(int i = 0; i > e; i--) {
                output /= v;
            }
        }
        return output;
    } 
    public override string ToString() {
        return "abc";
    }
    private static bool hasMultiplicatives(char[] operators) {
        for(int i = 0; i < operators.Length; i++) {
            if(Character.IsMultiplicative(operators[i])) {
                return true;
            }
        }
        return false;
    }
    public static IEnumerable<int> GetIndexesOfSubTerms(MathTerm mathTerm) {
        for(int i = 0; i < mathTerm.Operands.Length; i++) {
            if(mathTerm.Operands[i].IsSolved == false) {
                yield return i;
            }
        }
        yield break;
    }
    public static IEnumerable<int> GetIndexesOfFactorOperands(MathTerm mathTerm) {
        for(int i = 0; i < mathTerm.Operands.Length; i++) {
            if(mathTerm.Operands[i].Factor is not null) {
                yield return i;
            }
        }
        yield break;
    }
    public static IEnumerable<int> GetIndexesOfExponentOperands(MathTerm mathTerm) {
        for(int i = 0; i < mathTerm.Operands.Length; i++) {
            if(mathTerm.Operands[i].Exponent is not null) {
                yield return i;
            }
        }
        yield break;
    }
}