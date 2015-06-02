using System;
using System.Net;
using OdooRpcWrapper;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void TestLogin()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.Success);
        }

        [Test]
        public void TestLoginWithBadUsername()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, "qsmdlfkjqsmdflkjqsdm", TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidCredentials);
        }

        [Test]
        public void TestLoginWithBadPassword()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, "blablabla");
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidCredentials);
        }

        [Test]
        public void TestLoginWithBadLink()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials("total.jiberish", TestCreds.Database, "qsmdlfkjqsmdflkjqsdm", TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidUri);
        }

        [Test]
        public void TestLoginWithBadDatabase()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, "blablabla", TestCreds.Username, TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidDatabase);
        }
    }
}
