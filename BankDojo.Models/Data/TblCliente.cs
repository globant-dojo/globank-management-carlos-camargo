using System;
using System.Collections.Generic;

namespace BankDojo.Models.Data
{
    public partial class TblCliente
    {
        public TblCliente()
        {
            TblCuenta = new HashSet<TblCuentum>();
        }

        public int IdCliente { get; set; }
        public int IdPersonaFk { get; set; }
        public string? Contrasena { get; set; }
        public bool Estado { get; set; }

        public virtual TblPersona IdPersonaFkNavigation { get; set; } = null!;
        public virtual ICollection<TblCuentum> TblCuenta { get; set; }
    }
}
