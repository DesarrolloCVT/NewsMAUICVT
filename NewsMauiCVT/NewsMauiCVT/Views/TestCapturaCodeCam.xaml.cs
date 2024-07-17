using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class TestCapturaCodeCam : ContentPage
{
    public TestCapturaCodeCam()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {

        base.OnAppearing();
        lblProducto.Text = string.Empty;
        lblCodPro.Text = string.Empty;

    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        DatosProductosCaptura dt = new DatosProductosCaptura();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            List<ProductosCapturaCod> pr = dt.DatosProductos(txtCodigo.Text);
            int con = pr.Count;

            if (con != 0)
            {
                foreach (var t in pr)
                {
                    lblProducto.Text = t.Producto.ToString();
                    lblCodPro.Text = t.Codigo.ToString();
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Codigo No Existe", "Aceptar");
                lblProducto.Text = string.Empty;
                lblCodPro.Text = string.Empty;

            }


        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

    }

    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}