using System;
using OdooTypedClasses;
using System.Collections.Generic;
using OdooRpcWrapper;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class UserTest
    {
        private OdooAPI _api;
        private OdooRepository<Bank> _bankRepo;
        private OdooRepository<Partner> _partnerRepo;
        private IOdooRepository<User> _repository;
        private System.Net.WebProxy _proxy;

        [TestFixtureSetUp]
        public void Init()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, 
                TestCreds.Database, TestCreds.Username, TestCreds.Password);

            _proxy = TestCreds.Proxy;
            _api = new OdooAPI(creds, _proxy);
            _api.Login();
            _repository = new OdooRepository<User>(_api);
            _partnerRepo = new OdooRepository<Partner>(_api);
            _bankRepo = new OdooRepository<Bank>(_api);
        }

        [Test]
        public void TestRead()
        {
            bool success = false;
            object[] filter = new object[1];
            filter[0] = new object[3] {"login", "=", "admin"};

            IList<User> result = _repository.Read(filter);
            IList<Partner> partners = new List<Partner>();

            foreach(User user in result)
            {
                partners.Add(user.Partner);
            }

            _api.Read<Partner>(partners);
            foreach(Partner p in partners)
            {
                _api.Read<Bank>(p.Banks);
            }
            
            //Assert.IsTrue(success);
        }

        [Test]
        public void TestPartnerAll()
        {
            _partnerRepo.ReadAll();
        }

        [Test]
        public void GetAllPartners()
        {
            // Create Credentials
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link,
                TestCreds.Database, TestCreds.Username, TestCreds.Password);

            // Create API and Log in
            IOdooAPI api = new OdooAPI(creds);
            api.Login();

            // Create Partner repo
            IOdooRepository<Partner> partnerRepo = new OdooRepository<Partner>(api);
            
            // Read all
            IList<Partner> partners = partnerRepo.ReadAll();

            // Read less
            object[] filter = new object[1];
            filter[0] = new object[3] { "customer", "=", true };
            IList<Partner> customers = partnerRepo.Read(filter);


        }
    }
}
