using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_TansferenciaDetalle : ContentPage
{
    int _foliTrans = 0;
    public SMM_TansferenciaDetalle(int FolioTrans)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        _foliTrans = FolioTrans;
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();
        txtNPallet.Focus();
        txtNPallet.Text = string.Empty;
        //stData.IsVisible = false;


    }
    private void btnGuardarDetalle_Clicked(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {


            DatosTransferenciaSMM rc = new DatosTransferenciaSMM();

            List<FiltoTransferenciaSMM> lt = rc.FiltroTransferencia(Convert.ToInt32(txtNPallet.Text), _foliTrans);

            if (lt.Count != 0)
            {
                foreach (var t in lt)
                {

                    int idbod = t.Site_Id;
                    int pk = t.Package_Id;
                    int lay = t.Layout_Id;
                    int idus = App.Iduser;
                    int transID = _foliTrans;
                    string Cantidad = Convert.ToString(t.Package_Quantity);

                    bool res = rc.AgregaBultoTransferSMM(idbod, pk, lay, idus, transID, Cantidad);

                    if (res == true)
                    {
                        lblBodega.Text = string.Empty;
                        lblLote.Text = string.Empty;
                        lblCodPro.Text = string.Empty;
                        lblProducto.Text = string.Empty;
                        lblCantidad.Text = string.Empty;
                        lblPosicion.Text = string.Empty;
                        lblCodUbicacion.Text = string.Empty;

                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "bulto Registrado", "Aceptar");
                        txtNPallet.Text = string.Empty;
                        txtNPallet.Focus();
                        //stData.IsVisible = false;
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        DisplayAlert("Alerta", "Error al Registrar Bulto Favor Verificar", "Aceptar");
                    }

                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "No se encontro el bulto", "Aceptar");

            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private async void txtNPallet_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            if (txtNPallet.Text != string.Empty)
            {
                DatosTransferenciaSMM rc = new DatosTransferenciaSMM();

                List<FiltoTransferenciaSMM> lt = rc.FiltroTransferencia(Convert.ToInt32(txtNPallet.Text), _foliTrans);

                if (lt.Count != 0)
                {
                    foreach (var t in lt)
                    {

                        lblBodega.Text = t.Site_Description;
                        lblLote.Text = t.Package_Lot;
                        lblCodPro.Text = t.ItemCode;
                        lblProducto.Text = t.ItemName;
                        lblCantidad.Text = Convert.ToString(t.Package_Quantity);
                        lblPosicion.Text = t.Layout_Description;
                        lblCodUbicacion.Text = t.Layout_Id.ToString();
                        //stData.IsVisible = true;

                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "No se encontro el bulto", "Aceptar");

                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "ingrese N° de pallet", "Aceptar");
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private async void btnVerRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SMM_TransferenciaBultosCargados(_foliTrans) { Title = "Finalizar" });
    }
    private async void btnSalir_Clicked(object sender, EventArgs e)
    {
        var res = await DisplayAlert("Message", "Desea Terminar de ingresar Bultos?", "SI", "NO");
        if (res)
        {
            await Navigation.PopToRootAsync(true);

        }
        else
        {

            txtNPallet.Focus();
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}