namespace Aviaseils;

using System.Data;
using System.Data.SqlClient;
public partial class MainPage : ContentPage
{
	
    
    public MainPage()
	{
        
        InitializeComponent();
        Navigation.PushAsync(new IndexPage("Не авторизован", 1,0));
	}


}

