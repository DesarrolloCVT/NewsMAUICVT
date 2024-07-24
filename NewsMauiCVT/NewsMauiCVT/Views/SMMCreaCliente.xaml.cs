using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Text.RegularExpressions;

namespace NewsMauiCVT.Views;

public partial class SMMCreaCliente : ContentPage
{
    public SMMCreaCliente()
    {

        InitializeComponent();
        txtRut.Focus();
        txtRut.SetSelection(0, 0);

    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        txtRut.SetSelection(0, 0);
        txtDigito.Text = string.Empty;
        txtRut.Text = string.Empty;
        txtNomCliente.Text = string.Empty;
        txtRazonSocial.Text = string.Empty;
        txtGiro.Text = string.Empty;
        txtDirFact.Text = string.Empty;
        txtDirDesp.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtCorreo.Text = string.Empty;
        txtRazonSocial.HasError = false;
        txtGiro.HasError = false;
        txtDirFact.HasError = false;
        txtDirDesp.HasError = false;
        btnUbicacion.IsEnabled = true;
        App.lati = string.Empty;
        App.longi = string.Empty;
        App.altit = string.Empty;

        txtRut.Focus();

    }
    private void btnGuardaCliente_Clicked(object sender, EventArgs e)
    {
        if (txtRazonSocial.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtRazonSocial.Focus();
            txtRazonSocial.HasError = true;
            txtRazonSocial.ErrorText = "ingrese telefono";
        }
        else
        if (txtGiro.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtGiro.Focus();
            txtGiro.HasError = true;
            txtGiro.ErrorText = "ingrese telefono";
        }
        else if (txtGiro.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtDirFact.Focus();
            txtDirFact.HasError = true;
            txtDirFact.ErrorText = "ingrese telefono";
        }
        else if (txtDirDesp.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtDirDesp.Focus();
            txtDirDesp.HasError = true;
            txtDirDesp.ErrorText = "ingrese telefono";
        }
        else
        {

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosSMMOrdenDeVenta pr = new DatosSMMOrdenDeVenta();
                string con = pr.ValidaRutCliente("C" + txtRut.Text + "-" + txtDigito.Text);


                if (con.Equals("-1"))
                {

                    try
                    {
                        string rut = "C" + txtRut.Text + "-" + txtDigito.Text;
                        string nombre = txtNomCliente.Text;
                        string raz = txtRazonSocial.Text;
                        string gir = txtGiro.Text;
                        string direcFac = txtDirFact.Text;
                        string direcDes = txtDirDesp.Text;
                        string tel = txtTelefono.Text;
                        string cor = txtCorreo.Text;
                        string alt = App.altit.ToString();
                        string lon = App.longi.ToString();
                        string lati = App.lati.ToString();

                        bool rest = pr.insertacliente(rut, nombre, raz, gir, direcFac, direcDes, tel, cor, lati, lon, alt);

                        if (rest == true)
                        {
                            DisplayAlert("Alerta", "Cliente Registrado con exito !", "Aceptar");
                            txtRut.Text = string.Empty;
                            txtDigito.Text = string.Empty;
                            txtNomCliente.Text = string.Empty;
                            txtRazonSocial.Text = string.Empty;
                            txtGiro.Text = string.Empty;
                            txtDirFact.Text = string.Empty;
                            txtDirDesp.Text = string.Empty;
                            txtTelefono.Text = string.Empty;
                            txtCorreo.Text = string.Empty;
                            txtRazonSocial.HasError = false;
                            txtGiro.HasError = false;
                            txtDirFact.HasError = false;
                            txtDirDesp.HasError = false;
                            btnUbicacion.IsEnabled = true;

                            App.lati = string.Empty;
                            App.longi = string.Empty;
                            App.altit = string.Empty;

                            txtRut.Focus();
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            DisplayAlert("Alerta", "Error Al registrar Cliente Favor Verificar", "Aceptar");
                        }

                    }
                    catch { }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Rut Cliente ya Existe", "Aceptar");
                    txtRut.Focus();
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }

        }


    }

