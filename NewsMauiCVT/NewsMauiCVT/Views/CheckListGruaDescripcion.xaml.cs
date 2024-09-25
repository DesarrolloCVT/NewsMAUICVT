namespace NewsMauiCVT.Views;

public partial class CheckListGruaDescripcion : ContentPage
{
	public CheckListGruaDescripcion()
	{
		InitializeComponent();
	}
    private async void btnSiguiente_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CheckListGruaObs());
    }
    private void cbxLucesNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxLucesNo.IsChecked == true)
            cbxLucesSi.IsChecked = false;
    }
    private void cbxLucesSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(cbxLucesSi.IsChecked == true)
        cbxLucesNo.IsChecked = false;
    }

    private void cbxMotorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxMotorSi.IsChecked == true)
            cbxMotorNo.IsChecked = false;
    }

    private void cbxMotorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxMotorNo.IsChecked == true)
            cbxMotorSi.IsChecked = false;
    }

    private void cbxHidraulicoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxHidraulicoNo.IsChecked == true)
            cbxHidraulicoSi.IsChecked = false;
    }

    private void cbxFugasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxFugasNo.IsChecked == true)
            cbxFugasSi.IsChecked = false;
    }

    private void cbxDireccionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxDireccionNo.IsChecked == true)
            cbxDireccionSi.IsChecked = false;
    }

    private void cbxTransmisionNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxTransmisionNo.IsChecked == true)
            cbxTransmisionSi.IsChecked = false;
    }

    private void cbxEscaleraNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxEscaleraNo.IsChecked == true)
            cbxEscaleraSi.IsChecked = false;
    }

    private void cbxBocinaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBocinaNo.IsChecked == true)
            cbxBocinaSi.IsChecked = false;
    }

    private void cbxAlarmaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxAlarmaNo.IsChecked == true)
            cbxAlarmaSi.IsChecked = false;
    }

    private void cbxEspejosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxEspejosNo.IsChecked == true)
            cbxEspejosSi.IsChecked = false;
    }

    private void cbxTableroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxTableroNo.IsChecked == true)
            cbxTableroSi.IsChecked = false;
    }

    private void cbxExtintorNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxExtintorNo.IsChecked == true)
            cbxExtintorSi.IsChecked = false;
    }

    private void cbxBateriaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBateriaNo.IsChecked == true)
            cbxBateriaSi.IsChecked = false;
    }

    private void cbxAsientoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxAsientoNo.IsChecked == true)
            cbxAsientoSi.IsChecked = false;
    }

    private void cbxCinturonNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxCinturonNo.IsChecked == true)
            cbxCinturonSi.IsChecked = false;
    }

    private void cbxBalizaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBalizaNo.IsChecked == true)
            cbxBalizaSi.IsChecked = false;
    }

    private void cbxNeumaticosNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxNeumaticosNo.IsChecked == true)
            cbxNeumaticosSi.IsChecked = false;
    }

    private void cbxLlantasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxLlantasNo.IsChecked == true)
            cbxLlantasSi.IsChecked = false;
    }
    private void cbxCadenasNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxCadenasNo.IsChecked == true)
            cbxCadenasSi.IsChecked = false;
    }

    private void cbxUñasHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxUñasHorquillaNo.IsChecked == true)
            cbxUñasHorquillaSi.IsChecked = false;
    }

    private void cbxSoporteCilindroNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxSoporteCilindroNo.IsChecked == true)
            cbxSoporteCilindroSi.IsChecked = false;
    }

    private void cbxFlexibleNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxFlexibleNo.IsChecked == true)
            cbxFlexibleSi.IsChecked = false;
    }

    private void cbxSeguroHorquillaNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxSeguroHorquillaNo.IsChecked == true)
            cbxSeguroHorquillaSi.IsChecked = false;
    }

    private void cbxPuntoDeBloqueoNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxPuntoDeBloqueoNo.IsChecked == true)
            cbxPuntoDeBloqueoSi.IsChecked = false;
    }

    private void cbxHidraulicoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxHidraulicoSi.IsChecked == true)
            cbxHidraulicoNo.IsChecked = false;
    }

    private void cbxFugasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxFugasSi.IsChecked == true)
            cbxFugasNo.IsChecked = false;
    }

    private void cbxDireccionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxDireccionSi.IsChecked == true)
            cbxDireccionNo.IsChecked = false;
    }

    private void cbxTransmisionSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxTransmisionSi.IsChecked == true)
            cbxTransmisionNo.IsChecked = false;
    }

    private void cbxEscaleraSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxEscaleraSi.IsChecked == true)
            cbxEscaleraNo.IsChecked = false;
    }

    private void cbxBocinaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBocinaSi.IsChecked == true)
            cbxBocinaNo.IsChecked = false;
    }

    private void cbxAlarmaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxAlarmaSi.IsChecked == true)
            cbxAlarmaNo.IsChecked = false;
    }

    private void cbxEspejosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxEspejosSi.IsChecked == true)
            cbxEspejosNo.IsChecked = false;
    }

    private void cbxTableroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxTableroSi.IsChecked == true)
            cbxTableroNo.IsChecked = false;
    }

    private void cbxExtintorSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxExtintorSi.IsChecked == true)
            cbxExtintorNo.IsChecked = false;
    }

    private void cbxBateriaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBateriaSi.IsChecked == true)
            cbxBateriaNo.IsChecked = false;
    }

    private void cbxAsientoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxAsientoSi.IsChecked == true)
            cbxAsientoNo.IsChecked = false;
    }

    private void cbxCinturonSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxCinturonSi.IsChecked == true)
            cbxCinturonNo.IsChecked = false;
    }

    private void cbxBalizaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxBalizaSi.IsChecked == true)
            cbxBalizaNo.IsChecked = false;
    }

    private void cbxNeumaticosSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxNeumaticosSi.IsChecked == true)
            cbxNeumaticosNo.IsChecked = false;
    }

    private void cbxLlantasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxLlantasSi.IsChecked == true)
            cbxLlantasNo.IsChecked = false;
    }

    private void cbxCadenasSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxCadenasSi.IsChecked == true)
            cbxCadenasNo.IsChecked = false;
    }

    private void cbxUñasHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxUñasHorquillaSi.IsChecked == true)
            cbxUñasHorquillaNo.IsChecked = false;
    }

    private void cbxSoporteCilindroSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxSoporteCilindroSi.IsChecked == true)
            cbxSoporteCilindroNo.IsChecked = false;
    }

    private void cbxFlexibleSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxFlexibleSi.IsChecked == true)
            cbxFlexibleNo.IsChecked = false;
    }

    private void cbxSeguroHorquillaSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxSeguroHorquillaSi.IsChecked == true)
            cbxSeguroHorquillaNo.IsChecked = false;
    }

    private void cbxPuntoDeBloqueoSi_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (cbxPuntoDeBloqueoSi.IsChecked == true)
            cbxPuntoDeBloqueoNo.IsChecked = false;
    }
}