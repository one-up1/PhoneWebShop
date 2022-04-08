using PhoneWebShop.Domain.Interfaces;

namespace PhoneWebShop.Console
{
    class SearchMenu : Menu
    {
        public SearchMenu(IPhoneService phoneService, Menu caller) : base(phoneService, caller)
        {
            _title = "Search menu.";
        }

        public override void Show()
        {
            base.Show();

            TextFormatter.PrintColoredText("$3Uw zoekopdracht: $0", false);
            var input = ReadInput();

            new SearchResultMenu(_phoneService, _caller, input).Show();
            ExitSelf();
        }
    }
}
