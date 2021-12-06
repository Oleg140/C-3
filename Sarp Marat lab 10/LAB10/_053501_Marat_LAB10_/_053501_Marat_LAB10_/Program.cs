using System;
using System.Collections.Generic;
using System.Reflection;
using _053501_Marat_LAB10_.Entities;

namespace _053501_Marat_LAB10_
{
    class Program
    {
        static void Main()
        {
            const string jsonPath =
                "/Users/lnxd/Desktop/BSUIR/THIRD TERM/ISP/LABS/LAB10/_053501_Marat_LAB10_/json.json";

            const string dllLibPath =
                "/Users/lnxd/Desktop/BSUIR/THIRD TERM/ISP/LABS/LAB10/_053501_Marat_LAB10_/ClassLibrary/bin/Debug/net5.0/ClassLibrary.dll";

            List<Employee> employees = new List<Employee>();

            employees.Add(new Employee(23, "Vasya", true));
            employees.Add(new Employee(33, "Gena", true));
            employees.Add(new Employee(53, "Vitya", false));
            employees.Add(new Employee(45, "Vova", true));
            employees.Add(new Employee(54, "Stas", true));

            IEnumerable<Employee> data = employees;

            Assembly asm = Assembly.LoadFrom(dllLibPath);

            Type[] test = asm.GetTypes(); // test all types

            var type = asm.GetType("ClassLibrary.FileService`1", true, true)
                .MakeGenericType(typeof(Employee));

            object instance = Activator.CreateInstance(type);

            var temp = type.GetMethods(); // test all methods

            MethodInfo saveDataMethod = type.GetMethod("SaveData");
            MethodInfo readFileMethod = type.GetMethod("ReadFile");

            saveDataMethod?.Invoke(instance, new Object[] {data, jsonPath});

            var result = readFileMethod.Invoke(instance, new object[] {jsonPath});
            var resultAsList = result as List<Employee>;

            if (resultAsList != null)
                foreach (var employee in resultAsList)
                {
                    Console.WriteLine(
                        $"Age: {employee.Age.ToString()}, Name: {employee.Name}, IsVaccianted: {employee.IsVaccinated.ToString()}");
                }
        }
    }
}