using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class InformeStock : ContentPage
{
    public InformeStock()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        CargaDatos();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();
        //GvDatos.IsVisible = false;
        LogUsabilidad("Ingreso");
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtCodProd.Focus();
        });
    }
    void ClearComponent()
    {
        lblError.IsVisible = false;
        cboBodega.SelectedIndex = -1;
        lblError.Text = string.Empty;
        txtCodProd.Text = string.Empty;
    }
    void CargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosFefoStock dfs = new DatosFefoStock();
            List<Site> lsBod = dfs.DatosBodegas();

            List<bodeg> bod = new List<bodeg>();
            foreach (var t in lsBod)
            {
                bod.Add(new bodeg { Supportive_Description = t.Site_Description });
            }
            cboBodega.BindingContext = bod;
        }
    }
    public class bodeg { public string Supportive_Description { get; set; } }
    private async void BtnBuscar_Clicked(object sender, EventArgs e)
    {   
        using (UserDialogs.Instance.Loading("Cargando"))
        {
            await Task.Delay(10);

            DatosFefoStock dF = new DatosFefoStock();
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                if (String.IsNullOrWhiteSpace(txtCodProd.Text))
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.Text = "Seleccione un Producto";
                    txtCodProd.Focus();
                }
                else if (cboBodega.SelectedItem == null)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.Text = "Seleccione una Bodega";
                    cboBodega.Focus();

                }
                else
                {
                    int IdBod = dF.TraeIdBodega(cboBodega.SelectedValue.ToString());
                    string CodPro = txtCodProd.Text;

                    DataTable dt = dF.Sp_Fefo(IdBod, CodPro);
                    if (dt.Columns.Count == 0)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        await DisplayAlert("Alerta", "No se encontraron datos", "Aceptar");
                    }
                    else
                    {
                        GvData.IsVisible = true;
                        GvData.ItemsSource = dt;

                        GvData.Columns["SSCC"].Caption = "N°Pallet";
                        GvData.Columns["SSCC"].Width = 110;
                        GvData.Columns["SSCC"].HeaderFontSize = 15;
                        GvData.Columns["SSCC"].HorizontalContentAlignment = TextAlignment.Center;
                        GvData.Columns["Cantidad"].Caption = "Cantidad";
                        GvData.Columns["Cantidad"].Width = 110;
                        GvData.Columns["Cantidad"].HeaderFontSize = 15;
                        GvData.Columns["Cantidad"].HorizontalContentAlignment = TextAlignment.Center;
                        GvData.Columns["Ubicacion"].Caption = "Ubicacion";
                        GvData.Columns["Ubicacion"].Width = 110;
                        GvData.Columns["Ubicacion"].HeaderFontSize = 15;
                        GvData.Columns["Ubicacion"].HorizontalContentAlignment = TextAlignment.Center;
                        GvData.Columns["FProduccion"].DisplayFormat = "dd-MM-yyyy";
                        GvData.Columns["FProduccion"].Width = 130;
                        GvData.Columns["FProduccion"].HeaderFontSize = 15;
                        GvData.Columns["FProduccion"].HorizontalContentAlignment = TextAlignment.Center;
                        GvData.Columns["FVencimiento"].DisplayFormat = "dd-MM-yyyy";
                        GvData.Columns["FVencimiento"].Width = 130;
                        GvData.Columns["FVencimiento"].HeaderFontSize = 15;
                        GvData.Columns["FVencimiento"].HorizontalContentAlignment = TextAlignment.Center;

                        LogUsabilidad("Carga de Informe de Stock");
                        //GvData.Columns["Bultos"].Caption = "Bultos";
                        //GvData.Columns["Cantidad_Pedidos"].Caption = "Cant.Pedidos";
                    }
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 193;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}