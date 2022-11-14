using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace AssemblyToMachineCode
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            File.WriteAllText("Comands.json", JsonSerializer.Serialize(MainWindow1.commands));
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if(File.Exists("Comands.json"))
                MainWindow1.commands = JsonSerializer.Deserialize<ObservableCollection<Commands>>(File.ReadAllText("Comands.json"));
        }
    }
}
