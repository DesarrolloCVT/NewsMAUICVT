using NewsMauiCVT.Datos;
using NewsMauiCVT.Model;

namespace NewsMauiCVT.Views;

public partial class CheckListGruaObs : ContentPage
{
	private Dictionary<string,string> checkList;
	public CheckListGruaObs(Dictionary<string,string> keyValues)
	{
		InitializeComponent();
		checkList = keyValues;
	}
	private async void btnEnviar_Clicked(object sender, EventArgs e)
	{
        if(string.IsNullOrEmpty(txtObservaciones.Text))
        {
            var result = await DisplayAlert("Alerta", "¿Está seguro de enviar los datos sin observaciones?", "SI", "NO");
            if (result)
            {
                checkList.Add("Observaciones", " ");
                var ACC = Connectivity.NetworkAccess;
                if (ACC == NetworkAccess.Internet)
                {
                    DatosCheckListGruas dclg = new DatosCheckListGruas();
                    bool resp = dclg.InsertaCheckListGrua(checkList);

                    if (resp)
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                        await DisplayAlert("Alerta", "Los datos se han registrado con éxito ", "OK");
                        await Navigation.PushAsync(new CheckListGrua());
                    }
                    else
                    {
                        DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                        await DisplayAlert("Alerta", "No se han registrado los datos, favor revisar e intentarlo nuevamente ", "OK");
                    }
                }
            }
            else
            {
                Console.WriteLine("Continua la edicion del CheckListGrua");
            }
        }
        else
        {
            checkList.Add("Observaciones", txtObservaciones.Text);
            var ACC = Connectivity.NetworkAccess;
            if (ACC == NetworkAccess.Internet)
            {
                DatosCheckListGruas dclg = new DatosCheckListGruas();
                bool resp = dclg.InsertaCheckListGrua(checkList);

                if (resp)
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("Correcto.mp3");
                    await DisplayAlert("Alerta", "Los datos se han registrado con éxito ", "OK");
                    await Navigation.PushAsync(new CheckListGrua());
                }
                else
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("terran-error.mp3");
                    await DisplayAlert("Alerta", "No se han registrado los datos, favor revisar e intentarlo nuevamente ", "OK");

                }
            }
        }
	}
}