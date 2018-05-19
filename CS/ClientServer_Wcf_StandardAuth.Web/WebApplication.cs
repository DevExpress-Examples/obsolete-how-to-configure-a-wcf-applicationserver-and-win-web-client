using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;

namespace ClientServer_Wcf_StandardAuth.Web
{
    public partial class ClientServer_Wcf_StandardAuthAspNetApplication : WebApplication
    {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule module3;
        private System.Data.SqlClient.SqlConnection sqlConnection1;

        public ClientServer_Wcf_StandardAuthAspNetApplication()
        {
            InitializeComponent();
        }
        private void ClientServer_Wcf_StandardAuthAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new ClientServer_Wcf_StandardAuth.Module.ClientServer_Wcf_StandardAuthModule();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = @"Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=ClientServer_Wcf_StandardAuth";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            // 
            // ClientServer_Wcf_StandardAuthAspNetApplication
            // 
            this.ApplicationName = "ClientServer_Wcf_StandardAuth";
            this.Connection = this.sqlConnection1;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.module3);

            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.ClientServer_Wcf_StandardAuthAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
