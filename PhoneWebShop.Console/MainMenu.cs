using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;

namespace PhoneWebShop.Console
{
    public class MainMenu : ListMenu
    {
        private readonly IBrandService _brandService;

        public MainMenu(IPhoneService phoneService, IBrandService brandService, Menu caller) : base(phoneService, caller)
        {
            _title = "Main Menu";
            _brandService = brandService;
        }

        public override void Show()
        {
            base.Show();

            TextFormatter.PrintColoredText("$1Voor meer informatie over een model, typ het nummer in. \n");
            
            IEnumerable<Phone> results = Enumerable.Empty<Phone>();
            var task = Task.Run(async () => { results = await _phoneService.GetAll(); });
            task.Wait();
            DisplayPhones(results.ToList());
            TextFormatter.PrintColoredText("Druk op $2A$0 om een nieuwe telefoon toe te voegen. \nDruk op $2S$0 om een zoekopdracht te geven.");

            System.Console.Write("Uw Keuze: ");
            string input = ReadInput();
            if (input.ToLower() == "a")
                new AddMenu(_phoneService, _brandService, this).Show();
            else if (input.ToLower() == "s")
                new SearchMenu(_phoneService, this).Show();
            else if (int.TryParse(input, out int choice))
                ShowInfo(choice);
            else
                new ErrorMenu(_phoneService, this, "Voer een valide nummer in!", new Exception()).Show();
            Show();
        }
    }
}