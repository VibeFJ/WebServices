using System;
using Sensores.MVVM.Models;
using Sensores.MVVM.ViewModels;
using Sensores.MVVM.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace Sensores.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PrincipalViewModel
    {
        private ContentView detallesView;
        public ContentView DetallesView
        {
            get => detallesView;
            set
            {
                if (detallesView != value)
                {
                    detallesView = value;
                }
            }
        }

        public ICommand CamaraCommand { get; }
        public ICommand PrestadosCommand { get; }
        public ICommand DevueltosCommand { get; }

        public PrincipalViewModel()
        {
            CamaraCommand = new Command(Camara);
            PrestadosCommand = new Command(Prestados);
            DevueltosCommand = new Command(Devueltos);

            Task.Run(async () =>
            {
                await Task.Delay(2000);
                Devueltos();
            });
        }

        private void Camara()
        {
            DetallesView = new CamaraView();
        }

        private void Prestados()
        {
            DetallesView = new LibrosView(this, 0);
        }

        private void Devueltos()
        {
            DetallesView = new LibrosView(this, 1);
        }
    }
}
