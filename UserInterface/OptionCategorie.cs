using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UserInterface {
    public struct OptionCategorie {
        public string? Name;
        public string[] Options;
        public bool HasName;
        public OptionCategorie(string name, string[] options) {
            Name = name;
            Options = options;
            HasName = true;
        }
        public OptionCategorie(string[] options) {   //Should only be used if this is the only option categorie that will be used
            Options = options;
            HasName = false;
        }
    }
}
