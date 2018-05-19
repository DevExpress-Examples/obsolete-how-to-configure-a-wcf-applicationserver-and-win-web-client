using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using System.Reflection;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security.Strategy;


namespace ClientServer_Wcf_StandardAuth.Module {
    public sealed partial class ClientServer_Wcf_StandardAuthModule : ModuleBase {
        public ClientServer_Wcf_StandardAuthModule() {
            InitializeComponent();
        }
        protected override IEnumerable<Type> GetDeclaredExportedTypes() {
            List<Type> result = new List<Type>(base.GetDeclaredExportedTypes());
            result.AddRange(new Type[] { typeof(SecuritySystemUser), typeof(SecuritySystemRole), typeof(Person) });
            return result;
        }
    }
}
