using PhoneWebShop.Business;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System.Threading.Tasks;

namespace PhoneWebShop.Console
{
    internal class AddMenu : Menu
    {
        private readonly IBrandService _brandService;
        public AddMenu(IPhoneService phoneService, IBrandService brandService,Menu caller) : base(phoneService, caller)
        {
            _title = "Add Phone";
            _brandService = brandService;
        }

        public override void Show()
        {
            base.Show();
            System.Console.CursorVisible = true;
            TextFormatter.PrintColoredText("$8Telefoon toevoegen...");
            TextFormatter.PrintColoredText("$3Welk Merk: ", false);
            var brandName = ReadInput();
            TextFormatter.PrintColoredText("$3Welk Model: ", false);
            var model = ReadInput();
            TextFormatter.PrintColoredText("$3Hoe Duur: ", false);
            var price = System.Convert.ToInt32(ReadInput());
            TextFormatter.PrintColoredText("$3Omschrijving: ", false);
            var description = ReadInput();

            Brand brand = null;
            var task = Task.Run(async () => await _brandService.AddIfNotExists(new Brand { Name = brandName }));
            task.Wait();
            Task.Run(() => _phoneService.AddAsync(new Phone{
                Brand = brand,
                Type = model,
                VATPrice = price, 
                Description = description,
                Stock = 2 })).Wait();
            ExitSelf();
        }
    }
}