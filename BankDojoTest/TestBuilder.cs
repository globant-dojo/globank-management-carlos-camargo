using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDojoTest
{
    public class TestBuilder : IDisposable
    {
        protected HttpClient TestClient;
        public enum TipoUsuarioPrestamo { AFILIADO = 1, EMPLEADO, INVITADO }
        private bool Disposed;

        protected IntegrationTestBuilder()
        {
            BootstrapTestingSuite();
        }

        protected void BootstrapTestingSuite()
        {
            Disposed = false;
            var appFactory = new WebApplicationFactory<PruebaIngresoBibliotecario.Api.Startup>();
            TestClient = appFactory.CreateClient();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                TestClient.Dispose();
            }

            Disposed = true;
        }
    }
}
