using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class Calidad_ControlHigiene : ContentPage
{
    public Calidad_ControlHigiene()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        CargaDatos();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
        LogUsabilidad("Ingreso");
    }
    void ClearComponent()
    {
        lblError2.Text = string.Empty;
        txtAccionCorrectiva.Text = string.Empty;
        cboMonitor.SelectedIndex = -1;
        cboArea.SelectedIndex = -1;
        cboTipoContraro.SelectedIndex = -1;
        cboPersona.SelectedIndex = -1;
        cboLimpUniforme.SelectedIndex = -1;
        cboAfeitadoCorto.SelectedIndex = -1;
        cboUnas.SelectedIndex = -1;
        cboJoyas.SelectedIndex = -1;
        cboHigiene.SelectedIndex = -1;
        CboEstUniforme.SelectedIndex = -1;
        cboHeridaExpuesta.SelectedIndex = -1;
    }
    private async void CargaDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            DatosCalidadControlHigiene ti = new DatosCalidadControlHigiene();
            List<PersonalClass> lt = ti.ListaPerdonal();
            List<AreaClass> lta = ti.ListaAreas();
            List<PersonalClass> lt2 = ti.ListaPerdonalFull();

            List<PersonalClass> fl = new List<PersonalClass>();
            List<PersonalClass> fl2 = new List<PersonalClass>();
            List<AreaClass> fla = new List<AreaClass>();

            foreach (var t in lt)
            {
                fl.Add(new PersonalClass { Id_Personal = t.Id_Personal, Nombre = t.Nombre + " " + t.apellido });

            }
            foreach (var t in lt2)
            {
                fl2.Add(new PersonalClass { Id_Personal = t.Id_Personal, Nombre = t.Nombre + " " + t.apellido });

            }

            foreach (var ta in lta)
            {
                fla.Add(new AreaClass { Id_Area = ta.Id_Area, Nombre = ta.Nombre });
            }

            List<CVTtipoContrato> lst = new List<CVTtipoContrato>();

            lst.Add(new CVTtipoContrato { tipoCont = "P", NombreCont = "Planta" });
            lst.Add(new CVTtipoContrato { tipoCont = "E", NombreCont = "Contratista" });

            cboMonitor.BindingContext = fl;
            cboArea.BindingContext = fla;
            cboTipoContraro.BindingContext = lst;
            cboPersona.BindingContext = fl2;
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    public class CVTtipoContrato
    {
        public string tipoCont { get; set; }
        public string NombreCont { get; set; }
    }
    private async void btn_agregar_Clicked(object sender, EventArgs e)
    {   
        int v_IdMonitor = cboMonitor.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboMonitor.SelectedValue);
        int v_IdArea = cboArea.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboArea.SelectedValue);
        string v_tipoContrato = cboTipoContraro.SelectedIndex == -1 ? "0" : cboTipoContraro.SelectedValue.ToString();
        int v_Persona = cboPersona.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboPersona.SelectedValue);
        int v_LimpUnifor = cboLimpUniforme.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboLimpUniforme.SelectedValue);
        int v_EstUnifor = CboEstUniforme.SelectedIndex == -1 ? 0 : Convert.ToInt32(CboEstUniforme.SelectedValue);
        int v_AfeiCorto = cboAfeitadoCorto.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboAfeitadoCorto.SelectedValue);
        int v_unas = cboUnas.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboUnas.SelectedValue);
        int v_Joyas = cboJoyas.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboJoyas.SelectedValue);
        int v_higi = cboHigiene.SelectedIndex == -1 ? 0 : Convert.ToInt32(cboHigiene.SelectedValue);
        string v_heri = cboHeridaExpuesta.SelectedIndex == -1 ? "0" : cboHeridaExpuesta.SelectedValue.ToString();
        string v_accion = txtAccionCorrectiva.Text;

        if (v_IdMonitor == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione monitor";
        }
        else if (v_IdArea == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Area";
        }
        else if (v_tipoContrato.Equals("0"))
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Tipocontrato";
        }
        else if (v_Persona == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Persona";
        }
        else if (v_LimpUnifor == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Limpieza Uniforme";
        }
        else if (v_EstUnifor == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Estado uniforme";
        }
        else if (v_AfeiCorto == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Afeitado / pelo corto";
        }
        else if (v_unas == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione uñas";
        }
        else if (v_Joyas == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione joya";
        }
        else if (v_higi == 0)
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Higiene";
        }
        else if (v_heri.Equals("0"))
        {
            lblError2.IsVisible = true;
            lblError2.Text = "Selecione Herida Expuesta";
        }
        else
        {
            lblError2.IsVisible = false;

            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                CVT_ControlHigieneClass c = new CVT_ControlHigieneClass();
                DatosCalidadControlHigiene dtc = new DatosCalidadControlHigiene();

                c.Id_Monitor = v_IdMonitor;
                c.Id_Area = v_IdArea;
                c.Tipo_Contrato = v_tipoContrato;
                c.Id_Persona = v_Persona;
                c.Limpieza_Uniforme = v_LimpUnifor;
                c.Afeitado_PeloCorto = v_AfeiCorto;
                c.Uñas = v_unas;
                c.Joyas = v_Joyas;
                c.Higene = v_higi;
                c.Estado_Uniforme = v_EstUnifor;
                c.Herida_Expuesta = v_heri.Equals("NO") ? 1 : 7;
                c.Accion_Correctiva = txtAccionCorrectiva.Text;

                string ret = dtc.CreaDatosControlHigiene(c);

                if (ret != "-1")
                {

                    lblError2.Text = string.Empty;
                    cboPersona.SelectedIndex = -1;
                    cboLimpUniforme.SelectedIndex = -1;
                    cboAfeitadoCorto.SelectedIndex = -1;
                    cboUnas.SelectedIndex = -1;
                    cboJoyas.SelectedIndex = -1;
                    cboHigiene.SelectedIndex = -1;
                    txtAccionCorrectiva.Text = string.Empty;
                    CboEstUniforme.SelectedIndex = -1;
                    cboHeridaExpuesta.SelectedIndex = -1;
                    cboPersona.Focus();
                    LogUsabilidad("Registro de Higiene");
                    await DisplayAlert("Alerta", "Registrado", "Aceptar");
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "Error al Registrar favor verificar", "Aceptar");
                }
            }
            else
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
            }
        }
    }
    private void cboLimpUniforme_SelectionChanged(object sender, EventArgs e)
    {
        CboEstUniforme.Focus();
    }
    private void CboEstUniforme_SelectionChanged(object sender, EventArgs e)
    {
        cboAfeitadoCorto.Focus();
    }
    private void cboAfeitadoCorto_SelectionChanged(object sender, EventArgs e)
    {
        cboUnas.Focus();
    }
    private void cboUnas_SelectionChanged(object sender, EventArgs e)
    {
        cboJoyas.Focus();
    }
    private void cboJoyas_SelectionChanged(object sender, EventArgs e)
    {
        cboHigiene.Focus();
    }
    private void cboHeridaExpuesta_SelectionChanged(object sender, EventArgs e)
    {
        cboHeridaExpuesta.Focus();
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
        var IdSubMenu = 13;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}