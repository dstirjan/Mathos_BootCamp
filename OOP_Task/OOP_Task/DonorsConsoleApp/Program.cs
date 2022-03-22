using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonorsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($" Do you want to add Doctors (Press 'D') or add Patients ( press 'P') ?");
            string Tip = Console.ReadLine();
            Console.Clear();
            string ExitString = "exit";

            while (Tip != "P" & Tip != "D" & Tip != "p" & Tip != "d")
            {
                Console.WriteLine($"Wrong input. \n Do you want to add Doctors (Press 'D') or add Patients ( press 'P') ?");
                Tip = Console.ReadLine();
                Console.Clear();
            }
            if (Tip == "P" ^ Tip == "p")
            {
                List<Patient> ListPatient = new List<Patient>();
                //string ExitString = "exit";

                do
                {
                    Console.WriteLine($"Insert following information about donor: ");
                    Patient Patient = new Patient();
                    Console.WriteLine($"First Name: ");
                    Patient.Name = Console.ReadLine();
                    Console.WriteLine($"Last Name: ");
                    Patient.LastName = Console.ReadLine();
                    Address Address = new Address();
                    Console.WriteLine($"Street name: ");
                    Address.Street = Console.ReadLine();
                    Console.WriteLine($"Street number: ");
                    Address.StreetNumber = Console.ReadLine();
                    Console.WriteLine($"City: ");
                    Address.City = Console.ReadLine();
                    Console.WriteLine($"Donate number: ");
                    Patient.PatientID = Console.ReadLine();
                    Patient.Code = Guid.NewGuid();
                    ListPatient.Add(Patient);
                    Console.WriteLine($"Write 'exit' for list patients, or another key to contine with patient entry");
                    ExitString = Console.ReadLine();
                    Patient.Address = Address;
                    Console.Clear();
                }
                while (ExitString != "exit");

                foreach (Patient Patient in ListPatient)

                {
                    Patient.PrintPerson();
                }
                Console.ReadLine();
            }
            else
            {
                List<Doctor> ListDoctor = new List<Doctor>();
                //string ExitString = "Exit";

                do
                {
                    Console.WriteLine($"Insert following information about doctor: ");
                    Doctor Doctor = new Doctor();
                    Console.WriteLine($"First Name: ");
                    Doctor.Name = Console.ReadLine();
                    Console.WriteLine($"Last Name: ");
                    Doctor.LastName = Console.ReadLine();
                    Console.WriteLine($"Field of specilization: ");
                    Doctor.Field = Console.ReadLine();
                    Console.WriteLine($"OIB: ");
                    Doctor.OIB = Console.ReadLine();
                    ListDoctor.Add(Doctor);

                    Console.WriteLine($"Write 'exit' for list patients, or another key to contine with patient entry");
                    ExitString = Console.ReadLine();
                    Console.Clear();
                }
                while (ExitString != "exit");

                foreach (Doctor Doctor in ListDoctor)
                {
                    Console.WriteLine($"Doctor name: {Doctor.Name} {Doctor.LastName} \n Field of specilization:{Doctor.Field}  " +
                        $"\n OIB:{Doctor.OIB}\n");
                }
                Console.ReadLine();
            }
        }
    }
}