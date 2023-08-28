using System.Text;
using System.Xml;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        public string AppSettings { get; private set; }

        public ISerializationService SerializationService { get; private set; }

        public abstract string Path { get; set; }

        public abstract int LastId { get; set; }

        public GenericRepository(string appSettings, ISerializationService serializationService)
        {
            AppSettings = appSettings;
            SerializationService = serializationService;
        }

        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Append(source), Formatting.Indented));
            SerializationService.Serialize(Path, GetAll().Append(source));

            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Where(x => x.Id != id), Formatting.Indented));
            SerializationService.Serialize(Path, GetAll().Where(x => x.Id != id));

            return true;
        }
        
        public IEnumerable<TSource> GetAll()
        {
            /*
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "[]");
            }

            var json = File.ReadAllText(Path);

            if (string.IsNullOrWhiteSpace(json))
            {
                File.WriteAllText(Path, "[]");
                json = "[]";
            }

            return JsonConvert.DeserializeObject<List<TSource>>(json)!;
            */

            return SerializationService.Deserialize<List<TSource>>(Path);
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            //File.WriteAllText(Path, JsonConvert.SerializeObject(GetAll().Select(x => x.Id == id ? source : x), Formatting.Indented));
            SerializationService.Serialize(Path, GetAll().Select(x => x.Id == id ? source : x));

            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();

        protected Repository ReadFromAppSettings()
        {
            //var json = File.ReadAllText(Constants.AppSettingsPath);
            //return JsonConvert.DeserializeObject<dynamic>(json);
            //

            return SerializationService.Deserialize<Repository>(AppSettings);
        }
    }
}
