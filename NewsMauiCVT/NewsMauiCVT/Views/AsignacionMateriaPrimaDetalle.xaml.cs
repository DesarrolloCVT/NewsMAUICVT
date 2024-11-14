using DevExpress.Maui.Core.Internal;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Collections;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrimaDetalle : ContentPage
{
    int folioRecibido;
    string ItemCode;
    string Lote;
	public AsignacionMateriaPrimaDetalle(int folio)
	{
        InitializeComponent();
        folioRecibido = folio;
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
            txtPallet.Focus();
        });
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
        lblResultado.Text = string.Empty;
        lblResultado.IsVisible = false;
    }
    private async void LoadData()
    {
        try
        {
            DatosPallets datos = new DatosPallets();
            int NPallet = int.Parse(txtPallet.Text);
            List<string> DatosPallet = datos.ObtieneDatos(NPallet);

            Lote = DatosPallet[0];
            ItemCode = DatosPallet[1];
        }

        catch (Exception ex) 
        {
            Console.WriteLine("LoadData: " + ex.Message);
        }
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
    private void txtPallet_Completed(object sender, EventArgs e)
    {   
        DatosPallets dp = new DatosPallets();
        List<PalletClass> list = dp.ObtieneInfoPallet(txtPallet.Text);

        if (list.Count > 0)
        {
            LoadData();

            DatosAsignacionMP asignacionMP = new DatosAsignacionMP();
            bool Respuesta = asignacionMP.InsertarAsignacionMP(folioRecibido, ItemCode, Lote);

            if (Respuesta)
            {
                lblResultado.Text = "Pallet agregado correctamente. ";
                lblResultado.TextColor = Colors.Green;
                lblResultado.IsVisible = true;
                LogUsabilidad("Se asigna Materia Prima");
            }
            else
            {
                lblResultado.Text = "No ha sido posible agregar Pallet. ";
                lblResultado.TextColor = Colors.Red;
                lblResultado.IsVisible = true;
                txtPallet.Text = string.Empty;
            }
        }
        else
        {
            lblResultado.Text = "El Pallet ingresado no es válido";
            lblResultado.TextColor = Colors.Red;
            lblResultado.IsVisible = true;
            txtPallet.Text = string.Empty;
        }
    }
}