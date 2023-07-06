using BibliotecaVirtual.MVVM.ViewModels;

namespace BibliotecaVirtual.MVVM.Views;

public partial class Formulario : ContentPage
{
    public Formulario()
	{
		InitializeComponent();
        BindingContext = new FormularioViewModel();
	}
}