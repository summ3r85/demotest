using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DemoTests.Framework
{
    public static class CredentialsData
    {
        public static IEnumerable testData
        {
            get
            {
                return GetPasswords();
            }

        }

        private static Credential m_LazyValidCred;
        public static Credential ValidCredentials
        {
            get { return m_LazyValidCred; }
            set { m_LazyValidCred = value; }
        }

        static CredentialsData()
        {
            ValidCredentials = GetPasswords().FirstOrDefault();
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