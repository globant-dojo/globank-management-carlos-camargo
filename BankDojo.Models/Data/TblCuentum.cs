using System;
using System.Collections.Generic;

namespace BankDojo.Models.Data
{
    public partial class TblCuentum
    {
        public TblCuentum()
        {
            TblMovimientos = new HashSet<TblMovimiento>();
        }

        public int IdCuenta { get; set; }
        public int IdClienteFk { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public decimal? SaldoInicial { get; set; }
        public bool? Estado { get; set; }

        public virtual TblCliente IdClienteFkNavigation { get; set; } = null!;
        public virtual ICollection<TblMovimiento> TblMovimientos { get; set; }
    }
}
