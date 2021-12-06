using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using _053501_Marat_LAB10_.Entities;
using _053501_Marat_LAB10_.Interfaces;

namespace ClassLibrary
{
    public class WrapperClass
    {
        FileService<Employee> _fileService = new FileService<Employee>();

        public void SaveData(IEnumerable<Employee> data, string filename)
        {
            _fileService.SaveData(data, filename);
        }

        public List<Employee> ReadFile(string fileName)
        {
            List<Employee> employeesToReturn;
            employeesToReturn = _fileService.ReadFile(fileName).ToList();
            return employeesToReturn;
        }
    }

    public class FileService<T> : IFileService<T> where T : Employee
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                var options = new JsonSerializerOptions {IncludeFields = true};
                var obj = JsonSerializer.DeserializeAsync<List<T>>(fileStream, options).Result;
                return obj;
            }
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                var options = new JsonSerializerOptions {IncludeFields = true};
                JsonSerializer.SerializeAsync<List<T>>(fileStream, data.ToList(), options).Wait();
            }
        }
    }
}