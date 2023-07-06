using BibliotecaVirtual.MVVM.ViewModels;

namespace BibliotecaVirtual.MVVM.Views;

public partial class Principal : FlyoutPage
{
	public Principal()
	{
		InitializeComponent();
        BindingContext = new PrincipalViewModel();
    }
}