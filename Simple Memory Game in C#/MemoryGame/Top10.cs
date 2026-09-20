using System;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Top10 : Form
    {
        OleDbConnection conn;  

        public Top10()
        {
            InitializeComponent();
            find10();
        }

         
        internal void find10()
        {
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Memory.mdb;");
                conn.Open();  

                 
                OleDbCommand cmd = new OleDbCommand("SELECT TOP 10 Username,Tries,TimeInSec FROM Users ORDER BY TimeInSec,Tries;", conn);

                OleDbDataReader reader = cmd.ExecuteReader();

                StringBuilder sb = new StringBuilder();  

                if (reader.HasRows)  
                {
                    int counter = 1;
                    while (reader.Read())  
                    {
                        sb.Append(counter.ToString());
                        sb.Append(". ");
                        sb.Append("Name: ");
                        sb.Append(reader.GetString(0));
                        sb.Append(" , ");
                        sb.Append("Time: ");
                        sb.Append(reader.GetInt16(2).ToString());
                        sb.Append(" , ");
                        sb.Append("Tries: ");
                        sb.Append(reader.GetInt16(1).ToString());
                        sb.Append(Environment.NewLine);

                        counter++;
                    }

                    textBox1.Text = sb.ToString();  
                }

            }

            catch (Exception ex)  
            
            {
                MessageBox.Show("A problem with the database occured!");
                MessageBox.Show(ex.Message);
            }

            finally
            
            {
                conn.Close();  
            }
        }

    }
}
