using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonConfigAPP
{
    public partial class Layout : Form
    {
        static int NR_PAGES = 3;
        static int NR_BUTONS = 6;

        static int NR_LINES = 1024;
        static int page_index = 0;

        static string[] PAGES =
        {
                "Confs\\ButtonsPage1.ini",
                "Confs\\ButtonsPage2.ini",
                "Confs\\ButtonsPage3.ini"
        };

        static string[] modes = { "Launch App", "Action", "Sound Effect" };

        static string[] buttons = { "Button1", "Button2", "Button3", "Button4", "Button5", "Button6" };
        
        static int[] buttons_mask =
        {
            0b00000001, //button1
            0b00000010, //button2
            0b00000100, //button3
            0b00001000, //button4
            0b00010000, //button5
            0b00100000  //button6

        };

        static string[,] PAGE_line = new string[NR_PAGES, NR_LINES];

        static int[] Page_nr_lines = new int[NR_PAGES];

        static int finde = 0;

        public Layout()
        {
            string SYS_PATH = Directory.GetCurrentDirectory() + '\\';

            StreamReader[] reader = { null, null, null };

            InitializeComponent();
            Page_label.Text = (page_index + 1).ToString();

            for (int i = 0; i < NR_PAGES; i++)
            {
                if (!File.Exists(SYS_PATH + PAGES[i]))
                {
                    MessageBox.Show("Config file " + i + " not exist will be created\n", "WARRNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
                else
                {
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

            
            if (Page_nr_lines[page_index] != 0)
            {
                for (int index = 0; index < NR_BUTONS; index++)
                {
                    int cnt_line = 0;
                    do
                    {

                        if (PAGE_line[page_index, cnt_line].Contains(buttons[index]))
                        {
                            finde |= buttons_mask[index];
                        }
                        cnt_line++;
                    } while (PAGE_line[page_index, cnt_line] != null);
                }
            }
            else
            {
                finde = 0;
            }

            Button[] button_vect = 
            {
                button1_layout,
                button2_layout,
                button3_layout,
                button4_layout,
                button5_layout,
                button6_layout
            };
      
            for (int index = 0; index < NR_BUTONS; index++)
            {
                
                if ((finde & buttons_mask[index]) != 0)
                {
                    button_vect[index].BackColor = Color.LightGreen;
                }
                else
                {
                    button_vect[index].BackColor = Color.LightGray;
                }
            }
        }

        private void button1_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[0]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if(finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] == '\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void button2_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[1]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if (finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] == '\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void button3_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[2]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if (finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] == '\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void button4_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[3]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if (finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] == '\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void button5_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[4]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if (finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] =='\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void button6_layout_Click(object sender, EventArgs e)
        {
            int finde = 0;
            if (Page_nr_lines[page_index] != 0)
            {
                int cnt_line = 0;
                do
                {

                    if (PAGE_line[page_index, cnt_line].Contains(buttons[5]))
                    {
                        finde = 1;
                        break;
                    }
                    cnt_line++;
                } while (PAGE_line[page_index, cnt_line] != null);

                if (finde == 1)
                {
                    Button_label.Text = buttons[0];
                    Button_label.Visible = true;
                    Mode_label.Text = PAGE_line[page_index, cnt_line + 1].Substring(5);
                    Mode_label.Visible = true;

                    if (string.Compare(PAGE_line[page_index, cnt_line + 1].Substring(5), modes[1]) == 0)
                    {
                        Launch_label.Text = PAGE_line[page_index, cnt_line + 3].Substring(7);
                        Launch_label.Visible = true;
                    }
                    else
                    {
                        string test = PAGE_line[page_index, cnt_line + 2].Substring(5);
                        int path_index = 0;
                        for (int i = 0; i < test.Length; i++)
                        {
                            if (test[i] == '\\')
                                path_index = i;
                        }

                        Launch_label.Text = test.Substring(path_index + 1);
                        Launch_label.Visible = true;
                    }
                }
                else
                {
                    Button_label.Visible = false;
                    Mode_label.Visible = false;
                    Launch_label.Visible = false;
                }
            }
            else
            {
                Button_label.Visible = false;
                Mode_label.Visible = false;
                Launch_label.Visible = false;
            }
        }

        private void Layout_Load(object sender, EventArgs e)
        {

        }

        private void Page_minus_Click(object sender, EventArgs e)
        {
            if ((page_index + 1) > 1)
            {
                page_index -=1;
                Page_label.Text = (page_index + 1).ToString();
            }
            finde = 0;
            
            if (Page_nr_lines[page_index] != 0)
            {
                for (int index = 0; index < NR_BUTONS; index++)
                {
                    int cnt_line = 0;
                    do
                    {

                        if (PAGE_line[page_index, cnt_line].Contains(buttons[index]))
                        {
                            finde |= buttons_mask[index];
                        }
                        cnt_line++;
                    } while (PAGE_line[page_index, cnt_line] != null);
                }
            }
            else
            {
                finde = 0;
            }
            
            Button[] button_vect =
            {
                button1_layout,
                button2_layout,
                button3_layout,
                button4_layout,
                button5_layout,
                button6_layout
            };

            for (int index = 0; index < NR_BUTONS; index++)
            {

                if ((finde & buttons_mask[index]) != 0)
                {
                    button_vect[index].BackColor = Color.LightGreen;
                }
                else
                {
                    button_vect[index].BackColor = Color.LightGray;
                }
            }

            Button_label.Visible = false;
            Mode_label.Visible = false;
            Launch_label.Visible = false;
        }

        private void Page_plus_Click(object sender, EventArgs e)
        {
            if((page_index + 1) < NR_PAGES)
            {
                page_index++;
                Page_label.Text = (page_index + 1).ToString();
            }
            finde = 0;
           
            if (Page_nr_lines[page_index] != 0)
            {
                for (int index = 0; index < NR_BUTONS; index++)
                {
                    int cnt_line = 0;
                    do
                    {

                        if (PAGE_line[page_index, cnt_line].Contains(buttons[index]))
                        {
                            finde |= buttons_mask[index];
                        }
                        cnt_line++;
                    } while (PAGE_line[page_index, cnt_line] != null);
                }
            }
            else
            {
                finde = 0;
            }

            Button[] button_vect =
            {
                button1_layout,
                button2_layout,
                button3_layout,
                button4_layout,
                button5_layout,
                button6_layout
            };

            for (int index = 0; index < NR_BUTONS; index++)
            {

                if ((finde & buttons_mask[index]) != 0)
                {
                    button_vect[index].BackColor = Color.LightGreen;
                }
                else
                {
                    button_vect[index].BackColor = Color.LightGray;
                }
            }

            Button_label.Visible = false;
            Mode_label.Visible = false;
            Launch_label.Visible = false;
        }
    }
}
