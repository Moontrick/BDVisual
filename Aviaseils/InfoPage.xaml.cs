using System.Data.SqlClient;
using System.Data;

namespace Aviaseils;

public partial class InfoPage : ContentPage
{
    int _Companyid = 0;
    int _Planeid = 0;
    int _Airstid = 0;
    int _Airendid = 0;
    DateTime _dateStart;
    DateTime _dateEnd;
    string _Timestart = "";
    string _TimeEnd = "";
    int _Price = 0;
    int _idflight = 0;
    string _Company = "";
    string _Plane = "";
    string _PositionStart = "";
    string _PositionEnd = "";
    int countbuy = 0;
    List<string> pols = new List<string>();

    DBconnector datebase = new DBconnector();
    public void findbilet(int idi)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        string querystring = $"SELECT Plane, Id_companie, Flight_start, Flight_end, CONVERT(nvarchar(50), Time_start), CONVERT(nvarchar(50), Time_end), price, idAiroport_start, idAiroport_end FROM Flight WHERE id = '{idi}'";
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
                _Planeid = dataRecord.GetInt32(0);
                _Companyid = dataRecord.GetInt32(1);
                _dateStart = dataRecord.GetDateTime(2);
                _dateEnd = dataRecord.GetDateTime(3);
                _Timestart = dataRecord.GetString(4);
                _TimeEnd = dataRecord.GetString(5);
                _Price = dataRecord.GetInt32(6);
                _Airstid = dataRecord.GetInt32(7);
                _Airendid = dataRecord.GetInt32(8);
            }
            reader.Close();
        }

        int idishnik = _Companyid;
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
                _Company = dataRecord.GetString(0);
            }
            reader.Close();
        }
        idishnik = _Planeid;
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
                _Plane = dataRecord.GetString(0);
            }
            reader.Close();
        }
        idishnik = _Airstid;
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
                _PositionStart = dataRecord.GetString(0);
            }
            reader.Close();
        }
        idishnik = _Airendid;
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
                _PositionEnd = dataRecord.GetString(0);
            }
            reader.Close();
        }
        idishnik = idi;
        querystring = $"SELECT Count(idflight) FROM Biletick Where idflight = '{idishnik}'";
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
                countbuy = dataRecord.GetInt32(0);
            }
            reader.Close();
        }
        idishnik = idi;
        List<int> idpas = new List<int>();
        querystring = $"SELECT IdPassenger FROM Biletick Where idflight = '{idishnik}'";
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
                idpas.Add(dataRecord.GetInt32(0));
            }
            reader.Close();
        }
        for (int i = 0; i < idpas.Count; i++)
        {
            idishnik = idpas[i];
            querystring = $"SELECT LoginUser FROM Autorisation Where ID = '{idishnik}'";
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
                    pols.Add(dataRecord.GetString(0));
                }
                reader.Close();
            }
        }
    }
    public InfoPage(string id)
	{
        datebase.openConnection();
        InitializeComponent();
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
        
            findbilet(System.Convert.ToInt32(id));
            Label airstart = new Label()
            {
                Text = "Место отбытия: " + _PositionStart,
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,

            };
            Label tire = new Label()
            {
                Text = "Компания: " + _Company,
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,

            };
            Label Plane = new Label()
            {
                Text = "Самолет: " + _Plane,
                FontSize = 18,
                Margin = new Thickness(0, 0, 0, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };
            Label airEnd = new Label()
            {
                Text = "Место прибытия: " + _PositionEnd,
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,

            };
            string de = System.Convert.ToString(_dateStart);
            Label datestart = new Label()
            {
                Text = "Дата вылета: " + de.Split(' ')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,

            };
            string ds = System.Convert.ToString(_dateEnd);
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
            string ts = System.Convert.ToString(_Timestart);
            Label timestart = new Label()
            {
                Text = "Время вылета: " + ts.Split('.')[0],
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,

            };
            string te = System.Convert.ToString(_TimeEnd);
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
                Text = "Цена: " + System.Convert.ToString(_Price),
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
        StackLayout st = new StackLayout()
        {

        };
        Label lcount = new Label()
            {
                Text = "Кол-во купивших билет = " + System.Convert.ToString(countbuy),
                FontSize = 18,
                Margin = new Thickness(10, 0, 10, 0),
                TextColor = Color.FromRgb(255, 255, 255),
                HorizontalOptions = LayoutOptions.Start,
            };
        st.Children.Add(lcount);
            for(int i = 0; i < pols.Count; i++)
            {
                Label lpos = new Label()
                {
                    Text = "Пользователь с именем " + pols[i] + " купил билет",
                    FontSize = 18,
                    Margin = new Thickness(10, 0, 10, 0),
                    TextColor = Color.FromRgb(255, 255, 255),
                    HorizontalOptions = LayoutOptions.Start,
                };
            st.Children.Add(lpos);
            }
        Frame frend = new Frame()
        {
            Background = Color.FromHex("#64B5F6"),
            Content = st,
        };
        gridmain.Children.Add(frend);
        ScrollView scrol = new ScrollView()
        {
            Content = gridmain,
        };
        Content = scrol;
    }
}