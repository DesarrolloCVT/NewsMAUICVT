namespace NewsMauiCVT.Views;

public partial class AsignacionMateriaPrimaDetalle : ContentPage
{
	public AsignacionMateriaPrimaDetalle()
	{
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearComponent();
    }
    private void ClearComponent()
    {
        txtPallet.Text = string.Empty;
    }
    private void txtPallet_Completed(object sender, EventArgs e)
    {
        DisplayAlert("Alerta", "El pallet es: \n" + txtPallet.Text, "OK");
    }
}