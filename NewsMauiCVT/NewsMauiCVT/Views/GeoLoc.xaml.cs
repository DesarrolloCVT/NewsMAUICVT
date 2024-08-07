namespace NewsMauiCVT.Views;

public partial class GeoLoc : ContentPage
{
    public GeoLoc()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
    }
    protected override void OnAppearing()
    {

        base.OnAppearing();

        lblLat.Text = string.Empty;
        lblLong.Text = string.Empty;
        lblAlt.Text = string.Empty;
    }
    public async void btn_clicked(object sender, System.EventArgs e)
    {
        lblLat.Text = string.Empty;
        lblLong.Text = string.Empty;
        lblAlt.Text = string.Empty;

        //try
        //{
        var location = await Geolocation.GetLastKnownLocationAsync();
        if (location != null)
        {
            lblLat.Text += location.Latitude.ToString();
            lblLong.Text += location.Longitude.ToString();
            lblAlt.Text += location.Altitude.ToString();
            //https://www.google.cl/maps/@x
        }
        else { await DisplayAlert("Confirmar", "Active GPS", "OK"); }
    }
    private async void Mapa_Clicked(object sender, EventArgs e)
    {
        var location = new Location(Convert.ToDouble(lblLat.Text), Convert.ToDouble(lblLong.Text));
        var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
        await Map.OpenAsync(location, options);
    }
    protected override bool OnBackButtonPressed()
    {
        //return true to prevent back, return false to just do something before going back. 
        return true;
    }
}