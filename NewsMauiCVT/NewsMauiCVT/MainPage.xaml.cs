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
        }

        private async void Loging_ClickedAsync(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Verificando Datos"))
            {
                await Task.Delay(10);
                try
                {
                    var ssid = DependencyService.Get<IGetSSID>().GetSSID();
                    string sd = Regex.Replace(ssid, @"[^\w\d]", string.Empty);
                }
                catch (Exception ex)
                {
                    {
                        Console.WriteLine(ex.Message);
                    }

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
                        HttpClient ClientHttp = new HttpClient();
                        ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                        //para Consultar variables
                        var rest = ClientHttp.GetAsync("api/Usuario?usuario=" + usuario + "&pass=" + clave).Result;

                        if (rest.IsSuccessStatusCode)
                        {
                            var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                            var listado = JsonConvert.DeserializeObject<int>(resultadoStr);

                            if (listado != 0)
                            {
                                try
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                                    var rest2 = ClientHttp.GetAsync("api/Usuario?idUser=" + listado).Result;
                                    var resultadoStr2 = rest2.Content.ReadAsStringAsync().Result;
                                    List<UsuarioClass> du = JsonConvert.DeserializeObject<List<UsuarioClass>>(resultadoStr2);

                                    //if (du.Count != 0) { }
                                    foreach (var d in du)
                                    {
                                        App.Iduser = listado;
                                        App.UserSistema = d.UsuarioSistema;
                                        App.NombreUsuario = d.NombreUsuario.ToString();
                                        App.idPerfil = d.IdPerfilMovile;
                                        // App.vali = true;
                                    }

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
                        //}

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

}
