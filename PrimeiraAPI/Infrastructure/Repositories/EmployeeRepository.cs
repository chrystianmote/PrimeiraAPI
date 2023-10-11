using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ConnectionContext _context = new();

        //POST Cadastro
        void IEmployeeRepository.Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        //GET Todos
        List<EmployeeDTO> IEmployeeRepository.Get(int pageNumber, int pageQuantity)
        {

            //Usa o Model Employee no Entity mas através do Método Select posso retorna outro modelo DTO
            //(Objeto de Tranferência de Dados)
            return _context.Employees.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(b => new EmployeeDTO()
                {
                    Id = b.id,
                    NameEmployee = b.name,
                    Photo = b.photo

                })
                .ToList();
        }

        //GET id
        public Employee? Get(long id)
        {
            return _context.Employees.Find(id);
        }
    }
}
