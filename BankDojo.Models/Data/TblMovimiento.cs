using System;
using System.Collections.Generic;

namespace BankDojo.Models.Data
{
    public partial class TblMovimiento
    {
        public int IdMovimiento { get; set; }
        public int IdCuentaFk { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public string? TipoMovimiento { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Saldo { get; set; }

        public virtual TblCuentum IdCuentaFkNavigation { get; set; } = null!;
    }
}
