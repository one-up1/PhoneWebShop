using PhoneWebShop.Domain.Entities;
using System;

namespace PhoneWebShop.Domain.Events
{
    public class PhoneAddedEventArgs : EventArgs
    {
        public Phone AddedPhone { get; set; }
        public PhoneAddedEventArgs(Phone phone) : base()
        {
            AddedPhone = phone;
        }
    }
}
