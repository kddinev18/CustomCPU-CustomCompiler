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
        private List<string> labels = new List<string>();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] container = ASSY_CODE.Text.Split(';');
            foreach (var command in container)
            {
                if (command == "")
                    continue;
                if(labels.Contains(command))
                {

                }
                if(command.Contains("label"))
                {
                    labels.Add(command.Split(':')[1]);
                    continue
                }
                string temp = RemoveSpecialCharacters(command);
                if (commands.Where(x => x.Command == temp) != null)
                {
                    MachineCODE.Text += commands.Where(x => x.Command == temp).First().MachineCode + '\n';
                }
            }

            File.WriteAllText("Code", MachineCODE.Text);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Commands dataRow = (Commands)Commands.SelectedItem;
            commands.Remove(dataRow);
            Commands.ItemsSource = commands;
        }
    }
}
