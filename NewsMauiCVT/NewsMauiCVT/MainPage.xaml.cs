 using Controls.UserDialogs.Maui;
using Microsoft.Maui.Controls.Platform.Compatibility;
using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;
using NewsMauiCVT.Views;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace NewsMauiCVT
{
    public partial class MainPage : ContentPage 
    {
        public MainPage()
        {   
            InitializeComponent();
            SetMobileScreen();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFocusText();
        }
        private void SetFocusText()
        {
            _ = Task.Delay(400).ContinueWith(t => {
                txtUsuario.Focus();
            });
        }
        private void SetMobileScreen()
        {
            var ACC = Connectivity.NetworkAccess;
            try
            {
                if (ACC == NetworkAccess.Internet)
                {
                    DatosApp datos = new DatosApp();
                    txt_version.Text = ("Versión " + datos.TraeVersion());
                }
                else
                {
                    txt_version.Text = ("Versión 0.0.0");
                }
                if (DeviceInfo.Model != "MC33" && DeviceInfo.Model != "MC3300x" && DeviceInfo.Model != "RFD0020")
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    SlStackLayout.VerticalOptions = LayoutOptions.CenterAndExpand;
#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoginPage SetMobileScreen: " + ex.Message);
            }
            
        }
        private async void Loging_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                using (UserDialogs.Instance.Loading("Verificando Datos"))
                {
                    await Task.Delay(10);
                    var ssid = DependencyService.Get<IGetSSID>().GetSSID();
                    string sd = Regex.Replace(ssid, @"[^\w\d]", string.Empty);
                    loging.IsEnabled = false;

                    if (!string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Text))
                    {
                        var ACC = Connectivity.NetworkAccess;
                        if (ACC == NetworkAccess.Internet)
                        {
                            string usuario = txtUsuario.Text.ToLower();
                            string clave = txtContraseña.Text.ToLower();

                            try
                            {
                                HttpClient ClientHttp = new()
                                {
                                    BaseAddress = new Uri("http://wsintranet.cvt.local/")
                                };
                                try
                                {
                                    //Consulta
                                    var rest = ClientHttp.GetAsync("api/Usuario?usuario=" + usuario + "&pass=" + clave).Result;
                                    if (rest.IsSuccessStatusCode)
                                    {
                                        var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                                        var listado = JsonConvert.DeserializeObject<int>(resultadoStr);

                                        if (listado != 0)
                                        {
                                            try
                                            {
                                                var rest2 = ClientHttp.GetAsync("api/Usuario?idUser=" + listado).Result;
                                                var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                                                List<UsuarioClass> du = JsonConvert.DeserializeObject<List<UsuarioClass>>(resultadoStr2) ??
                                            throw new InvalidOperationException();

                                                //if (du.Count != 0) { }
                                                foreach (var d in du)
                                                {
                                                    App.Iduser = listado;
                                                    App.UserSistema = d.UsuarioSistema;
                                                    App.NombreUsuario = d.NombreUsuario.ToString();
                                                    App.idPerfil = d.IdPerfilMovile;
                                                    // App.vali = true;
                                                }
                                                DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                                await Navigation.PushAsync(new PageMain());
                                                txtUsuario.Text = string.Empty;
                                                txtContraseña.Text = string.Empty;

                                            }
                                            catch
                                            {
                                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                                await DisplayAlert("Alerta", "No tiene los perfiles necesarios para poder acceder a esta APP", "OK");
                                                txtUsuario.Text = string.Empty;
                                                txtContraseña.Text = string.Empty;
                                                txtUsuario.Focus();
                                                loging.IsEnabled = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                        await DisplayAlert("Alerta", "Usuario o Contraseña No Existen ", "Aceptar");
                                        txtUsuario.Text = string.Empty;
                                        txtContraseña.Text = string.Empty;
                                        txtUsuario.Focus();
                                        loging.IsEnabled = true;
                                    }
                                }
                                catch (AggregateException ex)
                                {
                                    Console.WriteLine("StackTrace Exception: " + ex.StackTrace);
                                    Console.WriteLine("InnerException: " + ex.InnerException);
                                    Console.WriteLine("InnerException.Message: " + ex.InnerException.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                            await DisplayAlert("Alerta", "Debe Conectarse a la Red Local", "Aceptar");
                            loging.IsEnabled = true;
                        }
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        await DisplayAlert("Alerta", "Ingrese Usuario y Contraseña", "Aceptar");
                        loging.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                Console.WriteLine(ex.ToString());
                await DisplayAlert("Alerta", "Revise conexión", "Aceptar");
            }
        }
        private async void LoginQR_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginQR());
        }
    }
}
