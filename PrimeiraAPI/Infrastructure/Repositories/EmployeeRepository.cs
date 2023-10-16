using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ConnectionContext _context;

        public EmployeeRepository(ConnectionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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
                    Id = b.Id,
                    NameEmployee = b.Name,
                    Photo = b.Photo

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
