//Author: Daniel aka Fishii aka F15h11
//Version: v1.2.1

using System;
using System.Data;
using Calculator;
using Calculator.UserInterface;

public static class Program {
    public const string Version = "1.2.1";
    public const string Title = $"Calculator v{Version}";

    public static bool Debug = false;

    private static readonly OptionCategorie functionsOptions = new OptionCategorie("Functions: ", new string[] { "Calculator" });
    private static readonly int functions = 0;
    private static readonly int runCalculator = 0;
    private static readonly OptionCategorie programOptions = new OptionCategorie("Program:", new string[] { "Settings", "Exit" });
    private static readonly int program = 1;
    private static readonly int runSettings = 0;
    private static readonly int exit = 1;
    private static readonly OptionCategorie[] options = { functionsOptions, programOptions };
    public static void Main() {
        Console.Title = Title;
        while(true) {
            Console.Clear();
            Menu menu = new Menu("Start Menu", options);
            if(!runSelectedFunction(menu.Run())) {
                break;
            }
        }
        Environment.Exit(0);
    }
    static bool runSelectedFunction(int[] input) {
        while(true) {
            if(input[0] == functions) {
                if(input[1] == runCalculator) {
                    while(Calculator.Calculator.Run()) ;
                    return true;
                }
            }
            if(input[0] == program) {
                if(input[1] == runSettings) {
                    while(Settings.Run()) ;
                    return true;
                }
                if(input[1] == exit) {
                    return false;
                }
            }
        }
    }
}