using PhoneWebShop.Domain.Interfaces;
using System;

namespace PhoneWebShop.Console
{
    public abstract class Menu 
    {
        protected string _title = "Menu";
        protected IPhoneService _phoneService;
        protected Menu _caller;
        public Menu(IPhoneService phoneService, Menu caller)
        {
            _phoneService = phoneService;
            _caller = caller;
        }

        /// <summary>
        /// Displays the menu
        /// </summary>
        public virtual void Show()
        {
            System.Console.CursorVisible = false;
            System.Console.Title = _title;
            System.Console.Clear();
        }

        /// <summary>
        /// Called when a menu should exit, then shows the caller.
        /// </summary>
        protected void ExitSelf()
        {
            // We have no menu to go back to.
            if (_caller == null)
                Environment.Exit(0);

            _caller.Show();
        }

        /// <summary>
        /// Read user input and return once user presses enter.
        /// </summary>
        /// <returns>The user input as a string</returns>
        protected string ReadInput()
        {
            System.Console.CursorVisible = true;
            string input = "";
            while (true)
            {
                ConsoleKeyInfo key = System.Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    ExitSelf();
                else if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if ((input.Length > 0))
                    {
                        System.Console.Write("\x1B[1P");
                        input = input.Remove(input.Length - 1);
                    } else
                    {
                        System.Console.CursorLeft++;
                    }
                }
                else
                    input += key.KeyChar;

            }
            System.Console.WriteLine();
            return input;
        }
    }
}
