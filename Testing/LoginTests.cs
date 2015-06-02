using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using OdooRpcWrapper;

namespace Testing
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void TestLogin()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.Success);
        }

        [TestMethod]
        public void TestLoginWithBadUsername()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, "qsmdlfkjqsmdflkjqsdm", TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidCredentials);
        }

        [TestMethod]
        public void TestLoginWithBadPassword()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, "blablabla");
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidCredentials);
        }

        [TestMethod]
        public void TestLoginWithBadLink()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials("total.jiberish", TestCreds.Database, "qsmdlfkjqsmdflkjqsdm", TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidUri);
        }

        [TestMethod]
        public void TestLoginWithBadDatabase()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, "blablabla", TestCreds.Username, TestCreds.Password);
            OdooAPI api = new OdooAPI(creds, TestCreds.Proxy);
            Assert.AreEqual(api.Login(), OdooLoginResult.InvalidDatabase);
        }
    }
}
