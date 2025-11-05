using System;
using System.IO;
using System.Text.Json;
using FinalTask.Core.Interfaces;

namespace FinalTask.Core.Services
{
    public class FileSystemSaveLoadService<T> : ISaveLoadService<T>
    {
        private readonly string _path;

        public FileSystemSaveLoadService(string path)
        {
            _path = path;
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
        }

        public void SaveData(T data, string identifier)
        {
            var filePath = Path.Combine(_path, identifier + ".txt");
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }

        public T LoadData(string identifier)
        {
            var filePath = Path.Combine(_path, identifier + ".txt");
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
