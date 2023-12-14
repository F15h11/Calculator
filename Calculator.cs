using Calculator.Math;
using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.UserInterface;
using System.Reflection.Metadata;

namespace Calculator {
    public static class Calculator {
        private const string instruction = "Enter a math term: ";
        public static bool Run() {
            Console.Clear();
            Console.Out.Write("Calculator\n");
            StringToMathTermConverter stringToMathTermConverter = new StringToMathTermConverter(GetUserInput.StringInput(instruction, true));
            MathTermCalculator mathTermCalculator = new MathTermCalculator(stringToMathTermConverter.Term);

            Console.Clear();
            Console.Out.WriteLine(mathTermCalculator.ToString());
            Console.Out.WriteLine("Press Enter to enter another term or press Space to get back to menu..");

            while(true) {
                ConsoleKey input = Console.ReadKey().Key;

                if(input == ConsoleKey.Enter) {
                    return true;
                }
                if(input == ConsoleKey.Spacebar) {
                    return false;
                }
                else {
                    continue;
                }
            }
        }
    }
}
