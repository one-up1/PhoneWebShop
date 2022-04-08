using System;
using System.Windows.Forms;

namespace PhoneWebShop.WinForms.Strategy.Interfaces
{
    public interface IFormStrategy
    {
        Form CreateForm(Type formType);
        Form CreateForm<T>() where T : Form;
    }
}
