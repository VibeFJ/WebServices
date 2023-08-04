using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sensores.MVVM.Models;
using Sensores.MVVM.Views;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Text.Json;
using System.Windows.Input;

namespace Sensores.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LibrosViewModel
    {
        public PrincipalViewModel principal;
        private const string BaseUrl = "https://servidorvibe.somee.com/api";

        public bool validacion = true;

        private ObservableCollection<Libros> _lvm;
        public ObservableCollection<Libros> LVM
        {
            get { return _lvm; }
            set
            {
                _lvm = value;
            }
        }

        public ICommand LibroCommand { get; }

        public LibrosViewModel(PrincipalViewModel principal, int estatus)
        {
            this.principal = principal;
            if (validacion)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    Mostrar(estatus);
                });
            }

            LibroCommand = new Command<int>(Libro);
        }
        private void Libro(int id)
        {
            principal.DetallesView = new ContenidoView(id);
        }

        public async void Mostrar(int estatus)
        {
            validacion = false;
            LVM = new ObservableCollection<Libros>();

            Libros[] Libros = await ObtenerLibros(estatus);
            foreach (var libro in Libros)
            {
                LVM.Add(new Libros()
                {
                    LibroId = libro.LibroId,
                    Titulo = libro.Titulo,
                    Caratula = libro.Caratula,
                    Autor = libro.Autor,
                    AñoPublicacion = libro.AñoPublicacion
                });
            }
        }

        public async Task<Libros[]> ObtenerLibros(int estatus)
        {
            var url = $"{BaseUrl}/Libros/Obtener";
            using (HttpClient client = new HttpClient())
            {
                var data = new { Estatus = estatus };
                var json = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                var respuesta = await client.PostAsync(url, json);

                try
                {
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string jsonString = await respuesta.Content.ReadAsStringAsync();
                        var Libros = JsonSerializer.Deserialize<Libros[]>(jsonString);
                        return Libros;
                    }
                    else
                    {
                        throw new Exception("Error al conectar con la  API.");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex);
                }
            }
        }
    }
}
