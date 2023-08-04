using Sensores.MVVM.ViewModels;

namespace Sensores.MVVM.Views;

public partial class CamaraView : ContentView
{
    private readonly CamaraViewModel viewModel;

    public CamaraView()
    {
        InitializeComponent();
        viewModel = new CamaraViewModel();
        BindingContext = viewModel;
    }

    //C:\DRATENZ\CURSO MAUI\Sensores\obj\Debug\net7.0-android\AndroidManifest.xml

    private async void OnCameraViewLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            await viewModel.InicializarCamaraAsync(cameraView);
        }
    }

    private void OnBarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        viewModel.BarcodeDetected(args);
    }
}