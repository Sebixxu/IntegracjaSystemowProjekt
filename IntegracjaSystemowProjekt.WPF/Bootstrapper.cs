using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.WPF.ViewModels;
using ISP.WCF;

namespace IntegracjaSystemowProjekt.WPF
{
    public class Bootstrapper : BootstrapperBase
    {
        // Step 1: Create a URI to serve as the base address.
        private static Uri baseAddress = new Uri("http://localhost:6968/WCFServiceST/");

        // Step 2: Create a ServiceHost instance.
        ServiceHost selfHost = new ServiceHost(typeof(LaptopService), baseAddress);

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
            StartServiceOnOtherThread();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);

            if(selfHost.State == CommunicationState.Opened)
                selfHost.Close();
        }

        private void StartServiceOnOtherThread()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                StartService();
            }).Start();
        }

        private void StartService()
        {
            try
            {
                // Step 3: Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(ILaptopService), new WSHttpBinding(), "CalculatorService");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5: Start the service.
                selfHost.Open();
            }
            catch (CommunicationException ce)
            {
                selfHost.Abort();
            }
        }
    }
}