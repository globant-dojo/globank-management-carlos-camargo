using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDojo.Models.Data
{
    public class Movimiento
    {
        public string? fechaMovimiento { get; set; }
        public string? tipoMovimiento { get; set; }
        public decimal valor { get; set; }
        public string? numeroCuenta { get; set; }
        public string? tipoCuenta { get; set; }
        public decimal saldoInicial { get; set; }
        public string? estado { get; set; }

    }
}
