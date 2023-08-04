using Sensores.MVVM.ViewModels;

namespace Sensores.MVVM.Views;

public partial class ContenidoView : ContentView
{
    public ContenidoView(int id)
    {
        InitializeComponent();
        BindingContext = new ContenidoViewModel(id);
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