Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports ClientServer_Wcf_StandardAuth.Module.BusinessObjects

Namespace ClientServer_Wcf_StandardAuth.Module.Controllers
    Public Class ExceptionDemonstrateController
        Inherits ViewController

        Public Sub New()
            TargetObjectType = GetType(TestBO)
            Dim action As New SimpleAction(Me, "Modify Name programmatically", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
            AddHandler action.Execute, AddressOf action_Execute
        End Sub

        Private Sub action_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
            Try
                CType(View.CurrentObject, TestBO).Name = "Modify"
                View.ObjectSpace.CommitChanges()
            Catch e1 As Exception
                View.ObjectSpace.Rollback()
                Throw
            End Try

        End Sub
    End Class
End Namespace
