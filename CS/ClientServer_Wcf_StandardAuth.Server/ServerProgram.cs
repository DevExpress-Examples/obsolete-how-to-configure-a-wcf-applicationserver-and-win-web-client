using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp;
using System.ServiceModel;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Data;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.MiddleTier;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Web.SystemModule;

namespace ClientServer_Wcf_StandardAuth.Server {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            try {
                Console.WriteLine("Starting...");
                DataSet dataSet = new DataSet();
                ValueManager.ValueManagerType = typeof(MultiThreadValueManager<>).GetGenericTypeDefinition();

                ServerApplication serverApplication = new ServerApplication();
                serverApplication.Modules.Add(new ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule());
                serverApplication.Modules.Add(new SystemWindowsFormsModule());
                serverApplication.Modules.Add(new SystemAspNetModule());
                serverApplication.CreateCustomObjectSpaceProvider += delegate(object sender, CreateCustomObjectSpaceProviderEventArgs e) {
                    e.ObjectSpaceProvider = new XPObjectSpaceProvider(new MemoryDataStoreProvider(dataSet));
                };
                serverApplication.DatabaseVersionMismatch += delegate(object sender, DatabaseVersionMismatchEventArgs e) {
                    e.Updater.Update();
                    e.Handled = true;
                };

                Console.WriteLine("Setup...");
                serverApplication.Setup();
                Console.WriteLine("CheckCompatibility...");
                serverApplication.CheckCompatibility();
                serverApplication.Dispose();

                Console.WriteLine("Starting server...");
                QueryRequestSecurityStrategyHandler securityProviderHandler = delegate() {
                    return new SecurityStrategyComplex(typeof(SecuritySystemUser), typeof(SecuritySystemRole), new AuthenticationStandard());
                };

                IDataLayer dataLayer = new SimpleDataLayer(XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary, new DataSetDataStore(dataSet, AutoCreateOption.SchemaAlreadyExists));
                SecuredDataServer dataServer = new SecuredDataServer(dataLayer, securityProviderHandler);

                ServiceHost serviceHost = new ServiceHost(new WcfSecuredDataServer(dataServer));
                serviceHost.AddServiceEndpoint(typeof(IWcfSecuredDataServer), WcfDataServerHelper.CreateDefaultBinding(), "http://localhost:1451/DataServer");
                serviceHost.Open();

                Console.WriteLine("Server is started. Press Enter to stop.");
                Console.ReadLine();
                Console.WriteLine("Stopping...");
                serviceHost.Close();
                Console.WriteLine("Server is stopped.");
            }
            catch(Exception e) {
                Console.WriteLine("Exception occurs: " + e.Message);
                Console.WriteLine("Press Enter to close.");
                Console.ReadLine();
            }
        }
    }
}
