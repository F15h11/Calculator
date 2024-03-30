using System;

public struct Operand {
    public double? Value {get; set;}
    public MathTerm? MathTerm {get;}
    public bool? IsSolved {get; set;}
    public bool IsVariable {get; set;}
    public int? Exponent {get; set;}
    public double? Factor {get; set;}
    public char? Name {get; set;}
    public bool? IsNegative {get;}
    public Operand(double? v) {
        this.Value = v;
        this.MathTerm = null;
        this.IsSolved = true;
        this.IsVariable = false;
        this.Exponent = null;
        this.Factor = null;
        this.Name = null;
        this.IsNegative = null;
    }
    public Operand(double v, int? exponent) {
        this.Value = v;
        this.MathTerm = null;
        this.IsSolved = true;
        this.IsVariable = false;
        this.Exponent = exponent;
        this.Factor = null;
        this.Name = null;
        this.IsNegative = null;
    }
    public Operand(MathTerm mathTerm, bool isNegative) {
        this.Value = null;
        this.MathTerm = mathTerm;
        this.IsSolved = false;
        this.IsVariable = false;
        this.Exponent = null;
        this.Factor = null;
        this.Name = null;
        this.IsNegative = isNegative;
    }
    public Operand(MathTerm mathTerm, bool isNegative, double? factor) {
        this.Value = null;
        this.MathTerm = mathTerm;
        this.IsSolved = false;
        this.IsVariable = false;
        this.Exponent = null;
        this.Factor = factor;
        this.Name = null;
        this.IsNegative = isNegative;
    }
    public Operand(char name, bool isNegative) {
        this.Value = null;
        this.MathTerm = null;
        this.IsSolved = null;
        this.IsVariable = true;
        this.Exponent = null;
        this.Factor = null;
        this.Name = name;
        this.IsNegative = isNegative;
    }
    public Operand(char name, int exponent) {
        this.Value = null;
        this.MathTerm = null;
        this.IsSolved = null;
        this.IsVariable = true;
        this.Exponent = exponent;
        this.Factor = null;
        this.Name = name;
        this.IsNegative = null;
    }  
    public Operand(char name, double factor) {
        this.Value = null;
        this.MathTerm = null;
        this.IsSolved = null;
        this.IsVariable = true;
        this.Exponent = null;
        this.Factor = factor;
        this.Name = name;
        this.IsNegative = null;
    }
    public override string ToString() {
        string output = "";
        if(this.Factor is not null) {
            output += $"{this.Factor}";
        }
        if(this.IsVariable) {
            output += $"{this.Name}";
        } else if(this.IsSolved == true) {
            output += $"{this.Value}";
        } else {
            if(this.IsNegative == true) {
                output += "-";
            }
            output += $"({this.MathTerm.ToString()})";
        }
        if(this.Exponent is not null) {
            output += $"^{this.Exponent} ";
        }
        return output;
    }
}