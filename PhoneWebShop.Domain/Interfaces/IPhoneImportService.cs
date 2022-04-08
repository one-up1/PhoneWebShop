using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.IO;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IPhoneImportService
    {
        /// <summary>
        /// Load a list of phones from a file.
        /// </summary>
        /// <param name="reader">The streamreader of the source.</param>
        /// <returns>The list of loaded phones (Id is not set, because these phones do not exist in the DB yet).</returns>
        List<Phone> LoadPhones(StreamReader reader);
    }
}
