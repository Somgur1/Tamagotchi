namespace Tamagotchi;

using System.Collections.ObjectModel;
using System.Text.Json;

public partial class tamaChoose : ContentPage
{
    string path = FileSystem.AppDataDirectory;
    public ObservableCollection<TamaClass> TamaClasses { get; set; } = new ObservableCollection<TamaClass>();
	public tamaChoose()
	{
		InitializeComponent();
        //string json = File.ReadAllText("C:/Users/vince/source/repos/Tamagotchi/json/tamagotchi.json");
        //var data = JsonConvert.DeserializeObject<TamaClass>(json);
        string file = string.Concat(path, "TamaJson.json");
        if (File.Exists(file))
        {
            var RawData = File.ReadAllText(file);
            //ObservableCollection<TamaClass> x = System.Text.Json.JsonSerializer.Deserialize<ObservableCollection<TamaClass>>(RawData);
            TamaClasses = JsonSerializer.Deserialize<ObservableCollection<TamaClass>>(RawData);
            
        }
        BindingContext = this;


        // BindingContext = data;
        //List<TamaClass> list = new List<TamaClass>();
        //list.Add(
        //new TamaClass("")
        //{
        //});
    }
    
    public void NewTamagotchi(object sender, EventArgs e)
    {
        TamaClasses.Add(new TamaClass("Chimchar", 0, "egg.png", 1, 1, 1, false));
        string file = string.Concat(path, "TamaJson.json");
        var serializeData = JsonSerializer.Serialize(TamaClasses);
        File.WriteAllText(file, serializeData);
    }

    private async void cmb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        TamaClass tamagotchi = e.CurrentSelection.FirstOrDefault() as TamaClass;
        var navigationParameter = new Dictionary<string, object>
    {
        { "name", tamagotchi }
    };
        await Shell.Current.GoToAsync($"///MainPage", navigationParameter);
    }

        public async void newTama(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());
    }
}