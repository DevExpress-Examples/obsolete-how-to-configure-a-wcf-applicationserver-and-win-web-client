Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
Imports ClientServer_Wcf_StandardAuth.Module.BusinessObjects

Namespace ClientServer_Wcf_StandardAuth.Module.DatabaseUpdate
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()

            Dim administratorRole As SecuritySystemRole = CreateAdministratorRole()

            Dim userAdmin As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "Admin"))
            If userAdmin Is Nothing Then
                userAdmin = ObjectSpace.CreateObject(Of SecuritySystemUser)()
                userAdmin.UserName = "Admin"
                userAdmin.IsActive = True
                userAdmin.SetPassword("")
                userAdmin.Roles.Add(administratorRole)
                userAdmin.Save()
            End If

            Dim userRole As SecuritySystemRole = CreateUserRole()

            Dim userJohn As SecuritySystemUser = ObjectSpace.FindObject(Of SecuritySystemUser)(New BinaryOperator("UserName", "User"))
            If userJohn Is Nothing Then
                userJohn = ObjectSpace.CreateObject(Of SecuritySystemUser)()
                userJohn.UserName = "User"
                userJohn.IsActive = True
                userJohn.Roles.Add(userRole)
                userJohn.Save()
            End If

            ' Create test objects
            Dim testBO As TestBO = ObjectSpace.CreateObject(Of TestBO)()
            testBO.Name = "Available object"

            Dim testBO2 As TestBO = ObjectSpace.CreateObject(Of TestBO)()
            testBO2.Name = "Protected object"

            ObjectSpace.CommitChanges()
        End Sub
        Private Function CreateUserRole() As SecuritySystemRole
            Dim result As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Users"))
            If result Is Nothing Then
                result = ObjectSpace.CreateObject(Of SecuritySystemRole)()
                result.Name = "Users"

                result.SetTypePermissions(Of TestBO)(SecurityOperations.ReadOnlyAccess, SecuritySystemModifier.Allow)
                result.AddObjectAccessPermission(Of TestBO)("[Name] == 'Available object'", SecurityOperations.Write)

                result.Save()
            End If
            Return result
        End Function
        Private Function CreateAdministratorRole() As SecuritySystemRole
            Dim result As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Administrators"))
            If result Is Nothing Then
                result = ObjectSpace.CreateObject(Of SecuritySystemRole)()
                result.Name = "Administrators"
                result.IsAdministrative = True
                result.Save()
            End If
            Return result
        End Function
    End Class
End Namespace
