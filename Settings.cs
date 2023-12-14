using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

using Calculator;
using Calculator.UserInterface;

namespace Calculator {
    public static class Settings {
        private static readonly OptionCategorie debugModeOptions = new OptionCategorie("Debug mode:", new string[] { "On", "Off" });
        private static readonly int debugModeSettings = 0;     //Option categorie index
        private static readonly int setDebugModeOn = 0;   //Opttion index
        private static readonly int setDebugModeOff = 1;  //Option index
        private static readonly OptionCategorie settingsOptions = new OptionCategorie("Settings:", new string[] { "Exit" });
        private static readonly int settingsSettings = 1;      //Option categorie index
        private static readonly int exitSettings = 0;  //Opttion index
        private static readonly OptionCategorie[] options = {debugModeOptions, settingsOptions};
        public static bool Run() {
            Menu menu = new Menu("Settings", options);
            int[] input = menu.Run();
            while(true) {
                if(input[0] == debugModeSettings) {
                    if(input[1] == setDebugModeOn) {
                        if(!Program.Debug) {
                            Program.Debug = true;
                            return true;
                        }
                        else {
                            return true;
                        }
                    }
                    if(input[1] == setDebugModeOff) {
                        if(Program.Debug) {
                            Program.Debug = false;
                            return true;
                        }
                        else {
                            return true;
                        }
                    }
                }
                if(input[0] == settingsSettings) {
                    if(input[1] == exitSettings) {
                        return false;
                    }
                }
            }
        }
    }
}
