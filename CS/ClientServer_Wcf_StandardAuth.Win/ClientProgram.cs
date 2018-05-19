using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;
using System.ServiceModel;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Xpo;
using System.Data;

namespace ClientServer_Wcf_StandardAuth.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
			DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;

            ClientServer_Wcf_StandardAuthWindowsFormsApplication winApplication = new ClientServer_Wcf_StandardAuthWindowsFormsApplication();
            string connectionString = "http://localhost:1451/DataServer";
            WcfSecuredDataServerClient clientDataServer = new WcfSecuredDataServerClient(WcfDataServerHelper.CreateDefaultBinding(), 
                 new EndpointAddress(connectionString));
            ServerSecurityClient securityClient = new ServerSecurityClient(clientDataServer, new ClientInfoFactory());
            try {
                winApplication.ApplicationName = "ClientServer_Wcf_StandardAuth";
                winApplication.Security = securityClient;

                winApplication.CreateCustomObjectSpaceProvider += delegate(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
                    e.ObjectSpaceProvider = new DataServerObjectSpaceProvider(clientDataServer, securityClient);
                };
                winApplication.DatabaseVersionMismatch += delegate(object sender, DatabaseVersionMismatchEventArgs e) {
                    e.Updater.Update();
                    e.Handled = true;
                };

                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }
        }
    }
}
