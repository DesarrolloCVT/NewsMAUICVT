using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionPedidosDetalle : ContentPage
{
    int folioRecibido;
    string ItemCode;
    string Lote;
    int Cantidad;
    int CantidadXLote;
    DatosPallets datos;
    bool LoteValido;
    DataTable tabla;
    public AsignacionPedidosDetalle(int folio)
	{
		InitializeComponent();
        folioRecibido = folio;
        datos = new DatosPallets();
        tabla = new DataTable();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetFocusText();
        ClearComponent();
        LogUsabilidad("Ingreso");
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
            DatosAsignacionPedidos dap = new DatosAsignacionPedidos();
            List<PedidosAsignacion> transferAsignacion = dap.PedidosAsignados(folioRecibido);
            List<string> LoteAsignado = new List<string>();
            List<string> DatosPallet = datos.ObtieneDatos(txtPallet.Text);
            
            Lote = DatosPallet[0];
            ItemCode = DatosPallet[1];
            Cantidad = int.Parse(DatosPallet[2]);

            for (int i = 0; i < transferAsignacion.Count; i++)
            {
                LoteAsignado.Add(transferAsignacion[i].Lote);
                if(transferAsignacion[i].Lote == Lote)
                {
                    CantidadXLote += transferAsignacion[i].Cantidad;
                }
            }
            LoteValido = LoteAsignado.Contains(Lote);
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

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
    private void txtEdit_Completed(object sender, EventArgs e)
    {   
        LoadData();
        bool PalletValido = datos.ValidaPallet(txtPallet.Text);

        if (PalletValido)
        {
            txtPallet.Text = string.Empty;
            SetFocusText();
            if (LoteValido)
            {
                if(Cantidad < CantidadXLote)
                {
                    DatosAsignacionPedidos asignacionPedidos = new DatosAsignacionPedidos();
                    bool Respuesta = asignacionPedidos.InsertarAsignacionPedidos(folioRecibido, ItemCode, Lote, Cantidad);

                    if (Respuesta)
                    {
                        lblResultado.Text = "Asignación agregada correctamente. ";
                        lblResultado.TextColor = Colors.Green;
                        lblResultado.IsVisible = true;
                        LogUsabilidad("Asigancion de pedido");
                    }
                    else
                    {
                        lblResultado.Text = "No ha sido posible agregar la asignación. ";
                        lblResultado.TextColor = Colors.Red;
                        lblResultado.IsVisible = true;
                        txtPallet.Text = string.Empty;
                        SetFocusText();
                    }
                }
                else
                {
                    lblResultado.Text = "La cantidad requerida supera el stock dentro del Lote.";
                    lblResultado.IsVisible = true;
                    lblResultado.TextColor = Colors.Red;
                    txtPallet.Text = string.Empty;
                    SetFocusText();
                }
            }
        }
        else
        {
            lblResultado.Text = string.Empty;
            lblResultado.Text = "El pallet ingresado no existe";
            lblResultado.IsVisible= true;
            lblResultado.TextColor= Colors.Red;
            SetFocusText();
        }
    }
    private async void btn_asignaciones_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenPedidosAsignacion(tabla));
    }
}