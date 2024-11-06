using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class AsignacionPedidos : ContentPage
{
	public AsignacionPedidos()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        ClearComponent();   
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LogUsabilidad("Ingreso");
        SetFocusText();
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtPallet.Focus();
        });
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
        lblError.Text = string.Empty;
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 0;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private async void txtEdit_Completed(object sender, EventArgs e)
    {
        DatosPallets dp = new DatosPallets();
        List<PalletClass> list = dp.ObtieneInfoPallet(txtPallet.Text);

        if (list.Count > 0)
        {
            await Navigation.PushAsync(new AsignacionPedidosDetalle());
        }
        else
        {
            lblError.Text = "El pallet ingresado no existe";
            txtPallet.Text = string.Empty;
            SetFocusText();
        }
    }
}