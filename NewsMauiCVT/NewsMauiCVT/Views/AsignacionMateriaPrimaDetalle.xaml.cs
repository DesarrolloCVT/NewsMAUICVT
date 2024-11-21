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
    bool LoteValido;
    DatosPallets datos;
    DataTable tabla;

    public AsignacionMateriaPrimaDetalle(int folio)
	{
        InitializeComponent();
        folioRecibido = folio;
        datos = new DatosPallets();
        tabla = new DataTable();
        CreateTable();
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
        btn_asignaciones.IsVisible = false;
    }
    private void CreateTable()
    {
        tabla = new("TablaAsignacionesRealizadas");
        tabla.Columns.Add("Transfer_ID", typeof(string));
        tabla.Columns.Add("ItemCode", typeof(string));
        tabla.Columns.Add("Lote", typeof(string));
    }
    private void LoadData()
    {
        try
        {
            DatosAsignacionMP dta = new DatosAsignacionMP();
            List<TransferenciaAsignacion> transferAsignacion = dta.TransferenciasAsignadas(folioRecibido);
            List<string> LoteAsignado = new List<string>();
            List<string> DatosPallet = datos.ObtieneDatos(txtPallet.Text);

            Lote = DatosPallet[0];
            ItemCode = DatosPallet[1];

            for (int i = 0; i < transferAsignacion.Count; i++)
            {
                LoteAsignado.Add(transferAsignacion[i].Lote);
            }
            LoteValido = LoteAsignado.Contains(Lote);
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
        LoadData();
        bool PalletValido = datos.ValidaPallet(txtPallet.Text);
        if (PalletValido)
        {
            txtPallet.Text = string.Empty;
            SetFocusText();
            if (LoteValido) 
            {
                DatosAsignacionMP asignacionMP = new DatosAsignacionMP();
                bool Respuesta = asignacionMP.InsertarAsignacionMP(folioRecibido, ItemCode, Lote);

                if (Respuesta)
                {
                    try
                    {
                        tabla.Rows.Add(folioRecibido.ToString(), ItemCode, Lote);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Catch_txtPallet_Completed: " + ex.ToString());
                    }
                    

                    btn_asignaciones.IsVisible = true;
                    lblResultado.Text = "Asignacion correcta. ";
                    lblResultado.TextColor = Colors.Green;
                    lblResultado.IsVisible = true;
                    LogUsabilidad("Se asigna Materia Prima");
                }
                else
                {
                    lblResultado.Text = "No ha sido posible agregar Asignacion. ";
                    lblResultado.TextColor = Colors.Red;
                    lblResultado.IsVisible = true;
                    txtPallet.Text = string.Empty;
                }
            }
            else
            {
                lblResultado.Text = "Lote asignado no es válido";
                lblResultado.TextColor = Colors.Red;
                lblResultado.IsVisible = true;
                txtPallet.Text = string.Empty;
            }
        }
        else 
        {
            lblResultado.Text = "El N° de Pallet no es válido";
            lblResultado.TextColor = Colors.Red;
            lblResultado.IsVisible = true;
            txtPallet.Text = string.Empty;
        }
    }
    private async void btn_asignaciones_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenAsignacionMP(tabla));
    }
}