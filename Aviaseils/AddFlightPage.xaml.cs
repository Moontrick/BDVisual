using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Diagnostics;

namespace Aviaseils;

public partial class AddFlightPage : ContentPage
{
    DBconnector datebase = new DBconnector();

    int FindNewID()
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        int idi = 0;
        string querystring = $"SELECT count(id) FROM Flight";
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
                idi = dataRecord.GetInt32(0);
            }
            reader.Close();
        }
        return idi;
    }

    int FindAir(string na)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        int idi = -1;
        string querystring = $"SELECT TOP(1) id FROM Airport Where Name = '{na}'";
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
                idi = dataRecord.GetInt32(0);
            }
            reader.Close();
        }
        return idi;
    }
    void MakeFlight(int id, int plane, int idcomp, string datestart, string dateend, string timestart, string timeend, int price, int idairstart, int idairend)
    {


        string querystring = $"INSERT INTO Flight (id, Plane, Id_companie, Flight_start, Flight_end, Time_start,Time_end, price,width ,idAiroport_start, idAiroport_end) VALUES ('{id}', '{plane}', '{idcomp}', '{datestart}', '{dateend}', '{timestart}', '{timeend}', '{price}',4324 ,'{idairstart}','{idairend}');";

        SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
        if (sqlCommand.ExecuteNonQuery() == 1)
        {
            DisplayAlert("Успешно", "Новый билет был создан!", "Ок");
        }
        else
        {
            DisplayAlert("Ошибка!!!", "Ошибка в создании билета!", "Ок");
        }




    }

    string FindComapny(int na)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string idi = "";
        string querystring = $"SELECT TOP(1) Name FROM Companies Where id = '{na}'";
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
                idi = dataRecord.GetString(0);
            }
            reader.Close();
        }
        return idi;
    }


    string FindPlane(int na)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        string idi = "";
        string querystring = $"SELECT TOP(1) Name FROM Plane Where id = '{na}'";
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
                idi = dataRecord.GetString(0);
            }
            reader.Close();
        }
        return idi;
    }
    public AddFlightPage()
    {
        datebase.openConnection();
        InitializeComponent();
        int newid = FindNewID();
        StackLayout stackmain = new StackLayout();
        StackLayout gridmain = new StackLayout();
        TapGestureRecognizer das = new TapGestureRecognizer()
        {

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
            Text = "Âûéòè",
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
        }; Shadow sh = new Shadow()
        {
            Brush = Color.FromRgb(0, 0, 0),
            Radius = 20,
            Opacity = 0.5f,
            Offset = new Point(0, 5),
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
        stackmain.Children.Add(gridmain);
        Label ltit = new Label()
        {
            Text = "Введите значения для создания нового билета",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 25,
            TextColor = Color.FromHex("#2395F3"),
            Margin = new Thickness(0, 10, 0, 0),
        };

        stackmain.Children.Add(ltit);
        Grid grfind = new Grid();
        StackLayout stackleft = new StackLayout()
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
        };
        StackLayout stackright = new StackLayout()
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
        };
        StackLayout stackcenter = new StackLayout()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
        };

        Entry findplane = new Entry
        {
            Placeholder = "Укажите id самолета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry findcompany = new Entry
        {
            Placeholder = "Укажите id компании...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry pricecost = new Entry
        {
            Placeholder = "Укажите цену билета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        stackright.Children.Add(findplane);
        stackright.Children.Add(findcompany);
        stackright.Children.Add(pricecost);
        string fp = "";
        findplane.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            fp = findplane.Text;


        };
        string fc = "";
        findcompany.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            fc = findcompany.Text;
        };
        string pc = "";
        pricecost.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            pc = pricecost.Text;
        };


        Entry Airstart = new Entry
        {
            Placeholder = "Место вылета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry Airend = new Entry
        {
            Placeholder = "Место прилета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        string astart = "";
        Airstart.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            astart = Airstart.Text;
        };
        string aend = "";
        Airend.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            aend = Airend.Text;
        };

        stackcenter.Children.Add(Airstart);
        stackcenter.Children.Add(Airend);

        Entry timestart = new Entry
        {
            Placeholder = "Время вылета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry timeend = new Entry
        {
            Placeholder = "Время прилета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        }; Entry datestart = new Entry
        {
            Placeholder = "Дата вылета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        Entry dateend = new Entry
        {
            Placeholder = "Дата прилета...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 200,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),

            Shadow = sh,
            TextColor = Color.FromRgb(0, 0, 0),
            PlaceholderColor = Color.FromRgb(0, 0, 0),
        };
        string ts = "";
        timestart.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            ts = timestart.Text;
        };
        string te = "";
        timeend.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            te = timeend.Text;
        };
        string dt = "";
        datestart.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            dt = datestart.Text;
        };
        string de = "";
        dateend.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            de = dateend.Text;
        };
        stackleft.Children.Add(timestart);
        stackleft.Children.Add(timeend);
        stackleft.Children.Add(datestart);
        stackleft.Children.Add(dateend);

        Button Btad = new Button()
        {
            Text = "Создать",
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 25,
            WidthRequest = 300,
            HeightRequest = 50,
            Background = Color.FromHex("#C2954D"),
            TextColor = Color.FromRgb(255, 255, 255),
        };
        grfind.Children.Add(stackright);
        grfind.Children.Add(stackcenter);
        grfind.Children.Add(stackleft);
        stackmain.Children.Add(grfind);
        stackmain.Children.Add(Btad);
        Btad.Clicked += async (s, e) =>
        {
            if (fp == "" || fc == "" || pc == "" || astart == "" || aend == "" || ts == "" || te == "" || dt == "" || de == "")
            {
                await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, заполните все поля", "ОK");

            }
            else
            {
                int idastart = FindAir(astart);
                if (idastart == -1)
                {
                    await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, такого города нету", "ОK");

                }
                else
                {
                    int idaend = FindAir(aend);
                    if (idaend == -1)
                    {
                        await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, такого города нету", "ОK");

                    }
                    else
                    {
                        string namecomp = FindComapny(System.Convert.ToInt32(fc));
                        if (namecomp == "")
                        {
                            await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, такой компании нету", "ОK");

                        }
                        else
                        {
                            string nameplane = FindPlane(System.Convert.ToInt32(fp));
                            if (nameplane == "")
                            {
                                await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, такого самолета нету", "ОK");

                            }
                            else
                            {
                                Label airstart = new Label()
                                {
                                    Text = "Мето вылета: " + astart,
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start,

                                };
                                Label tire = new Label()
                                {
                                    Text = "Компания: " + namecomp,
                                    FontSize = 18,
                                    Margin = new Thickness(0, 0, 0, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Start,

                                };
                                Label Plane = new Label()
                                {
                                    Text = "Самолет: " + nameplane,
                                    FontSize = 18,
                                    Margin = new Thickness(0, 0, 0, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Center,

                                };
                                Label airEnd = new Label()
                                {
                                    Text = "Место прибытия: " + aend,
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.End,
                                    VerticalOptions = LayoutOptions.Start,

                                };

                                Label datestart = new Label()
                                {
                                    Text = "Дата вылета: " + dt,
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Center,

                                };

                                Label dateend = new Label()
                                {
                                    Text = "Дата прилета: " + de,
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

                                Label timestart = new Label()
                                {
                                    Text = "Время вылета: " + ts,
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Start,
                                    VerticalOptions = LayoutOptions.Start,

                                };

                                Label timeend = new Label()
                                {
                                    Text = "Время прилета: " + te,
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.End,
                                    VerticalOptions = LayoutOptions.Start,

                                };
                                Label Price = new Label()
                                {
                                    Text = "Цена: " + System.Convert.ToString(pc),
                                    FontSize = 18,
                                    Margin = new Thickness(10, 0, 10, 0),
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.Start,

                                };
                                Grid centercentr = new Grid()
                                {
                                    Margin = new Thickness(0, 0, 0, 0),

                                    Children = { timestart, Price, timeend }

                                };
                                Button btbron = new Button()
                                {
                                    Shadow = sh,
                                    Text = "Подтвердите создание билета ",
                                    FontSize = 20,
                                    Background = Color.FromHex("#C2954D"),
                                    WidthRequest = 400,
                                    HeightRequest = 50,
                                    TextColor = Color.FromRgb(255, 255, 255),
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.End,


                                };
                                StackLayout stackpov = new StackLayout()
                                {

                                    HorizontalOptions = LayoutOptions.Start,
                                    Children = { headcentr, headdcentr, centercentr, btbron }
                                };
                                Frame framebilet = new Frame()
                                {
                                    Shadow = sh,
                                    Margin = new Thickness(0, 15, 0, 15),
                                    Background = Color.FromHex("#64B5F6"),
                                    Content = stackpov,
                                };
                                stackmain.Children.Add(framebilet);
                                btbron.Clicked += async (s, e) =>
                                {
                                    MakeFlight(newid, System.Convert.ToInt32(fp), System.Convert.ToInt32(fc), dt, de, ts, te, System.Convert.ToInt32(pc), idastart, idaend);
                                };
                            }
                        }
                    }
                }
            }
        };
        Content = stackmain;
    }
}