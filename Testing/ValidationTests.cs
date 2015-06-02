using System;
using System.Net;
using OdooTypedClasses;
using Testing.TypedTestClasses;
using OdooRpcWrapper.Exceptions;
using System.Collections.Generic;
using OdooRpcWrapper;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class ValidationTests
    {
        private OdooAPI _api;
        private OdooRepository<BadUser1> _UsersRepo;
        private WebProxy _proxy;

        [TestFixtureSetUp]
        public void Init()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, TestCreds.Password);
            _proxy = TestCreds.Proxy;
            _api = new OdooAPI(creds, _proxy);
            _api.Login();
            _UsersRepo = new OdooRepository<BadUser1>(_api);
        }

        [Test]
        public void GoodModelTest()
        {
            // Check if missing OdooModel attribute throws exception

            try
            {
                _api.ValidateModelType(typeof(GoodUser));
            }
            catch (MissingOdooObjectAttributeException ex)
            {
                Assert.Fail();
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void MissingModelNameTest()
        {
            // Check if missing OdooModel attribute throws exception

            try
            {
                _api.ValidateModelType(typeof(BadUser1));
                Assert.Fail();
            }
            catch (MissingOdooObjectAttributeException ex)
            {
                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void InvalidModelNameTest()
        {
            // Check if bad OdooModel attribute throws exception

            try
            {
                _api.ValidateModelType(typeof(BadUser2));
                Assert.Fail();
            }
            catch (OdooModelNotFoundException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void InvalidFieldNameTest()
        {
            // Check if bad field attribute throws exception

            try
            {
                _api.ValidateModelType(typeof(BadUser3));
                Assert.Fail();
            }
            catch (OdooFieldNotFoundException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void IdFieldMissing()
        {
        }

        [Test]
        public void RepoTest()
        {
            try
            {
                IList<BadUser1> users = _UsersRepo.ReadAll();
                Assert.Fail();
            }
            catch (MissingOdooObjectAttributeException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
