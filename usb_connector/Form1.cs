using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;

namespace usb_connector
{
    public partial class Form1 : Form
    {

        /** objects **/
        public static System.Timers.Timer _timer;
        public static SerialPort _serialPort;
        public static FileSystemWatcher _wacher;

        static StreamReader[] reader = { null, null, null };

        /** DLL's inport function for actions **/
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        /** Int's and constant for numbers **/
        const int NR_PAGES = 3; //nr of buttons pages
        const int NR_LINES = 1024; //nr of max lines in a pages

        static int[] PAGES_nr_lines = new int[NR_PAGES]; //nr lines after readings

        /** Strings and char matrix's **/
        static string _portName = null;
        static string SYS_PATH = Directory.GetCurrentDirectory().ToString() + '\\';
        static string NOTE_PAD_PATH = "C:\\Program Files\\WindowsApps\\Microsoft.WindowsNotepad_11.2208.25.0_x64__8wekyb3d8bbwe\\Notepad\\Notepad.exe";

        static string[] modes =
        {
            "Launch App",
            "Action",
            "Sound Effect"
        };

        static string[] PAGES =
        {
            "Confs\\ButtonsPage1.ini",
            "Confs\\ButtonsPage2.ini",
            "Confs\\ButtonsPage3.ini"
        };

        static string[,] PAGE_line = new string[NR_PAGES, NR_LINES];

        /** Boolian Flags **/
        static bool port_is_connected = false, i_can_connect = false;

        static bool[] USED_PAGES = { false, false, false };


        /** mask's **/
        static int[] Pages_mask =
        {
            0b01000000, //page 1
            0b10000000, //page 2
            0b11000000  //page 3
        };

        static int[] Buttons_mask =
        {
            0b00000001, //button 1
            0b00000010, //button 2
            0b00000100, //button 3
            0b00001000, //button 4
            0b00010000, //button 5
            0b00100000  //button 6
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /** set notification ballon **/
            notifyIcon1.BalloonTipTitle = "USB_Key_macro";
            notifyIcon1.BalloonTipText = "Usb keypad macro listen";
            notifyIcon1.Text = "USB_KEY_MACRO";
            
            /** set app code section **/
            //set exit handler
            //read conf data
            _readConfs();
            //set file system wacher - one file is channged clean al data and read again 
            _setWacher();
            i_can_connect = true;
            port_is_connected = false;
            //set usb conection
            _setSerialPort();
            //set timer event
            _setTimer(50);
            this.WindowState = FormWindowState.Minimized;
        }


        public static void _setWacher()
        {
            _wacher = new FileSystemWatcher(SYS_PATH);
            _wacher.NotifyFilter =
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName;

            _wacher.Changed += _configUpdate;
            _wacher.Renamed += _configUpdate;
            _wacher.Deleted += _configUpdate;
            _wacher.Created += _configUpdate;

            _wacher.Filter = "*.ini";
            _wacher.IncludeSubdirectories = true;
            _wacher.EnableRaisingEvents = true;
        }

        /** function to update buttons data **/
        public static void _configUpdate(Object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine("Update files");
            _disconnectPort();
            _cleanAllPages();
            i_can_connect = false;
            _readConfs();
            i_can_connect = true;
            //Console.WriteLine("after Update files");
            port_is_connected = false;
        }

        /** function to clean all pages **/
        public static void _cleanAllPages()
        {
            for (int ini_index = 0; ini_index < NR_PAGES; ini_index++)
            {
                for (int nr_line = 0; nr_line < PAGES_nr_lines[ini_index]; nr_line++)
                {
                    PAGE_line[ini_index, nr_line] = string.Empty;
                }
            }
        }

