using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace PhoneWebShop.WinForms
{
    public partial class AddPhoneForm : Form
    {
        private readonly IPhoneService _phoneService;
        private readonly IBrandService _brandService;

        public AddPhoneForm(IPhoneService phoneService, IBrandService brandService)
        {
            InitializeComponent();
            _phoneService = phoneService;
            _brandService = brandService;
        }

        private void Confirm()
        {
            if (!IsNumericallyValid(txtPrice))
            {
                MessageBox.Show("Price value was invalid!");
                return;
            }

            if (!IsNumericallyValid(txtStock))
            {
                MessageBox.Show("Stock value was invalid!");
                return;
            }

            CreatePhone();
            Close();
            DialogResult = DialogResult.OK;
        }

        private static bool IsNumericallyValid(TextBox textBox)
        {
            var isNumeric = int.TryParse(textBox.Text, out int textValue);
            if (!isNumeric)
                return false;

            if (textValue < 0)
                return false;

            return true;
        }

        private void CreatePhone()
        {
            Brand brand = null;
            var task = Task.Run(async () =>
            {
                brand = await _brandService.AddIfNotExists(new Brand { Name = txtBrand.Text });
            });
            task.Wait();

            var phone = new Phone
            {
                Brand = brand,
                Type = txtType.Text,
                Description = txtDescription.Text,
                VATPrice = Convert.ToDouble(txtPrice.Text),
                Stock = Convert.ToInt32(txtStock.Text),
            };

            _phoneService.AddAsync(phone);
            MessageBox.Show($"Added {phone.FullName}");
        }

        #region Events
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BrandValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = sender as TextBox;

            if (string.IsNullOrEmpty(box.Text))
            {
                e.Cancel = true;
                MessageBox.Show("The brand that you entered is invalid!");
            }
        }

        private void TypeValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                e.Cancel = true;
                MessageBox.Show("The type that you entered is invalid!");
            }
        }

        private void DescriptionValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (string.IsNullOrEmpty(box.Text))
            {
                e.Cancel = true;
                MessageBox.Show("The description that you entered is invalid!");
            }
        }

        private void PriceValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (!double.TryParse(txtPrice.Text, out double _))
            {
                e.Cancel = true;
                MessageBox.Show("The price that you entered is invalid!");
            }
        }

        private void StockValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (!int.TryParse(txtStock.Text, out int _))
            {
                e.Cancel = true;
                MessageBox.Show("The stock that you entered is invalid!");
            }
        }
        #endregion Events
    }
}
