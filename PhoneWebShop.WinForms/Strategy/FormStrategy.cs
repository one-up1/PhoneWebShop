using PhoneWebShop.WinForms.Strategy.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PhoneWebShop.WinForms.Strategy
{
    internal class FormStrategy : IFormStrategy
    {
        private readonly IFormFactory[] formFactories;

        public FormStrategy(IFormFactory[] formFactories)
        {
            this.formFactories = formFactories ?? throw new ArgumentNullException(nameof(formFactories));
        }

        public Form CreateForm(Type formType)
        {
            var factory = formFactories.FirstOrDefault(x => x.AppliesTo(formType));

            if (factory == null)
                throw new InvalidOperationException($"{formType.FullName} not registered.");

            return factory.CreateForm();
        }

        public Form CreateForm<T>() where T : Form
        {
            return CreateForm(typeof(T));
        }
    }
}
