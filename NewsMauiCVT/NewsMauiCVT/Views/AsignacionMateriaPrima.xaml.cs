using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrima : ContentPage
{
    public int folioSelected = 0;
    public AsignacionMateriaPrima()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LoadData();
        LogUsabilidad("ingreso");
    }
    private void ClearComponent()
    {
        cboFolioTransfer.SelectedIndex = -1;
    }
    public class Datos
    {
        public string data { get; set; }
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
                    List<FolTransfer> fl = [];

                    foreach (var t in dt)
                    {
                        fl.Add(new FolTransfer { folioTransfer = t.Transfer_Id });
                    }
                    cboFolioTransfer.BindingContext = fl;
                }

                var cantidad = cboFolioTransfer.Height;
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
    public class FolTransfer
    {   public string folioTransfer { get; set; }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(cboFolioTransfer.SelectedIndex != -1)
        {
            try
            {
                folioSelected = cboFolioTransfer.SelectedIndex == -1 ? 0 : int.Parse(cboFolioTransfer.SelectedValue.ToString());
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
        if (cboFolioTransfer.SelectedIndex != -1)
        {
            await Navigation.PushAsync(new AsignacionMateriaPrimaDetalle(folioSelected));
        }
        else
        {
            DisplayAlert("Alerta", "Seleccione un folio válido", "OK");
        }
    }
    private async void btn_asignado_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResumenAsignadosMP(folioSelected));
    }
}