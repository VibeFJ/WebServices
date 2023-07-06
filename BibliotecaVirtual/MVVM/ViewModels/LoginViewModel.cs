using PropertyChanged;
using BibliotecaVirtual.MVVM.Models;
using BibliotecaVirtual.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BibliotecaVirtual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        private INavigation Navegacion;
        private string nombreUsuario;
        private string contraseña;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                if (nombreUsuario != value)
                {
                    nombreUsuario = value;
                }
            }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                if (contraseña != value)
                {
                    contraseña = value;
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

        public ICommand LoginCommand { get; private set; }
        public ICommand RegistroCommand { get; private set; }

        public LoginViewModel(INavigation Navigation)
        {
            Navegacion = Navigation;
            LoginCommand = new Command(Login);
            RegistroCommand = new Command(Formulario);
        }

        private void Login()
        {
            if (ValidarLogin(NombreUsuario, Contraseña))
            {
                Application.Current.MainPage = new Principal();
            }
            else
            {
                MensajeError = "Usuario y/o Contraseña Incorrectos";
            }
        }

        private void Formulario()
        {
            Navegacion.PushAsync(new Formulario());
        }

        private bool ValidarLogin(string username, string password)
        {
            var usuario = App.CustomerRepo.conexion.Table<Usuario>().FirstOrDefault(u => u.NombreUsuario == username && u.Contraseña == password);
            return usuario != null;
        }
    }
}
