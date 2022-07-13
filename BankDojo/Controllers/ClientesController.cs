using BankDojo.Models.Data;
using BankDojo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankDojo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IRepository<TblCliente> _repository;
        private readonly IRepository<TblPersona> _repositoryPersona;
        public ClientesController(IRepository<TblCliente> repository, IRepository<TblPersona> repositoryPersona)
        {
            _repository = repository;
            _repositoryPersona = repositoryPersona;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClienteLegacy contenido)
        {
            try
            {
                TblPersona p = new TblPersona();
                p.Nombre = contenido.Nombre;
                p.Direccion = contenido.Direccion;
                p.Telefono = contenido.Telefono;
                _repositoryPersona.Add(p);
                _repositoryPersona.Save();

                TblCliente c = new TblCliente();
                c.Contrasena = contenido.Contrasena;
                c.Estado = Convert.ToBoolean(contenido.Estado);
                c.IdPersonaFk = (from _p in _repositoryPersona.Get()
                                 where _p.Nombre == contenido.Nombre
                                 select _p.IdPersona).FirstOrDefault();

                _repository.Add(c);
                _repository.Save();
                var queryOK = new
                {
                    id = 1,
                    resultado = "Cliente creado correctamente."
                };
                return Ok(queryOK);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] TblCliente contenido)
        {
            try
            {
                TblCliente? obj = _repository.Get(id);
                if (obj != null)
                {
                    contenido.IdCliente = id;
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
                    return NotFound();
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
                              join p in _repositoryPersona.Get() on c.IdPersonaFk equals p.IdPersona
                                select new
                                {
                                    Id = c.IdCliente,
                                    Nombre = p.Nombre,
                                    Telefono = p.Telefono,
                                    Contrasena = c.Contrasena,
                                    Estado = c.Estado
                                };
                return Ok(content);
            }
            catch(Exception ex) { 
                return Problem(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Detele(int id)
        {
            try
            {
                TblCliente? obj = _repository.Get(id);
                if (obj != null)
                {
                    _repository.Delete(id);
                    var queryOK = new
                    {
                        id = 1,
                        resultado = "Cliente fue eliminado correctamente."
                    };
                    return Ok(queryOK);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
