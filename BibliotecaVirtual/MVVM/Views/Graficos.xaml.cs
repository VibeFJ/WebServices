using BibliotecaVirtual.MVVM.ViewModels;
using LiveChartsCore;

namespace BibliotecaVirtual.MVVM.Views;

public partial class Graficos : ContentView
{
    public Graficos()
	{
		InitializeComponent();
        BindingContext = new GraficosViewModel();
    }

}