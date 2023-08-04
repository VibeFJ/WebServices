using Sensores.MVVM.ViewModels;

namespace Sensores.MVVM.Views;

public partial class LibrosView : ContentView
{
	public LibrosView(PrincipalViewModel principal, int estatus)
	{
		InitializeComponent();
        BindingContext = new LibrosViewModel(principal, estatus);
    }

    private bool isHorizontal;

    public bool IsHorizontal
    {
        get { return isHorizontal; }
        set
        {
            isHorizontal = value;
            UpdateSpan();
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        IsHorizontal = width > height;
    }

    private void UpdateSpan()
    {
        if (IsHorizontal)
        {
            gridLayout.Span = 4;
        }
        else
        {
            gridLayout.Span = 2;
        }
    }
}