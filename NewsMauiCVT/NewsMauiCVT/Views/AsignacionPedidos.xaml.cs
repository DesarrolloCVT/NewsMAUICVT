using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionPedidos : ContentPage
{
    int folioSelected;
	public AsignacionPedidos()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LoadData();
        LogUsabilidad("Ingreso");
    }
    private void ClearComponent()
    {
        cboFolioOrder.SelectedIndex = -1;
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
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void LoadData()
    {
        try
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("FoliosAsignaciones").Result;

                if (rest.IsSuccessStatusCode)
                {
                    var resultadoStr = rest.Content.ReadAsStringAsync().Result;

                    List<TransferenciasClass> dt = JsonConvert.DeserializeObject<List<TransferenciasClass>>(resultadoStr) ??
                                    throw new InvalidOperationException();
                    List<FolOrder> fo = [];

                    foreach (var t in dt)
                    {
                        fo.Add(new FolOrder { folioOrder = t.Transfer_Id });
                    }
                    cboFolioOrder.BindingContext = fo;
                }
                var cantidad = cboFolioOrder.Height;
                Console.WriteLine("cantidad de Folios: " + cantidad);
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("LoadData: " + ex.ToString());
        }
    }
    public class FolOrder
    {
        public string folioOrder { get; set; }
    }
    private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboFolioOrder.SelectedIndex != -1)
        {
            try
            {
                folioSelected = cboFolioOrder.SelectedIndex == -1 ? 0 : int.Parse(cboFolioOrder.SelectedValue.ToString());
                btn_asignado.IsVisible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CboFolio_SelectedIndexChanged: {ex.Message}");
            }
        }
        else
        {
            btn_asignado.IsVisible = false;
        }
    }

    private async void Btn_seleccionar_Clicked(object sender, EventArgs e)
    {   
        if (cboFolioOrder.SelectedIndex != -1)
        {
            await Navigation.PushAsync(new AsignacionPedidosDetalle(folioSelected));
        }
        else
        {
            DisplayAlert("Alerta", "Seleccione un folio válido", "OK");
        }
    }
    private async void btn_asignado_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenPedidosAsignados(folioSelected));
    }
}