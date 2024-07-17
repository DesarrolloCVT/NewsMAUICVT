using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMMOrdenDeVentaDetalle : ContentPage
{
    int _folio = 0;
    public SMMOrdenDeVentaDetalle(int folio)
    {
        InitializeComponent();
        cargaDatos();

        _folio = folio;
        lblFolioOrden.Text = "ORDEN DE VENTA N°" + _folio;


    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        cboProducto.SelectedIndex = -1;
        txtCantidad.Text = string.Empty;
        txtPorcDesc.Text = string.Empty;

        cboProducto.HasError = false;
        cboProducto.ErrorText = string.Empty;

        txtCantidad.HasError = false;
        txtCantidad.ErrorText = string.Empty;
    }

    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosSMMOrdenDeVenta ti = new DatosSMMOrdenDeVenta();
            List<SMMProductosOrdenDeVenta> lt = ti.ListaProductosOrdenVenta();

            List<Product> lPro = new List<Product>();

            foreach (var t in lt)
            {
                lPro.Add(new Product { Codigo = t.Codproducto, NombrePro = t.Producto });

            }

            this.BindingContext = lPro;

        }

        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");

        }
    }

    public class Product
    {
        public string Codigo { get; set; }
        public string NombrePro { get; set; }


    }

    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }

    private void btnGuardarDetalle_Clicked(object sender, EventArgs e)
    {
        if (cboProducto.SelectedIndex == -1)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            cboProducto.HasError = true;
            cboProducto.ErrorText = "Seleccione Producto";
        }
        else if (txtCantidad.Text.Equals(""))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            cboProducto.HasError = true;
            cboProducto.ErrorText = "Ingrese Cantidad";
        }
        else
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                int idOrdenVenta = _folio;
                string codProducto = cboProducto.SelectedValue.ToString();
                int CantidadOrden = Convert.ToInt32(txtCantidad.Text);
                int PorcDescuento = txtPorcDesc.Text.Equals("") ? 0 : Convert.ToInt32(txtPorcDesc.Text);

                DatosSMMOrdenDeVenta ov = new DatosSMMOrdenDeVenta();

                int FolioOrden = ov.insertaDetalleOrden(idOrdenVenta, codProducto, CantidadOrden, PorcDescuento);

                if (FolioOrden != 0)
                {
                    //  UserDialogs.Instance.Alert("Registrado");

                    DisplayAlert("Alerta", "Registado !", "Aceptar");
                    //Navigation.PushAsync(new SMMOrdenDeVentaDetalle(FolioOrden) { Title = "Finalizar" });
                    cboProducto.SelectedIndex = -1;
                    txtCantidad.Text = string.Empty;
                    txtPorcDesc.Text = string.Empty;

                    cboProducto.HasError = false;
                    cboProducto.ErrorText = string.Empty;

                    txtCantidad.HasError = false;
                    txtCantidad.ErrorText = string.Empty;
                }
                else
                {
                    //    UserDialogs.Instance.Alert("Registrado");

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

    private void cboProducto_SelectionChanged(object sender, EventArgs e)
    {
        txtCantidad.Focus();
        cboProducto.HasError = false;
        cboProducto.ErrorText = string.Empty;
    }

    private void txtCantidad_Completed(object sender, EventArgs e)
    {
        txtPorcDesc.Focus();
        txtCantidad.HasError = false;
        txtCantidad.ErrorText = string.Empty;
    }

    private void txtPorcDesc_Completed(object sender, EventArgs e)
    {
        btnGuardarDetalle.Focus();
    }

    private async void btnVerRegistro_Clicked(object sender, EventArgs e)
    {
        using (UserDialogs.Instance.Loading("Cargando"))
        {
            await Task.Delay(10);
            //vCArga.IsVisible = true;
            //activity.IsEnabled = true;
            //activity.IsRunning = true;
            //activity.IsVisible = true;
            await Navigation.PushAsync(new SMMOrdenDeVentaRegistros(_folio));

        }

    }

    private async void btnSalir_Clicked(object sender, EventArgs e)
    {
        var res = await DisplayAlert("Message", "Desea Terminar ?", "SI", "NO");
        if (res)
        {
            Navigation.PopToRootAsync(true);

        }
        else
        {

            cboProducto.Focus();
        }

    }
}