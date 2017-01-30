using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DemoTests.Framework
{
    public static class CredentialsData
    {
        private static IEnumerable ms_Testdata;
        public static IEnumerable TestData
        {
            get { return ms_Testdata; }
            private set
            {
                if (value != null) ms_Testdata = value;
            }
        }

        private static Credential ms_ValidCredential;
        public static Credential ValidCredentials
        {
            get { return ms_ValidCredential; }
            private set
            {
                if (value != null) ms_ValidCredential = value;
            }
        }

        static CredentialsData()
        {
            var testdata = GetPasswords();
            ValidCredentials = testdata.FirstOrDefault();
            TestData = testdata;
        }
        private static IEnumerable<Credential> GetPasswords()
        {
            var doc = XDocument.Load("c:/project/passwords.xml");
            var query = from c in doc.Root.Descendants("Credential")
                select new Credential(c.Attribute("user").Value , c.Attribute("password").Value);
            return query;
        }
    
    }
}