using System.Windows;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.WPF.ViewModels;

namespace IntegracjaSystemowProjekt.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}