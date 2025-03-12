using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;

namespace NewsMauiCVT.Views;

public partial class ActualizaTipoPallet : ContentPage
{
    public ActualizaTipoPallet()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent(); 
        btn_generar.IsEnabled = false;
        txt_pallet.Focus();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();
        LogUsabilidad("Ingreso");
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txt_pallet.Focus();
        });
    }
    private async void TxtNPallet_Completed(object sender, EventArgs e)
    {   
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (txt_pallet.Text.Equals(string.Empty))
            {
                lblError.Text = "Ingrese un N° de Pallet. ";
                lblError.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                //DisplayAlert("Alerta",, "Aceptar");          
                txt_pallet.Focus();
            }
            else if (!txt_pallet.Text.ToCharArray().All(Char.IsDigit))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblError.Text = "Ingrese solo Numeros. ";
                lblError.IsVisible = true;
                txt_pallet.Focus();
                txt_pallet.Text = string.Empty;
            }
            else
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };

                //verifica numero palllet
                var rest = ClientHttp.GetAsync("api/TomaInventario?NumPallet=" + Convert.ToInt32(txt_pallet.Text)).Result;
                if (rest.IsSuccessStatusCode)
                {
                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                    int NumPallet = JsonConvert.DeserializeObject<int>(resultadoStr);

                    DatosPallets dp = new DatosPallets();
                    List<PalletClass> list = dp.ObtieneInfoPallet(txt_pallet.Text);

                    if(list.Count <= 0)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.Text = "N° de pallet no existe";
                        lblError.IsVisible = true;
                        txt_pallet.Focus();
                        txt_pallet.Text = string.Empty;
                    }
                    else
                    {
                        DatosTipoPallet dti = new DatosTipoPallet();
                        List<TipoPalletClass> dt2 = dti.ListaTipoPallet();

                        List<TipoPall> lPro = new List<TipoPall>();

                        cboTipoPallet.IsVisible = true;

                        foreach (var s in dt2)
                        {
                            lPro.Add(new TipoPall { Supportive_Description = s.Supportive_Description });
                        }
                        cboTipoPallet.BindingContext = lPro;
                        lblError.IsVisible = true;
                        lblError.Text = "Pallet Actual = " + dti.TraeNombreTipo(NumPallet).ToString();
                        cboTipoPallet.IsVisible = true;
                        cboTipoPallet.Focus();
                    }
                }
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    void ClearComponent()
    {
        btn_generar.IsEnabled = false;
        cboTipoPallet.SelectedIndex = -1;
        cboTipoPallet.IsVisible = false;
        lblError.IsVisible = false;
        txt_pallet.Text = string.Empty;
        lblError.Text = string.Empty;
    }
    public class TipoPall
    {
        public string Supportive_Description { get; set; }
    }
    private void CboTipoPallet_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_generar.IsEnabled = true;
    }
    private void Btn_generar_Clicked(object sender, EventArgs e)
    {
        #region AccTipoPallet
        DatosTipoPallet dTip = new DatosTipoPallet();

        int IdTipoPal = dTip.TraeIdTipoPallet(cboTipoPallet.SelectedValue.ToString());

        bool res = dTip.ActualizaTipoPallet(Convert.ToInt32(txt_pallet.Text), IdTipoPal);

        if (res == true)
        {
            DisplayAlert("Alerta", "Tipo de Pallet Actualizado", "Aceptar");
            txt_pallet.Text = string.Empty;
            cboTipoPallet.SelectedIndex = -1;
            lblError.Text = string.Empty;
            lblError.IsVisible = false;
            txt_pallet.Focus();
            btn_generar.IsEnabled = false;
            LogUsabilidad("Actualizacion de pallet ");
        }
        else
        {
            DisplayAlert("Alerta", "Error al Actualizar Tipo de Pallet , Favor Verificar", "Aceptar");
            txt_pallet.Text = string.Empty;
            cboTipoPallet.SelectedIndex = -1;
            lblError.Text = string.Empty;
            lblError.IsVisible = false;
        }


        #endregion
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
        var IdSubMenu = 305;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}