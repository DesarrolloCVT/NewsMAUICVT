using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class Revision_Extintor_DatosCab : ContentPage
{
    public Revision_Extintor_DatosCab()
    {
        InitializeComponent();
        txtNExtintor.Focus();
    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        txtNExtintor.Text = string.Empty;
        txtUbicacion.Text = string.Empty;
        txtPesoExtintor.Text = string.Empty;
        txtTipoAgente.Text = string.Empty;
        btn_generar.IsEnabled = false;

        txtNExtintor.Focus();
    }

    private void TxtNExtintor_Completed(object sender, EventArgs e)
    {
        txtUbicacion.Focus();
    }

    private void TxtUbicacion_Completed(object sender, EventArgs e)
    {
        txtPesoExtintor.Focus();
    }

    private void TxtPesoExtintor_Completed(object sender, EventArgs e)
    {
        vRecarga.Focus();
    }

    private void VRecarga_DateSelected(object sender, DateChangedEventArgs e)
    {
        txtTipoAgente.Focus();
    }

    private void TxtTipoAgente_Completed(object sender, EventArgs e)
    {
        btn_generar.IsEnabled = true;
    }

    private void Btn_generar_Clicked(object sender, EventArgs e)
    {

        if (txtNExtintor.Equals(string.Empty))
        {
            txtNExtintor.Focus();
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
        }
        else if (txtUbicacion.Text.Equals(string.Empty))
        {
            lblError2.Text = "ingrese ubicacion";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txtUbicacion.Focus();
        }
        else if (txtPesoExtintor.Text.Equals(string.Empty))
        {
            lblError2.Text = "ingrese peso";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txtPesoExtintor.Focus();
        }
        else if (txtTipoAgente.Text.Equals(string.Empty))
        {
            lblError2.Text = "ingrese tipo Agente";
            lblError2.IsVisible = true;
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            txtTipoAgente.Focus();
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                DatosExtintores dEx = new DatosExtintores();

                int usuario = App.Iduser;
                int numExtintor = Convert.ToInt32(txtNExtintor.Text);
                string ubicaExtintor = txtUbicacion.Text;
                int pesoExtintor = Convert.ToInt32(txtPesoExtintor.Text);
                string vigencia = vRecarga.Date.Date.Year + "-" + vRecarga.Date.Date.Month + "-" + vRecarga.Date.Date.Day;
                string tipoAgente = txtTipoAgente.Text;

                int res = dEx.insertaNuevoCheck(usuario, numExtintor, vigencia, ubicaExtintor, pesoExtintor, tipoAgente);

                if (res == 0)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "ERROR AL REGISTRAR,CONTACTAR CON ADMINISTRADOR", "Aceptar");
                }
                else
                {
                    Navigation.PushAsync(new Revision_Extintor_Detalle(res));
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }

    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}