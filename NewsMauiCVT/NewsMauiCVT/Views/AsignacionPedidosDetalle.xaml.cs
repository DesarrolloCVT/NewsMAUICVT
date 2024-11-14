using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class AsignacionPedidosDetalle : ContentPage
{
    int folioRecibido;
    string ItemCode;
    string Lote;
    int Cantidad;
    public AsignacionPedidosDetalle(int folio)
	{
		InitializeComponent();
        folioRecibido = folio;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetFocusText();
        ClearComponent();
        LogUsabilidad("Ingreso");
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtPallet.Focus();
        });
    }
    private void LoadData(int folioOrder)
    {
        try
        {
            DatosPallets datos = new DatosPallets();
            int NPallet = int.Parse(txtPallet.Text);
            List<string> DatosPallet = datos.ObtieneDatos(NPallet);

            Lote = DatosPallet[0];
            ItemCode = DatosPallet[1];
            Cantidad = int.Parse(DatosPallet[2]);
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
        DatosPallets dp = new DatosPallets();
        List<PalletClass> list = dp.ObtieneInfoPallet(txtPallet.Text);

        if (list.Count > 0)
        {
            LoadData(folioRecibido);

            DatosAsignacionPedidos asignacionPedidos = new DatosAsignacionPedidos();
            bool Respuesta = asignacionPedidos.InsertarAsignacionPedidos(folioRecibido, ItemCode, Lote, Cantidad);

            if (Respuesta)
            {
                lblResultado.Text = "Pallet agregado correctamente. ";
                lblResultado.TextColor = Colors.Green;
                lblResultado.IsVisible = true;
                LogUsabilidad("Asigancion de pedido");
            }
            else
            {
                lblResultado.Text = "No ha sido posible agregar Pallet. ";
                lblResultado.TextColor = Colors.Red;
                lblResultado.IsVisible = true;
                txtPallet.Text = string.Empty;
                SetFocusText();
            }
        }
        else
        {
            lblResultado.Text = "El pallet ingresado no existe";
            lblResultado.IsVisible= true;
            lblResultado.TextColor= Colors.Red;
            txtPallet.Text = string.Empty;
            SetFocusText();
        }
    }
}