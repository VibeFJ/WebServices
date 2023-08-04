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
    public class ContenidoViewModel
    {
        public PrincipalViewModel principal;

        private const string BaseUrl = "https://servidorvibe.azurewebsites.net/api";

        public bool validacion = true;

        private ObservableCollection<Contenido> _cvm;
        public ObservableCollection<Contenido> CVM
        {
            get { return _cvm; }
            set
            {
                _cvm = value;
            }
        }

        public ICommand CapitulosCommand { get; }

        public ContenidoViewModel(int id)
        {
            if (validacion)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(500);
                    Mostrar();
                });
            }

            CapitulosCommand = new Command<int>(Capitulos);
        }

        private void Capitulos(int id)
        {
            //foreach (Contenido serie in SVM)
            //{
            //    if (serie.SerieId == id)
            //    {
            //        principal.DetallesView = new LibrosView(serie.Nombre, id, principal);
            //        break;
            //    }
            //}
        }

        public async Task<Contenido[]> ObtenerSeries()
        {
            var url = $"{BaseUrl}/Series/Obtener";
            using (HttpClient client = new HttpClient())
            {
                var data = new { };
                var json = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, json);

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        Contenido[] series = JsonSerializer.Deserialize<Contenido[]>(jsonString);
                        return series;
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

        public async void Mostrar()
        {
            validacion = false;
            CVM = new ObservableCollection<Contenido>();

            Contenido[] Series = await ObtenerSeries();
            foreach (var serie in Series)
            {
                CVM.Add(new Contenido()
                {
                    SerieId = serie.SerieId,
                    Nombre = serie.Nombre,
                    Portada = serie.Portada,
                    Temporadas = serie.Temporadas
                });
            }
        }
    }
}
