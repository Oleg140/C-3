namespace _053501_Marat_LAB10_.Entities
{
    public class Employee
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public bool IsVaccinated { get; set; }

        public Employee(int age, string name, bool isVaccinated)
        {
            Age = age;
            Name = name;
            IsVaccinated = isVaccinated;
        }
    }
}