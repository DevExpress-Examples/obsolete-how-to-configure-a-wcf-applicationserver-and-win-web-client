using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using ClientServer_Wcf_StandardAuth.Module.BusinessObjects;

namespace ClientServer_Wcf_StandardAuth.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            SecuritySystemRole administratorRole = CreateAdministratorRole();

            SecuritySystemUser userAdmin = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<SecuritySystemUser>();
                userAdmin.UserName = "Admin";
                userAdmin.IsActive = true;
                userAdmin.SetPassword("");
                userAdmin.Roles.Add(administratorRole);
                userAdmin.Save();
            }

            SecuritySystemRole userRole = CreateUserRole();

            SecuritySystemUser userJohn = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "User"));
            if(userJohn == null) {
                userJohn = ObjectSpace.CreateObject<SecuritySystemUser>();
                userJohn.UserName = "User";
                userJohn.IsActive = true;
                userJohn.Roles.Add(userRole);
                userJohn.Save();
            }

            // Create test objects
            TestBO testBO = ObjectSpace.CreateObject<TestBO>();
            testBO.Name = "Available object";

            TestBO testBO2 = ObjectSpace.CreateObject<TestBO>();
            testBO2.Name = "Protected object";

            ObjectSpace.CommitChanges();
        }
        private SecuritySystemRole CreateUserRole() {
            SecuritySystemRole result = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Users"));
            if(result == null) {
                result = ObjectSpace.CreateObject<SecuritySystemRole>();
                result.Name = "Users";

                result.SetTypePermissions<TestBO>(SecurityOperations.ReadOnlyAccess, SecuritySystemModifier.Allow);
                result.AddObjectAccessPermission<TestBO>("[Name] == 'Available object'", SecurityOperations.Write);
                
                result.Save();
            }
            return result;
        }
        private SecuritySystemRole CreateAdministratorRole() {
            SecuritySystemRole result = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", "Administrators"));
            if(result == null) {
                result = ObjectSpace.CreateObject<SecuritySystemRole>();
                result.Name = "Administrators";
                result.IsAdministrative = true;
                result.Save();
            }
            return result;
        }
    }
}
