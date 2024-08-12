using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class Revision_Extintor_Detalle : ContentPage

{
    int _idReg = 0;
    int _nPreg = 0;
    public Revision_Extintor_Detalle(int res)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        lblFolio.Text = "Check del Extintor Folio :" + res.ToString();
        cboResp.SelectedIndex = 0;
        _idReg = res;
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        //btn_generar.IsEnabled = false;
        cboResp.SelectedIndex = 0;
        _nPreg = 0;
    }
    private void Btn_generar_Clicked(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosExtintores dEx = new DatosExtintores();
            int idRegistro = _idReg;
            string resp = cboResp.SelectedItem.ToString();
            string obser = txtComentarios.Text;
            string pregunta = lblPregunta.Text;

            int res = dEx.insertaNuevoDetalle(idRegistro, resp, obser, pregunta);
            //_nPreg++;
            if (res == 0)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
            }
            else
            {
                _nPreg++;
                switch (_nPreg)
                {
                    case 1:
                        lblPregunta.Text = "2. ¿Mantiene el sello de seguridad?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 2:
                        lblPregunta.Text = "3. ¿Manómetro indica presión correcta?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 3:
                        lblPregunta.Text = "4. ¿Manguera está operativa?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 4:
                        lblPregunta.Text = "5. ¿Percutor se encuentra en buen estado?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 5:
                        lblPregunta.Text = "6. ¿Cilindro se encuentra libre de abolladuras o desgaste?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 6:
                        lblPregunta.Text = "7. ¿Boquilla está operativa?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 7:
                        lblPregunta.Text = "8. ¿La palanca percutora está operativa, con su seguro de bloqueo?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 8:
                        lblPregunta.Text = "9. ¿Las intrucciones estan claras?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    case 9:
                        lblPregunta.Text = "10. ¿Indica claramente el tipo de fuego a extinguir?";
                        cboResp.SelectedIndex = 0;
                        txtComentarios.Text = string.Empty;
                        break;
                    default:
                        Navigation.PushAsync(new Revision_Extintor_Entorno(_idReg));
                        break;
                }
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}