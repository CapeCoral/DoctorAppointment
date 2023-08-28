using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment(string appSettings, ISerializationService serializationService)
        {
            _doctorService = new DoctorService(appSettings, serializationService);
        }

        public void Menu()
        {
            Console.WriteLine("Current doctros list: ");
            var docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }

            Console.WriteLine("Adding doctor: ");

            var newDoctor = new Doctor
            {
                Name = "Vasya",
                Surname = "Petrov",
                Experiance = 20,
                DoctorType = Domain.Enums.DoctorTypes.Dentist
            };

            _doctorService.Create(newDoctor);

            Console.WriteLine("Current doctros list: ");
            docs = _doctorService.GetAll();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.Name);
            }
        }
    }


    public static class Program
    {
        public static void Main()
        {
            string basePath = AppContext.BaseDirectory;
            Console.WriteLine(basePath);

            Console.WriteLine("Виберите куда сохранять/откуда считывать данные:" +
                "1 - XML файл, 2 - JSON файл");

            string choice = Console.ReadLine();

            DoctorAppointment doctorAppointment = null;

            while (true)
            {
                if (choice.Equals("1"))
                {
                    doctorAppointment = new DoctorAppointment(Constants.XmlAppSettingsPath, new XmlSerializationService());
                    break;
                }
                else if (choice.Equals("2"))
                {
                    doctorAppointment = new DoctorAppointment(Constants.JsonAppSettingsPath, new JsonSerializationService());
                    break;
                }
                else
                {
                    Console.WriteLine("Не корректный ввод");
                    choice = Console.ReadLine();
                }
            }

            doctorAppointment.Menu();

            Console.ReadKey();
        }
    }
}
