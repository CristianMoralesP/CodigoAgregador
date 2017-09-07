using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agregador
{
    public class Cuenta
    {
        public string id { get; set; }
        public string name { get; set; }
        public string foreignId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string roleType { get; set; }
        public string creation_time { get; set; }
        public string last_update_time { get; set; }
        public string last_login_time { get; set; }
        public string is_active { get; set; }
        public string simplification_mode { get; set; }
        public string use_external_login { get; set; }
        public string has_terms_flag { get; set; }
        public string onboarding_complited { get; set; }
        public string must_change_pwd { get; set; }
        public string company_name { get; set; }
    }
}