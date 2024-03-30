internal static class Program {
    public static void Main() {
        bool exit = false;
        do {
            Console.WriteLine(">>> Start Menu <<<");
            Console.WriteLine(">>> Press 1 for the normal Calculator");
            Console.WriteLine(">>> Or press 2 to enter functions");
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string input = Console.ReadLine();
            Console.ResetColor();
            switch(input) {
                case "1":
                    calculatorUI();
                    break;
                case "2": 
                    functionsUI();
                    break;
                case "3":
                    test();
                    break;
                case "exit":
                    exit = true;
                    break;
                default: 
                    break;
            }
        } while(!exit);
    }
    private static void calculatorUI() {
        Console.WriteLine(">>> Calculator <<<");
        bool exit = false;
        do {
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string input = Console.ReadLine();
            Console.ResetColor();
            if(input == "exit") {
                exit = true;
            } else {
            var convertToMathTerm = new Converter(input).GetTerm();
                if(convertToMathTerm.Success) {
                    string mathTerm = convertToMathTerm.MathTerm.ToString();
                    var calculator = new MathTermCalculator((MathTerm)convertToMathTerm.MathTerm).Solve();
                    if(calculator.Success) {
                        Console.Write(">>> ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{mathTerm} = {calculator.Result}");
                    } else {
                        Console.Write(">>> ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error: {calculator.ErrorMessage}^.");
                    }
                } else {
                    Console.Write(">>> ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {convertToMathTerm.ErrorMessage}.");
                }
            }
            Console.ResetColor();
        } while(!exit);
    }
    private static void functionsUI() {
        Console.WriteLine(">>> Functions <<<");
        Console.WriteLine(">>> Enter a function, for example: '2x + 5'");
        bool exit = false;
        do {
            Console.Write(">> ");
            string input = Console.ReadLine();
            if(input == "exit") {
                exit = true;
            } else {
                var convertToMathTerm = new Converter(input, ['x']).GetTerm();
                if(convertToMathTerm.Success) {
                    MathFunction mathFunction = new MathFunction((MathTerm)convertToMathTerm.MathTerm, 'f');
                    mathFunction.ValueTable = MathFunction.CalculateValueTable(mathFunction.FunctionTerm, -15, 15);
                    Console.WriteLine(mathFunction);
                }
            }
        } while(!exit);
    }
    private static void test() {
        Console.WriteLine("---Test Additive Operations---");
        test("1+1", 2);
        test("3+4-3", 4);
        Console.WriteLine("---Test Multiplicative Operations---");
        test("3 / 4 / 5", 0.15);
        test("4*7/9", 3.111111111111111);
        Console.WriteLine("---Test Both---");
        test("3+4*5", 23);
        test("3/4+43-52/3*3.5- -3+5", -8.916666666666664);
        Console.WriteLine("---Test Brackets---");
        //test("(2*2)(2*2)");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
    private static void test(string str, double result) {
        var convertToMathTerm = new Converter(str).GetTerm();
        if(convertToMathTerm.Success) {
            var calculator = new MathTermCalculator((MathTerm)convertToMathTerm.MathTerm).Solve();
            if(calculator.Success) {
                if(calculator.Result == result) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Success     Input string: \"{str}\" | MathTerm: {convertToMathTerm.MathTerm} | Result: {calculator.Result}");
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed     Input string: \"{str}\" | MathTerm: {convertToMathTerm.MathTerm} | Result {calculator.Result} | Expected Result: {result}");
                }
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed     Input string: \"{str}\" | MathTerm: {convertToMathTerm.MathTerm} | ErrorMessage: {calculator.ErrorMessage}");
            }
        } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed     Input string: \"{str}\" | ErrorMessage: {convertToMathTerm.ErrorMessage}");
        }
        Console.ResetColor();
    }
}