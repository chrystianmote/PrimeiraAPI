using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PrimeiraAPI.Application.ViewModel;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Domain.Model.EmployeeAggregate;

namespace PrimeiraAPI.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/employee")]
    [ApiVersion("1.0")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepositary;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepositary, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepositary = employeeRepositary ?? throw new ArgumentNullException(nameof(employeeRepositary));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        //mandando em formato de texto json (swagger)
        //public IActionResult Add(EmployeeViewModel employeeView)
        //mandando em formato de formulario (swagger)
        //public IActionResult Add([FromForm]EmployeeViewModel employeeView)
        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            //Combina partes do diretório 'Storage/nomeFoto.jpg'
            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            // Gera o arquivo dentro do diretório gerado
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepositary.Add(employee);

            return Ok();
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Error, "Houve um Erro");

            //throw new Exception("Erro de Teste");

            var employees = _employeeRepositary.Get(pageNumber, pageQuantity);

            _logger.LogInformation("Teste");
            return Ok(employees);
        }


        //[Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Search(int id)
        {
            //Traz do Modo Original e mapeia o modelo original (entidade) pro tipo substituto(DTO)
            var employees = _employeeRepositary.Get(id);
            var employeesDTOs = _mapper.Map<EmployeeDTO>(employees);

            return Ok(employeesDTOs);
        }



        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(long id)
        {
            var employee = _employeeRepositary.Get(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.Photo);
            return File(dataBytes, "image/jpg");
        }

    }
}