    private void txtRut_Completed(object sender, EventArgs e)
    {
        //   var ACC = Connectivity.NetworkAccess;
        if (txtRut.Text.Equals("") || txtDigito.Text.Equals(""))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtRut.Focus();
            txtRut.HasError = true;
            txtRut.ErrorText = "ingrese rut";
            txtDigito.HasError = true;
            txtDigito.ErrorText = "ingrese rut";
        }
        else
        {
            txtRut.HasError = false;
            txtDigito.HasError = false;
            txtNomCliente.Focus();
        }
    }

    private void txtNomCliente_Completed(object sender, EventArgs e)
    {

        if (txtNomCliente.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtNomCliente.Focus();
            txtNomCliente.HasError = true;
            txtNomCliente.ErrorText = "ingrese nombre";
        }
        else
        {
            txtNomCliente.HasError = false;

            txtTelefono.Focus();
        }
    }
    private void txtTelefono_Completed(object sender, EventArgs e)
    {
        if (txtTelefono.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtTelefono.Focus();
            txtTelefono.HasError = true;
            txtTelefono.ErrorText = "ingrese telefono";
        }
        else
        {
            txtTelefono.HasError = false;

            txtCorreo.Focus();
        }
    }

    private void txtCorreo_Completed(object sender, EventArgs e)
    {
        bool isEmail = Regex.IsMatch(txtCorreo.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        if (!isEmail || txtCorreo.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtCorreo.Focus();
            txtCorreo.HasError = true;
            txtCorreo.ErrorText = "ingrese Correo";
        }
        else
        {
            txtCorreo.HasError = false;

            txtRazonSocial.Focus();
        }
    }

    private void txtRazonSocial_Completed(object sender, EventArgs e)
    {
        if (txtRazonSocial.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtRazonSocial.Focus();
            txtRazonSocial.HasError = true;
            txtRazonSocial.ErrorText = "ingrese razon";
        }
        else
        {
            txtRazonSocial.HasError = false;

            txtGiro.Focus();
        }
    }

    private void txtGiro_Completed(object sender, EventArgs e)
    {
        if (txtGiro.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtGiro.Focus();
            txtGiro.HasError = true;
            txtGiro.ErrorText = "ingrese Giro";
        }
        else
        {
            txtGiro.HasError = false;

            txtDirFact.Focus();
        }
    }

    private void txtDirFact_Completed(object sender, EventArgs e)
    {
        if (txtGiro.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtDirFact.Focus();
            txtDirFact.HasError = true;
            txtDirFact.ErrorText = "ingrese Direccion";
        }
        else
        {
            txtDirFact.HasError = false;

            txtDirDesp.Focus();
        }
    }

    private void txtDirDesp_Completed(object sender, EventArgs e)
    {
        if (txtDirDesp.Text.Equals(string.Empty))
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            //DisplayAlert("Alerta", "ingrese rut", "Aceptar");
            txtDirDesp.Focus();
            txtDirDesp.HasError = true;
            txtDirDesp.ErrorText = "ingrese Direccion";
        }
        else
        {
            txtDirDesp.HasError = false;

            btnGuarda.Focus();
        }
    }

    private async void btnUbicacion_Clicked(object sender, EventArgs e)
    {
        var location = await Geolocation.GetLastKnownLocationAsync();

        if (location != null)
        {
            App.lati += location.Latitude.ToString();
            App.longi += location.Longitude.ToString();
            App.altit += location.Altitude.ToString();

            await DisplayAlert("Alerta", "Ubicacion registrada con Exito!", "OK");
            btnUbicacion.IsEnabled = false;
            btnGuarda.Focus();

        }
        else { await DisplayAlert("Confirmar", "Active GPS", "OK"); }
    }

}