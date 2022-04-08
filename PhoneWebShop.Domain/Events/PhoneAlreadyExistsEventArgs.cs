using PhoneWebShop.Domain.Entities;
using System;

namespace PhoneWebShop.Domain.Events
{
    public class PhoneAlreadyExistsEventArgs : EventArgs
    {
        public Phone ExistingPhone {  get; set; }
        public PhoneAlreadyExistsEventArgs(Phone phone) : base()
        {
            ExistingPhone = phone;
        }
    }
}
