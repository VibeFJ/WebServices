using BibliotecaVirtual.MVVM.Models;
using BibliotecaVirtual.MVVM.ViewModels;

namespace BibliotecaVirtual.MVVM.Views;

public partial class Login : ContentPage
{
	public Login()
	{
        InitializeComponent();
        BindingContext = new LoginViewModel(Navigation);
    }
}