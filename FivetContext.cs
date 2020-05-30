using System.Reflection;
using Fivet.ZeroIce.model;
using Microsoft.EntityFrameworkCore;

namespace Fivet.Dao
{
    /// <summary>
    ///  The Connection to the FivetDataBase
    /// </summary>
    public class FivetContext: DbContext
    {   
        /// <summary>
        ///  The Connection to the DataBase
        /// </summary>
        /// <value></value>
        public DbSet<Persona> Personas {get;set;}
        public DbSet<Ficha> Fichas {get;set;}
        public DbSet<Control> Controles {get;set;}
        public DbSet<Foto> Fotos {get;set;}
        public DbSet<Examen> Examenes {get;set;}
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SqlLite.
            optionsBuilder.UseSqlite("Data Source = fivet.db",options=>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        ///  Create The ER from  Entity.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   // Update The Model
            modelBuilder.Entity<Persona>(p =>
            {
                // Primary Key
                p.HasKey(p=> p.uid);
                // Index in Email
                p.Property(p=> p.email).IsRequired();
                p.HasIndex(p=> p.email).IsUnique();
            });

            // Insert The Data
            modelBuilder.Entity<Persona>().HasData(
                new Persona()
                {
                    uid = 1,
                    nombre ="Martin",
                    rut ="19.396.034-0",
                    apellido="Osorio",
                    direccion="Villa Dulce #6969",
                    email = "mob010@alumnos.ucn.cl"
                }
            );

            
        }
    }   
}