using BibliotecaVirtual.Repositories;

namespace BibliotecaVirtual;

public partial class App : Application
{
	public static CustomRepository CustomerRepo { get; private set; }
	public App(CustomRepository repo)
	{
		InitializeComponent();
		CustomerRepo = repo;
		MainPage = new AppShell();
	}
}
