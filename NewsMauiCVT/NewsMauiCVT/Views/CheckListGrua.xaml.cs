using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using static NewsMauiCVT.Views.CheckListGrua;

namespace NewsMauiCVT.Views;

public partial class CheckListGrua : ContentPage
{
    #region Variables Globales
    public Dictionary<string, string> CheckListData = new Dictionary<string, string>();  // = new Dictionary<string, string>();
    public string _horometro;
    public DateTime _fecha;
    public string _turno;
    public string _areaDeTrabajo;
    public string _numeroDeGrua;
    public string _tipoDeMaquinaria;

    public static Dictionary<string, string> _checkListData
    {
        get => _checkListData;
    }
    public string Horometro
    {
        get => _horometro;
        set
        {
            _horometro = value;
            OnPropertyChanged(nameof(Horometro));
        }
    }
    public DateTime Fecha
    {
        get => _fecha;
        set
        {
            _fecha = value;
            OnPropertyChanged(nameof(Fecha));
        }
    }
    public string Turno
    {
        get => _turno;
        set
        {
            _turno = value;
            OnPropertyChanged(nameof(Turno));
        }
    }
    public string AreaDeTrabajo
    {
        get => _areaDeTrabajo;
        set
        {
            _areaDeTrabajo = value;
            OnPropertyChanged(nameof(AreaDeTrabajo));
        }
    }
    public string NumeroDeGrua
    {
        get => _numeroDeGrua;
        set
        {
            _numeroDeGrua = value;
            OnPropertyChanged(nameof(NumeroDeGrua));
        }
    }
    public string TipoDeMaquinaria
    {
        get => _tipoDeMaquinaria;
        set
        {
            _tipoDeMaquinaria = value;
            OnPropertyChanged(nameof(TipoDeMaquinaria));
        }
    }
    #endregion

    public CheckListGrua()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        LoadDatos();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        clearComponent();
        LogUsabilidad("Ingreso a CheckList Grua");
    }
    public async void LoadDatos()
    {
        var ACC = Connectivity.NetworkAccess;
        if (ACC == NetworkAccess.Internet)
        {
            FechaCheckList.Date = DateTime.Now;

            List<Turnos> dataTurnos = [];
            List<AreaTrabajo> dataAreaTrabajo = [];
            List<NumGrua> numeroGrua = [];
            List<TipoGrua> tipoDeGrua = [];

            dataTurnos.Add(new Turnos { turnos = "Primer Turno" });
            dataTurnos.Add(new Turnos { turnos = "Segundo Turno" });

            dataAreaTrabajo.Add(new AreaTrabajo { area = "BodegaPT" });
            dataAreaTrabajo.Add(new AreaTrabajo { area = "Envasado" });
            dataAreaTrabajo.Add(new AreaTrabajo { area = "Inventario" });

            cboTurno.BindingContext = dataTurnos;
            cboAreaTrabajo.BindingContext = dataAreaTrabajo;

            DatosCheckListGruas DatosGruas = new();
            var ListadoTipoGruas = DatosGruas.TipoMaquinarias();
            var ListadoNumeroGruas = DatosGruas.NumeroGruas();

            foreach (var l in ListadoNumeroGruas)
            {
                numeroGrua.Add(new NumGrua { numGrua = l });
            }
            foreach (var t in ListadoTipoGruas)
            {
                tipoDeGrua.Add(new TipoGrua { tipoGrua = t });
            }

            cboNumGrua.BindingContext = numeroGrua;
            cboTipoMaquina.BindingContext = tipoDeGrua;
        }
        else
        {
            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
        }
    }
    public class Turnos
    {
        public string turnos { get; set; }
    }
    public class AreaTrabajo
    {
        public string area { get; set; }
    }
    public class NumGrua
    {
        public string numGrua { get; set; }
    }
    public class TipoGrua
    {
        public string tipoGrua { get; set; }
    }
    private void FechaCheckList_DateSelected(object sender, DateChangedEventArgs e)
    {
        Fecha = FechaCheckList.Date;
    }
    private async void btnSiguiente_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Fecha.ToString()))
        {
            Fecha = DateTime.Now;
        }
        Console.WriteLine("Fecha: " + Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
        Horometro = txtHorometro.Text;

        if (cboNumGrua.SelectedIndex != -1 && cboAreaTrabajo.SelectedIndex != -1
            && cboTipoMaquina.SelectedIndex != -1 && cboTurno.SelectedIndex != -1
            && !string.IsNullOrEmpty(txtHorometro.Text) && !string.IsNullOrEmpty(Fecha.ToString()))
        {   
            CheckListData.Add("NumeroGrua", NumeroDeGrua);
            CheckListData.Add("AreaTrabajo", AreaDeTrabajo);
            CheckListData.Add("TipoMaquinaria", TipoDeMaquinaria);
            CheckListData.Add("Turno", Turno);
            CheckListData.Add("Horometro", Horometro.ToString());
            CheckListData.Add("Fecha", Fecha.ToString("yyyy-MM-dd HH:mm:ss"));

            foreach (var item in CheckListData)
            {
                Console.WriteLine(item.Value);
            }
            await Navigation.PushAsync(new CheckListGruaDescripcion(CheckListData));
        }
        else 
        {
            await DisplayAlert("Alerta", "Ingrese la información solicitada ", "OK");
        }
    }
    private void cboTurno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(cboTurno.SelectedIndex != -1)
            {
                Turno = cboTurno.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cboTurno_SelectedIndexChanged: " + ex.ToString());
        }
    }
    private void CboAreaTrabajo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cboAreaTrabajo.SelectedIndex != -1)
            {
                AreaDeTrabajo = cboAreaTrabajo.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("CboAreaTrabajo_SelectedIndexChanged: " + ex.ToString());
        }
    }
    private void cboNumGrua_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cboNumGrua.SelectedIndex != -1)
            {
                NumeroDeGrua = cboNumGrua.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cboNumGrua_SelectedIndexChanged: " + ex.ToString());
        }
    }
    private void cboTipoMaquina_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cboTipoMaquina.SelectedIndex != -1)
            {
                TipoDeMaquinaria = cboTipoMaquina.SelectedValue.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cboTipoMaquina_SelectedIndexChanged: " + ex.ToString());
        }
    }
    private void txtHorometro_Completed(object sender, EventArgs e)
    {
        Horometro = txtHorometro.Text;
    }
    private void clearComponent()
    {
        cboNumGrua.SelectedIndex = -1;
        cboAreaTrabajo.SelectedIndex = -1;
        cboTipoMaquina.SelectedIndex = -1;
        cboTurno.SelectedIndex = -1;
        txtHorometro.Text = string.Empty;
        Fecha = DateTime.Now;
        CheckListData.Clear();
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
        var IdSubMenu = 0;

        DatosApp datosApp = new DatosApp();
        datosApp.LogUsabilidad(IdSubMenu, TipoRegistro);
    }
}