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
            char type;
            type = Console.ReadLine()[0];
            Console.Clear();
            string ExitString = "exit";

            while (type != 'P' & type != 'D' & type != 'p' & type != 'd')
            {
                Console.WriteLine($"Wrong input. \n Do you want to add Doctors (Press 'D') or add Patients ( press 'P') ?");
                type = Console.ReadLine()[0];
                Console.Clear();
            }
            if (type == 'P' || type == 'p')
            {
                List<Patient> listPatient = new List<Patient>();
                //string ExitString = "exit";

                do
                {
                    Console.WriteLine($"Insert following information about donor: ");
                    Patient patient = new Patient();
                    Console.WriteLine($"First Name: ");
                    patient.Name = Console.ReadLine();
                    Console.WriteLine($"Last Name: ");
                    patient.LastName = Console.ReadLine();
                    Address address = new Address();
                    Console.WriteLine($"Street name: ");
                    address.Street = Console.ReadLine();
                    Console.WriteLine($"Street number: ");
                    address.StreetNumber = Console.ReadLine();
                    Console.WriteLine($"City: ");
                    address.City = Console.ReadLine();
                    Console.WriteLine($"Donate number: ");
                    patient.PatientID = Console.ReadLine();
                    patient.Code = Guid.NewGuid();
                    listPatient.Add(patient);
                    Console.WriteLine($"Write 'exit' for list patients, or another key to contine with patient entry");
                    ExitString = Console.ReadLine();
                    patient.Address = address;
                    Console.Clear();
                }
                while (ExitString != "exit");

                foreach (Patient patient in listPatient)

                {
                    patient.PrintPerson();
                }
                Console.ReadLine();
            }
            else
            {
                List<Doctor> listDoctor = new List<Doctor>();
                //string ExitString = "Exit";

                do
                {
                    Console.WriteLine($"Insert following information about doctor: ");
                    Doctor doctor = new Doctor();
                    Console.WriteLine($"First Name: ");
                    doctor.Name = Console.ReadLine();
                    Console.WriteLine($"Last Name: ");
                    doctor.LastName = Console.ReadLine();
                    Console.WriteLine($"Field of specilization: ");
                    doctor.Field = Console.ReadLine();
                    Console.WriteLine($"OIB: ");
                    doctor.OIB = Console.ReadLine();
                    listDoctor.Add(doctor);

                    Console.WriteLine($"Write 'exit' for list patients, or another key to contine with patient entry");
                    ExitString = Console.ReadLine();
                    Console.Clear();
                }
                while (ExitString != "exit");

                foreach (Doctor doctor in listDoctor)
                {
                    Console.WriteLine($"Doctor name: {doctor.Name} {doctor.LastName} \n Field of specilization:{doctor.Field}  " +
                        $"\n OIB:{doctor.OIB}\n");
                }
                Console.ReadLine();
            }
        }
    }
}


            


