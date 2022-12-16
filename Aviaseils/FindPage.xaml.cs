using System.Data.SqlClient;
using System.Data;

namespace Aviaseils;

public partial class FindPage : ContentPage
{
    DBconnector datebase = new DBconnector();
    List<string> _Timestart = new List<string>();
    List<string> _TimeEnd = new List<string>();
    List<DateTime> _dateStart = new List<DateTime>();
    List<DateTime> _dateEnd = new List<DateTime>();
    List<string> _PositionStart = new List<string>();
    List<string> _PositionEnd = new List<string>();
    List<string> _Company = new List<string>();
    List<string> _Plane = new List<string>();
    List<int> _Price = new List<int>();
    List<int> _idflight = new List<int>();
    public void findbilet(string star, string end)
    {
        _Timestart.Clear();
        _TimeEnd.Clear();
        _dateStart.Clear();
        _dateEnd.Clear();
        _PositionStart.Clear();
        _PositionEnd.Clear();
        _Company.Clear();
        _Plane.Clear();
        _Price.Clear();
        _idflight.Clear();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        List<int> _Companyid = new List<int>();
        List<int> _Planeid = new List<int>();
        List<int> _Airstid = new List<int>();
        List<int> _Airendid = new List<int>();
        string querystring = "";
        if (star != "" && end == "")
        {
            querystring = $"SELECT Plane, Id_companie, Flight_start, Flight_end, CONVERT(nvarchar(50), Time_start), CONVERT(nvarchar(50), Time_end), price, idAiroport_start, idAiroport_end, id FROM Flight WHERE idAiroport_start = (SELECT TOP (1) id FROM Airport WHERE Name = '{star}')";
        }else if(star == "" && end != "")
        {
            querystring = $"SELECT Plane, Id_companie, Flight_start, Flight_end, CONVERT(nvarchar(50), Time_start), CONVERT(nvarchar(50), Time_end), price, idAiroport_start, idAiroport_end, id FROM Flight WHERE idAiroport_end = (SELECT TOP (1) id FROM Airport WHERE Name = '{end}')";
        }
        else
        {
            querystring = $"SELECT Plane, Id_companie, Flight_start, Flight_end, CONVERT(nvarchar(50), Time_start), CONVERT(nvarchar(50), Time_end), price, idAiroport_start, idAiroport_end, id FROM Flight WHERE idAiroport_end = (SELECT TOP (1) id FROM Airport WHERE Name = '{end}') and idAiroport_start = (SELECT TOP (1) id FROM Airport WHERE Name = '{star}')";
        }
        SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
        adapter.SelectCommand = sqlCommand;
        adapter.Fill(table);
        if (table.Rows.Count > 0)
        {

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow((IDataRecord)reader);
            }

            void ReadSingleRow(IDataRecord dataRecord)
            {
                _Planeid.Add(dataRecord.GetInt32(0));
                _Companyid.Add(dataRecord.GetInt32(1));
                _dateStart.Add(dataRecord.GetDateTime(2));
                _dateEnd.Add(dataRecord.GetDateTime(3));
                _Timestart.Add(dataRecord.GetString(4));
                _TimeEnd.Add(dataRecord.GetString(5));
                _Price.Add(dataRecord.GetInt32(6));
                _Airstid.Add(dataRecord.GetInt32(7));
                _Airendid.Add(dataRecord.GetInt32(8));
                _idflight.Add(dataRecord.GetInt32(9));
            }
            reader.Close();
        }

        for (int i = 0; i < _Price.Count; i++)
        {
            int idishnik = _Companyid[i];
            querystring = $"SELECT Name FROM Companies Where id = '{idishnik}'";
            sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

                void ReadSingleRow(IDataRecord dataRecord)
                {
                    _Company.Add(dataRecord.GetString(0));
                }
                reader.Close();
            }
            idishnik = _Planeid[i];
            querystring = $"SELECT Name FROM Plane Where id = '{idishnik}'";
            sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

