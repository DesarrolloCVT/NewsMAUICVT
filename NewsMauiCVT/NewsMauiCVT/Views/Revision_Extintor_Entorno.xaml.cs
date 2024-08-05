using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class Revision_Extintor_Entorno : ContentPage
{
    int _idRegistro = 0;
    int _nPregunta = 0;
    public Revision_Extintor_Entorno(int idRegExtintor)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        lblFolioEntorno.Text = "Check del Entorno Folio :" + idRegExtintor.ToString();
        cboRespEntorno.SelectedIndex = 0;
        _idRegistro = idRegExtintor;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        cboRespEntorno.SelectedIndex = 0;
        _nPregunta = 0;
    }
    private void Btn_generarEntorno_Clicked(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosExtintores dEx = new DatosExtintores();

            string respuesta = cboRespEntorno.SelectedItem.ToString();
            string observasion = txtComentariosEntorno.Text;
            string pregunta = lblPregunta.Text;

            int res = dEx.insertaNuevoEntorno(pregunta, respuesta, observasion, _idRegistro);

            if (res == 0)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
            }
            else
            {
                _nPregunta++;
                switch (_nPregunta)
                {
                    case 1:
                        lblPregunta.Text = "2. ¿Se encuentra protegido? (sólo si se encuentra en el exterior)";
                        cboRespEntorno.SelectedIndex = 0;
                        txtComentariosEntorno.Text = string.Empty;
                        break;
                    case 2:
                        lblPregunta.Text = "3. ¿Hay señalización sobre la presencia del extintor?";
                        cboRespEntorno.SelectedIndex = 0;
                        txtComentariosEntorno.Text = string.Empty;

                        break;
                    default:
                        lblPregunta.IsVisible = false;
                        cboRespEntorno.IsVisible = false;
                        txtComentariosEntorno.IsVisible = false;
                        btn_generarEntorno.Text = "Finalizar";
                        Navigation.PopAsync(true);
                        break;
                }
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

        if (btn_generarEntorno.Text.Equals("Finalizar"))
        {
            DisplayAlert("Alerta", "Registro Finalizado", "Aceptar");
            DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");

            Navigation.PopAsync(true);
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}