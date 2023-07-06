using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using System.Text;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using BibliotecaVirtual.MVVM.Models;
using System.Windows.Input;
using System.Text.Json;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GraficosViewModel
    {
        private const string BaseUrl = "http://servidorvibe.somee.com/api";
        public ISeries[] Series { get; set; }

        List<int> Datos = new List<int> {};

        double[] datosGrafica = new double[]{0};

        private int contador = 0;
        private int año = 2001;
        private double valorB;
        private double valorM;
        private int valor_x;
        private int valor_y;
        public int ValorX
        {
            get { return valor_x; }
            set
            {
                if (valor_x != value)
                {
                    valor_x = value;
                }
            }
        }

        public int ValorY
        {
            get { return valor_y; }
            set
            {
                if (valor_y != value)
                {
                    valor_y = value;
                }
            }
        }

        public ICommand GuardarCommand { get; set; }
        public ICommand GraficarMCommand { get; set; }
        public ICommand GraficarBCommand { get; set; }

        public GraficosViewModel()
        {
            ValorX = año;
            GuardarCommand = new Command(GuardarDatos);
            GraficarMCommand = new Command(GraficarM);
            GraficarBCommand = new Command(GraficarB);
        }

        private void GuardarDatos()
        {
            Datos.Add(valor_y);
            var nuevosDatos = new double[] { valor_y };

            var listaTemporal = new List<double>(datosGrafica);
            listaTemporal.AddRange(nuevosDatos);
            datosGrafica = listaTemporal.ToArray();

            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = datosGrafica,
                    Fill = null
                }
            };

            ValorY = 0;
            contador++;
            ValorX++;
        }
        private void GraficarM()
        {
            Graficar(true);
        }

        private void GraficarB()
        {
            Graficar(false);
        }

        private async void Graficar(bool ope)
        {
            var url = $"{BaseUrl}/Graficos/Obtener";
            using (HttpClient client = new HttpClient())
            {
                var data = new Graficos
                {
                    contador = contador,
                    Datos = Datos,
                    Operacion = ope
                };
                var json = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, json);

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var valor = JsonSerializer.Deserialize<int>(jsonString);
                        await Application.Current.MainPage.DisplayAlert("Aviso", "El valor es: " + $"{valor}", "Aceptar");
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
