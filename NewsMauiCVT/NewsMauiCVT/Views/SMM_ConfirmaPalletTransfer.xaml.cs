using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_ConfirmaPalletTransfer : ContentPage
{
    int _varPk = 0;

    public SMM_ConfirmaPalletTransfer()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        SetFocusText();

        /*Shell shell = new Shell();
        Shell.SetFlyoutBehavior(this, FlyoutBehavior.Flyout);
        shell.FlyoutHeaderBehavior = FlyoutHeaderBehavior.Fixed;
        shell.FlyoutVerticalScrollMode = ScrollMode.Auto;*/
    }
    private void SetFocusText()
    {
        _ = Task.Delay(200).ContinueWith(t => {
            txtFolioTransfer.Focus();
        });
    }
    void ClearComponent()
    {
        txtNPallet.IsVisible = false;
        btnSalir.IsVisible = false;
        btnSalir.IsEnabled = false;
        txtNPallet.Text = string.Empty;
        txtFolioTransfer.Text = string.Empty;

    }
    private async void txtFolioTransfer_Completed(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            int nFolio = Convert.ToInt32(txtFolioTransfer.Text);
            DatosConfirmaPalletTransferSMM df = new DatosConfirmaPalletTransferSMM();

            int exFol = df.ConfirmaFolio(nFolio);

            if (exFol != 0)
            {
                txtNPallet.IsVisible = true;
                txtNPallet.Focus();
            }
            else
            {

                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Folio Transferencia no Existe o Transferencia Cerrada", "Aceptar");
                txtFolioTransfer.Text = string.Empty;
                txtFolioTransfer.Focus();
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");


        }
    }
    private async void txtNPallet_Completed(object sender, EventArgs e)
    {

        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosRepaletizadoSMM dr = new DatosRepaletizadoSMM();

            List<SMMPackageClass> ls = dr.ObtieneDatosPaletSMM(Convert.ToInt32(txtNPallet.Text));

            if (ls.Count == 0)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Numero de pallet No existe", "Aceptar");
                txtNPallet.Focus();
                txtNPallet.Text = string.Empty;

            }
            else
            {

                foreach (var p in ls)
                {
                    int nFolio = Convert.ToInt32(txtFolioTransfer.Text);
                    DatosConfirmaPalletTransferSMM df = new DatosConfirmaPalletTransferSMM();

                    int pkID = df.ConfirmaPallet(nFolio, p.Package_Id);

                    if (pkID != 0)
                    {
                        lbl_codproducto.Text = "Cod.Producto: " + p.ItemCode;
                        lbl_producto.Text = "Producto: " + dr.ObtieneNombreProducSMM(p.ItemCode);
                        lbl_Cantidad.Text = "Cantidad :" + p.Package_Quantity;
                        _varPk = pkID;

                        btn_agregar.IsEnabled = true;
                        btnSalir.IsEnabled = true;
                        btnSalir.IsVisible = true;

                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        await DisplayAlert("Alerta", "Numero de pallet No existe en la trasferecia", "Aceptar");

                        txtNPallet.Focus();
                        txtNPallet.Text = string.Empty;
                    }

                }
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");

        }

    }    
    private void btn_agregar_Clicked(object sender, EventArgs e)
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosConfirmaPalletTransferSMM dr = new DatosConfirmaPalletTransferSMM();

            int res = dr.ActualizaConfirma(_varPk, Convert.ToInt32(txtFolioTransfer.Text));

            if (res != 1)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Error Al confirmar favor verificar o contactar con administrador", "Aceptar");
            }
            else
            {
                DisplayAlert("Alerta", "Pallet Confirmado", "Aceptar");
                lbl_codproducto.Text = string.Empty;
                lbl_producto.Text = string.Empty;
                lbl_Cantidad.Text = string.Empty;
                txtNPallet.Text = string.Empty;
                txtNPallet.Focus();
            }

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    private void btnSalir_Clicked(object sender, EventArgs e)
    {
        txtNPallet.IsVisible = false;
        txtNPallet.Text = string.Empty;
        btnSalir.IsVisible = false;
        btnSalir.IsEnabled = false;
        txtFolioTransfer.Focus();
        txtFolioTransfer.Text = string.Empty;
        btn_agregar.IsEnabled = false;
    }
    protected override bool OnBackButtonPressed()
    { //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}