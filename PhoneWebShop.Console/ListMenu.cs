using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneWebShop.Console
{
    public class ListMenu : Menu
    {

        protected readonly Dictionary<int, int> _indexDictionary = new();

        public ListMenu(IPhoneService phoneService, Menu caller) : base(phoneService, caller)
        { }

        protected void ShowInfo(int displayIndex)
        {
            int id;
            if (!_indexDictionary.TryGetValue(displayIndex, out id)) {
                new ErrorMenu(_phoneService, this, "Deze index bestaat niet.", new NullReferenceException()).Show();
                return;
            }

            Phone result = null;
            var task = Task.Run(async () => { result = await _phoneService.GetAsync(id); });
            task.Wait();

            Phone selectedPhone = result;
            if (selectedPhone == null)
            {
                new ErrorMenu(_phoneService, this, "Nummer staat niet in de lijst", new NullReferenceException()).Show();
                return;
            }
            new InfoMenu(_phoneService, this, selectedPhone).Show();
        }

        protected void DisplayPhones(List<Phone> phones)
        {
            TextFormatter.PrintListOfPhones(phones, "$3#{0} - $0{1} {2}", _indexDictionary);
            TextFormatter.PrintColoredText("Druk op $2ESC$0 om te sluiten.");
        }
    }
}