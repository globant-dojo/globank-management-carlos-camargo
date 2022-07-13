using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDojo.Models.Data
{
    public class ClienteLegacy
    {

        public string? Nombre { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Contrasena { get; set; }
        public bool Estado { get; set; }

    }
}
