using System;
using Fivet.Dao;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fivet.ZeroIce
{
    /// <summary>
    /// The Implementation of the contratos. 
    /// </summary>
    public class ContratosImpl : ContratosDisp_
    {
        /// <summary>
        /// The Logger 
        /// </summary>
        private readonly ILogger<ContratosImpl> _logger;

        /// <summary>
        /// The Provider of DBcontext
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceScopeFactory"></param>
        public ContratosImpl( ILogger<ContratosImpl> logger, IServiceScopeFactory serviceScopeFactory){
            _logger = logger;
            _logger.LogDebug("Building the ContratosImpl..");
            _serviceScopeFactory = serviceScopeFactory;
            
            // Create The DataBase
            _logger.LogInformation("Creating the Database...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Database.EnsureCreated();
                fc.SaveChanges();
            }
            _logger.LogInformation("Done.");
        }

         /// <summary>
        /// Get The Persona.
        /// </summary>
        /// <param name="numero">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
         public override Persona obtenerPersona(string rut, Current current = null)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                Persona persona = fc.Personas.Find(rut);
                fc.SaveChanges();
                return persona;
            }
        }

        /// <summary>
        /// Get The Ficha.
        /// </summary>
        /// <param name="numero">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Ficha obtenerFicha(int numero, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                Ficha ficha = fc.Fichas.Find(numero);
                fc.SaveChanges();
                return ficha;
            }
        }
        /// <summary>
        /// Create the Ficha
        /// </summary>
        /// <param name="ficha">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Ficha crearFicha(Ficha ficha, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Fichas.Add(ficha);
                fc.SaveChanges();
                return ficha;
            }
        }
        /// <summary>
        /// Create the Persona
        /// </summary>
        /// <param name="persona">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Persona crearPersona(Persona persona, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Personas.Add(persona);
                fc.SaveChanges();
                return persona;
            }
        }
        /// <summary>
        /// Create the Control
        /// </summary>
        /// <param name="control">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Control crearControl(int numeroFicha, Control control, Current current)
        {
            using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Controles.Add(control);
                fc.SaveChanges();
                return control;
            }
        }
        /// <summary>
        /// Insert The Foto
        /// </summary>
        /// <param name="foto">to save</param>
        /// <param name="current">the context of ZeroIce</param>
        /// <returns></returns>
        public override Foto agregarFoto(Foto foto, Current current)
        {
           using (var scope = _serviceScopeFactory.CreateScope()){
                FivetContext fc = scope.ServiceProvider.GetService<FivetContext>();
                fc.Fotos.Add(foto);
                fc.SaveChanges();
                return foto;
            }
        }
        /// <summary>
        /// Delay
        /// </summary>
        /// <param name="clientTime"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }

       
    }
}