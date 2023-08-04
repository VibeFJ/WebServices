using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sensores.MVVM.Models;
using Sensores.MVVM.Views;
using System.Linq;
using System.Text;
using PropertyChanged;
using System.Text.Json;
using Camera.MAUI;
using ZXing.Net.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Sensores.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CamaraViewModel
    {
        private const string BaseUrl = "https://servidorvibe.somee.com/api";
        public bool validacion = true;

        private string codigo;
        private string resultado;
        public string Resultado
        {
            get => resultado;
            set
            {
                if (resultado != value)
                {
                    resultado = value;
                }
            }
        }

        private string envio;
        public string Envio
        {
            get => envio;
            set
            {
                if (envio != value)
                {
                    envio = value;
                }
            }
        }

        public CamaraViewModel()
        {

        }

        public void Actualizar(int estatus)
        {
            envioDatos(codigo, estatus);
        }

        public Task InicializarCamaraAsync(CameraView camara)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await camara.StopCameraAsync();
                await camara.StartCameraAsync();
            });
            return Task.CompletedTask;
        }

        public void BarcodeDetected(Camera.MAUI.ZXingHelper.BarcodeEventArgs QR)
        {
            codigo = QR.Result[0].Text;
            revision(codigo);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Resultado = $"Contenido: {codigo}";
            });
        }

        public async void revision(string codigoLibro)
        {
            var url = $"{BaseUrl}/Libros/Obtener";
            using (var client = new HttpClient())
            {
                var data = new Libros{ Estatus = 2 };
                var json = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                var respuestaAPI = await client.PostAsync(url, json);

                try
                {
                    if (respuestaAPI.IsSuccessStatusCode)
                    {
                        string jsonString = await respuestaAPI.Content.ReadAsStringAsync();
                        var Libros = JsonSerializer.Deserialize<Libros[]>(jsonString);
                        foreach(var libro in Libros )
                        {
                            if(libro.CodigoLibro == codigoLibro)
                            {
                                if (libro.Estatus == 1)
                                {
                                    Actualizar(0);
                                }
                                else
                                {
                                    Actualizar(1);
                                }
                            }
                        }
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

        public async void envioDatos(string codigoLibro, int estatus)
        {
            var url = $"{BaseUrl}/Libros/Actualizar";
            using (var client = new HttpClient())
            {
                var data = new Libros
                {
                    CodigoLibro = codigoLibro,
                    Estatus = estatus
                };
                var json = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                var respuesta = await client.PutAsync(url, json);

                try
                {
                    if (respuesta.IsSuccessStatusCode)
                    {
                        var jsonString = await respuesta.Content.ReadAsStringAsync();
                        Console.WriteLine(jsonString.ToString());
                        var estado = JsonSerializer.Deserialize<bool>(jsonString);
                        if (estado)
                        {
                            Envio = "Actualizado correctamente";
                        }
                        else
                        {
                            Envio = "Error al Actualizar";
                        }
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
