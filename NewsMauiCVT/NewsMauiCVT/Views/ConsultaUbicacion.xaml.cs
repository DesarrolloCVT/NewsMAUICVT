using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Text.RegularExpressions;

namespace NewsMauiCVT.Views;

public partial class ConsultaUbicacion : ContentPage
{
    public ConsultaUbicacion()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();
        LogUsabilidad("Ingreso");
    }
    private void ClearComponent()
    {   
        txtPosicion.Text = string.Empty;
        txtPosicion.Focus();
        lblError.Text = string.Empty;
        lblError.IsVisible = false;
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtPosicion.Focus();
        });
    }
    private async void TxtPosicion_Completed(object sender, EventArgs e)
    {   
        using (UserDialogs.Instance.Loading("Cargando"))
        {
            try
            {
                await Task.Delay(10);
                int num = Convert.ToInt32(txtPosicion.Text);
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                    bool resultado = Regex.IsMatch(txtPosicion.Text, caractEspecial, RegexOptions.IgnoreCase);

                    if (String.IsNullOrWhiteSpace(txtPosicion.Text))
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "Ingrese Posicion";
                        txtPosicion.Text = string.Empty;
                        txtPosicion.Focus();
                    }
                    else
                    if (resultado == true)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "No se aceptan caracteres especiales";
                        txtPosicion.Text = string.Empty;
                        txtPosicion.Focus();
                    }
                    else if (!txtPosicion.Text.ToCharArray().All(Char.IsDigit))
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "Ingrese Solo Numeros";
                        txtPosicion.Text = string.Empty;
                        txtPosicion.Focus();
                    }
                    else
                    {
                        DatosConsultaUbicacion u = new DatosConsultaUbicacion();
                        int estado = u.EvaluaExistenDatosEnPosision(num);
                        if (estado == 0)
                        {

                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            lblError.IsVisible = true;
                            lblError.Text = "Ubicacion sin Datos";
                            txtPosicion.Text = string.Empty;
                            txtPosicion.Focus();
                        }
                        else
                        {
                            LogUsabilidad("Consultando ubicacion");
                            await Navigation.PushAsync(new DetalleConsultaUbicacion(txtPosicion.Text) { Title = "Volver" });
                        }
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
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
        var IdSubMenu = 19;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}