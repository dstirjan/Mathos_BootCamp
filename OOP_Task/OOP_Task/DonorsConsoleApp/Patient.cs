using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorsConsoleApp
{
    class Patient : Person, IPerson
    {
        public string PatientID { get; set; }
        public Guid Code { get; set; }

        public void PrintPerson()
        {
            Console.WriteLine($"Donor name: {Name} {LastName} \n Donor address:{Address.Street}{Address.StreetNumber} {Address.City} " +
                $"\n Donation number: {PatientID} \n Reference code: {Code} \n");
        }
    }
}
