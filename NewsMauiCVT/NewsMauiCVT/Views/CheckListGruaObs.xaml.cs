namespace NewsMauiCVT.Views;

public partial class CheckListGruaObs : ContentPage
{
	private Dictionary<string,string> checkList;
	public CheckListGruaObs(Dictionary<string,string> keyValues)
	{
		InitializeComponent();
		checkList = keyValues;
	}
    private void btnEnviar_Clicked(object sender, EventArgs e)
    {
		checkList.Add("observaciones",txtObservaciones.Text);
		foreach (var item in checkList)
		{
			Console.WriteLine("Key: " + item.Key.ToString() + " Value: " + item.Value.ToString());
		}
    }
}