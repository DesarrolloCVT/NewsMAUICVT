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
        if (CheckList.Count >= 30)
        {
            await Navigation.PushAsync(new CheckListGruaObs(CheckList));
        }
        else
        {
            Console.WriteLine("CheckList.Count: " + CheckList.Count);
            await DisplayAlert("Alerta", "Complete la información solicitada ", "OK");
        }
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
        cbxUnasHorquillaNo.IsChecked = false;
        cbxUnasHorquillaSi.IsChecked = false;
    }
    private void cbxLucesNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Luces")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }

            if (cbxLucesNo.IsChecked == true)
            {
                cbxLucesSi.IsChecked = false;
                CheckList.Add("Luces", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxLucesNo_CheckedChanged: " + ex.ToString());
        }   
    }
    private void cbxLucesSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Luces")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxLucesSi.IsChecked == true)
            {
                cbxLucesNo.IsChecked = false;
                CheckList.Add("Luces", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxLucesSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxMotorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Motor")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxMotorSi.IsChecked == true)
            {
                cbxMotorNo.IsChecked = false;
                CheckList.Add("Motor", "Buen Estado");
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine("cbxMotorSi_CheckedChanged: " + ex.ToString());
        }
        
    }
    private void cbxMotorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Motor")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxMotorNo.IsChecked == true)
            {
                cbxMotorSi.IsChecked = false;
                CheckList.Add("Motor", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxMotorNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxHidraulicoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
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
                CheckList.Add("hidraulico", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxHidraulicoNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxFugasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Fugas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxFugasNo.IsChecked == true)
            {
                cbxFugasSi.IsChecked = false;
                CheckList.Add("Fugas", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxFugasNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxDireccionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Direccion")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxDireccionNo.IsChecked == true)
            {
                cbxDireccionSi.IsChecked = false;
                CheckList.Add("Direccion", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxDireccionNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxTransmisionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Transmision")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxTransmisionNo.IsChecked == true)
            {
                cbxTransmisionSi.IsChecked = false;
                CheckList.Add("Transmision", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxTransmisionNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxEscaleraNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Escalera")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxEscaleraNo.IsChecked == true)
            {
                cbxEscaleraSi.IsChecked = false;
                CheckList.Add("Escalera", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxEscaleraNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBocinaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Bocina")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBocinaNo.IsChecked == true)
            {
                cbxBocinaSi.IsChecked = false;
                CheckList.Add("Bocina", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBocinaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxAlarmaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Alarma")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxAlarmaNo.IsChecked == true)
            {
                cbxAlarmaSi.IsChecked = false;
                CheckList.Add("Alarma", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxAlarmaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxEspejosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Espejos")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxEspejosNo.IsChecked == true)
            {
                cbxEspejosSi.IsChecked = false;
                CheckList.Add("Espejos", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxEspejosNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxTableroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Tablero")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxTableroNo.IsChecked == true)
            {
                cbxTableroSi.IsChecked = false;
                CheckList.Add("Tablero", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxTableroNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxExtintorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Extintor")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxExtintorNo.IsChecked == true)
            {
                cbxExtintorSi.IsChecked = false;
                CheckList.Add("Extintor", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxExtintorNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBateriaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Bateria")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBateriaNo.IsChecked == true)
            {
                cbxBateriaSi.IsChecked = false;
                CheckList.Add("Bateria", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBateriaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxAsientoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Asiento")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxAsientoNo.IsChecked == true)
            {
                cbxAsientoSi.IsChecked = false;
                CheckList.Add("Asiento", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxAsientoNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxCinturonNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Cinturon")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxCinturonNo.IsChecked == true)
            {
                cbxCinturonSi.IsChecked = false;
                CheckList.Add("Cinturon", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxCinturonNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBalizaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Baliza")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBalizaNo.IsChecked == true)
            {
                cbxBalizaSi.IsChecked = false;
                CheckList.Add("Baliza", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBalizaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxNeumaticosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Neumaticos")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxNeumaticosNo.IsChecked == true)
            {
                cbxNeumaticosSi.IsChecked = false;
                CheckList.Add("Neumaticos", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxNeumaticosNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxLlantasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Llantas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxLlantasNo.IsChecked == true)
            {
                cbxLlantasSi.IsChecked = false;
                CheckList.Add("Llantas", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxLlantasNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxCadenasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Cadenas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxCadenasNo.IsChecked == true)
            {
                cbxCadenasSi.IsChecked = false;
                CheckList.Add("Cadenas", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxCadenasNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxUnasHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Unashorquilla")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxUnasHorquillaNo.IsChecked == true)
            {
                cbxUnasHorquillaSi.IsChecked = false;
                CheckList.Add("Unashorquilla", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxUnasHorquillaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxUnasHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Unashorquilla")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxUnasHorquillaSi.IsChecked == true)
            {
                cbxUnasHorquillaNo.IsChecked = false;
                CheckList.Add("Unashorquilla", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxUnasHorquillaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxSoporteCilindroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Soportecilindro")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxSoporteCilindroNo.IsChecked == true)
            {
                cbxSoporteCilindroSi.IsChecked = false;
                CheckList.Add("Soportecilindro", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxSoporteCilindroNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxFlexibleNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Flexible")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxFlexibleNo.IsChecked == true)
            {
                cbxFlexibleSi.IsChecked = false;
                CheckList.Add("Flexible", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxFlexibleNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxSeguroHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Segurohorquilla")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxSeguroHorquillaNo.IsChecked == true)
            {
                cbxSeguroHorquillaSi.IsChecked = false;
                CheckList.Add("Segurohorquilla", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxSeguroHorquillaNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxPuntoDeBloqueoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Puntodebloqueo")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxPuntoDeBloqueoNo.IsChecked == true)
            {
                cbxPuntoDeBloqueoSi.IsChecked = false;
                CheckList.Add("Puntodebloqueo", "Mal Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxPuntoDeBloqueoNo_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxHidraulicoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "hidraulico")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxHidraulicoSi.IsChecked == true)
            {
                cbxHidraulicoNo.IsChecked = false;
                CheckList.Add("Hidraulico", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxHidraulicoSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxFugasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Fugas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxFugasSi.IsChecked == true)
            {
                cbxFugasNo.IsChecked = false;
                CheckList.Add("Fugas", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxFugasSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxDireccionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Direccion")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxDireccionSi.IsChecked == true)
            {
                cbxDireccionNo.IsChecked = false;
                CheckList.Add("Direccion", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxDireccionSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxTransmisionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Transmision")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxTransmisionSi.IsChecked == true)
            {
                cbxTransmisionNo.IsChecked = false;
                CheckList.Add("Transmision", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxTransmisionSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxEscaleraSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Escalera")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxEscaleraSi.IsChecked == true)
            {
                cbxEscaleraNo.IsChecked = false;
                CheckList.Add("Escalera", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxEscaleraSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBocinaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Bocina")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBocinaSi.IsChecked == true)
            {
                cbxBocinaNo.IsChecked = false;
                CheckList.Add("Bocina", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBocinaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxAlarmaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Alarma")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxAlarmaSi.IsChecked == true)
            {
                cbxAlarmaNo.IsChecked = false;
                CheckList.Add("Alarma", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxAlarmaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxEspejosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Espejos")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxEspejosSi.IsChecked == true)
            {
                cbxEspejosNo.IsChecked = false;
                CheckList.Add("Espejos", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxEspejosSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxTableroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Tablero")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxTableroSi.IsChecked == true)
            {
                cbxTableroNo.IsChecked = false;
                CheckList.Add("Tablero", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxTableroSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxExtintorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Extintor")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxExtintorSi.IsChecked == true)
            {
                cbxExtintorNo.IsChecked = false;
                CheckList.Add("Extintor", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxExtintorSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBateriaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Bateria")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBateriaSi.IsChecked == true)
            {
                cbxBateriaNo.IsChecked = false;
                CheckList.Add("Bateria", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBateriaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxAsientoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Asiento")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxAsientoSi.IsChecked == true)
            {
                cbxAsientoNo.IsChecked = false;
                CheckList.Add("Asiento", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxAsientoSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxCinturonSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Cinturon")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxCinturonSi.IsChecked == true)
            {
                cbxCinturonNo.IsChecked = false;
                CheckList.Add("Cinturon", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxCinturonSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxBalizaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Baliza")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxBalizaSi.IsChecked == true)
            {
                cbxBalizaNo.IsChecked = false;
                CheckList.Add("Baliza", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxBalizaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxNeumaticosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Neumaticos")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxNeumaticosSi.IsChecked == true)
            {
                cbxNeumaticosNo.IsChecked = false;
                CheckList.Add("Neumaticos", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxNeumaticosSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxLlantasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Llantas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxLlantasSi.IsChecked == true)
            {
                cbxLlantasNo.IsChecked = false;
                CheckList.Add("Llantas", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxLlantasSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxCadenasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Cadenas")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxCadenasSi.IsChecked == true)
            {
                cbxCadenasNo.IsChecked = false;
                CheckList.Add("Cadenas", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxCadenasSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxSoporteCilindroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Soportecilindro")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxSoporteCilindroSi.IsChecked == true)
            {
                cbxSoporteCilindroNo.IsChecked = false;
                CheckList.Add("Soportecilindro", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxSoporteCilindroSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxFlexibleSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Flexible")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxFlexibleSi.IsChecked == true)
            {
                cbxFlexibleNo.IsChecked = false;
                CheckList.Add("Flexible", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxFlexibleSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxSeguroHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Segurohorquilla")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxSeguroHorquillaSi.IsChecked == true)
            {
                cbxSeguroHorquillaNo.IsChecked = false;
                CheckList.Add("Segurohorquilla", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxSeguroHorquillaSi_CheckedChanged: " + ex.ToString());
        }
    }
    private void cbxPuntoDeBloqueoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            foreach (var l in CheckList)
            {
                if (l.Key.ToString() == "Puntodebloqueo")
                {
                    CheckList.Remove(l.Key.ToString());
                }
            }
            if (cbxPuntoDeBloqueoSi.IsChecked == true)
            {
                cbxPuntoDeBloqueoNo.IsChecked = false;
                CheckList.Add("Puntodebloqueo", "Buen Estado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("cbxPuntoDeBloqueoSi_CheckedChanged: " + ex.ToString());
        }
    }
}