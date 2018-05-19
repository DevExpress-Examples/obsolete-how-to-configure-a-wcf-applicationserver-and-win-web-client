using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace ClientServer_Wcf_StandardAuth.Module.BusinessObjects {
    [DefaultClassOptions]
    public class TestBO : XPObject {
        public TestBO(Session session) : base(session) { }
        public string Name {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue<string>("Name", value); }
        }
    }

    [DefaultClassOptions]
    public class FullAccessBO : XPObject {
        public FullAccessBO(Session session) : base(session) { }
        public string Name {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue<string>("Name", value); }
        }
    }

    [DefaultClassOptions]
    public class InaccessibleByUserBO : XPObject {
        public InaccessibleByUserBO(Session session) : base(session) { }
        public string Name {
            get { return GetPropertyValue<string>("Name"); }
            set { SetPropertyValue<string>("Name", value); }
        }
    }
}
