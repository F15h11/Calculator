using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UserInterface {
    public class Menu {
        private const string indexer = " <-";
        private const string info = "Use the arrow keys to navigate through the menu.\nUse Enter to select and Space to deselect.\n";

        private string _title; 
        private OptionCategorie[] _optionCategories;
        private int _optionCategoriesCount;
        private int[] _optionsCount;        //Index = Index of Option categorie
        private int _indexerPositionCategorie;
        private int _indexerPositionOption;
        private bool _inOptionCategorie;
        private bool _multipleOptionCategories;
        public Menu(string title, OptionCategorie[] optionCategories) {
            _title = title;
            _optionCategories = optionCategories;
            _optionCategoriesCount = optionCategories.Length;
            _optionsCount = getOptionsCount(optionCategories);
            _indexerPositionCategorie = 0;
            if(optionCategories.Length > 1) {
                checkIfValid(optionCategories);
                _inOptionCategorie = false;
                _multipleOptionCategories = true;
            }
            else {
                _inOptionCategorie = true;
                _multipleOptionCategories= false;
            }
        }
        public int[] Run() {
            while(update());
            return new int[2] { _indexerPositionCategorie, _indexerPositionOption };
        }
        private bool update() {
            Console.Out.Write(display());

            if(Program.Debug) {
                Console.Out.Write(displayDebugInfo());
            }

            int input = 0;
            do {
                input = getKeyInput();
            } while(input == 0);
            if(input == 1) {
                moveIndexer(true);
            }
            if(input == 2) {
                moveIndexer(false);
            }
            if(input == 3) {
                if(!_inOptionCategorie & _multipleOptionCategories) {
                    _inOptionCategorie = true;
                }
                else {
                    Console.Clear();
                    return false;
                }
            }
            if(input == 4 & _inOptionCategorie & _multipleOptionCategories) {
                _inOptionCategorie = false;
                _indexerPositionOption = 0;
            }

            Console.Clear();
            return true;
        }

        private string display() {
            string output = "";

            output += _title;
            output += "\n";

            for(int i = 0; i < _optionCategories.Length; i++) {
                if(_optionCategories[i].HasName) {
                    output += _optionCategories[i].Name;
                    if(_indexerPositionCategorie == i & !_inOptionCategorie) {
                        output += indexer;
                    }
                    output += "\n";
                }
                for(int j = 0; j < _optionCategories[i].Options.Length; j++) {
                    output += $"  {_optionCategories[i].Options[j]}";
                    if(_indexerPositionCategorie == i & _indexerPositionOption == j & _inOptionCategorie) {
                        output += indexer;
                    }
                    output += "\n";
                }
            }
            output += info;

            return output;
        }
        static private int getKeyInput() {
            ConsoleKey input = Console.ReadKey().Key;
            if(input == ConsoleKey.UpArrow) {
                return 1;
            }
            if(input == ConsoleKey.DownArrow) {
                return 2;
            }
            if(input == ConsoleKey.Enter) {
                return 3;
            }
            if(input == ConsoleKey.Spacebar) {
                return 4;
            }
            else {
                return 0;
            }
        }
        private void moveIndexer(bool up) {
            if(!_inOptionCategorie) {
                if(up) {
                    if(_indexerPositionCategorie > 0) {
                        _indexerPositionCategorie--;
                    }
                    else {
                        _indexerPositionCategorie = _optionCategoriesCount - 1;
                    }
                }
                if(!up) {
                    if(_indexerPositionCategorie < _optionCategoriesCount - 1) {
                        _indexerPositionCategorie++;
                    }
                    else {
                        _indexerPositionCategorie = 0;
                    }
                }
            }
            if(_inOptionCategorie) {
                if(up) {
                    if(_indexerPositionOption > 0) {
                        _indexerPositionOption--;
                    }
                    else {
                        _indexerPositionOption = _optionsCount[_indexerPositionCategorie] - 1;
                    }
                }
                if(!up) {
                    if(_indexerPositionOption < _optionsCount[_indexerPositionCategorie] - 1) {
                        _indexerPositionOption++;
                    }
                    else {
                        _indexerPositionOption = 0;
                    }
                }
            }
        }
        private string displayDebugInfo() {
            string output = "\n";
            output += $"Debug info (Menu)\n";
            output += $"{nameof(_optionCategoriesCount)}: {_optionCategoriesCount} \n";
            output += $"{nameof(_optionsCount)}: {string.Join(", ", _optionsCount)}\n";
            output += $"{nameof(_indexerPositionCategorie)}: {_indexerPositionCategorie}\n";
            output += $"{nameof(_indexerPositionOption)}: {_indexerPositionOption}\n";
            output += $"{nameof(_inOptionCategorie)}: {_inOptionCategorie}\n";
            output += $"{nameof(_multipleOptionCategories)}: {_multipleOptionCategories}\n";

            return output;
        }
        static void checkIfValid(OptionCategorie[] optionCategories) {
            for(int i = 0;i < optionCategories.Length;i++) {
                if(!optionCategories[i].HasName) {
                    throw new Exception("Contructor got more than one option categories and one of them didnt have a name");
                }
            }
        }
        static private int[] getOptionsCount(OptionCategorie[] optionCategories) {
            int[] c = new int[optionCategories.Length];
            int counter = 0;
            for(int i = 0;i < optionCategories.Length;i++) {
                for(int j = 0;j < optionCategories[i].Options.Length;j++) {
                    counter++;
                }
                c[i] = counter;
                counter = 0;
            }
            return c;
        }
    }
}