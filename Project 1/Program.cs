using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    class Program
    {
        static Patient currentUser = null;
        static Appointment currentAppointment = null;
        static Random random = new Random();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Welcome to Hospital Management System ====");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.Write("Enter your choice (1/2): ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 2) break;

                Login();
                if (currentUser != null)
                {
                    ShowMainMenu();
                }
            }

            Console.WriteLine("Exiting the system. Have a nice day!");
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("==== Login ====");
            Console.Write("Enter ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = ReadPassword();

            // Assuming ID, name, and password are always correct for this exercise.
            currentUser = new Patient("Hospital A", "123 Main St", "General", 1, name, password, "Male", 25, "123 Patient St", 123456789);

            Console.WriteLine("\nLogin successful.");
            Console.ReadKey();
        }

        static string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return password.ToString();
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Main Menu ====");
                Console.WriteLine("1. Check User's Information");
                Console.WriteLine("2. Book Appointment");
                Console.WriteLine("3. Bill Payment");
                Console.WriteLine("4. Check Ongoing Appointment");
                Console.WriteLine("5. Logout");
                Console.Write("Enter your choice (1/2/3/4/5): ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 1)
                {
                    CheckUserInfo();
                }
                else if (choice == 2)
                {
                    ScheduleAppointment();
                }
                else if (choice == 3)
                {
                    BillPayment();
                }
                else if (choice == 4)
                {
                    CheckOngoingAppointment();
                }
                else if (choice == 5)
                {
                    currentUser = null;
                    currentAppointment = null;
                    return;
                }
            }
        }

        static void CheckUserInfo()
        {
            Console.Clear();
            Console.WriteLine("==== User Information ====");
            Console.WriteLine(currentUser.GetPatientInfo());
            Console.WriteLine("History: " + GenerateRandomHistory());

            Console.WriteLine("\n1. Edit Information");
            Console.WriteLine("2. Back to Main Menu");
            Console.Write("Enter your choice (1/2): ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice == 1)
            {
                EditUserInfo();
            }
        }

        static string GenerateRandomHistory()
        {
            string[] treatments = { "Checkup", "Vaccination", "Surgery", "Physical Therapy" };
            int count = random.Next(1, 5);
            string history = "";
            for (int i = 0; i < count; i++)
            {
                history += treatments[random.Next(treatments.Length)] + (i < count - 1 ? ", " : "");
            }
            return history;
        }

        static void EditUserInfo()
        {
            Console.Clear();
            Console.WriteLine("==== Edit User Information ====");
            Console.Write("Enter new Name: ");
            currentUser.Name = Console.ReadLine();
            Console.Write("Enter new Gender: ");
            currentUser.Gender = Console.ReadLine();
            Console.Write("Enter new Age: ");
            currentUser.Age = int.Parse(Console.ReadLine());
            Console.Write("Enter new Address: ");
            currentUser.PatientAddress = Console.ReadLine();
            Console.Write("Enter new Phone Number: ");
            currentUser.PhoneNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Information updated successfully.");
            Console.ReadKey();
        }

        static void ScheduleAppointment()
        {
            Console.Clear();
            Console.WriteLine("==== Schedule Appointment ====");

            Console.WriteLine("Choose your checkup type:");
            Console.WriteLine("1. General Checkup");
            Console.WriteLine("2. Follow-up Checkup");
            Console.WriteLine("3. Specialized Checkup");
            Console.Write("Enter your choice (1/2/3): ");

            int checkupChoice;
            if (!int.TryParse(Console.ReadLine(), out checkupChoice) || checkupChoice < 1 || checkupChoice > 3)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                Console.ReadKey();
                return;
            }

            string checkupType = "";
            string additionalQuestions = "";
            switch (checkupChoice)
            {
                case 1:
                    checkupType = "General Checkup";
                    additionalQuestions = AskGeneralCheckupQuestions();
                    break;
                case 2:
                    checkupType = "Follow-up Checkup";
                    additionalQuestions = AskFollowUpCheckupQuestions();
                    break;
                case 3:
                    checkupType = "Specialized Checkup";
                    additionalQuestions = AskSpecializedCheckupQuestions();
                    break;
            }

            Console.Write("Enter the date for the appointment (YYYY-MM-DD): ");
            string date = Console.ReadLine();
            Console.Write("Enter the time for the appointment (HH:MM): ");
            string time = Console.ReadLine();

            currentAppointment = new Appointment
            {
                PatientName = currentUser.Name,
                CheckupType = checkupType,
                AdditionalInfo = additionalQuestions,
                Date = date,
                Time = time
            };

            Console.WriteLine("\n==== Appointment Receipt ====");
            Console.WriteLine($"Patient Name: {currentAppointment.PatientName}");
            Console.WriteLine($"Checkup Type: {currentAppointment.CheckupType}");
            Console.WriteLine($"Additional Information: {currentAppointment.AdditionalInfo}");
            Console.WriteLine($"Appointment Date: {currentAppointment.Date}");
            Console.WriteLine($"Appointment Time: {currentAppointment.Time}");
            Console.WriteLine("Thank you for trusting our services!");

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        static string AskGeneralCheckupQuestions()
        {
            Console.Write("Do you have any ongoing symptoms? (yes/no): ");
            string symptoms = Console.ReadLine();
            string details = "";
            if (symptoms.ToLower() == "yes")
            {
                Console.Write("Please describe your symptoms in detail: ");
                details = Console.ReadLine();
            }
            return $"Ongoing Symptoms: {symptoms}. {details}";
        }

        static string AskFollowUpCheckupQuestions()
        {
            Console.Write("When was your last checkup? (date): ");
            string lastCheckupDate = Console.ReadLine();
            Console.Write("Any new symptoms since your last checkup? (yes/no): ");
            string newSymptoms = Console.ReadLine();
            string details = "";
            if (newSymptoms.ToLower() == "yes")
            {
                Console.Write("Please describe your new symptoms in detail: ");
                details = Console.ReadLine();
            }
            return $"Last Checkup Date: {lastCheckupDate}. New Symptoms: {newSymptoms}. {details}";
        }

        static string AskSpecializedCheckupQuestions()
        {
            Console.Write("Please specify the specialty for your checkup (e.g., Cardiology, Neurology): ");
            string specialty = Console.ReadLine();
            Console.Write("Any specific concerns or symptoms? (yes/no): ");
            string specificConcerns = Console.ReadLine();
            string details = "";
            if (specificConcerns.ToLower() == "yes")
            {
                Console.Write("Please describe your concerns or symptoms in detail: ");
                details = Console.ReadLine();
            }
            return $"Specialty: {specialty}. Specific Concerns: {specificConcerns}. {details}";
        }

        static void CheckOngoingAppointment()
        {
            Console.Clear();
            Console.WriteLine("==== Ongoing Appointment ====");
            if (currentAppointment == null)
            {
                Console.WriteLine("No ongoing appointment found.");
            }
            else
            {
                Console.WriteLine($"Patient Name: {currentAppointment.PatientName}");
                Console.WriteLine($"Checkup Type: {currentAppointment.CheckupType}");
                Console.WriteLine($"Additional Information: {currentAppointment.AdditionalInfo}");
                Console.WriteLine($"Appointment Date: {currentAppointment.Date}");
                Console.WriteLine($"Appointment Time: {currentAppointment.Time}");
            }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        static void BillPayment()
        {
            Console.Clear();
            Console.WriteLine("==== Bill Payment ====");

            // Generate hardcoded bill
            Console.WriteLine("Name\t\tQuantity\tPrice\t\tRemarks");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Medical1\t2\t\t$50\t\tConsultation");
            Console.WriteLine("Medical2\t1\t\t$100\t\tSurgery");
            Console.WriteLine("Medical3\t5\t\t$20\t\tMedication");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Total: $250");

            Console.WriteLine("\n1. Pay");
            Console.WriteLine("2. Return to Main Menu");
            Console.Write("Enter your choice (1/2): ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice == 1)
            {
                GenerateQRCode();
            }
        }

        static void GenerateQRCode()
        {
            Console.Clear();
            Console.WriteLine("==== QR Code ====");
            Console.WriteLine("Scanning QR Code...");

            // Simulate QR code generation
            Console.WriteLine("[ QR CODE ]");
            Console.WriteLine("\n1. Paid");
            Console.WriteLine("2. Return to Main Menu");
            Console.Write("Enter your choice (1/2): ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice == 1)
            {
                Console.WriteLine("Payment successful.");
                Console.ReadKey();
            }
        }
    }

    class Hospital
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public int Service { get; set; }

        public Hospital(string name, string address, string department, int service)
        {
            Name = name;
            Address = address;
            Department = department;
            Service = service;
        }

        public virtual string GetInfo()
        {
            return $"Hospital: {Name}\nAddress: {Address}\nDepartment: {Department}\nService: {Service}";
        }

        public virtual string GetDepartment()
        {
            return Department;
        }
    }

    class Patient : Hospital
    {
        public string Password { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PatientAddress { get; set; }
        public int PhoneNumber { get; set; }

        public Patient(string h_name, string h_address, string h_department, int h_service,
                       string patientName, string password, string gender, int age,
                       string patientAddress, int phoneNumber)
                       : base(h_name, h_address, h_department, h_service)
        {
            Name = patientName;
            Password = password;
            Gender = gender;
            Age = age;
            PatientAddress = patientAddress;
            PhoneNumber = phoneNumber;
        }

        public string GetPatientInfo()
        {
            return $"ID: {ID}\nName: {Name}\nGender: {Gender}\nAge: {Age}\nAddress: {PatientAddress}\nPhone Number: {PhoneNumber}";
        }

        public override string GetDepartment()
        {
            return $"Patient is in {base.GetDepartment()} department.";
        }
    }

    class Appointment
    {
        public string PatientName { get; set; }
        public string CheckupType { get; set; }
        public string AdditionalInfo { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
} 
