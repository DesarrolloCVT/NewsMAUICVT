using Controls.UserDialogs.Maui;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class SMMTrazabilidadPallet : ContentPage
{
    public SMMTrazabilidadPallet()
    {
        InitializeComponent();
        txtNPallet.Focus();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        txtNPallet.Focus();
        lblError.IsVisible = false;
        GvData.IsVisible = false;

    }


    //private void BtnBuscar_Clicked(object sender, EventArgs e)
    //{ 

    //}

    void ClearComponent()
    {

        lblPallet.Text = string.Empty;
        lblLote.Text = string.Empty;
        lblCantidad.Text = string.Empty;
        lblCantReservada.Text = string.Empty;
        lblCodProd.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblBodega.Text = string.Empty;
        lblPosicion.Text = string.Empty;
        lblEstado.Text = string.Empty;
        //GvDatos.IsVisible = false;
    }

    private async void txtNPallet_Completed(object sender, EventArgs e)
    {
        using (UserDialogs.Instance.Loading("Cargando"))
        {
            await Task.Delay(60);
            DatosTrazabilidadSMM tp = new DatosTrazabilidadSMM();
            List<SMMTrazabilidadBusqueda> lt = new List<SMMTrazabilidadBusqueda>();

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {

                if (String.IsNullOrWhiteSpace(txtNPallet.Text))
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.IsVisible = true;
                    lblError.Text = "Ingrese N° Pallet";
                }
                else if (!txtNPallet.Text.ToCharArray().All(Char.IsDigit))
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.IsVisible = true;
                    lblError.Text = "Ingrese Solo Numeros";
                    txtNPallet.Text = string.Empty;
                    txtNPallet.Focus();
                }
                else
                {
                    lblError.Text = string.Empty;
                    lblError.IsVisible = false;

                    string nPallet = txtNPallet.Text;

                    lt = tp.ObtienedatosTraza(nPallet);

                    if (lt.Count() == 0)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "N° de pallet no existe";
                        txtNPallet.Text = string.Empty;
                        txtNPallet.Focus();
                    }
                    else
                    {
                        foreach (var t in lt)
                        {
                            GvData.IsVisible = true;

                            lblPallet.Text = "SSCC: " + t.NPallet;
                            lblLote.Text = "Lote: " + t.Lote;
                            lblCantidad.Text = "Cantidad: " + t.CantInicial;
                            lblCantReservada.Text = "Cant.Reservada: " + t.CantReserva;
                            lblCodProd.Text = "Cod.Producto: " + t.CodProducto;
                            lblProducto.Text = "Prodcuto: " + t.Producto;
                            lblBodega.Text = "Bodega: " + t.Site_Description;
                            lblPosicion.Text = "Ubicacion: " + t.Ubicacion;


                            if (t.Estado == 2)
                            {
                                lblEstado.Text = "Estado: Activo";
                            }
                            else
                            {
                                lblEstado.Text = "Estado: Despachado";
                            }

                            DataTable dt = tp.DetalleTrazaSMM(txtNPallet.Text);
                            GvData.ItemsSource = dt;

                            GvData.Columns["fecha"].Caption = "Fecha";
                            GvData.Columns["fecha"].Width = 110;
                            GvData.Columns["Entidad"].Caption = "Entidad";
                            GvData.Columns["Entidad"].Width = 110;
                            GvData.Columns["IDentidad"].Caption = "Folio";
                            GvData.Columns["IDentidad"].Width = 110;
                            GvData.Columns["Operacion"].Caption = "Operacion";
                            GvData.Columns["Operacion"].Width = 110;
                            GvData.Columns["NombreUsuario"].Caption = "Nombre Usuario";
                            GvData.Columns["NombreUsuario"].Width = 110;
                            GvData.Columns["CantidadInicial"].Caption = "Cantidad";
                            GvData.Columns["CantidadInicial"].Width = 110;
                            GvData.Columns["CodProducto"].Caption = "CodProducto";
                            GvData.Columns["CodProducto"].Width = 110;
                            GvData.Columns["Article_Description"].Caption = "Producto";
                            GvData.Columns["Article_Description"].Width = 110;
                            GvData.Columns["Bodega"].Caption = "Bodega";
                            GvData.Columns["Bodega"].Width = 110;
                            GvData.Columns["Package_Id"].IsVisible = false;
                            GvData.Columns["NPallet"].IsVisible = false;
                            GvData.Columns["Tipo"].IsVisible = false;
                            GvData.Columns["Ndocumento"].IsVisible = false;
                            GvData.Columns["Package_ProductionDate"].IsVisible = false;
                            GvData.Columns["Package_ExpiresDate"].IsVisible = false;
                            GvData.Columns["Reception_Note"].IsVisible = false;
                            GvData.Columns["Package_Lot"].IsVisible = false;
                            GvData.Columns["Estado"].IsVisible = false;
                            GvData.Columns["Bodega"].IsVisible = false;
                            GvData.RowHeight = 200;
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


    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }


}