        /** set timer function **/
        public static void _setTimer(int ms)
        {
            _timer = new System.Timers.Timer(ms);
            _timer.Elapsed += _timerEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        /** reset timer function **/
        public static void _releaseTimer()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        /** event function for timer **/
        public static void _timerEvent(Object source, ElapsedEventArgs e)
        {
            //conect to usb
            if (port_is_connected == false)
            {
                if (i_can_connect == true)
                {
                   // Console.WriteLine("TRY TO Connect");
                    _portName = _conectPort();
                    try
                    {
                        if (string.Compare(_portName, "default") != 0)
                        {
                            _serialPort.PortName = _portName;
                            _serialPort.Open();
                            port_is_connected = true;
                                                 
                            // Console.WriteLine("conect to " + _portName);
                        }
                    }
                    catch { }

                }
            }
            else
            {
                //read msg
                try
                {
                    int msg = _serialPort.ReadByte();

                    //Console.WriteLine("message " + msg.ToString() + "\n");
                    int page_index = _what_page_i_am(msg);
                    if (page_index != -1)
                    {
                        int button = 0;
                        button = _what_button_i_am(msg);
                        if (button != 0)
                        {
                            _executeCommand(page_index, button);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (string.Compare(ex.GetType().ToString(), "System.InvalidOperationException") == 0)
                    {
                        _disconnectPort();
                        _serialPort.Close();
                        _portName = string.Empty;
                        port_is_connected = false;
                        
                    }
                }
            }
        }

        public static void _executeCommand(int page, int button)
        {
            string button_string = "[Button" + button + "]";
            //Console.WriteLine(button_string);

            int cnt = 0;
            string path = null;
            if (PAGES_nr_lines[page] != 0)
            {
                while (PAGE_line[page, cnt] != null)
                {
                    if (string.Compare(
                        button_string, PAGE_line[page, cnt]) == 0)
                    {
                        string mode = PAGE_line[page, cnt + 1].Substring(5);
                        if (string.Compare(modes[0], mode) == 0)
                        {
                            path = PAGE_line[page, cnt + 2].Substring(5);
                            Process process = new Process();
                            process.StartInfo.FileName = path;
                            process.StartInfo.CreateNoWindow = true;
                            process.Start();
                            break;
                        }
                        if (string.Compare(modes[1], mode) == 0)
                        {
                            string action = PAGE_line[page, cnt + 3].Substring(7);
                            if (string.Compare(action, "Volume up") == 0)
                            {
                                keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
                            }
                            if (string.Compare(action, "Volume down") == 0)
                            {
                                keybd_event((byte)Keys.VolumeDown, 0, 0, 0);
                            }
                            if (string.Compare(action, "Volume mute") == 0)
                            {
                                keybd_event((byte)Keys.VolumeMute, 0, 0, 0);
                            }
                            if (string.Compare(action, "Print Screen") == 0)
                            {
                                keybd_event((byte)Keys.PrintScreen, 0, 0, 0);
                            }
                            if (string.Compare(action, "Open new text file") == 0)
                            {
                                Process process = new Process();
                                process.StartInfo.FileName = NOTE_PAD_PATH;
                                process.StartInfo.CreateNoWindow = true;
                                process.Start();
                            }
                            if (string.Compare(action, "Lock Screen") == 0)
                            {
                                LockWorkStation();
                            }
                            break;
                        }

                        if (string.Compare(modes[2], mode) == 0)
                        {
                            path = PAGE_line[page, cnt + 2].Substring(5);
                            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                            player.Play();
                        }
                    }
                    cnt++;
                }
            }

        }
        public static int _what_page_i_am(int msg)
        {
            int page = -1;

            switch ((msg & Pages_mask[2]))
            {
                case 0b01000000:
                    page = 0;
                    break;
                case 0b10000000:
                    page = 1;
                    break;
                case 0b11000000:
                    page = 2;
                    break;
                default: page = 0; break;
            }

            return page;
        }

        public static int _what_button_i_am(int msg)
        {
            int butt = 0;

            for (int i = 0; i < Buttons_mask.Length; i++)
            {
                if ((msg & Buttons_mask[i]) == Buttons_mask[i])
                {
                    butt = i + 1;
                    break;
                }
            }

            return butt;
        }

        /** function to set serial port connection atributes **/
        public static void _setSerialPort()
        {
            _serialPort = new SerialPort();

            _serialPort.ReadTimeout = 500; //define time out for read and write 500 ms
            _serialPort.WriteTimeout = 500;

            _serialPort.BaudRate = int.Parse("9600"); //set budrate
            _serialPort.Parity = Parity.None; //set pariti none
            _serialPort.StopBits = StopBits.Two; //set parity bits 
            _serialPort.DataBits = 8; //set data bits lenght
            _serialPort.Handshake = Handshake.None; //set handsake to none
        }

        public static string _conectPort()
        {
            string port = "default";
            bool _portFinde = false;

            char[] send_msg = new char[1];
            int recv = 0;

            foreach (string s in SerialPort.GetPortNames())
            {
                try
                {
                    _serialPort.PortName = s;
                    _serialPort.Open();

                    send_msg[0] = '1';
                    if (_portFinde == false)
                    {
                        _serialPort.Write(send_msg, 0, 1);
                        int i = 10;
                        while (i > 0)
                        {
                            recv = _serialPort.ReadByte();
                            if (recv == '1')
                            {
                                _portFinde = true;
                                port = s;
                                break;
                            }
                            else
                            {
                                _portFinde = false;
                                i--;
                            }
                        }
                    }
                    _serialPort.Close();
                }
                catch
                {
                    _serialPort.Close();
                }
            }

            return port;
        }

        /** Funtion to send a disconect msg to com port **/
        public static void _disconnectPort()
        {
            try
            {
                char[] disconnect_msg = new char[1];
                disconnect_msg[0] = '0';
                _serialPort.Write(disconnect_msg, 0, 1);
                port_is_connected = false;
            }
            catch { }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _disconnectPort();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    this.Hide();
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(1000);
                }
                else if (FormWindowState.Normal == this.WindowState)
                {
                    notifyIcon1.Visible = false;
                }
            }
            catch { }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _disconnectPort();
                this.Close();
            }
            catch { }
        }

        private void ComPortFiled_TextChanged(object sender, EventArgs e)
        {

        }

        /** function to read all conf files **/
        public static void _readConfs()
        {
            for (int ini_index = 0; ini_index < NR_PAGES; ini_index++)
            {
                if (File.Exists(SYS_PATH + PAGES[ini_index]))
                {
                    reader[ini_index] = new StreamReader(SYS_PATH + PAGES[ini_index]);
                    if (reader[ini_index] != null)
                    {
                        int cnt = -1;
                        do
                        {
                            cnt++;
                            PAGE_line[ini_index, cnt] = reader[ini_index].ReadLine();
                        } while (PAGE_line[ini_index, cnt] != null);
                        PAGES_nr_lines[ini_index] = cnt;
                        reader[ini_index].Close();
                    }
                    if (PAGES_nr_lines[ini_index] != 0)
                    {
                        USED_PAGES[ini_index] = true;
                    }
                    else
                    {
                        USED_PAGES[ini_index] = false;
                    }
                }


            }
        }
    }
}
