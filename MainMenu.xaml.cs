//using GoogleGson;
//using Newtonsoft.Json;

namespace Tamagotchi;

public partial class MainMenu : ContentPage
{
	public MainMenu()
	{
		InitializeComponent();
        //string json = File.ReadAllText("C:/Users/vince/source/repos/Tamagotchi/json/tamagotchi.json");
        //var data = JsonConvert.DeserializeObject<MainMenu>(json);
       // dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        // foreach (dynamic obj in jsonObj)
        //{
        //     testLabel.Text = obj;
        // }
        //testLabel.Text = jsonObj[0]["name"];
        //var data = JsonConvert.DeserializeObject<TamaClass>(json);
        // BindingContext = data;
    }


    private async void toTamagochi(object sender, EventArgs e)
    {
        tambutton.Text = "Go to Tamagochi";
        await Navigation.PushAsync(new tamaChoose());

    }
}