using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class SMM_Picking_Consolidado_Cabecera : ContentPage
{
    public SMM_Picking_Consolidado_Cabecera()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        CargaDatosDpto();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        //GvDatos.IsVisible = false;
    }
    void ClearComponent()
    {
        cboDpto.SelectedIndex = -1;
        btnPikear.IsEnabled = false;
    }
    void CargaDatosDpto()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosPickingConsolidadoSMM pi = new DatosPickingConsolidadoSMM();
            List<SMMDepartamentos> lt = pi.TraeDepartamentos();

            foreach (var t in lt)
            {
                cboDpto.Items.Add(t.GroupName.ToString());
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void FConsolidado_DateSelected(object sender, DateChangedEventArgs e)
    {

        cboDpto.Focus();
    }
    private void BtnBuscar_Clicked(object sender, EventArgs e)
    {
        DatosPickingConsolidadoSMM tp = new DatosPickingConsolidadoSMM();
        // List<SMMPickingConsolidadoClass> lt = new List<SMMPickingConsolidadoClass>();

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;
            try
            {
                if (string.IsNullOrEmpty(cboDpto.SelectedItem.ToString()))
                {
                    Console.WriteLine("Null response: Departamento no seleccionado");
                }
                else
                {
                    string Depa = cboDpto.SelectedItem.ToString() ??
                                    throw new InvalidOperationException();
                    DataTable dt = tp.DetallePickingSMM(fecha, Depa);

                    if (dt.Rows.Count != 0)
                    {
                        GvData.IsVisible = true;
                        GvData.ItemsSource = dt;

                        GvData.Columns["CodProducto"].Caption = "CodProducto";
                        GvData.Columns["Producto"].Caption = "Producto";
                        GvData.Columns["DeptoVentas"].IsVisible = false;
                        GvData.Columns["FechaEntrega"].IsVisible = false;
                        //GvData.RowHeight = 150;
                        //GvData.ColumnsAutoWidth = true;
                        GvData.Columns["CantidadSolicitada"].Caption = "Cant.Soli";
                        GvData.Columns["CantidadPikear"].Caption = "Cant.Pendiente";
                        //GvDa+lta.Columns["Package_SSCC"].IsVisible = false;


                        btnPikear.IsEnabled = true;
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "No se encontraron Datos", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("¡Error!", "Seleccione una Fecha y Departamento válidos.", "Ok");
                Console.WriteLine(ex.ToString());
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void BtnPikear_Clicked(object sender, EventArgs e)
    {
        string fecha = FConsolidado.Date.Year + "-" + FConsolidado.Date.Month + "-" + FConsolidado.Date.Day;
        string Depa = cboDpto.SelectedItem.ToString();
        App.Fconsoli = fecha;
        App.DptoConsolidado = Depa;
        Navigation.PushAsync(new SMM_Picking_Consolidado());
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}