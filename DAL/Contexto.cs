using Aula.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Aula.DAL
{
    public class Contexto : System.Data.Entity.DbContext
    {

            public Contexto() : base("Contexto")
            {

            }

            public System.Data.Entity.DbSet<Curso> Cursos { get; set; }
            public System.Data.Entity.DbSet<Usuario> Usuarios { get; set; }
            public System.Data.Entity.DbSet<Rol> Rols { get; set; }
            public System.Data.Entity.DbSet<Usuario_Curso> Usuario_Cursos { get; set; }


            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //para que las tablas no se creen con sus nombres en plural
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        
    }
}