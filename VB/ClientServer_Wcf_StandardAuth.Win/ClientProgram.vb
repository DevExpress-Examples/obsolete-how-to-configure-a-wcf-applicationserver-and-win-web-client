Imports System.Configuration

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.Security.ClientServer.Wcf
Imports System.ServiceModel
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.ExpressApp.Xpo

Namespace ClientServer_Wcf_StandardAuth.Win
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
#If EASYTEST Then
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached

            Dim winApplication As New ClientServer_Wcf_StandardAuthWindowsFormsApplication()
            Dim connectionString As String = "http://localhost:1451/DataServer"
            Dim clientDataServer As New WcfSecuredDataServerClient(WcfDataServerHelper.CreateDefaultBinding(), New EndpointAddress(connectionString))
            Dim securityClient As New ServerSecurityClient(clientDataServer, New ClientInfoFactory())
            Try
                winApplication.ApplicationName = "ClientServer_Wcf_StandardAuth"
                winApplication.Security = securityClient

                AddHandler winApplication.CreateCustomObjectSpaceProvider, Sub(sender As Object, e As CreateCustomObjectSpaceProviderEventArgs) e.ObjectSpaceProvider = New DataServerObjectSpaceProvider(clientDataServer, securityClient)
                AddHandler winApplication.DatabaseVersionMismatch, Sub(sender As Object, e As DatabaseVersionMismatchEventArgs)
                    e.Updater.Update()
                    e.Handled = True
                End Sub

                winApplication.Setup()
                winApplication.Start()
            Catch e As Exception
                winApplication.HandleException(e)
            End Try
        End Sub
    End Class
End Namespace
