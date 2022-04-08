using System;
using System.Windows.Forms;

namespace PhoneWebShop.WinForms.Strategy.Interfaces
{
    public interface IFormFactory
    {
        Form CreateForm();
        bool AppliesTo(Type formType);
    }
}
