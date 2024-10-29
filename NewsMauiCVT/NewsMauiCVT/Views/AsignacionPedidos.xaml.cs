using NewsMauiCVT.Datos;

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
    }
    private void buttonAceptar_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Texto escrito: " + txtEdit.Text, "Ok");
    }
    private void ClearComponent()
    {
        txtEdit.Text = string.Empty;
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
    private void txtEdit_Completed(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "Texto escrito: " + txtEdit.Text, "Ok");
    }
}