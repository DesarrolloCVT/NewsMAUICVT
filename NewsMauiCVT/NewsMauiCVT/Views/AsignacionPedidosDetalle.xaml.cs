using DevExpress.Maui.Core.Internal;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionPedidosDetalle : ContentPage
{
    #region Variables globales
    int folioRecibido;
    string ItemCode;
    string Lote;
    int Cantidad;
    int cantidadTotal;
    DatosPallets datos;
    bool LoteValido;
    bool ItemCodeValido;
    DataTable tabla;
    DatosAsignacionPedidos dap;
    List<string> LoteAsignado;
    List<string> ItemCodeAsignado;
    DatosApp datosApp;
    DatosUsuario datosUsuario;
    DatosProduccion datosProduccion;
    string username;
    int ordenCantidad;
    #endregion
    public AsignacionPedidosDetalle(int folio)
	{
		InitializeComponent();
        #region Inicializadores
        folioRecibido = folio;
        datos = new DatosPallets();
        tabla = new DataTable();
        dap = new DatosAsignacionPedidos();
        LoteAsignado = new List<string>();
        ItemCodeAsignado = new List<string>();
        datosApp = new DatosApp();
        datosUsuario = new DatosUsuario();
        datosProduccion = new DatosProduccion();
        username = App.UserSistema;
        #endregion
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetFocusText();
        ClearComponent();
        LogUsabilidad("Ingreso a Asignacion Pedido Detalle");
    }
    private void CreateTable()
    {
        tabla = new("TablaAsignacionesRealizadas");
        tabla.Columns.Add("Transfer_ID", typeof(string));
        tabla.Columns.Add("ItemCode", typeof(string));
        tabla.Columns.Add("Lote", typeof(string));
        tabla.Columns.Add("Cantidad", typeof(string));
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtPallet.Focus();
        });
    }
    private void LoadData()
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPallet.Text))
            {
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    List<PedidosAsignacion> pedidoAsignacion = dap.PedidosAsignados(folioRecibido);
                    List<string> DatosPallet = datos.ObtieneDatos(txtPallet.Text);

                    Lote = DatosPallet[0];
                    ItemCode = DatosPallet[1];


                    Cantidad = pedidoAsignacion.First().Cantidad; //int.Parse(DatosPallet[2]);


                    //Limpieza de listado:
                    LoteAsignado.Clear();
                    ItemCodeAsignado.Clear();

                    for (int i = 0; i < pedidoAsignacion.Count; i++)
                    {
                        LoteAsignado.Add(pedidoAsignacion[i].Lote);
                        ItemCodeAsignado.Add(pedidoAsignacion[i].ItemCode);
                    }
                    cantidadTotal = datosProduccion.CantidadPorLoteOrderDetail(folioRecibido, Lote);
                    LoteValido = LoteAsignado.Contains(Lote);
                    ItemCodeValido = ItemCodeAsignado.Contains(ItemCode);
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                lblResultado.Text = string.Empty;
                lblResultado.Text = "Debe ingresar un valor. ";
                lblResultado.IsVisible = true;
                lblResultado.TextColor = Colors.Red;
                SetFocusText();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("LoadData: " + ex.Message);
        }
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
        lblResultado.Text = string.Empty;
        lblResultado.IsVisible = false;
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 0;
        
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
    private void txtEdit_Completed(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPallet.Text))
            {
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    LoadData();
                    if (!string.IsNullOrEmpty(txtPallet.Text))
                    {
                        bool PalletValido = datos.ValidaPallet(txtPallet.Text);
                        bool UbicacionAgregada;
                        int PackageID = 0;
                        int LayoutID = 0;
                        int StaffID = 0;

                        List<PalletClass> pallets = datos.ObtieneInfoPallet(txtPallet.Text);

                        pallets.ForEach(p => {
                            if (p.Package_Id != 0 && p.Layout_Id != 0)
                            {
                                PackageID = p.Package_Id;
                                LayoutID = p.Layout_Id;
                            }
                        });

                        StaffID = datosUsuario.ObtenerUsuarioStaff(username);

                        if (PalletValido) // Validacion de Pallet valido.
                        {
                            txtPallet.Text = string.Empty;
                            SetFocusText();

                            if (LoteValido && ItemCodeValido) // Validacion de LoteValido e ItemCode correspondiente.
                            {
                                // Cantidad: Es la cantidad registrada en la asignacion de transferencia.

                                // Cantidad Total: Es la suma de las cantidades (Package_Quantity) de todas Pallet
                                // que correspondan al Lote dentro de la order_id seleccionada.
                                if (Cantidad < cantidadTotal) // Validacion de cantidad. 
                                {
                                    UbicacionAgregada = datosProduccion.AddLocation(PackageID, LayoutID, StaffID);
                                    if (UbicacionAgregada) // Validacion de ubicacion agregada.
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                        lblResultado.Text = "Asignacion de pedido agregado correctamente. ";
                                        lblResultado.TextColor = Colors.Green;
                                        lblResultado.IsVisible = true;
                                        LogUsabilidad("Asigancion de pedido");
                                    }
                                    else
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                        DisplayAlert("Alerta", "La asignacion ya se encuentra registrada. ", "Aceptar");
                                    }
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    lblResultado.Text = "La cantidad requerida supera el stock dentro del Lote. ";
                                    lblResultado.IsVisible = true;
                                    lblResultado.TextColor = Colors.Red;
                                    txtPallet.Text = string.Empty;
                                    SetFocusText();
                                }
                            }
                            else
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                lblResultado.Text = string.Empty;
                                lblResultado.Text = "Lote asignado no es válido. ";
                                lblResultado.IsVisible = true;
                                lblResultado.TextColor = Colors.Red;
                                SetFocusText();
                            }
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            lblResultado.Text = string.Empty;
                            lblResultado.Text = "El pallet ingresado no existe. ";
                            lblResultado.IsVisible = true;
                            lblResultado.TextColor = Colors.Red;
                            SetFocusText();
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        lblResultado.Text = string.Empty;
                        lblResultado.Text = "Debe ingresar un valor. ";
                        lblResultado.IsVisible = true;
                        lblResultado.TextColor = Colors.Red;
                        SetFocusText();
                    }
                }
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }
    }
    private async void btn_asignaciones_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenPedidosAsignacion(tabla));
    }
}