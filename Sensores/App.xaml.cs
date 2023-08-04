using Sensores.MVVM.Views;

namespace Sensores;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new PrincipalView();
    }
}
