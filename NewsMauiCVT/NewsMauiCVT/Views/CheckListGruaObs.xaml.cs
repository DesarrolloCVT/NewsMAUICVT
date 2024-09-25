namespace NewsMauiCVT.Views;

public partial class CheckListGruaObs : ContentPage
{
	public CheckListGruaObs()
	{
		InitializeComponent();
	}
    private void btnEnviar_Clicked(object sender, EventArgs e)
    {
		var Obs = txtObservaciones.Text;

		var trend = Obs.Substring(0).ToLower().Trim();





    }
}