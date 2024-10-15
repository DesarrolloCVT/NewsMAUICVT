using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;
public partial class ControlFosfina : ContentPage
{
    public ControlFosfina()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        LogUsabilidad("Ingreso");
    }
    private void FFumi_DateSelected(object sender, DateChangedEventArgs e)
    {
        txt_Bodega.Focus();
    }
    private void Txt_Bodega_Completed(object sender, EventArgs e)
    {
        txt_MayorPP.Focus();
    }
    private void Txt_MayorPP_Completed(object sender, EventArgs e)
    {
        cboA1.Focus();
    }
    private void CboA1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Distancia.Focus();
    }
    private void Txt_Distancia_Completed(object sender, EventArgs e)
    {
        btn_agregar.Focus();
        btn_agregar.IsEnabled = true;

    }
    private async void Btn_agregar_Clicked(object sender, EventArgs e)
    {
        string hr = hora.Time.ToString();

        if (hr.Equals("00:00:00"))
        {
            hora.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Ingrese Hora", "Aceptar");
        }
        else if (txt_Bodega.Equals(string.Empty))
        {
            txt_Bodega.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Ingrese Bodega", "Aceptar");
        }
        else if (txt_MayorPP.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_Bodega.Focus();
            await DisplayAlert("Alerta", "Ingrese Cantidad", "Aceptar");
        }
        else if (cboA1.SelectedIndex == -1)
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Seleccione", "Aceptar");
            cboA1.Focus();
        }
        else if (txt_Distancia.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txt_Bodega.Focus();
            await DisplayAlert("Alerta", "Ingrese distancia", "Aceptar");
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosExtintores de = new DatosExtintores();
                string fFumigacion = FFumi.Date.Year + "-" + FFumi.Date.Month + "-" + FFumi.Date.Day;
                string bod = txt_Bodega.Text;
                string mayor = txt_MayorPP.Text.Replace(".", ",");
                string a1 = cboA1.SelectedItem.ToString();
                string distancia = txt_Distancia.Text;

                int rest = de.insertaRegistroFosfina(hr, fFumigacion, bod, mayor, a1, distancia);
                if (rest != 0)
                {
                    LogUsabilidad("Ingresar registro de Fosfina ");
                    await DisplayAlert("Alerta", "Registrado", "Aceptar");
                    txt_Bodega.Text = string.Empty;
                    txt_MayorPP.Text = string.Empty;
                    cboA1.SelectedIndex = -1;
                    txt_Distancia.Text = string.Empty;
                    btn_agregar.IsEnabled = false;
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
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
        var IdSubMenu = 320;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}