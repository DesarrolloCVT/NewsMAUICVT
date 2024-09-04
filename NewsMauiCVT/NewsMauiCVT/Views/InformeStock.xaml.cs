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
        #region Código para cargar página de Scan BarCode desde el teléfono.
        BarcodePage barcodePage = new BarcodePage();
        #endregion

        base.OnAppearing();
        ClearComponent();
        SetFocusText();
        //GvDatos.IsVisible = false;

        #region Código para cargar página de Scan BarCode desde el teléfono.
        if (DeviceInfo.Model != "MC33" && DeviceInfo.Model != "MC3300x" && DeviceInfo.Model != "RFD0020")
        {
            btn_escanear.IsVisible = true;
            btn_escanear.IsEnabled = true;
            if (barcodePage.Flag && barcodePage.CodigoDetectado) //True
            {
                txtCodProd.Text = barcodePage.SetBarcode(); //Set text -> Codigo de barras recuperado.
                barcodePage.Flag = !barcodePage.Flag;
                barcodePage.CodigoDetectado = !barcodePage.CodigoDetectado;
            }
        }
        #endregion
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
                        GvData.Columns["Cantidad"].Caption = "Cantidad";
                        GvData.Columns["Cantidad"].Width = 110;
                        GvData.Columns["Ubicacion"].Caption = "Ubicacion";
                        GvData.Columns["Ubicacion"].Width = 110;
                        GvData.Columns["FProduccion"].DisplayFormat = "dd-MM-yyyy";
                        GvData.Columns["FProduccion"].Width = 110;
                        GvData.Columns["FVencimiento"].DisplayFormat = "dd-MM-yyyy";
                        GvData.Columns["FVencimiento"].Width = 110;

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
    private void OnKeyDown()
    {
#if ANDROID
        var imm = (Android.Views.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);
        if (imm != null)
        {
            var activity = Platform.CurrentActivity;
            Android.OS.IBinder wToken = activity.CurrentFocus?.WindowToken;
            imm.HideSoftInputFromWindow(wToken, 0);
        }
#endif
    }
    protected override bool OnBackButtonPressed()
    {
        OnKeyDown();
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void Btn_escanear_Clicked(object sender, EventArgs e)
    {
        BarcodePage barcodePage = new BarcodePage();
        barcodePage.Flag = true;

        OnKeyDown();
        Application.Current?.MainPage?.Navigation
            .PushModalAsync(new NavigationPage(new BarcodePage())
            { BarTextColor = Colors.White, BarBackgroundColor = Colors.CadetBlue }, true);
    }
}