using DevExpress.Utils.KeyboardHandler;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Controls.UserDialogs.Maui;
using NewsMauiCVT.Model;
using NewsMauiCVT.Views;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace NewsMauiCVT;

public partial class LoginQR : INotifyPropertyChanged
{
	public LoginQR()
	{
        InitializeComponent();
        SetFocusText();
	}
    private void SetFocusText()
    {
        _ = Task.Delay(150).ContinueWith(t => {
            txtCredenciales.Focus();
        });
    }
    private async void txtCredenciales_Completed(object sender, EventArgs e)
    {
        try
        {
            using (UserDialogs.Instance.Loading("Verificando Datos"))
            {
                await Task.Delay(10);
                var ssid = DependencyService.Get<IGetSSID>().GetSSID();
                string sd = Regex.Replace(ssid, @"[^\w\d]", string.Empty);

                if (!string.IsNullOrEmpty(txtCredenciales.Text))
                {
                    var ACC = Connectivity.NetworkAccess;
                    if (ACC == NetworkAccess.Internet)
                    {
                        string usuario = txtCredenciales.Text.Split(';')[0].Trim();
                        string clave = txtCredenciales.Text.Split(';')[1].Trim();

                        try
                        {
                            HttpClient ClientHttp = new()
                            {
                                BaseAddress = new Uri("http://wsintranet2.cvt.local/")
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

                                        }
                                        catch
                                        {
                                            DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                            await DisplayAlert("Alerta", "No tiene los perfiles necesarios para poder acceder a esta APP", "OK");
                                        }
                                    }
                                }
                                else
                                {
                                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                                    await DisplayAlert("Alerta", "Usuario o Contraseña No Existen ", "Aceptar");
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
                    }
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "Ingrese Usuario y Contraseña", "Aceptar");
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
}