using BibliotecaVirtual.MVVM.Models;
using Bogus;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace BibliotecaVirtual.Repositories
{
    public class CustomRepository
    {
        public SQLiteConnection conexion;
        public string EstatusMensaje { get; set; }

        public CustomRepository()
        {
            var faker = new Faker("es");

            conexion = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);

            if (!TablasExisten())
            {
                conexion.CreateTable<Usuario>();
            }
        }

        public bool TablasExisten()
        {
            var tablasExistentes = conexion.GetTableInfo("Usuario").Any();

            return tablasExistentes;
        }
    }
}
