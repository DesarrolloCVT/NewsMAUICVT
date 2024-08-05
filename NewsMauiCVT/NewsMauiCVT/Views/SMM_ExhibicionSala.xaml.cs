using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_ExhibicionSala : ContentPage
{
    public SMM_ExhibicionSala()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        lblError2.IsVisible = false;
        lblError2.Text = string.Empty;
        lblProducto.IsVisible = false;
        cboPasillo.Focus();
        btn_agregar.IsEnabled = false;
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        cboPasillo.SelectedIndex = -1;
        cboColumna.SelectedIndex = -1;
        cboNivel.SelectedIndex = -1;
        txtCodBarra.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblProducto.IsVisible = false;
        cboCantCaras.SelectedIndex = -1;
        lblError2.Text = string.Empty;
        lblError2.IsVisible = false;
        cboPasillo.Focus();
        btn_agregar.IsEnabled = false;
    }
    private void CboPasillo_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboColumna.Focus();
    }
    private void CboColumna_SelectedIndexChanged(object sender, EventArgs e)
    {
        cboNivel.Focus();
    }
    private void CboNivel_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCodBarra.Focus();
    }
    private void TxtCodBarra_Completed(object sender, EventArgs e)
    {
        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string codpro = dti.ValidaCodProducto(txtCodBarra.Text);
            if (txtCodBarra.Text.Equals(string.Empty))
            {
                lblError2.Text = "ingrese Codigo Producto";
                lblError2.IsVisible = true;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                txtCodBarra.Focus();
            }
            else if (codpro.Equals(""))
            {
                lblError2.IsVisible = true;
                txtCodBarra.Focus();
                lblError2.Text = "Cod.Prodcuto No Existe";
                txtCodBarra.Text = string.Empty;
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");

            }
            else
            {

                // Fvenci.Focus();
                cboCantCaras.Focus();
                lblProducto.IsVisible = true;
                lblProducto.Text = codpro.ToString();
                lblError2.Text = string.Empty;
                lblError2.IsVisible = false;
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void CboCantCaras_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_agregar.Focus();
        btn_agregar.IsEnabled = true;
    }
    private void Btn_agregar_Clicked(object sender, EventArgs e)
    {

        DatosSMM_TomaInventario dti = new DatosSMM_TomaInventario();
        DatosExhibicionSMM dEx = new DatosExhibicionSMM();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (cboPasillo.SelectedIndex == -1)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Seleccione un pasillo", "Aceptar");
            }
            else if (cboColumna.SelectedIndex == -1)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Seleccione una Columna", "Aceptar");
            }
            else if (cboNivel.SelectedIndex == -1)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Seleccione un nivel", "Aceptar");
            }
            else if (txtCodBarra.Text.Equals(string.Empty))
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "ingrese codigo  de producto", "Aceptar");
            }
            else if (cboCantCaras.SelectedIndex == -1)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Seleccione cantidad de caras", "Aceptar");
            }
            else
            {
                string CodPro = dti.TraeCodProducti(txtCodBarra.Text);

                string pasi = cboPasillo.SelectedItem.ToString() ??
                                throw new InvalidOperationException();
                int col = Convert.ToInt32(cboColumna.SelectedItem);
                int niv = Convert.ToInt32(cboNivel.SelectedItem);
                string cBar = txtCodBarra.Text;
                int cCar = Convert.ToInt32(cboCantCaras.SelectedItem);

                bool res = dEx.InsertaRegistroExhibicion(pasi, col, niv, cBar, CodPro, cCar);

                if (res == true)
                {
                    DisplayAlert("Alerta", "Registrado", "Aceptar");
                    txtCodBarra.Text = string.Empty;
                    lblProducto.Text = string.Empty;
                    lblProducto.IsVisible = false;
                    cboCantCaras.SelectedIndex = -1;
                    lblError2.Text = string.Empty;
                    lblError2.IsVisible = false;
                    btn_agregar.IsEnabled = false;
                    txtCodBarra.Focus();

                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error Al Registar Contactar Con Administrador", "Aceptar");
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