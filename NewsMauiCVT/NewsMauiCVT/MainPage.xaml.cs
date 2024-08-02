using Controls.UserDialogs.Maui;
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
            NavigationPage.SetHasNavigationBar(this, false);
            txtUsuario.Focus();
        }
        private async void Loging_ClickedAsync(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Verificando Datos"))
            {
                await Task.Delay(10);
                var ssid = DependencyService.Get<IGetSSID>().GetSSID();
                string sd = Regex.Replace(ssid, @"[^\w\d]", string.Empty);
                loging.IsEnabled = false;

                //UserDialogs.Instance.HideLoading();

                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    if (DeviceInfo.Model == "MC33")
                    {

                    }
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
                            //para Consultar
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
                        } catch (AggregateException ex)
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
        }
    }

}
