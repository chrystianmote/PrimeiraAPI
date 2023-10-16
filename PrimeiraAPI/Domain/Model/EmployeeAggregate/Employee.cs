
namespace PrimeiraAPI.Domain.Model.EmployeeAggregate
{
    public class Employee

    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string? Photo { get; private set; }

        public Employee(string name, int age, string photo)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Age = age;
            this.Photo = photo;
        }
    }
}
