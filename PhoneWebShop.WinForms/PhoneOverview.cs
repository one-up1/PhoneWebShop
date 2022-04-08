using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.WinForms.Strategy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace PhoneWebShop.WinForms
{
    public partial class PhoneOverview : Form
    {
        private const string LabelTextNoPhoneSelection = "No Selection";

        private readonly IPhoneService _phoneService;
        private readonly IFormStrategy _formStrategy;
        private BindingList<Phone> _phonesList;

        public PhoneOverview(IPhoneService phoneService, IFormStrategy formStrategy)
        {
            InitializeComponent();
            _formStrategy = formStrategy;
            _phoneService = phoneService;
            _phonesList = new();

            PhoneListBox.DataSource = _phonesList;
            PhoneListBox.DisplayMember = nameof(Phone.FullName);

            ShowAllPhones();
            PhoneListBox.ClearSelected();
            ResetLabels();
        }

        private void ShowAllPhones()
        {
            IEnumerable<Phone> phones = Enumerable.Empty<Phone>();
            var task = Task.Run(async () => { phones = await _phoneService.GetAll(); });
            task.Wait();
            SetPhoneList(phones.ToList());
        }

        private void SetPhoneList(List<Phone> list)
        {
            _phonesList.Clear();
            list.ForEach(phone => _phonesList.Add(phone));
        }

        private void ShowSearchResults()
        {
            var query = searchBox.Text;
            IEnumerable<Phone> results = Enumerable.Empty<Phone>();
            var task = Task.Run(async () => { results = await _phoneService.Search(query); });
            task.Wait();
            SetPhoneList(results.ToList());
        }

        private void SetLabels(Phone selectedPhone)
        {
            lblBrand.Text = selectedPhone.Brand.Name;
            lblType.Text = selectedPhone.Type;
            lblPrice.Text = selectedPhone.VATPrice.ToString("C2", CultureInfo.CurrentCulture);
            lblStock.Text = selectedPhone.Stock.ToString();
            txtDescription.Text = selectedPhone.Description;
        }

        private void ExitApp()
        {
            Application.Exit();
        }

        private void RemovePhoneDialog()
        {
            if (PhoneListBox.SelectedItem == null)
            {
                MessageBox.Show("You must select a phone!");
                return;
            }
            var phone = PhoneListBox.SelectedItem as Phone ?? new Phone { };

            var confirmResult = MessageBox.Show($"Are you sure to delete {phone.FullName} ?",
                                     "Confirm Delete",
                                     MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
                DeletePhone(phone);
        }

        private void DeletePhone(Phone phone)
        {
            _phoneService.DeleteAsync(phone.Id);
            _phonesList.Remove(phone);
            ResetLabels();
            PhoneListBox.ClearSelected();
        }

        private void ResetLabels()
        {
            lblBrand.Text = LabelTextNoPhoneSelection;
            lblPrice.Text = LabelTextNoPhoneSelection;
            lblType.Text = LabelTextNoPhoneSelection;
            lblStock.Text = LabelTextNoPhoneSelection;
        }


        #region Events

        private void SearchTextChanged(object sender, EventArgs e)
        {
            if (searchBox.Text.Length > 3)
                ShowSearchResults();
            else
                ShowAllPhones();
        }

        private void btnRemovePhone_Click(object sender, EventArgs e)
        {
            RemovePhoneDialog();
        }

        private void PhoneListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var box = (ListBox)sender;
            if (box.SelectedItem != null)
                btnRemovePhone.Enabled = true;
            else
                btnRemovePhone.Enabled = false;
        }

        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            using var dialog = _formStrategy.CreateForm<AddPhoneForm>();
            dialog.StartPosition = FormStartPosition.CenterParent;

            dialog.ShowDialog(this);
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            if (PhoneListBox.SelectedItem == null)
                return;
            Phone selectedPhone = (Phone)PhoneListBox.SelectedItem;

            SetLabels(selectedPhone);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void PhoneAddedSuccessfully(object sender, PhoneAddedEventArgs e)
        {
            _phonesList.Add(e.AddedPhone);
            SetPhoneList(_phonesList.OrderBy(phone => phone.FullName).ToList());
        }

        #endregion Events
    }
}
