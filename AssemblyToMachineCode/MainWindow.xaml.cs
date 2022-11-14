﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainWindow1()
        {
            InitializeComponent();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
