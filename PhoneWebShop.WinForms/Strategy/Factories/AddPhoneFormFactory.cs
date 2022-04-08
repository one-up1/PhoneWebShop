using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.WinForms.Strategy.Interfaces;
using System;
using System.Windows.Forms;

namespace PhoneWebShop.WinForms.Strategy.Factories
{
    internal class AddPhoneFormFactory : IFormFactory
    {
        private readonly IPhoneService phoneService;
        private readonly IBrandService brandService;

        public AddPhoneFormFactory(IPhoneService phoneService, IBrandService brandService)
        {
            this.phoneService = phoneService ?? throw new ArgumentNullException(nameof(phoneService));
            this.brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        public bool AppliesTo(Type formType)
        {
            return typeof(AddPhoneForm).Equals(formType);
        }

        public Form CreateForm()
        {
            return new AddPhoneForm(phoneService, brandService);
        }
    }
}
