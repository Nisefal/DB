using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;

namespace DB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void search()
        {
            SqlConnection connection;
            SqlCommand cmd;
            string connectionString = ConfigurationManager.ConnectionStrings["DB.Properties.Settings.DBConnectionString"].ConnectionString;
            string info = SearchBox.Text;
            string query = "";
            switch (choose.SelectedIndex)
            {
                case 0:
                    query = "SELECT * FROM Bisness_Process WHERE Dogovir_id LIKE '%" + info + "%' OR Description LIKE '%" + info + "%' OR Done LIKE '%" + info + "%' OR Date_when_done LIKE '%" + info + "%'";

                    List<SQL_BP> bp_list = new List<SQL_BP>();

                    using (connection = new SqlConnection(connectionString))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        connection.Open();
                        cmd = new SqlCommand(query, connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            bp_list.Add(new SQL_BP(Convert.ToInt32(dr["Id"].ToString()), Convert.ToInt32(dr["Dogovir_id"].ToString()), dr["Description"].ToString(), Convert.ToBoolean(dr["Done"].ToString()), dr["Date_when_done"].ToString()));
                        }
                        _Table.ItemsSource = bp_list;
                    }
                    break;
                case 1:
                    query = "SELECT * FROM Dogovir WHERE Yuridichna_osoba_id LIKE '%" + info + "%' OR Deadline LIKE '%" + info + "%' OR Number LIKE '%" + info + "%' OR Date_of_creation LIKE '%" + info + "%' OR Text LIKE '%" + info + "%'";

                    List<SQL_Dogovir> dg_list = new List<SQL_Dogovir>();


                    using (connection = new SqlConnection(connectionString))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        connection.Open();
                        cmd = new SqlCommand(query, connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            dg_list.Add(new SQL_Dogovir(Convert.ToInt32(dr["Id"].ToString()), Convert.ToInt32(dr["Yuridichna_osoba_id"].ToString()), dr["Deadline"].ToString(), Convert.ToInt32(dr["Number"].ToString()), dr["Date_of_creation"].ToString(), dr["Text"].ToString()));
                        }
                        _Table.ItemsSource = dg_list;
                    }
                    break;
                case 2:
                    query = "SELECT * FROM Podiya WHERE Bisness_process_id LIKE '%" + info + "%' OR Description LIKE '%" + info + "%' OR Done LIKE '%" + info + "%' OR Date_when_done LIKE '%" + info + "%'";

                    List<SQL_Podiya> pd_list = new List<SQL_Podiya>();

                    using (connection = new SqlConnection(connectionString))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        connection.Open();
                        cmd = new SqlCommand(query, connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            pd_list.Add(new SQL_Podiya(Convert.ToInt32(dr["Id"].ToString()), Convert.ToInt32(dr["Bisness_process_id"].ToString()), dr["Description"].ToString(), Convert.ToBoolean(dr["Done"].ToString()), dr["Date_when_done"].ToString()));
                        }
                        _Table.ItemsSource = pd_list;
                    }
                    break;
                case 3:
                    query = "SELECT * FROM Yuridichna_osoba WHERE PIB_Nazva LIKE '%" + info + "%' OR IPN_ERDPU LIKE '%" + info + "%'";

                    List<SQL_Yur_os> yo_list = new List<SQL_Yur_os>();

                    using (connection = new SqlConnection(connectionString))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        connection.Open();
                        cmd = new SqlCommand(query, connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                            foreach (DataRow dr in table.Rows)
                        {
                            yo_list.Add(new SQL_Yur_os(Convert.ToInt32(dr["Id"].ToString()), dr["PIB_Nazva"].ToString(), dr["IPN_ERDPU"].ToString()));
                        }
                        _Table.ItemsSource = yo_list;
                    }
                    break;
                default:
                    break;
            }

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            search();
        }
    }

    public class SQLGeter
    {
        public int id { get; set; }
        public SQLGeter(int Id) { id = Id; }
    }

    public class SQL_BP : SQLGeter
    {
        public virtual int dogov_id { get; set; }
        public string description { get; set; }
        public bool done { get; set; }
        public string Date_when_done { get; set; }

        public SQL_BP(int Id, int dog_id, string _desc, bool _done, string _dwd): base (Id)
        {
            id = Id;
            dogov_id = dog_id;
            description = _desc;
            done = _done;
            Date_when_done = _dwd;
        }

        public SQL_BP(int Id, string _desc, bool _done, string _dwd) : base(Id)
        {
            id = Id;
            description = _desc;
            done = _done;
            Date_when_done = _dwd;
        }
    }

    public class SQL_Podiya : SQL_BP
    {
        public int Bisness_proc_id { get; set; }

        public SQL_Podiya(int _bpi, int Id, string _desc, bool _done, string _dwd) : base (Id, _desc, _done, _dwd)
        {
            Bisness_proc_id = _bpi;
            id = Id;
            description = _desc;
            done = _done;
            Date_when_done = _dwd;
        }
    }

    public class SQL_Dogovir : SQLGeter
    {
        public int Yurid_osoba_id { get; set; }
        public string Deadline { get; set; }
        public int Number { get; set; }
        public string Date_of_creation { get; set; }
        public string Text { get; set; }

        public SQL_Dogovir(int Id, int yur_os_id, string _dead, int num, string _doc, string _txt) : base(Id)
        {
            id = Id;
            Yurid_osoba_id = yur_os_id;
            Deadline = _dead;
            Number = num;
            Date_of_creation = _doc;
            Text = _txt;
        }
    }

    public class SQL_Yur_os : SQLGeter
    {
        string PIB_Nazva { get; set; }
        string IPN_ERDPU { get; set; }
        
        public SQL_Yur_os(int Id, string _PIB, string _IPN) : base(Id)
        {
            id = Id;
            PIB_Nazva = _PIB;
            IPN_ERDPU = _IPN;
        }
    }
}
