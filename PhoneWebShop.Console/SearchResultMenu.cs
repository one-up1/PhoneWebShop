using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneWebShop.Console
{
    public class SearchResultMenu : ListMenu
    {
        private string _searchQuery;

        public SearchResultMenu(IPhoneService phoneService, Menu caller, string searchQuery) : base(phoneService, caller)
        {
            _title = "Search Results";
            _searchQuery = searchQuery;
        }

        public override void Show()
        {
            base.Show();

            IEnumerable<Phone> results = Enumerable.Empty<Phone>();
            var task = Task.Run(async () => { results = await _phoneService.Search(_searchQuery); });
            task.Wait();

            var searchResult = results.ToList();

            TextFormatter.PrintColoredText($"$3Showing results for query: $0{_searchQuery}");

            DisplayPhones(searchResult.ToList());
            TextFormatter.PrintColoredText($"Kies een telefoon of druk op $2ESC$0 om terug te gaan.");
            TextFormatter.PrintColoredText($"$3Uw Selectie: $0");

            var input = ReadInput();
            if (int.TryParse(input, out int selection))
            {
                ShowInfo(selection);
            }
            else
            {
                new ErrorMenu(_phoneService, this, "Dit is geen nummer!", new FormatException()).Show();
            }

            ExitSelf();

        }
    }
}
