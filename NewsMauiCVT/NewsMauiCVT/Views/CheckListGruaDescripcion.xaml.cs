using Newtonsoft.Json.Linq;

namespace NewsMauiCVT.Views;

public partial class CheckListGruaDescripcion : ContentPage
{
    public Dictionary<string, string> CheckList;
    public CheckListGruaDescripcion(Dictionary<string,string> keyValues)
	{
		InitializeComponent();
        CheckList = keyValues;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        clearComponent();
    }
    private async void btnSiguiente_Clicked(object sender, EventArgs e)
    {
        foreach (var check in CheckList) 
        {
            Console.WriteLine("Key: " + check.Key + " Value: " + check.Value);
        }
        await Navigation.PushAsync(new CheckListGruaObs(CheckList));
    }
    private void clearComponent()
    {
        cbxLucesNo.IsChecked = false;
        cbxLucesSi.IsChecked = false;
        cbxAlarmaNo.IsChecked = false;
        cbxAlarmaSi.IsChecked = false;
        cbxAsientoNo.IsChecked = false;
        cbxAsientoSi.IsChecked = false;
        cbxBalizaNo.IsChecked = false;
        cbxBalizaSi.IsChecked = false;
        cbxBateriaNo.IsChecked = false;
        cbxBateriaSi.IsChecked = false;
        cbxCadenasNo.IsChecked = false;
        cbxCadenasSi.IsChecked = false;
        cbxCinturonNo.IsChecked = false;
        cbxCinturonSi.IsChecked = false;
        cbxDireccionNo.IsChecked = false;
        cbxDireccionSi.IsChecked = false;
        cbxEspejosNo.IsChecked = false;
        cbxEspejosSi.IsChecked = false;
        cbxExtintorNo.IsChecked = false;
        cbxExtintorSi.IsChecked = false;
        cbxFlexibleNo.IsChecked = false;
        cbxFlexibleSi.IsChecked = false;
        cbxFugasNo.IsChecked = false;
        cbxFugasSi.IsChecked = false;
        cbxHidraulicoNo.IsChecked = false;
        cbxHidraulicoSi.IsChecked = false;
        cbxLlantasNo.IsChecked = false;
        cbxLlantasSi.IsChecked = false;
        cbxMotorNo.IsChecked = false;
        cbxMotorSi.IsChecked = false;
        cbxNeumaticosNo.IsChecked = false;
        cbxNeumaticosSi.IsChecked = false;
        cbxPuntoDeBloqueoNo.IsChecked = false;
        cbxPuntoDeBloqueoSi.IsChecked = false;
        cbxSeguroHorquillaNo.IsChecked = false;
        cbxSeguroHorquillaSi.IsChecked = false;
        cbxTableroNo.IsChecked = false;
        cbxTableroSi.IsChecked = false;
        cbxSoporteCilindroNo.IsChecked = false;
        cbxSoporteCilindroSi.IsChecked = false;
        cbxTransmisionNo.IsChecked = false;
        cbxTransmisionSi.IsChecked = false;
        cbxBocinaNo.IsChecked = false;
        cbxBocinaSi.IsChecked = false;
        cbxEscaleraNo.IsChecked = false;
        cbxEscaleraSi.IsChecked = false;
        cbxUñasHorquillaNo.IsChecked = false;
        cbxUñasHorquillaSi.IsChecked = false;
    }
    private void cbxLucesNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.ToString() == "luces")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }

        if (cbxLucesNo.IsChecked == true)
        {
            cbxLucesSi.IsChecked = false;
            CheckList.Add("luces", "Mal Estado");
        }
            
    }
    private void cbxLucesSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "luces")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxLucesSi.IsChecked == true)
        {
            cbxLucesNo.IsChecked = false;
            CheckList.Add("luces", "Buen Estado");
        }
        
    }
    private void cbxMotorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "motor")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxMotorSi.IsChecked == true)
        {
            cbxMotorNo.IsChecked = false;
            CheckList.Add("motor", "buen Estado");
        }
    }
    private void cbxMotorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "motor")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxMotorNo.IsChecked == true)
        {
            cbxMotorSi.IsChecked = false;
            CheckList.Add("motor", "mal Estado");
        }
    }
    private void cbxHidraulicoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "hidraulico")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxHidraulicoNo.IsChecked == true)
        {
            cbxHidraulicoSi.IsChecked = false;
            CheckList.Add("hidraulico", "mal Estado");
        }
    }
    private void cbxFugasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "fugas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxFugasNo.IsChecked == true)
        {
            cbxFugasSi.IsChecked = false;
            CheckList.Add("fugas", "mal Estado");
        }
    }
    private void cbxDireccionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "direccion")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxDireccionNo.IsChecked == true)
        {
            cbxDireccionSi.IsChecked = false;
            CheckList.Add("direccion", "mal Estado");
        }
    }
    private void cbxTransmisionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "transmision")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxTransmisionNo.IsChecked == true)
        {
            cbxTransmisionSi.IsChecked = false;
            CheckList.Add("transmision", "mal Estado");
        }
    }
    private void cbxEscaleraNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "escalera")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxEscaleraNo.IsChecked == true)
        {
            cbxEscaleraSi.IsChecked = false;
            CheckList.Add("escalera", "mal Estado");
        }
    }
    private void cbxBocinaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "bocina")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBocinaNo.IsChecked == true)
        {
            cbxBocinaSi.IsChecked = false;
            CheckList.Add("bocina", "mal Estado");
        }
    }
    private void cbxAlarmaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "alarma")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxAlarmaNo.IsChecked == true)
        {
            cbxAlarmaSi.IsChecked = false;
            CheckList.Add("alarma", "mal Estado");
        }
    }
    private void cbxEspejosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "espejos")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxEspejosNo.IsChecked == true)
        {
            cbxEspejosSi.IsChecked = false;
            CheckList.Add("espejos", "mal Estado");
        }
    }
    private void cbxTableroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "tablero")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxTableroNo.IsChecked == true)
        {
            cbxTableroSi.IsChecked = false;
            CheckList.Add("tablero", "mal Estado");
        }
    }
    private void cbxExtintorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "extintor")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxExtintorNo.IsChecked == true)
        {
            cbxExtintorSi.IsChecked = false;
            CheckList.Add("extintor", "mal Estado");
        }
    }
    private void cbxBateriaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "bateria")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBateriaNo.IsChecked == true)
        {
            cbxBateriaSi.IsChecked = false;
            CheckList.Add("bateria", "mal Estado");
        }
    }
    private void cbxAsientoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "asiento")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxAsientoNo.IsChecked == true)
        {
            cbxAsientoSi.IsChecked = false;
            CheckList.Add("asiento", "mal Estado");
        }
    }
    private void cbxCinturonNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "cinturon")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxCinturonNo.IsChecked == true)
        {
            cbxCinturonSi.IsChecked = false;
            CheckList.Add("cinturon", "mal Estado");
        }
    }
    private void cbxBalizaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "baliza")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBalizaNo.IsChecked == true)
        {
            cbxBalizaSi.IsChecked = false;
            CheckList.Add("baliza", "mal Estado");
        }
    }
    private void cbxNeumaticosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "neumaticos")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxNeumaticosNo.IsChecked == true)
        {
            cbxNeumaticosSi.IsChecked = false;
            CheckList.Add("neumaticos", "mal Estado");
        }
    }
    private void cbxLlantasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "llantas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxLlantasNo.IsChecked == true)
        {
            cbxLlantasSi.IsChecked = false;
            CheckList.Add("llantas", "mal Estado");
        }
    }
    private void cbxCadenasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "cadenas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxCadenasNo.IsChecked == true)
        {
            cbxCadenasSi.IsChecked = false;
            CheckList.Add("cadenas", "mal Estado");
        }
    }
    private void cbxUñasHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "uñashorquilla")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxUñasHorquillaNo.IsChecked == true)
        {
            cbxUñasHorquillaSi.IsChecked = false;
            CheckList.Add("uñashorquilla", "mal Estado");
        }
    }
    private void cbxSoporteCilindroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "soportecilindro")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxSoporteCilindroNo.IsChecked == true)
        {
            cbxSoporteCilindroSi.IsChecked = false;
            CheckList.Add("soportecilindro", "mal Estado");
        }
    }
    private void cbxFlexibleNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "flexible")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxFlexibleNo.IsChecked == true)
        {
            cbxFlexibleSi.IsChecked = false;
            CheckList.Add("flexible", "mal Estado");
        }
    }
    private void cbxSeguroHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "segurohorquilla")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxSeguroHorquillaNo.IsChecked == true)
        {
            cbxSeguroHorquillaSi.IsChecked = false;
            CheckList.Add("segurohorquilla", "mal Estado");
        }
    }
    private void cbxPuntoDeBloqueoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "puntodebloqueo")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxPuntoDeBloqueoNo.IsChecked == true)
        {
            cbxPuntoDeBloqueoSi.IsChecked = false;
            CheckList.Add("puntodebloqueo", "mal Estado");
        }
    }
    private void cbxHidraulicoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "hidraulico")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxHidraulicoSi.IsChecked == true)
            cbxHidraulicoNo.IsChecked = false;
    }
    private void cbxFugasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "fugas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxFugasSi.IsChecked == true)
        {
            cbxFugasNo.IsChecked = false;
            CheckList.Add("fugas", "buen Estado");
        }
    }
    private void cbxDireccionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "direccion")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxDireccionSi.IsChecked == true)
        {
            cbxDireccionNo.IsChecked = false;
            CheckList.Add("direccion", "buen Estado");
        }
    }
    private void cbxTransmisionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "transmision")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxTransmisionSi.IsChecked == true)
        {
            cbxTransmisionNo.IsChecked = false;
            CheckList.Add("transmision", "buen Estado");
        }
    }
    private void cbxEscaleraSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "escalera")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxEscaleraSi.IsChecked == true)
        {
            cbxEscaleraNo.IsChecked = false;
            CheckList.Add("escalera", "buen Estado");
        }
    }
    private void cbxBocinaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "bocina")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBocinaSi.IsChecked == true)
        {
            cbxBocinaNo.IsChecked = false;
            CheckList.Add("bocina", "buen Estado");
        }
    }
    private void cbxAlarmaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "alarma")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxAlarmaSi.IsChecked == true)
        {
            cbxAlarmaNo.IsChecked = false;
            CheckList.Add("alarma", "buen Estado");
        }
    }
    private void cbxEspejosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "espejos")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxEspejosSi.IsChecked == true)
        {
            cbxEspejosNo.IsChecked = false;
            CheckList.Add("espejos", "buen Estado");
        }
    }
    private void cbxTableroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "tablero")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxTableroSi.IsChecked == true)
        {
            cbxTableroNo.IsChecked = false;
            CheckList.Add("tablero", "buen Estado");
        }
    }
    private void cbxExtintorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "extintor")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxExtintorSi.IsChecked == true)
        {
            cbxExtintorNo.IsChecked = false;
            CheckList.Add("extintor", "buen Estado");
        }
    }
    private void cbxBateriaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "bateria")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBateriaSi.IsChecked == true)
        {
            cbxBateriaNo.IsChecked = false;
            CheckList.Add("bateria", "buen Estado");
        }
    }
    private void cbxAsientoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "asiento")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxAsientoSi.IsChecked == true)
        {
            cbxAsientoNo.IsChecked = false;
            CheckList.Add("asiento", "buen Estado");
        }
    }
    private void cbxCinturonSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "cinturon")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxCinturonSi.IsChecked == true)
        {
            cbxCinturonNo.IsChecked = false;
            CheckList.Add("cinturon", "buen Estado");
        }
    }
    private void cbxBalizaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "baliza")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxBalizaSi.IsChecked == true)
        {
            cbxBalizaNo.IsChecked = false;
            CheckList.Add("baliza", "buen Estado");
        }
    }
    private void cbxNeumaticosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "neumaticos")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxNeumaticosSi.IsChecked == true)
        {
            cbxNeumaticosNo.IsChecked = false;
            CheckList.Add("neumaticos", "buen Estado");
        }
    }
    private void cbxLlantasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "llantas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxLlantasSi.IsChecked == true)
        {
            cbxLlantasNo.IsChecked = false;
            CheckList.Add("llantas", "buen Estado");
        }
    }
    private void cbxCadenasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "cadenas")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxCadenasSi.IsChecked == true)
        {
            cbxCadenasNo.IsChecked = false;
            CheckList.Add("cadenas", "buen Estado");
        }
    }
    private void cbxUñasHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "uñashorquilla")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxUñasHorquillaSi.IsChecked == true)
        {
            cbxUñasHorquillaNo.IsChecked = false;
            CheckList.Add("uñashorquillas", "buen Estado");
        }
    }
    private void cbxSoporteCilindroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "soportecilindro")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxSoporteCilindroSi.IsChecked == true)
        {
            cbxSoporteCilindroNo.IsChecked = false;
            CheckList.Add("soportecilindro", "buen Estado");
        }
    }
    private void cbxFlexibleSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "flexible")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxFlexibleSi.IsChecked == true)
        {
            cbxFlexibleNo.IsChecked = false;
            CheckList.Add("flexible", "buen Estado");
        }
    }
    private void cbxSeguroHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "segurohorquilla")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxSeguroHorquillaSi.IsChecked == true)
        {
            cbxSeguroHorquillaNo.IsChecked = false;
            CheckList.Add("segurohorquilla", "buen Estado");
        }
    }
    private void cbxPuntoDeBloqueoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        foreach (var l in CheckList)
        {
            if (l.Key.ToString() == "puntodebloqueo")
            {
                CheckList.Remove(l.Key.ToString());
            }
        }
        if (cbxPuntoDeBloqueoSi.IsChecked == true)
        {
            cbxPuntoDeBloqueoNo.IsChecked = false;
            CheckList.Add("puntodebloqueo", "buen Estado");
        }
    }
}