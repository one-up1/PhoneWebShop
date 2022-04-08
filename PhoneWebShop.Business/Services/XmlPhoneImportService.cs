using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Services
{
    public class XmlPhoneImportService : IPhoneImportService
    {
        private readonly IBrandService _brandService;

        public XmlPhoneImportService(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public List<Phone> LoadPhones(StreamReader reader)
        {
            var phones = new List<Phone>();

            using (var xmlReader = XmlReader.Create(reader))
            {
                Phone tempPhone = new();
                while (xmlReader.Read())
                {
                    if (xmlReader.IsStartElement() && xmlReader.Name == "Phone")
                    {
                        tempPhone = new Phone();
                    }
                    else if (!xmlReader.IsStartElement() && xmlReader.Name == "Phone")
                    {
                        phones.Add(tempPhone);
                    }
                    else if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case "Brand":
                                tempPhone.Brand = GetBrand(xmlReader.ReadElementContentAsString());
                                break;
                            case "Type":
                                tempPhone.Type = xmlReader.ReadElementContentAsString();
                                break;
                            case "Price":
                                tempPhone.VATPrice = xmlReader.ReadElementContentAsDouble();
                                break;
                            case "Description":
                                tempPhone.Description = xmlReader.ReadElementContentAsString();
                                break;
                            case "Stock":
                                tempPhone.Stock = xmlReader.ReadElementContentAsInt();
                                break;
                        }
                    }
                }
            }

            return phones;

            /*var root = new XmlRootAttribute("Phones");

            try
            {
                var deserializer = new XmlSerializer(typeof(List<Phone>), root);
                var items = (List<Phone>)deserializer.Deserialize(reader);
                return items;
            } finally {
                reader.Close();
            }*/

        }

        private Brand GetBrand(string name)
        {
            Brand resultBrand = null;
            var task = Task.Run(async () =>
            {
                resultBrand = await _brandService.AddIfNotExists(new Brand
                {
                    Name = name
                });
            });
            task.Wait();

            return resultBrand;
        }
    }
}
