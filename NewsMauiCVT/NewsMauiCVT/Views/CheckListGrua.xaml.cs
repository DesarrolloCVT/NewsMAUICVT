using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using static NewsMauiCVT.Views.CheckListGrua;

namespace NewsMauiCVT.Views;

public partial class CheckListGrua : ContentPage
{
	public CheckListGrua()
	{
		InitializeComponent();
        LoadDatos();
	}

    public void LoadDatos()
    {
        List<Turnos> dataTurnos = [];
        dataTurnos.Add(new Turnos { turnos = "Primer Turno" });
        dataTurnos.Add(new Turnos { turnos = "Segundo Turno" });
        cboTurno.BindingContext = dataTurnos;

        List<AreaTrabajo> dataAreaTrabajo = [];
        dataAreaTrabajo.Add(new AreaTrabajo { area = "BodegaPT" });
        dataAreaTrabajo.Add(new AreaTrabajo { area = "Envasado" });
        dataAreaTrabajo.Add(new AreaTrabajo { area = "Inventario" });
        cboAreaTrabajo.BindingContext = dataAreaTrabajo;

        DatosApp datos = new DatosApp();
        var listado = datos.DatosMaquinarias(2);

        List<NumGrua> numeroGrua = []; 

        foreach (var l in listado)
        {
            numeroGrua.Add(new NumGrua { numGrua = l.Grua_Numero });
        }

        List<TipoGrua> tipoDeGrua = [];

        foreach (var t in listado)
        {
            tipoDeGrua.Add(new TipoGrua { tipoGrua = t.Tipo });
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
        FechaCheckList.Focus();
    }
    private async void btnSiguiente_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CheckListGruaDescripcion());
    }
    private void cboTurno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(CboAreaTrabajo_SelectedIndexChanged != null)
            {
                Console.WriteLine("cboTurno_SelectedIndexChanged: " + cboTurno.SelectedIndex);
            }
        }catch (Exception ex)
        {
            Console.WriteLine("cboTurno_SelectedIndexChanged: " + ex.ToString());
        }
    }
    private void CboAreaTrabajo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Console.WriteLine("CboAreaTrabajo_SelectedIndexChanged: " + cboAreaTrabajo.SelectedIndex);
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
            Console.WriteLine("cboNumGrua_SelectedIndexChanged: " + cboAreaTrabajo.SelectedIndex);
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

        }
        catch (Exception ex)
        {
            Console.WriteLine("cboTipoMaquina_SelectedIndexChanged: " + ex.ToString());
        }
    }
}