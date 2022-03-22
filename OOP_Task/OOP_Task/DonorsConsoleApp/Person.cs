using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorsConsoleApp
{
    abstract public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
        public virtual string GetAddress()
        {
            return $"Address: {Address.Street} {Address.StreetNumber} {Address.City}";
        }
    }
}
