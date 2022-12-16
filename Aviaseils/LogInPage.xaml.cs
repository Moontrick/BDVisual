namespace Aviaseils;
using System.Data.SqlClient;
using System.Data;
public partial class LogInPage : ContentPage
{
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
    DBconnector datebase = new DBconnector();
    public void findbilet(int idi)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        List<int> _Companyid = new List<int>();
        List<int> _Planeid = new List<int>();
        List<int> _Airstid = new List<int>();
        List<int> _Airendid = new List<int>();
        string querystring = $"SELECT Plane, Id_companie, Flight_start, Flight_end, CONVERT(nvarchar(50), Time_start), CONVERT(nvarchar(50), Time_end), idAiroport_start, idAiroport_end FROM Flight WHERE id = '{idi}'";
        SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
        adapter.SelectCommand = sqlCommand;
        adapter.Fill(table);
        if (table.Rows.Count == 1)
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
                _Airstid.Add(dataRecord.GetInt32(6));
                _Airendid.Add(dataRecord.GetInt32(7));

            }
            reader.Close();
        }
        
        for (int i = 0; i < _Airendid.Count; i++)
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
    List<int> numberplace = new List<int>();
    void findps(long id)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        List<int> idis = new List<int>();
        string querystring = $"SELECT number_of_place, idflight FROM Biletick WHERE idPassenger = '{id}'";
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
                numberplace.Add( dataRecord.GetInt32(0));
                idis.Add(dataRecord.GetInt32(1));
            }
            reader.Close();
        }
        for (int i = 0; i < idis.Count; i++) { findbilet(idis[i]); }

    }
    public LogInPage(bool flag, long id)
    {
        datebase.openConnection();
        InitializeComponent();
        StackLayout stackmain = new StackLayout();
        StackLayout gridmain = new StackLayout();
        
        Shadow sh = new Shadow()
        {
            Brush = Color.FromRgb(0, 0, 0),
            Radius = 20,
            Opacity = 0.5f,
            Offset = new Point(0, 5),
        };
        Label prod = new Label()
        {
            Text = "C.P. @ CSIT Production",
            FontSize = 15,
            TextColor = Color.FromRgb(0, 0, 0),
            Margin = new Thickness(0, 0, 0, 20),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
        };
        Grid pord = new Grid()
        {
            Children = { prod },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,

        };
        Frame fpr = new Frame()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            Content = pord,
        };
       
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
            HeightRequest = 35,
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

        if (!flag)
        {
            Entry entrylogin = new Entry
        {
            Placeholder = "Введите имя пользователя...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 300,
            HeightRequest = 50,
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Margin = new Thickness(0,10,0,0),
            Shadow = sh,
                PlaceholderColor = Color.FromRgb(0, 0, 0),
                TextColor = Color.FromRgb(0, 0, 0),
        };
        Entry entrypassword = new Entry
        {   IsPassword = true,
            Placeholder = "Введите имя пароль...",
            FontFamily = "Comic Sans MS",
            WidthRequest = 300,
            HeightRequest = 50,
            Margin = new Thickness(0, 10, 0, 0),
            Background = Color.FromHex("#64B5F6"),
            FontSize = 15,
            Shadow = sh,
            PlaceholderColor = Color.FromRgb(0, 0, 0),
            TextColor = Color.FromRgb(0, 0, 0),
        };
        string password = "";
        entrypassword.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            password = entrypassword.Text;
          

        };
        string login = "";
        entrylogin.TextChanged += async (sender, args) =>
        {
            string oldText = args.OldTextValue;
            string newText = args.NewTextValue;
            login = entrylogin.Text;
        };
        Button btlogin = new Button() { 
		Text = "Войти",
		WidthRequest = 300,
		HeightRequest = 50,
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),
            Shadow = sh,
            Background = Color.FromHex("#2395F3"),	
		};
        Button btsingup = new Button()
        {
            Text = "Зарегистрироваться",
            WidthRequest = 300,
            HeightRequest = 50,
            FontSize = 15,
            Margin = new Thickness(0, 10, 0, 0),
            Shadow = sh,
            Background = Color.FromHex("#2395F3"),
        }; 
            
        stackmain = new StackLayout()
        {
            Margin = new Thickness(0, 60, 0, 0),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children =
            {
                entrylogin,entrypassword,btlogin,btsingup
            }
        };
            gridmain.Children.Add(stackmain);
       
            string str1 = "";
        int acs = 0;
        long idis = 0;
        btlogin.Clicked += async (s, e) =>
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"SELECT ID, LoginUser, PasswordUser, typeacsess FROM Autorisation WHERE LoginUser = '{login}' and PasswordUser = '{password}'";
            SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }
              
                void ReadSingleRow(IDataRecord dataRecord)
                {
                    idis = dataRecord.GetInt64(0);
                    str1 = dataRecord.GetString(1);
                    acs = dataRecord.GetInt32(3);
                }
                reader.Close();
                Navigation.PushAsync(new IndexPage(str1, acs, idis));
                
            }
            else
            {
                await DisplayAlert("Ошибка", "При вводе данных произошла ошибка, пароль или логин не правильные", "ОK");
            }        
        };
            btsingup.Clicked += async (s, e) =>
            {
                if (login != "" && password != "")
                {
                    string querystring = $"SELECT COUNT(ID) FROM Autorisation";
                    SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    int idcount = 0;
                    while (reader.Read())
                    {
                        ReadSingleRow((IDataRecord)reader);
                    }

                    void ReadSingleRow(IDataRecord dataRecord)
                    {
                        idcount = dataRecord.GetInt32(0);
                    }
                    reader.Close();

                    querystring = $"SELECT ID, LoginUser, PasswordUser, typeacsess FROM Autorisation WHERE LoginUser = '{login}'";
                    sqlCommand = new SqlCommand(querystring, datebase.getConnection());
                    reader = sqlCommand.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable table = new DataTable();
                    adapter.SelectCommand = sqlCommand;
                    reader.Close();
                    adapter.Fill(table);
                    if (table.Rows.Count == 1)
                    {
                        await DisplayAlert("Ошибка", "Такой пользователь уже существует", "ОK");
                    }
                    else
                    {
                        if (idcount > 0)
                        {
                            //adapter.SelectCommand = sqlCommand;
                            //adapter.Fill(table);
                            querystring = $"INSERT into Autorisation (ID, LoginUser, PasswordUser, typeacsess) values('{idcount + 1}','{login}', '{password}','{1}')";
                            //SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
                            sqlCommand = new SqlCommand(querystring, datebase.getConnection());
                            if (sqlCommand.ExecuteNonQuery() == 1)
                            {
                                await DisplayAlert("Успешно", "Новый пользователь был создан!", "ОK");
                            }
                        }
                    }
                    reader.Close();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Неверно введены данные!", "ОK");
                }
            };
        }
        else
        {
            string posname = "";
            string pospassword = "";
            int acs = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"SELECT ID, LoginUser, PasswordUser, typeacsess FROM Autorisation WHERE ID = '{id}'";
            SqlCommand sqlCommand = new SqlCommand(querystring, datebase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

            void ReadSingleRow(IDataRecord dataRecord)
            {
                posname = dataRecord.GetString(1);
                pospassword = dataRecord.GetString(2);
                acs = dataRecord.GetInt32(3);
            }
            reader.Close();
            Image imgpol = new Image() { 
                Source = "pol.png",
                Margin = new Thickness(0, 20, 0, 0),
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 70,
                HeightRequest = 70,
                BackgroundColor = Color.FromRgba(0, 0, 0, 1),
                IsOpaque = false,
                Aspect = Aspect.Fill
            };
            Label lnametes = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 30, 0, 0),
                Text = "Логин:",
                FontSize = 20,
                TextColor = Color.FromRgb(0, 0, 0),

            };
            Label lname = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10, 0, 0),
                Text = posname,
                FontSize = 20,
                TextColor = Color.FromRgb(0,0,0),

            };
            Label lpasswordtes = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 30, 0, 0),
                Text = "Пароль:",
                FontSize = 20,
                TextColor = Color.FromRgb(0, 0, 0),

            };
            Label lpassword = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10, 0, 0),
                Text = pospassword,
                FontSize = 20,
                TextColor = Color.FromRgb(0, 0, 0),

            };
            gridmain.Children.Add(imgpol);
            gridmain.Children.Add(lnametes);
            gridmain.Children.Add(lname);
            gridmain.Children.Add(lpasswordtes);
            gridmain.Children.Add(lpassword);
           
            grheader.Children.Add(btexit);
            if(acs == 2)
            {
                 Label ldost = new Label()
                  {
                      HorizontalOptions = LayoutOptions.Center,
                      Margin = new Thickness(0, 30, 0, 0),
                      Text = "Доступ:",
                      FontSize = 20,
                      TextColor = Color.FromRgb(0, 0, 0),

                  };
                Label ldostt = new Label()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 10, 0, 0),
                    Text = "Администратор",
                    FontSize = 20,
                    TextColor = Color.FromRgb(0, 0, 0),

                };
                gridmain.Children.Add(ldost);
                gridmain.Children.Add(ldostt);
                Button adflight = new Button()
                {
                    Text = "Добавить билет",
                    Margin = new Thickness(0, 10, 0, 10),
                    HorizontalOptions = LayoutOptions.Center,
                    HeightRequest = 50,
                    Background = Color.FromHex("#2395F3"),
                    VerticalOptions = LayoutOptions.Start,
                    Shadow = sh,
                    WidthRequest = 200,
                };
                adflight.Clicked += async (s, e) =>
                {
                    await Navigation.PushAsync(new AddFlightPage());
                };
                gridmain.Children.Add(adflight);
            }

            Label lh = new Label()
            {
                Text = "Список купленных билетов",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 25,
                TextColor = Color.FromRgb(0, 0, 0),
                Margin = new Thickness(10, 0, 10, 0),
            };
            gridmain.Children.Add(lh);


            findps(id);
            for (int i = 0; i < numberplace.Count; i++)
            {
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
                Label prci = new Label()
                {
                    Text = "Номер места: " + System.Convert.ToString(numberplace[i]),
                    FontSize = 18,
                    Margin = new Thickness(10, 0, 10, 0),
                    TextColor = Color.FromRgb(255, 255, 255),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,

                };
                Grid centercentr = new Grid()
                {
                    Margin = new Thickness(0, 0, 0, 0),

                    Children = { timestart, prci, timeend }

                };

                StackLayout stackcenter = new StackLayout()
                {

                    HorizontalOptions = LayoutOptions.Start,
                    Children = { headcentr, headdcentr, centercentr }
                };
                Frame framebilet = new Frame()
                {
                    Margin = new Thickness(0, 15, 0, 15),
                    Background = Color.FromHex("#64B5F6"),
                    Content = stackcenter,
                };

                gridmain.Children.Add(framebilet);
            }

            

            btexit.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new IndexPage("Не авторизован", 1, 0));
            };
            }
        
        ScrollView scrol = new ScrollView()
        {
            Content = gridmain,
        };
        Content = scrol;
    }
}