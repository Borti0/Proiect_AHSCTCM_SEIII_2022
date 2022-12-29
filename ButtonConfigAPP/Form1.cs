using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ServiceProcess;

namespace ButtonConfigAPP
{
    public partial class Form1 : Form
    {

        string[] modes = { "Launch App", "Action", "Sound Effect" };
        string[] pages = { "Page1", "Page2", "Page3" };
        

        string mode = "",
            button = "",
            page = "",
            action = "",
            path = "";

        string SYS_PATH = Directory.GetCurrentDirectory() + '\\';
        static int NR_PAGES = 3;
        static int NR_LINES = 1024;
        string[] PAGES =
        {
                "Confs\\ButtonsPage1.ini",
                "Confs\\ButtonsPage2.ini",
                "Confs\\ButtonsPage3.ini"
        };

        string[,] PAGE_line = new string[NR_PAGES, NR_LINES];
        int[] Page_nr_lines = new int[NR_PAGES];

        StreamReader[] reader = { null, null, null };
        StreamWriter[] writer = { null, null, null };

        

        public Form1()
        {
            InitializeComponent();
            for(int i = 0 ; i < NR_PAGES; i++)
            {
                if (!File.Exists(SYS_PATH + PAGES[i]))
                {
                    MessageBox.Show("Config file " + i +" not exist will be created\n", "WARRNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    var file = File.CreateText(SYS_PATH + PAGES[i]);
                    file.Close();
                }
                reader[i] = new StreamReader(SYS_PATH + PAGES[i]);
                if (reader[i] == null)
                {
                    MessageBox.Show("Config file " + i + " can,t be opened", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                int cnt = -1;
                do
                {
                    cnt++;
                    PAGE_line[i, cnt] = reader[i].ReadLine();
                    
                } while (PAGE_line[i, cnt] != null);
                Page_nr_lines[i] = cnt;
                reader[i].Close();
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ADD_Click(object sender, EventArgs e)
        {

            Updates.Text = string.Empty;

            int err = 0;
            
            string[] errs =
            {
                "Need to select one button\n",
                "Need to select one mode\n",
                "Need to select one page\n",
                "Complet path of App\n",
                "Select Action\n",
                "Complet path of sound\n"
            };
            if (button.Length == 0)
                err++;
            if (mode.Length == 0)
                err++;
            if (page.Length == 0)
                err++;
            if (err != 0)
            { 
                if (err == 1)
                    MessageBox.Show(errs[0], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (err == 2)
                    MessageBox.Show(errs[0] + errs[1], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (err == 3)
                    MessageBox.Show(errs[0] + errs[1] + errs[2], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.Compare(mode, modes[0]) == 0 && path.Length == 0)
                MessageBox.Show(errs[3], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (string.Compare(mode, modes[1]) == 0 && action.Length == 0)
                MessageBox.Show(errs[4], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (string.Compare(mode, modes[2]) == 0 && path.Length == 0)
                MessageBox.Show(errs[5], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
            if(err == 0)
            {
                int page_index = (page[4] - '0') - 1;
                int cnt_line = 0, finde = 0, button_cnt = 0;
                
                if (Page_nr_lines[page_index] != 0)
                {
                    do
                    {

                        if (PAGE_line[page_index, cnt_line].Contains(button))
                        {
                            button_cnt = cnt_line;
                            cnt_line++;
                            if (PAGE_line[page_index, cnt_line].Contains("mode"))
                            {
                                string substring = PAGE_line[page_index, cnt_line].Replace(PAGE_line[page_index, cnt_line].Substring(5), mode);
                                PAGE_line[page_index, cnt_line] = substring;

                                if (string.Compare(mode, modes[0]) == 0 || string.Compare(mode, modes[2]) == 0)
                                {
                                    substring = PAGE_line[page_index, cnt_line + 1].Replace(PAGE_line[page_index, cnt_line + 1].Substring(5), path);
                                    PAGE_line[page_index, cnt_line + 1] = substring;
                                    substring = PAGE_line[page_index, cnt_line + 2].Replace(PAGE_line[page_index, cnt_line + 2].Substring(7), "null");
                                    PAGE_line[page_index, cnt_line + 2] = substring;
                                }
                                if (string.Compare(mode, modes[1]) == 0)
                                {
                                    substring = PAGE_line[page_index, cnt_line + 2].Replace(PAGE_line[page_index, cnt_line + 2].Substring(7), action);
                                    PAGE_line[page_index, cnt_line + 2] = substring;
                                    substring = PAGE_line[page_index, cnt_line + 1].Replace(PAGE_line[page_index, cnt_line + 1].Substring(5), "null");
                                    PAGE_line[page_index, cnt_line + 1] = substring;
                                }
                                Updates.Visible = true;
                                Updates.Text = "In Page" + (page_index + 1) + " Button" + button[6] + " are updated";
                                finde = 1;
                                break;
                            }
                        }
                        cnt_line++;
                    } while (PAGE_line[page_index, cnt_line] != null);
                }
                else
                {
                    button_cnt = cnt_line;
                    PAGE_line[page_index, button_cnt] = "[" + button + "]";
                    PAGE_line[page_index, button_cnt + 1] = "mode=" + mode;
                    if (string.Compare(mode, modes[0]) == 0 || string.Compare(mode, modes[2]) == 0)
                    {
                        PAGE_line[page_index, button_cnt + 2] = "path=" + path;
                        PAGE_line[page_index, button_cnt + 3] = "action=null";
                    }

                    if (string.Compare(mode, modes[1]) == 0)
                    {
                        PAGE_line[page_index, button_cnt + 2] = "path=null";
                        PAGE_line[page_index, button_cnt + 3] = "action=" + action;
                    }
                    PAGE_line[page_index, button_cnt + 4] = "\n";
                    Updates.Visible = true;
                    Updates.Text = "In Page" + (page_index + 1) + " Button" + button[6] + " are added";

                }

                if (finde == 0) //button negasit
                {
                    button_cnt = Page_nr_lines[page_index];

                    PAGE_line[page_index, button_cnt] = "[" + button + "]";
                    PAGE_line[page_index, button_cnt + 1] = "mode=" + mode;
                    if (string.Compare(mode, modes[0]) == 0 || string.Compare(mode, modes[2]) == 0)
                    {
                        PAGE_line[page_index, button_cnt + 2] = "path=" + path;
                        PAGE_line[page_index, button_cnt + 3] = "action=null";
                    }

                    if (string.Compare(mode, modes[1]) == 0)
                    {
                        PAGE_line[page_index, button_cnt + 2] = "path=null";
                        PAGE_line[page_index, button_cnt + 3] = "action=" + action;
                    }
                    PAGE_line[page_index, button_cnt + 4] = "\n";

                    Updates.Visible = true;
                    Updates.Text = "In Page" + (page_index + 1) + " Button" + button[6] + " are added";
                }
                Page_nr_lines[page_index] = button_cnt + 4;

            }
            Button_nr.SelectedItem = null;
            Mode.SelectedItem = null;
            Page_select.SelectedItem = null;
            Actions.SelectedItem = null;
            Path_to_app_.Clear();
        }

        private void DEL_Click(object sender, EventArgs e)
        {
            int err = 0;
            string[] errs =
            {
                "Need to select one button\n",
                "Need to select one page\n"
            };

            if (button.Length == 0)
                err++;
            if (page.Length == 0)
                err++;
            if(err != 0)
            {
                if(err == 1)
                    MessageBox.Show(errs[0], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(err == 2)
                    MessageBox.Show(errs[0] + errs[1], "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(err == 0)
            {
                int page_index = (page[4] - '0') - 1;
                int cnt_line = 0, finde = 0, button_cnt = 0;

                if (Page_nr_lines[page_index] != 0)
                {
                    do
                    {
                        if (PAGE_line[page_index, cnt_line].Contains(button))
                        {
                            finde = 1;
                            break;
                        }
                        else
                        {
                            finde = 0;
                        }
                        cnt_line++;
                    } while (PAGE_line[page_index, cnt_line] != null);

                    if (finde == 1)
                    {
                        button_cnt = cnt_line;
                        for (int index = button_cnt; index < button_cnt + 4; index++)
                        {
                            PAGE_line[page_index, index] = string.Empty;
                        }

                        do
                        {
                            string aux = PAGE_line[page_index, button_cnt];
                            PAGE_line[page_index, button_cnt] = PAGE_line[page_index, button_cnt + 4];
                            PAGE_line[page_index, button_cnt + 4] = aux;
                            button_cnt++;
                        } while (button_cnt < Page_nr_lines[page_index] - 4);
                        Page_nr_lines[page_index] -= 4;

                        Updates.Visible = true;
                        Updates.Text = "Button" + button[6] + " from Page" + (page_index + 1) + " are deleted";
                    }
                    else
                    {
                        MessageBox.Show("Button" +button[6] +" dosen't exist", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Page" + page_index + " is empty", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Button_nr.SelectedItem = null;
            Mode.SelectedItem = null;
            Page_select.SelectedItem = null;
            Actions.SelectedItem = null;
            Path_to_app_.Clear();
        }

        private void Button_nr_SelectedIndexChanged(object sender, EventArgs e)
        {
            button = Button_nr.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            for (int index = 0; index < NR_PAGES; index ++)
            {
                writer[index] = new StreamWriter(SYS_PATH + PAGES[index]);

                for (int i =0; i < Page_nr_lines[index]; i++)
                {
                    writer[index].WriteLine(@PAGE_line[index, i]);
                }

                writer[index].Close();
            }
            Updates.Visible = true;
            Updates.Text = "Pages are saved";

            Button_nr.SelectedItem = null;
            Mode.SelectedItem = null;
            Page_select.SelectedItem = null;
            Actions.SelectedItem = null;
            Path_to_app_.Clear();
        }

        private void Layout_button_Click(object sender, EventArgs e)
        {
            Layout layout_window = new Layout();
            layout_window.Show();
        }

        private void Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = Mode.Text;

            Path_to_app_.Clear();
            
            if (String.Compare(mode, modes[1]) == 0)
            {
                Actions_label.Visible = true;
                Actions.Visible = true;
            }
            else
            {
                Actions_label.Visible = false;
                Actions.Visible = false;
            }

            if (String.Compare(mode, modes[0]) == 0)
            {
                Path_to_app_.Visible = true;
                LabelPath.Text = "Path of App";
                LabelPath.Visible = true;
            }
            else if (String.Compare(mode, modes[2]) == 0)
            {
                Path_to_app_.Visible = true;
                LabelPath.Visible = true;
                LabelPath.Text = "Sound efect path";
            }
            else
            {
                Path_to_app_.Visible = false;
                LabelPath.Text = "Path of App or Sound efect:";
                LabelPath.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Page_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = Page_select.Text;
        }

        private void Actions_SelectedIndexChanged(object sender, EventArgs e)
        {
            action = Actions.Text;
        }

        private void Path_to_app__TextChanged(object sender, EventArgs e)
        {
            path = Path_to_app_.Text;
        }

    }
}
