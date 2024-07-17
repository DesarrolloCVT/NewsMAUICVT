using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMOdenDeVentaCabecera : ContentPage
{
    public SMMOdenDeVentaCabecera()
    {
        InitializeComponent();
        cargaDatos();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        cboCliente.SelectedIndex = -1;
        cboDirDespacho.SelectedIndex = -1;
        cboDirFact.SelectedIndex = -1;
        dteFechEntrega.Date = null;
    }

    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }

    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosSMMOrdenDeVenta ti = new DatosSMMOrdenDeVenta();
            List<SMMClienteOrdenDeVentas> lt = ti.TraeClientesOrden();

            List<Cli> nl = new List<Cli>();

            foreach (var t in lt)
            {
                nl.Add(new Cli { RutCliente = t.RutCliente, NombreCliente = t.NombreCliente });

            }

            this.BindingContext = nl;


        }



        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }



    public class Cli
    {
        public string RutCliente { get; set; }
        public string NombreCliente { get; set; }


    }


    private void ComboBoxEdit_SelectionChanged(object sender, EventArgs e)
    {
        string dat = cboCliente.SelectedValue.ToString();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosSMMOrdenDeVenta ti = new DatosSMMOrdenDeVenta();
            List<SMMDireccionOrdenDeVentas> dirt = ti.TraeDirDespachoOrden(dat);

            List<Direcciones> nl = new List<Direcciones>();

            foreach (var t in dirt)
            {
                nl.Add(new Direcciones { RutCliente = t.RutCliente, Direccion = t.DirecDesp });

            }

            cboDirDespacho.ItemsSource = nl;
            cboDirDespacho.BindingContext = nl;

            cboCliente.HasError = false;
            cboCliente.ErrorText = string.Empty;
            cboDirDespacho.Focus();

        }



        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }

    }

    public class Direcciones
    {
        public string RutCliente { get; set; }
        public string Direccion { get; set; }


    }

    private void cboDirDespacho_SelectionChanged(object sender, EventArgs e)
    {
        string dat2 = cboCliente.SelectedValue.ToString();


        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosSMMOrdenDeVenta ti = new DatosSMMOrdenDeVenta();
            List<SMMDireccionFactOrdenVentas> dirt = ti.TraeDirFacturacion(dat2);

            List<DireccionesFac> drF = new List<DireccionesFac>();

            foreach (var t in dirt)
            {
                drF.Add(new DireccionesFac { RutCliente = t.RutCliente, DireccionFac = t.DirecFact });

            }

            cboDirFact.ItemsSource = drF;
            cboDirFact.BindingContext = drF;

            cboDirDespacho.HasError = false;
            cboDirDespacho.ErrorText = string.Empty;
            cboDirFact.Focus();

        }



        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }

    public class DireccionesFac
    {
        public string RutCliente { get; set; }
        public string DireccionFac { get; set; }


    }
    private void cboDirFact_SelectionChanged(object sender, EventArgs e)
    {
        cboDirFact.HasError = false;
        cboDirFact.ErrorText = string.Empty;
        dteFechEntrega.Focus();
    }


    private void DateEdit_DateChanged(object sender, EventArgs e)
    {

        dteFechEntrega.HasError = false;
        dteFechEntrega.ErrorText = string.Empty;
        btnCrearOrden.Focus();
    }

    private void btnCrearOrden_Clicked(object sender, EventArgs e)
    {
        if (cboCliente.SelectedIndex == -1)
        {

            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            cboCliente.HasError = true;
            cboCliente.ErrorText = "Seleccione Cliente";
        }
        else if (cboDirDespacho.SelectedIndex == -1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            cboDirDespacho.HasError = true;
            cboDirDespacho.ErrorText = "Seleccione Direccion de Despacho";
        }
        else if (cboDirFact.SelectedIndex == -1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            cboDirFact.HasError = true;
            cboDirFact.ErrorText = "Seleccione Direccion de Facturacion";
        }
        else if (dteFechEntrega.Date == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            dteFechEntrega.HasError = true;
            dteFechEntrega.ErrorText = "Seleccione Direccion de Facturacion";
        }
        else
        {


            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                string DatCliente = cboCliente.SelectedValue.ToString();
                string DireccionDespacho = cboDirDespacho.SelectedValue.ToString();
                string DireccionFacturacion = cboDirFact.SelectedValue.ToString();
                string fecha = dteFechEntrega.Date.Value.Day + "-" + dteFechEntrega.Date.Value.Month + "-" + dteFechEntrega.Date.Value.Year;

                DatosSMMOrdenDeVenta ov = new DatosSMMOrdenDeVenta();

                int FolioOrden = ov.CreaOrdenVenta(DatCliente, fecha, DireccionDespacho, DireccionFacturacion);

                if (FolioOrden != 0)
                {
                    Navigation.PushAsync(new SMMOrdenDeVentaDetalle(FolioOrden) { Title = "Finalizar" });
                }
                else
                {
                    DisplayAlert("Alerta", "Error Al Crear Orden ,Contactar Con Administrador", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }

    }
}