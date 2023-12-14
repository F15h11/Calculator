using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UserInterface {
    public static class GetUserInput {
        public static string StringInput(string instruction, bool inputInSameLine) {
            if(inputInSameLine) {
                Console.Out.Write(instruction);
                string? output;
                output = Console.ReadLine();
                if(output != null) {
                    return output;
                }
                else {
                    return "";
                }
            }
            else {
                Console.Out.WriteLine(instruction);
                string? output;
                output = Console.ReadLine();
                if(output != null) {
                    return output;
                }
                else {
                    return "";
                }
            }
        }
    }
}
