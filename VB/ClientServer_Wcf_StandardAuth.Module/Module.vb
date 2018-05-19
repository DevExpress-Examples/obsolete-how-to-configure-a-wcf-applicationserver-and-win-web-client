Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security.Strategy


Namespace ClientServer_Wcf_StandardAuth.Module
    Public NotInheritable Partial Class ClientServer_Wcf_StandardAuthModule
        Inherits ModuleBase

        Public Sub New()
            InitializeComponent()
        End Sub
        Protected Overrides Function GetDeclaredExportedTypes() As IEnumerable(Of Type)
            Dim result As New List(Of Type)(MyBase.GetDeclaredExportedTypes())
            result.AddRange(New Type() { GetType(SecuritySystemUser), GetType(SecuritySystemRole), GetType(Person) })
            Return result
        End Function
    End Class
End Namespace
