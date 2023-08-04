using Sensores.MVVM.ViewModels;

namespace Sensores.MVVM.Views;

public partial class PrincipalView : FlyoutPage
{
	public PrincipalView()
	{
		InitializeComponent();
        BindingContext = new PrincipalViewModel();
    }
}