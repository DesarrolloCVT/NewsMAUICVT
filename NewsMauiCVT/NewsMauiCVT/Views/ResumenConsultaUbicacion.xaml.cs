using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class ResumenConsultaUbicacion : ContentPage
{
    public ResumenConsultaUbicacion(string idPosicion)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargadatos(idPosicion);
        LogUsabilidad("Resumen ubicacion");
        //string g = idpos;
    }
    void cargadatos(string idPosicion)
    {
        DatosConsultaUbicacion dCu = new DatosConsultaUbicacion();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DataTable dt = dCu.ResumenConsultaUbicacion(Convert.ToInt32(idPosicion));
            GvData.ItemsSource = dt;
            GvData.Columns["Layout_Id"].IsVisible = false;
            GvData.Columns["ArticleProvider_CodClient"].Caption = "Cod.Producto";
            GvData.Columns["ArticleProvider_CodClient"].Width = 110;
            GvData.Columns["ArticleProvider_CodClient"].HorizontalContentAlignment = TextAlignment.Center;
            GvData.Columns["ArticleProvider_Description"].Caption = "Producto";
            GvData.Columns["ArticleProvider_Description"].Width = 110;
            GvData.Columns["ArticleProvider_Description"].HorizontalContentAlignment = TextAlignment.Center;
            GvData.Columns["Cantidad"].Caption = "Cantidad";
            GvData.Columns["Cantidad"].Width = 110;
            GvData.Columns["Cantidad"].HorizontalContentAlignment = TextAlignment.Center;
            GvData.Columns["Bultos"].Caption = "Bultos";
            GvData.Columns["Bultos"].Width = 110;
            GvData.Columns["Bultos"].HorizontalContentAlignment = TextAlignment.Center;
            GvData.Columns["Cantidad_Pedidos"].IsVisible = false;
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 19;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}