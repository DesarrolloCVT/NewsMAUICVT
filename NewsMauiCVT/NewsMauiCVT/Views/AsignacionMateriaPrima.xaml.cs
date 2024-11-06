using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrima : ContentPage
{
	public AsignacionMateriaPrima()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
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
        lblResultado.Text = string.Empty;
    }
    public class Datos
    {
        public string data { get; set; }
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
    private async void txtPallet_Completed(object sender, EventArgs e)
    {
        DatosPallets dp = new DatosPallets();
        List<PalletClass> list = dp.ObtieneInfoPallet(txtPallet.Text);

        if (list.Count > 0)
        {
            Console.WriteLine("txtPallet_Completed: Pallet valido");
            await Navigation.PushAsync(new AsignacionMateriaPrimaDetalle());
        }
        else
        {
            Console.WriteLine("txtPallet_Completed: Pallet invalido");
            txtPallet.Text = string.Empty;
            lblResultado.TextColor = Colors.Red;
            lblResultado.Text = "El Pallet ingresado no es válido";
            lblResultado.IsVisible = true;
        }

    }
}