using System;
using System.Collections.Generic;

namespace BankDojo.Models.Data
{
    public partial class TblPersona
    {
        public TblPersona()
        {
            TblClientes = new HashSet<TblCliente>();
        }

        public int IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<TblCliente> TblClientes { get; set; }
    }
}
