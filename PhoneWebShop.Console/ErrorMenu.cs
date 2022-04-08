using PhoneWebShop.Domain.Interfaces;
using System;

namespace PhoneWebShop.Console
{
    public class ErrorMenu : Menu
    {
        private readonly string _message;
        private readonly Exception _exception;
        public ErrorMenu(IPhoneService phoneService, Menu caller, string message, Exception exception) : base(phoneService, caller)
        {
            _message = message;
            _exception = exception;
        }

        public override void Show()
        {
            base.Show();
            TextFormatter.PrintColoredText($"\n$1Er ging iets fout: {_message}");
            if (!(_exception == null))
                TextFormatter.PrintColoredText(TextFormatter.AddLineBreaks($"$1Error bericht: $0{_exception.Message}", 40));
            TextFormatter.PrintColoredText("$3Druk op een toets om terug te gaan.");

            // Wait for keypress
            System.Console.ReadKey();
            ExitSelf();
        }
    }
}