                void ReadSingleRow(IDataRecord dataRecord)
                {
                    _Plane.Add(dataRecord.GetString(0));
                }
                reader.Close();
            }
            idishnik = _Airstid[i];
            querystring = $"SELECT Name FROM Airport Where id = '{idishnik}'";
            sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

                void ReadSingleRow(IDataRecord dataRecord)
                {
                    _PositionStart.Add(dataRecord.GetString(0));
                }
                reader.Close();
            }
            idishnik = _Airendid[i];
            querystring = $"SELECT Name FROM Airport Where id = '{idishnik}'";
            sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

                void ReadSingleRow(IDataRecord dataRecord)
                {
                    _PositionEnd.Add(dataRecord.GetString(0));
                }
                reader.Close();
            }
        }

    }
    public FindPage(string start, string end, string loginName, long id, int acs)
    {
        datebase.openConnection();
        InitializeComponent();
        findbilet(start, end);
        StackLayout stackmain = new StackLayout();
        stackmain = sta();
        StackLayout gridmain = new StackLayout();
        TapGestureRecognizer das = new TapGestureRecognizer()
        {

        };
        Shadow sh = new Shadow()
        {
            Brush = Color.FromRgb(0, 0, 0),
            Radius = 20,
            Opacity = 0.5f,
            Offset = new Point(0, 5),
        };
        das.Tapped += async (s, e) =>
        {
            await Navigation.PopAsync();
        };
        Image img = new Image()
        {
            GestureRecognizers = { das },
            Margin = new Thickness(10, 0, 0, 0),
            Source = "iconlogo.png",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            WidthRequest = 30,
            HeightRequest = 30,
            BackgroundColor = Color.FromRgba(0, 0, 0, 1),
            IsOpaque = false,
            Aspect = Aspect.Fill
        };
        Button btexit = new Button()
        {
            Text = "Выйти",
            Margin = new Thickness(0, 0, 10, 0),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Color.FromHex("#E71C40"),
            Background = Color.FromHex("#2395F3"),
        };
        Grid grheader = new Grid()
        {

            VerticalOptions = LayoutOptions.Start,
            //Background = Color.FromHex("#121877"),
            Children =
            {
                img
            }
        };
        Frame frameHeader = new Frame()
        {

            VerticalOptions = LayoutOptions.Start,
            Background = Color.FromHex("#121877"),
            Content = grheader,
        };

        gridmain = new StackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,


        };
        gridmain.Children.Add(frameHeader);

        Grid grfind = new Grid();
        Entry entrystart = new Entry
        {
            Placeholder = "Место отправления...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry entryend = new Entry
        {
            Placeholder = "Место прибытия...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Margin = new Thickness(250, 10, 0, 0),
            Background = Color.FromHex("#64B5F6"),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            FontSize = 15,
            PlaceholderColor = Color.FromRgb(0, 0, 0),
            TextColor = Color.FromRgb(0, 0, 0),
        };
        Button btfind = new Button()
        {
            Margin = new Thickness(0, 10, 0, 0),
            Text = "Найти",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#2395F3"),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
        };
        grfind.Children.Add(entrystart);
        grfind.Children.Add(entryend);
        grfind.Children.Add(btfind);
        gridmain.Children.Add(grfind);
        string password = "";
        entrystart.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            password = entrystart.Text;


        };
        string login = "";
        entryend.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            login = entryend.Text;
        };

        Label resul = new Label()
        {
            Text = "Результаты поиска",
            FontSize = 25,
            TextColor = Color.FromHex("#2395F3"),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
        };
        gridmain.Children.Add(resul);

        void admininfo(object sender, EventArgs args)
        {
            Button batick = (Button)sender;
            string idi = batick.ClassId;
            Navigation.PushAsync(new InfoPage(idi));

        };
        void buybilet(object sender, EventArgs args)
        {
            Button batick = (Button)sender;
            string idi = batick.ClassId;
            Navigation.PushAsync(new BuyPage(idi, loginName, id));

        };
        StackLayout sta() {
        stackmain = new StackLayout();
        for (int i = 0; i < _Price.Count; i++)
        {
                Shadow sh = new Shadow()
                {
                    Brush = Color.FromRgb(0, 0, 0),
                    Radius = 20,
                    Opacity = 0.5f,
                    Offset = new Point(0, 5),
                };
                Label airstart = new Label()
            {
                Text = "Место отбытия: " + _PositionStart[i],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,

            };
            Label tire = new Label()
            {
                Text = "Компания: " + _Company[i],
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,

            };
            Label Plane = new Label()
            {
                Text = "Самолет: " + _Plane[i],
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };
            Label airEnd = new Label()
            {
                Text = "Место прибытия: " + _PositionEnd[i],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,

            };
            string de = System.Convert.ToString(_dateStart[i]);
            Label datestart = new Label()
            {
                Text = "Дата вылета: " + de.Split(' ')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,

            };
            string ds = System.Convert.ToString(_dateEnd[i]);
            Label dateend = new Label()
            {
                Text = "Место прилета: " + ds.Split(' ')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,

            };
            Grid headcentr = new Grid()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Children = { airstart, tire, airEnd }

            };
            Grid headdcentr = new Grid()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Children = { datestart, Plane, dateend }

            };
            string ts = System.Convert.ToString(_Timestart[i]);
            Label timestart = new Label()
            {
                Text = "Время вылета: " + ts.Split('.')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,

            };
            string te = System.Convert.ToString(_TimeEnd[i]);
            Label timeend = new Label()
            {
                Text = "Время прилета: " + te.Split('.')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,

            };
            Grid centercentr = new Grid()
            {
                Margin = new Thickness(0, 0, 0, 0),

                Children = { timestart, timeend }

            };
            Button btbron = new Button()
            {
                Shadow = sh,
                Text = "Купить билет за " + System.Convert.ToString(_Price[i]),
                FontSize = 20,
                Background = Color.FromHex("#C2954D"),
                WidthRequest = 300,
                HeightRequest = 50,
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                ClassId = System.Convert.ToString(i),

            };


            btbron.Clicked += buybilet;
            StackLayout stackcenter = new StackLayout()
            {

                HorizontalOptions = LayoutOptions.Start,
                Children = { headcentr, headdcentr, centercentr, btbron }
            };
            if (acs == 2)
            {
                Button btadmin = new Button()
                {
                    Shadow = sh,
                    Text = "Инфо",
                    FontSize = 20,
                    Background = Color.FromHex("#C2954D"),
                    WidthRequest = 100,
                    HeightRequest = 50,
                    TextColor = Color.FromRgb(255, 255, 255),
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.End,
                    ClassId = System.Convert.ToString(i),

                };
                stackcenter.Children.Add(btadmin);
                btadmin.Clicked += admininfo;
            }
            Frame framebilet = new Frame()
            {
                Shadow = sh,
                Margin = new Thickness(0, 15, 0, 15),
                Background = Color.FromHex("#64B5F6"),
                Content = stackcenter,
            };
            stackmain.Children.Add(framebilet);
           
        }
            return stackmain;
        }
        gridmain.Children.Add(stackmain);
        btfind.Clicked += async (s, e) =>
        {
            if (password == "" && login == "")
            {
                await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, введите хотя бы 1 значене", "ОK");

            }
            else
            {
                gridmain.Children.Remove(stackmain);
                findbilet(password, login);
                stackmain = sta();
                gridmain.Children.Add(stackmain);
            }
        };
        ScrollView scrol = new ScrollView()
        {
            Content = gridmain,
        };
        
        
        Content = gridmain;
    }
}