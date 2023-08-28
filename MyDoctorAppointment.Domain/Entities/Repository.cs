using System;
namespace MyDoctorAppointment.Domain.Entities
{
    public class Appointments
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }

    public class Database
    {
        public Doctors Doctors { get; set; }
        public Patients Patients { get; set; }
        public Appointments Appointments { get; set; }
    }

    public class Doctors
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }

    public class Patients
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }

    public class Repository
	{
        public Database Database { get; set; }
    }
}

