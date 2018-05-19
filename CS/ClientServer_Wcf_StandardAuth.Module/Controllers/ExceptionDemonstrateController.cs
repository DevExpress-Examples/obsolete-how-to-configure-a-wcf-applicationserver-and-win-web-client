using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using ClientServer_Wcf_StandardAuth.Module.BusinessObjects;

namespace ClientServer_Wcf_StandardAuth.Module.Controllers {
    public class ExceptionDemonstrateController : ViewController {
        public ExceptionDemonstrateController() {
            TargetObjectType = typeof(TestBO);
            SimpleAction action = new SimpleAction(this, "Modify Name programmatically", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit);
            action.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            action.Execute += new SimpleActionExecuteEventHandler(action_Execute);
        }

        void action_Execute(object sender, SimpleActionExecuteEventArgs e) {
            try {
                ((TestBO)View.CurrentObject).Name = "Modify";
                View.ObjectSpace.CommitChanges();
            } catch (Exception) {
                View.ObjectSpace.Rollback();
                throw;
            }

        }
    }
}
