using NewsMauiCVT.Datos;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrima : ContentPage
{
	public AsignacionMateriaPrima()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        LoadDatos();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
    }
    private async void DXButton_Clicked(object sender, EventArgs e)
    {
        if (cboMP.SelectedIndex != -1) 
        {
            await Navigation.PushAsync(new AsignacionMateriaPrimaDetalle());
        }
        else
        {
            await DisplayAlert("Alerta", "Ingrese una opción válida ", "OK");
        }
    }
    private void ClearComponent()
    {
        cboMP.SelectedIndex = -1;
    }
    private void LoadDatos()
    {
        List<Datos> dataInfo = [];

        dataInfo.Add(new Datos { data = "Dato1" });
        dataInfo.Add(new Datos { data = "Dato2" });
        dataInfo.Add(new Datos { data = "Dato3" });
        dataInfo.Add(new Datos { data = "Dato4" });
        dataInfo.Add(new Datos { data = "Dato5" });
        dataInfo.Add(new Datos { data = "Dato6" });

        cboMP.BindingContext = dataInfo;
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
}