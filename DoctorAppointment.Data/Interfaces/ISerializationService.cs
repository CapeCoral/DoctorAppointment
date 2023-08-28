using System;
namespace MyDoctorAppointment.Data.Interfaces
{
	public interface ISerializationService
	{
		void Serialize<T>(string path, T data);

		T Deserialize<T>(string path);
	}
}

