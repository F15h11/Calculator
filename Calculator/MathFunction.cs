internal struct MathFunction {
    public MathTerm FunctionTerm {get; set;}
    public char Name {get; set;}
    public double[,]? ValueTable {get; set;}
    public MathFunction(MathTerm functionTerm, char name) {
        this.FunctionTerm = functionTerm;
        this.Name = name;
    }
    public override string ToString() {
        string output = $"{this.Name}(x) = {this.FunctionTerm.ToString()}\n";
        if(this.ValueTable is not null) {
            for(int i = 0; i < this.ValueTable.GetLength(0); i++) {
                output += $"{this.Name}({this.ValueTable[i, 0]}) = {this.ValueTable[i, 1]}\n";
            }
        }
        return output;
    }
    public static double[,]? CalculateValueTable(MathTerm functionTerm, int xStart, int xEnd) {
        if(xStart > xEnd) {
            return null;
        }
        double[,] output = new double[xEnd - xStart + 1, 2];
        int index = 0;
        for(int i = xStart; i <= xEnd; i++) {
            for(int j = 0; j < functionTerm.Operands.Length; j++) {
                if(functionTerm.Operands[j].Name == 'x') {
                    if(functionTerm.Operands[j].IsNegative == true) {
                        functionTerm.Operands[j].Value = output[index, 0] = -i;
                        
                    } else {
                        functionTerm.Operands[j].Value = output[index, 0] = i;
                    }
                }
            }
            var calculator = new MathTermCalculator(functionTerm).Solve();
            if(calculator.Success) {
                output[index, 1] = (double)calculator.Result;
            } else {
                return null;
            }
            index++;
        }
        return output;
    }
}