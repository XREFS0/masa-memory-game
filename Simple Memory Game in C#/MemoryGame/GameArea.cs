using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace MemoryGame
{
    public partial class GameArea : Form
    {
        Pictures picture;  
        StringBuilder sb;  
        SoundPlayer player1,player2;  

        string name;  
        int tries = 0;  
        int time = 0;  
        bool click1, click2 = false;  
        bool end = true;  

         
         
         
        bool[] flag = new bool[24];

         
         
        string[] items = new string[24];

         
        string[] defaults = new string[24];

        PictureBox[] pictureBoxes;  

         
         
        List<int> path = new List <int> ();

         
         
        List<bool> finish = new List<bool>();

         
         
         
        List<string> newPaths = new List<string>();

         
        string[] filePaths = new string[100];

         
         
         
         
        List<string> alreadyLooked = new List<string>();

        public GameArea(string n)
        {
            InitializeComponent();

             
            name = n;
        }

        private void GameArea_Load(object sender, EventArgs e)
        {
            picture = new Pictures();  

             
            for (int i = 0; i < 23; i++)
            {
                flag[i] = false;  
            }

             
            for (int i = 1; i <= 12; i++)
            {
                items[i-1] = "images/Dinosaur_" + i.ToString() + ".jpg";
                defaults[i-1] = "images/Dinosaur_" + i.ToString() + ".jpg";
            }

            for (int i = 1; i <= 12; i++) 
            {
                items[12+i-1] = "images/Dinosaur_" + i.ToString() + ".jpg";
                defaults[12 + i - 1] = "images/Dinosaur_" + i.ToString() + ".jpg";
            }

             
            pictureBoxes = new PictureBox[24] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
            pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,pictureBox11,pictureBox12,pictureBox13,
            pictureBox14,pictureBox15,pictureBox16,pictureBox17,pictureBox18,pictureBox19,pictureBox20,pictureBox21,
            pictureBox22,pictureBox23,pictureBox24};

             
            player1 = new SoundPlayer("error_sound.wav");

             
            player2 = new SoundPlayer("finish_sound.wav");
        }

         
         
         
        void change(string[] array)
        {
            items = picture.shuffleArray(array);  

            int index = 0;  
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                 
                pictureBox.Tag = items[index];

                index++;  
            }
        }

         
         
        void searchNewPictures(string path)
        {
             
            filePaths = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);

            foreach (string location in filePaths)
            {
                 
                newPaths.Add(location);
                newPaths.Add(location);

                 
                if (newPaths.Count == 24)
                {
                    return;
                }
            }

             
            filePaths = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);

            foreach (string location in filePaths)
            {
                 
                newPaths.Add(location);
                newPaths.Add(location);

                 
                if (newPaths.Count == 24) 
                { 
                    return;
                }
            }

             
            filePaths = Directory.GetFiles(path, "*.bpm", SearchOption.TopDirectoryOnly);

            foreach (string location in filePaths)
            {
                 
                newPaths.Add(path);
                newPaths.Add(path);

                 
                if (newPaths.Count == 24) 
                {
                    return;
                }
            }
        }

         
        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sb = new StringBuilder();  

            newPaths.Clear();  
            alreadyLooked.Clear();  

             
            while (newPaths.Count < 24) 
            {
                sb.Append(newPaths.Count / 2 + " pictures have been loaded.");
                sb.Append(Environment.NewLine);
                sb.Append("Please select images from a directory.");
                
                MessageBox.Show(sb.ToString());  

                sb.Clear();  
   
                 
                if (!(folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)) 
                {
                     
                    if (alreadyLooked.Contains(folderBrowserDialog1.SelectedPath) == false)
                    {
                         
                        searchNewPictures(folderBrowserDialog1.SelectedPath);

                         
                        alreadyLooked.Add(folderBrowserDialog1.SelectedPath);
                    }
                    else
                    {
                        player1.Play();  
                        MessageBox.Show("Please, select another path!");
                    }    
                } 
                
                else
                
                {
                    return;  
                }
            }

            MessageBox.Show("Transfer completed!");

             
            for (int i = 0; i < 24; i++)
            {
                items[i] = newPaths[i];
            }

            change(items);
        }


         
        private void timer3_Tick(object sender, EventArgs e)
        {
            for (int j = 0; j < 24; j++)
            {
                flag[j] = false;
                pictureBoxes[j].ImageLocation = "images/other_side.jpg";
            }

            finish.Clear();  
            startToolStripMenuItem.Enabled = true;
            
            timer1.Enabled = true;  

            end = false;  
            
            timer3.Enabled = false;  
        }

         
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (end == true)  
            {
                timer1.Enabled = false;
                startToolStripMenuItem.Text = "Reset";  
                
                startToolStripMenuItem.Enabled = false;
                toolStripMenuItem2.Enabled = false;
                drawToolStripMenuItem.Enabled = false;

                 
                click1 = false;
                click2 = false;

                path.Clear();  

                label1.Text = "0";  
                label3.Text = "0";  

                 
                tries = 0;
                time = 0;

                change(items);  

                 
                for (int i = 0; i < 24; i++)
                {
                     
                    pictureBoxes[i].ImageLocation = pictureBoxes[i].Tag.ToString();
                }

                timer3.Enabled = true;  
            } 
            else  
            {
                timer1.Enabled = false;  

                 
                if (newPaths.Count <= 24)
                {
                    for (int m = 0; m < 24; m++)
                    {
                        items[m] = defaults[m];
                    }
                }

                startToolStripMenuItem.Text = "Start";  

                label1.Text = "0";  
                label3.Text = "0";  

                 
                for (int j = 0; j < 24; j++)
                {
                    flag[j] = false;
                    pictureBoxes[j].ImageLocation = "images/other_side.jpg";
                }

                 
                toolStripMenuItem2.Enabled = true;
                drawToolStripMenuItem.Enabled = true;

                end = true;  
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
             
            Top10 top = new Top10();
            top.ShowDialog();  
        }


         
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;  
            label3.Text = time.ToString();  

            if (finish.Count == 24)  
            {
                timer1.Enabled = false;  
                
                player2.Play();  

                MessageBox.Show("Congratulations!");  

                 
                new User().databaseOperations(name, tries, time); 

                 
                Results results = new Results(name, tries.ToString(), time.ToString());
                results.ShowDialog();  

                startToolStripMenuItem.Text = "Start";  
                label3.Text = "0";  
                label1.Text = "0";  

                 
                tries = 0;
                time = 0;

                end = true;  

                toolStripMenuItem2.Enabled = true;
                drawToolStripMenuItem.Enabled = true;

                 
                foreach (PictureBox pic in pictureBoxes)
                {
                    pic.ImageLocation = "images/other_side.jpg";
                }

                finish.Clear();  
                
            }
        }

        void onClickOperation(int number)
        {
            if (timer1.Enabled == true && flag[number-1]==false)
            {
                flag[number - 1] = true;  
                pictureBoxes[number - 1].ImageLocation = pictureBoxes[number - 1].Tag.ToString();
                path.Add(number - 1);  

                 
                 
                 
                 
                 
                if (click1 == false)
                {
                    click1 = true;
                    click2 = false;
                }
                else if (click2 == false)
                {
                    click1 = true;
                    click2 = true;
                }

                if (click1 == true && click2 == true && path.Count == 2)  
                {
                    click1 = click2 = false;  

                     
                    if (pictureBoxes[path[0]].ImageLocation.Equals(pictureBoxes[path[1]].ImageLocation)) 
                    {
                        tries += 1;  
                        label1.Text = tries.ToString();  

                         
                        path.Clear();

                         
                        finish.Add(true);
                        finish.Add(true);
                    }

                    else  

                    {
                        tries += 1;  
                        label1.Text = tries.ToString();  

                         
                        flag[path[0]] = false;
                        flag[path[1]] = false;

                         
                        timer2.Enabled = true;  
                    }
                    
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBoxes[path[0]].ImageLocation = "images/other_side.jpg";
            pictureBoxes[path[1]].ImageLocation = "images/other_side.jpg";

             
            path.Clear();

            timer2.Enabled = false;  
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(1);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(2);
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(3);
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(4);
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(5);
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(6);
        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(7);
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(8);
        }

        private void pictureBox9_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(9);
        }

        private void pictureBox10_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(10);
        }

        private void pictureBox11_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(11);
        }

        private void pictureBox12_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(12);
        }

        private void pictureBox13_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(13);
        }

        private void pictureBox14_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(14);
        }

        private void pictureBox15_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(15);
        }

        private void pictureBox16_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(16);
        }

        private void pictureBox17_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(17);
        }

        private void pictureBox18_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(18);
        }

        private void pictureBox19_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(19);
        }

        private void pictureBox20_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(20);
        }

        private void pictureBox21_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(21);
        }

        private void pictureBox22_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(22);
        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void pictureBox23_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(23);
        }

        private void pictureBox24_MouseClick(object sender, MouseEventArgs e)
        {
            onClickOperation(24);
        }
    }
}
