using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using Newtonsoft.Json;
using static NewsMauiCVT.Views.TomaInventario;

namespace NewsMauiCVT.Views;

public partial class Transferencias : ContentPage
{
    public int folioSelected;
	public Transferencias()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
        ClearComponent();
        LogUsabilidad("Ingreso");
    }
    private void LoadData()
    {   
        int value = 20;
        try
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                HttpClient ClientHttp = new()
                {
                    BaseAddress = new Uri("http://wsintranet2.cvt.local/")
                };
                var rest = ClientHttp.GetAsync("api/Bodega?tranferencias=" + value).Result;

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
    private void ClearComponent()
    {
        cboFolioTransfer.SelectedIndex = -1;
    }
    public class FolTransfer
    {
        public string folioTransfer { get; set; }
    }
    private void CboFolio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            folioSelected = cboFolioTransfer.SelectedIndex == -1 ? 0 : int.Parse(cboFolioTransfer.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CboFolio_SelectedIndexChanged: {ex.Message}");
        }
    }
    private async void Btn_seleccionar_Clicked(object sender, EventArgs e)
    {   
        try
        {
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                if (cboFolioTransfer.SelectedIndex != -1)
                {
                    LogUsabilidad("Selccion folio tranferencias");
                    //await Navigation.PushAsync(new TransferenciasDetalle(folioSelected));
                    await Navigation.PushAsync(new AsignacionPedidos());
                }   
                else
                    await DisplayAlert("Aviso", "Selecciona un Folio", "Ok");
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Btn_seleccionar_Clicked: " + ex.ToString());
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
    private void LogUsabilidad(string accion)
    {
        var Usuario = App.Iduser;
        var Fecha = DateTime.Now;
        var TipoRegistro = accion;
        var IdSubMenu = 22;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}