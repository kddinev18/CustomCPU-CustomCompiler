using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace AssemblyToMachineCode
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public class Commands
    {
        public string Command { get; set; }
        public string MachineCode { get; set; }
    }
    public partial class MainWindow1 : Window
    {
        public static ObservableCollection<Commands> commands = new ObservableCollection<Commands>();
        private Dictionary<string, int> labels = new Dictionary<string, int>();
        public MainWindow1()
        {
            InitializeComponent();
            Commands.ItemsSource = commands;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            commands.Add(new Commands()
            {
                Command = ASSY_Command_Name.Text,
                MachineCode = Machine_Code.Text
            });
            Commands.ItemsSource = commands;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ':')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        static string DecToBin(int decimalNumber)
        {
            int remainder = 0;
            char[] sb = new char[8];

            for (int i = 0; i < 8; i++)
            {
                remainder = decimalNumber % 2;
                decimalNumber /= 2;
                sb[i] = remainder == 0 ? '0' : '1';
            }
            Array.Reverse(sb);
            return String.Join("",sb);
        }

        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labels = new Dictionary<string, int>();
            MachineCODE.Text = "";
            string[] container = ASSY_CODE.Text.Split(';');
            int assemblyLine = 0;
            int number = 0;
            foreach (var command in container)
            {
                if (command == "")
                    continue;
                string temp = RemoveSpecialCharacters(command);
                if(int.TryParse(temp, out number))
                {
                    assemblyLine++;
                    MachineCODE.Text += BinaryStringToHexString(DecToBin(number));
                    MachineCODE.Text += '\n';
                }
                else if (labels.ContainsKey(temp))
                {
                    MachineCODE.Text += BinaryStringToHexString(DecToBin(labels[temp]));
                    MachineCODE.Text += '\n';
                }
                else if(temp.Contains("label"))
                {
                    if(labels.ContainsKey(temp))
                    {
                        // Error
                    }
                    else
                    {
                        labels.Add(temp.Split(':')[1], assemblyLine);
                    }
                }
                else if (commands.Where(x => x.Command == temp) != null)
                {
                    MachineCODE.Text += BinaryStringToHexString(commands.Where(x => x.Command == temp).First().MachineCode);
                    MachineCODE.Text += '\n';
                    assemblyLine++;
                }
                else
                {
                    // Error
                }
            }

            File.WriteAllText("Code.txt", MachineCODE.Text);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Commands dataRow = (Commands)Commands.SelectedItem;
            commands.Remove(dataRow);
            Commands.ItemsSource = commands;
        }
    }
}
