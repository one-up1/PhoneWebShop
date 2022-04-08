using PhoneWebShop.Business;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;

namespace PhoneWebShop.Console
{
    public class InfoMenu : Menu
    {
        private Phone _phone;

        public InfoMenu(IPhoneService phoneService, Menu caller, Phone phone) : base(phoneService, caller)
        {
            _phone = phone;
            _title = $"Informatie over {phone.Type}";
        }

        public override void Show()
        {
            base.Show();

            TextFormatter.PrintColoredText("$2Informatie\n");
            TextFormatter.PrintColoredText($"$3UID:            $0{_phone.Id}");
            TextFormatter.PrintColoredText($"$3Merk:           $0{_phone.Brand.Name}");
            TextFormatter.PrintColoredText($"$3Type:           $0{_phone.Type}");
            TextFormatter.PrintColoredText($"$3Prijs:          $0{_phone.VATPrice} EURO $3incl. BTW");
            TextFormatter.PrintColoredText($"$3\nOmschrijving: $0\n{TextFormatter.AddLineBreaks(_phone.Description, 25)}");
            TextFormatter.PrintColoredText("\nDruk op een toets om terug te gaan.");
            // Wait for keypress
            System.Console.ReadKey();
            ExitSelf();
        }
    }
}