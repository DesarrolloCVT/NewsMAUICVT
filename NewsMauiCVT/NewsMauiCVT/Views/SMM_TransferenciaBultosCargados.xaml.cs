using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using System.Data;

namespace NewsMauiCVT.Views;

public partial class SMM_TransferenciaBultosCargados : ContentPage
{
    #region Variables Globales
    int _fol = 0;
    #endregion

    public SMM_TransferenciaBultosCargados(int FolioTransFer)
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargadatos(FolioTransFer);
        _fol = FolioTransFer;
    }
    private void btnQuitar_Clicked(object sender, EventArgs e)
    {
        DatosTransferenciaSMM tr = new DatosTransferenciaSMM();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            int pk = tr.TraePKID(Convert.ToInt32(lblBodega.Text));
            if (pk != 0)
            {
                bool ret = tr.EliminaPalletDetTrans(_fol, pk);

                if (ret == true)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "bulto Quitado", "Aceptar");
                    cargadatos(_fol);
                    GvData.RefreshData();
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error al elimiar el bulto favor verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "N° de pallet no existe", "Aceptar");
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    void cargadatos(int idFolio)
    {
        DatosTransferenciaSMM tr = new DatosTransferenciaSMM();
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DataTable dt = tr.bultosCargadosTransferencia(Convert.ToInt32(idFolio));
            GvData.ItemsSource = dt;

            if (dt.Rows.Count != 0)
            {
                GvData.Columns["Transfer_Id"].IsVisible = false;
                GvData.Columns["Package_SSCC"].Caption = "SSCC";
                GvData.Columns["Package_SSCC"].Width = 110;
                GvData.Columns["ItemCode"].Caption = "CodProducto";
                GvData.Columns["ItemCode"].Width = 110;
                GvData.Columns["ItemName"].Caption = "Producto";
                GvData.Columns["ItemName"].Width = 110;
                GvData.Columns["Package_Quantity"].Caption = "C.Actual";
                GvData.Columns["Package_Quantity"].Width = 110;
                GvData.Columns["Site_Description"].Caption = "Bodega";
                GvData.Columns["Site_Description"].Width = 110;
                GvData.Columns["Layout_Description"].Caption = "Pos.Actual";
                GvData.Columns["Layout_Description"].Width = 110;
                GvData.Columns["Package_Lot"].Caption = "Lote";
                GvData.Columns["Package_Lot"].Width = 110;
                GvData.Columns["Status"].Caption = "Estado";
                GvData.Columns["Status"].Width = 110;
                GvData.Columns["Date"].Caption = "Fecha";
                GvData.Columns["Date"].DisplayFormat = "dd-MM-yyyy";
                GvData.Columns["Date"].Width = 110;
                GvData.Columns["Package_ProductionDate"].Caption = "Fech.Produccion";
                GvData.Columns["Package_ProductionDate"].Width = 110;
                GvData.Columns["Package_ProductionDate"].DisplayFormat = "dd-MM-yyyy";
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "no se encontraron datos", "Aceptar");
            }
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }
}