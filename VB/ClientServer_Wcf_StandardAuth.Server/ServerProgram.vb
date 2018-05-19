Imports System.ServiceProcess
Imports System.Text
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp
Imports System.ServiceModel
Imports DevExpress.ExpressApp.Security.ClientServer.Wcf
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.MiddleTier
Imports DevExpress.ExpressApp.Win.SystemModule
Imports DevExpress.ExpressApp.Web.SystemModule

Namespace ClientServer_Wcf_StandardAuth.Server
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Shared Sub Main()
            Try
                Console.WriteLine("Starting...")
                Dim dataSet As New DataSet()
                ValueManager.ValueManagerType = GetType(MultiThreadValueManager(Of )).GetGenericTypeDefinition()

                Dim serverApplication As New ServerApplication()
                serverApplication.Modules.Add(New ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule())
                serverApplication.Modules.Add(New SystemWindowsFormsModule())
                serverApplication.Modules.Add(New SystemAspNetModule())
                AddHandler serverApplication.CreateCustomObjectSpaceProvider, Sub(sender As Object, e As CreateCustomObjectSpaceProviderEventArgs) e.ObjectSpaceProvider = New XPObjectSpaceProvider(New MemoryDataStoreProvider(dataSet))
                AddHandler serverApplication.DatabaseVersionMismatch, Sub(sender As Object, e As DatabaseVersionMismatchEventArgs)
                    e.Updater.Update()
                    e.Handled = True
                End Sub

                Console.WriteLine("Setup...")
                serverApplication.Setup()
                Console.WriteLine("CheckCompatibility...")
                serverApplication.CheckCompatibility()
                serverApplication.Dispose()

                Console.WriteLine("Starting server...")
                Dim securityProviderHandler As QueryRequestSecurityStrategyHandler = Function() New SecurityStrategyComplex(GetType(SecuritySystemUser), GetType(SecuritySystemRole), New AuthenticationStandard())

                Dim dataLayer As IDataLayer = New SimpleDataLayer(XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary, New DataSetDataStore(dataSet, AutoCreateOption.SchemaAlreadyExists))
                Dim dataServer As New SecuredDataServer(dataLayer, securityProviderHandler)

                Dim serviceHost As New ServiceHost(New WcfSecuredDataServer(dataServer))
                serviceHost.AddServiceEndpoint(GetType(IWcfSecuredDataServer), WcfDataServerHelper.CreateDefaultBinding(), "http://localhost:1451/DataServer")
                serviceHost.Open()

                Console.WriteLine("Server is started. Press Enter to stop.")
                Console.ReadLine()
                Console.WriteLine("Stopping...")
                serviceHost.Close()
                Console.WriteLine("Server is stopped.")
            Catch e As Exception
                Console.WriteLine("Exception occurs: " & e.Message)
                Console.WriteLine("Press Enter to close.")
                Console.ReadLine()
            End Try
        End Sub
    End Class
End Namespace
