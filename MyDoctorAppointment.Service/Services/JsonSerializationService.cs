using System;
using Newtonsoft.Json;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Data.Interfaces;

namespace MyDoctorAppointment.Service.Services
{
	public class JsonSerializationService : ISerializationService
	{
        public T Deserialize<T>(string path)
        {
            var json = File.ReadAllText(path);
            return (T)JsonConvert.DeserializeObject<T>(json);
        }

        public void Serialize<T>(string path, T data)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}

