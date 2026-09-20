using System.Windows.Forms;
using System.Data.OleDb;
using System;
using System.Media;

namespace MemoryGame
{
    class User
    {
         
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Memory.mdb;");

         
        SoundPlayer player = new SoundPlayer("error_sound.wav");

        bool flag = false;  

         
         
         
         
        int time = -1;

         
        internal User()
        {

        }

        void updateDB(int tries,int time,string name)
        {
                try
                {
                    conn.Open();  
                    OleDbCommand command1 = new OleDbCommand("UPDATE Users SET Tries=@tries,TimeInSec=@time WHERE Username=@name;", conn);
                    command1.Parameters.AddWithValue("@tries", tries);
                    command1.Parameters.AddWithValue("@time", time);
                    command1.Parameters.AddWithValue("@name", name);

                    command1.Connection = conn;
                    command1.ExecuteNonQuery();  

                }
                catch (Exception e)
                {
                    player.Play();  

                     
                    MessageBox.Show("Problem with the database!");
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    conn.Close();  
                }
            
        }

        void readFromDb(string name)
        {
            try
            {
                conn.Open();  

                 
                OleDbCommand command = new OleDbCommand("SELECT TimeInSec FROM Users WHERE Username=@user;", conn);
                command.Parameters.AddWithValue("@user", name);

                 
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.HasRows)  
                {
                    flag = true;

                    while (reader.Read())
                    {
                        this.time = reader.GetInt16(0);  
                    }

                    reader.Close();  
                }
            }

            catch (Exception e)
            
            {
                player.Play();  

                 
                MessageBox.Show("Problem with the database!");
                MessageBox.Show(e.Message);
            }

            finally
            
            {
                conn.Close();  
            }
        }

        
         
        void addToDB(string name, int tries, int time)
        {
                try
                {
                    conn.Open();  
                    OleDbCommand cmd = new OleDbCommand();

                     
                    cmd.CommandText = @"INSERT INTO Users (Username,Tries,TimeInSec) VALUES (@userName,@tries,@time)";

                     
                    cmd.Parameters.AddWithValue("@userName", name);
                    cmd.Parameters.AddWithValue("@tries", tries);
                    cmd.Parameters.AddWithValue("@time", time);

                    cmd.Connection = conn;

                    int i = cmd.ExecuteNonQuery();

                    if (i != 1)  
                    {
                        MessageBox.Show("The Database was not updated!");
                    }
                }
                catch (Exception ex)  
                {
                    player.Play();  

                    MessageBox.Show("A problem with the database occured!");
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();  
                }
            
        }

        internal void databaseOperations(string name, int tries, int time)
        {
            readFromDb(name);  

            if (flag == true)  
            {
                 
                if (this.time >= time)
                {
                    updateDB(tries, time, name);
                }
            }
            else  
            {
                addToDB(name, tries, time);
            }

            flag = false;
            this.time = -1;
        }
    }
}
