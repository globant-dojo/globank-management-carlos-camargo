using BankDojo.Models.Data;
using BankDojo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankDojo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovimientosController : Controller
    {
        private readonly IRepository<TblMovimiento> _repository;
        private readonly IRepository<TblCuentum> _repositoryCuentas;

        public MovimientosController(IRepository<TblMovimiento> repository, IRepository<TblCuentum> repositoryCuentas)
        {
            _repository = repository;
            _repositoryCuentas = repositoryCuentas;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movimiento contenido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                TblMovimiento _contenido = new TblMovimiento();

                _contenido.FechaMovimiento = Convert.ToDateTime(contenido.fechaMovimiento);
                _contenido.TipoMovimiento = contenido.tipoMovimiento;
                _contenido.Valor = contenido.valor;

                TblCuentum? _cuenta = (from cuenta in _repositoryCuentas.Get()
                                       where cuenta.NumeroCuenta == contenido.numeroCuenta
                                       select cuenta).FirstOrDefault();
                if (_cuenta != null)
                {
                    _contenido.IdCuentaFkNavigation = _cuenta;
                }
                else
                {
                    return NotFound("No se encontro la cuenta.");
                }

                if (_contenido.TipoMovimiento == "Retiro")
                {
                    _contenido.Valor = _contenido.Valor * -1;
                }
                _contenido.IdCuentaFkNavigation = _cuenta;

                TblMovimiento? mov = (from mv in _repository.Get()
                                      where mv.IdCuentaFk == _cuenta.IdCuenta
                                      select mv).LastOrDefault();
                decimal? saldo = 0;
                if (mov != null)
                {
                    saldo = mov.Saldo;
                }


                if (saldo != null)
                {
                    if (saldo > 0)
                    {
                        decimal val = (decimal)_contenido.Valor;
                        if (saldo >= Math.Abs(val))
                        {
                            _contenido.Saldo = saldo + (_contenido.Valor);
                            _repository.Add(_contenido);
                            _repository.Save();
                        }
                        else
                        {
                            return Problem("Saldo no disponible");
                        }
                    }
                    else
                    {
                        return Problem("Saldo no disponible");
                    }
                }
                else
                {
                    if (Convert.ToDecimal(_cuenta.SaldoInicial) >= Math.Abs(Convert.ToDecimal(_contenido.Valor)))
                    {
                        _contenido.Saldo = (Convert.ToDecimal(_cuenta.SaldoInicial) + Convert.ToDecimal(_contenido.Valor));
                        _repository.Add(_contenido);
                        _repository.Save();
                    }
                    else
                    {
                        return Problem("Saldo no disponible");
                    }

                }

                var queryOK = new
                {
                    resultado = "Movimiento realizado correctamente."
                };
                return Ok(queryOK);

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
                              select new
                              {
                                  Fecha = c.FechaMovimiento,
                                  Cliente = c.IdCuentaFkNavigation.IdClienteFkNavigation.IdPersonaFkNavigation.Nombre,
                                  NumeroCuenta = c.IdCuentaFkNavigation.NumeroCuenta,
                                  Tipo = c.IdCuentaFkNavigation.TipoCuenta,
                                  SaldoInicial = c.IdCuentaFkNavigation.SaldoInicial,
                                  Estado = c.IdCuentaFkNavigation.Estado,
                                  Movimiento = c.TipoMovimiento,
                                  SaldoDisponible = c.Saldo

                              };
                return Ok(content);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}
