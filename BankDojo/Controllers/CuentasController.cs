using BankDojo.Models.Data;
using BankDojo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankDojo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CuentasController : Controller
    {
        private readonly IRepository<TblCuentum> _repository;
        private readonly IRepository<TblCliente> _repositoryCliente;
        private readonly IRepository<TblPersona> _repositoryPersona;
        public CuentasController(IRepository<TblCuentum> repository, IRepository<TblCliente> repositoryCliente, IRepository<TblPersona> repositoryPersona)
        {
            _repository = repository;
            _repositoryCliente = repositoryCliente;
            _repositoryPersona = repositoryPersona;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TblCuentum contenido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                TblCliente? c = (from p in _repositoryPersona.Get()
                               join cl in _repositoryCliente.Get() on p.IdPersona equals cl.IdPersonaFk
                               where p.Nombre == contenido.IdClienteFkNavigation.IdPersonaFkNavigation.Nombre
                               select cl).FirstOrDefault();
                if (c != null)
                {
                    contenido.IdClienteFk = c.IdCliente;
                    contenido.IdClienteFkNavigation = c;
                    contenido.IdClienteFkNavigation.IdPersonaFkNavigation = c.IdPersonaFkNavigation;
                }
                _repository.Add(contenido);
                _repository.Save();
                var queryOK = new
                {
                    resultado = "Cuenta creada correctamente."
                };
                return Ok(queryOK);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{numeroCuenta}")]
        public IActionResult Update(string numeroCuenta, [FromBody] TblCuentum contenido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                TblCuentum? obj = (from cuenta in _repository.Get()
                                  where cuenta.NumeroCuenta == numeroCuenta
                                  select cuenta).FirstOrDefault();
                if (obj != null)
                {
                    contenido.NumeroCuenta = numeroCuenta;
                    _repository.Update(contenido);
                    var queryOK = new
                    {
                        id = 1,
                        resultado = "Cliente fue actualizado correctamente."
                    };
                    return Ok(queryOK);
                }
                else
                {
                    return NotFound("No se encontro la cuenta.");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
      
                var content = from c in _repository.Get()
                              join cl in _repositoryCliente.Get() on c.IdClienteFk equals cl.IdCliente
                              join p in _repositoryPersona.Get() on cl.IdPersonaFk equals p.IdPersona
                              select new
                              {
                                  Id = c.NumeroCuenta,
                                  TipoCuenta = c.TipoCuenta,
                                  Telefono = c.SaldoInicial,
                                  Estado = c.Estado,
                                  Cliente = p.Nombre
                              };
                return Ok(content);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpDelete("{numeroCuenta}")]
        public IActionResult Detele(string numeroCuenta)
        {
            try
            {
                TblCuentum? obj = (from cuenta in _repository.Get()
                                   where cuenta.NumeroCuenta == numeroCuenta
                                   select cuenta).FirstOrDefault();
                if (obj != null)
                {
                    _repository.Delete(obj.IdCuenta);
                    return Ok("La cuenta fue eliminada correctamente.");
                }
                else
                {
                    return NotFound("No se encontro la cuenta");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

