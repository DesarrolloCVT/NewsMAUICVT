using NewsMauiCVT.Datos;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrimaDetalle : ContentPage
{
	public AsignacionMateriaPrimaDetalle()
	{
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LogUsabilidad("Ingreso");
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
    }
    private void txtPallet_Completed(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "El pallet es: \n" + txtPallet.Text, "OK");
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
}