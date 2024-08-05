using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class SMM_TransferenciaCabecera : ContentPage
{
    public SMM_TransferenciaCabecera()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        cargaDatos();
    }
    void cargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosTransferenciaSMM ti = new DatosTransferenciaSMM();
            List<SMMSite> lt = ti.ListaBodegas();
            List<SMMFolioSolicitud> fs = ti.ListadoFolios(2);



            List<Bodegas> fl = new List<Bodegas>();
            List<Entidad> en = new List<Entidad>();
            List<folios> fol = new List<folios>();

            en.Add(new Entidad { idEntidad = 1, NomEntidad = "Produccion" });
            en.Add(new Entidad { idEntidad = 2, NomEntidad = "Sala de Ventas" });
            en.Add(new Entidad { idEntidad = 3, NomEntidad = "Mermas" });

            cboEntidad.BindingContext = en;

            foreach (var f in fs)
            {
                fol.Add(new folios { folioSoli = f.Id_Solicitud });
            }


            foreach (var t in lt)
            {
                fl.Add(new Bodegas { idBodega = t.Site_Id.ToString(), NombreBodega = t.Site_Description });

            }
            cboBodega.BindingContext = fl;
            cboBodegaDestino.BindingContext = fl;
            cboFolioSoli.BindingContext = fol;

        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    public class Bodegas
    {
        public string idBodega { get; set; }
        public string NombreBodega { get; set; }


    }
    public class Entidad
    {
        public int idEntidad { get; set; }
        public string NomEntidad { get; set; }
    }
    public class folios
    {
        public int folioSoli { get; set; }
    }
    private void cboBodega_SelectionChanged(object sender, EventArgs e)
    {
        cboBodegaDestino.Focus();
    }
    private void cboBodegaDestino_SelectionChanged(object sender, EventArgs e)
    {
        int res = Convert.ToInt32(cboBodegaDestino.SelectedValue.ToString());
        int res2 = Convert.ToInt32(cboBodega.SelectedValue.ToString());
        if (res == res2)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Bodegas iguales favor verificar", "Aceptar");
        }
        else
        {

            cboEntidad.Focus();
        }
    }
    private void cboEntidad_SelectionChanged(object sender, EventArgs e)
    {
        txtFolioEntidad.Focus();
    }
    private void cboFolioSoli_SelectionChanged(object sender, EventArgs e)
    {
        txtComentarios.Focus();
    }
    private void btnCrearOrden_Clicked(object sender, EventArgs e)
    {
        if (cboBodega.SelectedValue == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Seleccione bodega de Origen ", "Aceptar");
        }
        else if (cboBodegaDestino.SelectedValue == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Seleccione bodega de destino ", "Aceptar");
        }
        else if (cboEntidad.SelectedValue == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Seleccione Entidad ", "Aceptar");
        }
        else if (txtFolioEntidad.Equals(string.Empty))
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "ingrese folio Entidad ", "Aceptar");
        }
        else if (cboFolioSoli == null)
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            DisplayAlert("Alerta", "Seleccione Folio Solicitud ", "Aceptar");
        }
        else
        {

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosTransferenciaSMM dt = new DatosTransferenciaSMM();

                int bodO = Convert.ToInt32(cboBodega.SelectedValue.ToString());
                int bodD = Convert.ToInt32(cboBodegaDestino.SelectedValue.ToString());
                int idus = App.Iduser;
                int ent = Convert.ToInt32(cboEntidad.SelectedValue.ToString());
                int foliE = Convert.ToInt32(txtFolioEntidad.Text);
                string Com = txtComentarios.Text;
                int fsol = Convert.ToInt32(cboFolioSoli.SelectedValue.ToString());


                int resp = dt.InsertaTransferenciaCab(bodO, bodD, idus, ent, foliE, Com, fsol);

                if (resp != 0)
                {
                    Navigation.PushAsync(new SMM_TansferenciaDetalle(resp) { Title = "Finalizar" });
                }
                else
                {

                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    DisplayAlert("Alerta", "Error al Registrar Favor Verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }

        }
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return false;
    }
}