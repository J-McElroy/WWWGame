using System;
using System.Data.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WWWGame.DataAccessLibrary.Model;

namespace WWWGame.DataAccessLibrary
{
    class TournsDataContext : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DbConnectionString = "Data Source=isostore:/Tourns.sdf";

        // Pass the connection string to the base class.
        public TournsDataContext(string connectionString)
            : base(connectionString)
        {
            this.TournItems = this.GetTable<Tourn>();
            this.QuestionItems = this.GetTable<Question>();
            this.ImageItems = this.GetTable<Model.Image>();
        }

        // Specify a single table for the to-do items.
        public Table<Tourn> TournItems { get; set; }

        public Table<Question> QuestionItems { get; set; }
        public Table<Model.Image> ImageItems { get; set; }
    }

}
