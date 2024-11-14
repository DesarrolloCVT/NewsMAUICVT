using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class TrazabilidadPallet : ContentPage
{
    public TrazabilidadPallet()
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
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtNPallet.Focus();
        });
    }
    void ClearComponent()
    {
        lblError.Text = string.Empty;
        lblError.IsVisible = false;
        GvData.IsVisible = false;
        lblPallet.Text = string.Empty;
        lblLote.Text = string.Empty;
        lblCantidad.Text = string.Empty;
        lblCantReservada.Text = string.Empty;
        lblCodProd.Text = string.Empty;
        lblProducto.Text = string.Empty;
        lblBodega.Text = string.Empty;
        lblPosicion.Text = string.Empty;
        lblEstado.Text = string.Empty;
        GvData.IsVisible = false;
        txtNPallet.Text = string.Empty;
    }
    private void TxtNPallet_Completed(object sender, EventArgs e)
    {   
        try
        {
            DatosTrazabilidadPallet tp = new DatosTrazabilidadPallet();
            List<TrazabilidadPaletClass> lt = new List<TrazabilidadPaletClass>();

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                lt = tp.BuscaTraabilidadPallet(int.Parse(txtNPallet.Text));
                if(int.Parse(txtNPallet.Text) != 0)
                {
                    if (txtNPallet.Text.ToCharArray().All(Char.IsDigit) && !String.IsNullOrWhiteSpace(txtNPallet.Text))
                    {
                        lblError.Text = string.Empty;
                        lblError.IsVisible = false;

                        if (lt.Count() != 0)
                        {
                            var pallet = int.Parse(txtNPallet.Text);
                            DataTable dt = tp.DetalleTrazabilidadPallet(pallet);

                            if (dt.Columns.Count == 0)
                            {
                                ClearComponent();
                            }
                            else
                            {
                                foreach (var t in lt)
                                {
                                    GvData.IsVisible = true;

                                    lblPallet.Text = "SSCC: " + t.Package_SSCC;
                                    lblLote.Text = "Lote: " + t.Package_Lot;
                                    lblCantidad.Text = "Cantidad: " + t.Package_Quantity;
                                    lblCantReservada.Text = "Cant.Reservada: " + t.Package_ReserveQuantity;
                                    lblCodProd.Text = "Cod.Producto: " + t.Codproducto;
                                    lblProducto.Text = "Prodcuto: " + t.Articulo;
                                    lblBodega.Text = "Bodega: " + t.Site;
                                    lblPosicion.Text = "Ubicacion: " + t.Ubicacion;
                                    lblEstado.Text = "Estado: " + t.Estado;

                                    GvData.ItemsSource = dt;

                                    GvData.Columns["FECHA"].Caption = "Fecha";
                                    GvData.Columns["FECHA"].Width = 110;
                                    GvData.Columns["FECHA"].HorizontalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["FECHA"].VerticalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Entidad"].Caption = "Entidad";
                                    GvData.Columns["Entidad"].Width = 120;
                                    GvData.Columns["Entidad"].HorizontalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Entidad"].VerticalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Operacion"].Caption = "Operacion";
                                    GvData.Columns["Operacion"].Width = 110;
                                    GvData.Columns["Operacion"].HorizontalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Operacion"].VerticalContentAlignment = TextAlignment.Center;
                                    GvData.RowHeight = 250;
                                    GvData.Columns["Cantidad"].Caption = "Cantidad";
                                    GvData.Columns["Cantidad"].Width = 110;
                                    GvData.Columns["Cantidad"].HorizontalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Cantidad"].VerticalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Staff_Name"].Caption = "Usuario";
                                    GvData.Columns["Staff_Name"].Width = 110;
                                    GvData.Columns["Staff_Name"].HorizontalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Staff_Name"].VerticalContentAlignment = TextAlignment.Center;
                                    GvData.Columns["Package_SSCC"].IsVisible = false;
                                }
                                txtNPallet.Text = string.Empty;
                                _ = Task.Delay(150).ContinueWith(t => {
                                    txtNPallet.Focus();
                                });

                                LogUsabilidad("Trazabilidad de Pallet");
                            }
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            lblError.IsVisible = true;
                            lblError.Text = "N° de pallet no existe ";
                            txtNPallet.Text = string.Empty;
                            _ = Task.Delay(150).ContinueWith(t => {
                                txtNPallet.Focus();
                            });
                        }
                    }
                    else if (String.IsNullOrWhiteSpace(txtNPallet.Text))
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "Ingrese N° Pallet ";
                        txtNPallet.Text = string.Empty;
                        _ = Task.Delay(150).ContinueWith(t => {
                            txtNPallet.Focus();
                        });
                    }
                    else if (!txtNPallet.Text.ToCharArray().All(Char.IsDigit))
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "Ingrese solo números ";
                        txtNPallet.Text = string.Empty;
                        _ = Task.Delay(150).ContinueWith(t => {
                            txtNPallet.Focus();
                        });
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblError.IsVisible = true;
                        lblError.Text = "Se ha producido un error, intentelo nuevamente ";
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    lblError.IsVisible = true;
                    lblError.Text = "Ingrese N° Pallet válido ";
                    txtNPallet.Text = string.Empty;
                    _ = Task.Delay(100).ContinueWith(t => {
                        txtNPallet.Focus();
                    });
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("TxtNPallet_Completed: " + ex.ToString());
            ClearComponent();
            _ = Task.Delay(200).ContinueWith(t => {
                _ = Task.Delay(200).ContinueWith(t => {
                    txtNPallet.Focus();
                });
            });
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void LogUsabilidad(string tipo)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = tipo;
        var IdSubMenu = 87;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}