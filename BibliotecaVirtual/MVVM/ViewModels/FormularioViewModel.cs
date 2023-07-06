using Microsoft.Maui.Controls;
using PropertyChanged;
using BibliotecaVirtual.MVVM.Models;
using BibliotecaVirtual.MVVM.Views;
using System.Windows.Input;
using Command = Microsoft.Maui.Controls.Command;
using System;
using System.Threading.Channels;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class FormularioViewModel
    {
        public INavigation navegation;
        private Usuario ObjUsuario;
        private string confirmarcontraseña;

        public Usuario Objeto
        {
            get { return ObjUsuario; }
            set
            {
                if (ObjUsuario != value)
                {
                    ObjUsuario = value;
                }
            }
        }

        public string ConfirmarContraseña
        {
            get { return confirmarcontraseña; }
            set
            {
                if (confirmarcontraseña != value)
                {
                    confirmarcontraseña = value;
                }
            }
        }

        private string mensajeError;
        public string MensajeError
        {
            get { return mensajeError; }
            set
            {
                if (mensajeError != value)
                {
                    mensajeError = value;
                }
            }
        }

        public ICommand AgregarUsuarioCommand { get; set; }

        public FormularioViewModel()
        {
            ObjUsuario = new Usuario();
            AgregarUsuarioCommand = new Command(AgregarUsuario);
        }

        static bool Validar(Usuario usuario, string confirmar)
        {
            return usuario.Nombre != null &&
                   usuario.ApellidoPaterno != null &&
                   usuario.ApellidoMaterno != null &&
                   usuario.NombreUsuario != null &&
                   usuario.Contraseña != null &&
                   confirmar != null &&
                   usuario.Contraseña == confirmar;
        }

        private void AgregarUsuario()
        {
            if (Validar(ObjUsuario, confirmarcontraseña))
            {
                try
                {
                    bool usuarioExistente = App.CustomerRepo.conexion.Table<Usuario>().Any(u => u.NombreUsuario == ObjUsuario.NombreUsuario);
                    if (usuarioExistente)
                    {
                        Application.Current.MainPage.DisplayAlert("Advertencia", "Nombre de Usuario Inválido", "Aceptar");
                    }
                    else
                    {
                        App.CustomerRepo.conexion.Insert(ObjUsuario);
                        Application.Current.MainPage.DisplayAlert("Bienvenido", $"{ObjUsuario.NombreUsuario} tu registro fue Exitoso", "Aceptar");

                        var usuarios = App.CustomerRepo.conexion.Table<Usuario>().ToList();

                        foreach (var usuario in usuarios)
                        {
                            Console.WriteLine($"ID: {usuario.UsuarioId}" + $" Nombre: {usuario.Nombre}" + $" Apellido Paterno: {usuario.ApellidoPaterno}");
                            Console.WriteLine($"Apellido Materno: {usuario.ApellidoMaterno}" + $" Correo: {usuario.NombreUsuario}" + $" Contraseña: {usuario.Contraseña}");
                            Console.WriteLine();
                        }
                        
                        Application.Current.MainPage = new Principal();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Advertencia", "Por favor llene todos los campos", "Aceptar");
            }
        }
    }
}
