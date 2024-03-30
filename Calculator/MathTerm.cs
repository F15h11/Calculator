using Calculator;
using Calculator.Utilities;

 using System.Runtime.Serialization.Formatters.Binary;
 using System.IO;
using System.Text.Json;

[Serializable]
public struct MathTerm {
    public Operand[]? Operands;
    public char[]? Operators;
    public MathTerm(Operand[] operands, char[] operators) {
        this.Operands = operands;
        this.Operators = operators;
    }
    public MathTerm() {
        this.Operands = null;
        this.Operators = null;
    }
    public override string ToString() {
        string output = "";
        for(int i = 0; i < this.Operands.Length; i++) {
            output += $"{this.Operands[i].ToString()}";
            if(i < this.Operators.Length ) {
                output += $" {this.Operators[i]} ";
            }
        }
        return output;
    }
}
