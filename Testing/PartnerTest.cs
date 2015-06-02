using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdooTypedClasses;
using System.Collections.Generic;
using OdooRpcWrapper;

namespace Testing
{
    [TestClass]
    public class PartnerTest
    {
        private OdooAPI _api;
        private IOdooRepository<Partner> _repository;
        
        [TestInitialize]
        public void Init()
        {
            OdooConnectionCredentials creds = new OdooConnectionCredentials(TestCreds.Link, TestCreds.Database, TestCreds.Username, TestCreds.Password);
            _api = new OdooAPI(creds, TestCreds.Proxy);
            _api.Login();
            _repository = new OdooRepository<Partner>(_api);
        }

        [TestMethod]
        public void TestCreate()
        {
            Partner _Newpartner = new Partner()
            {
                Name = String.Format("Test {0}", Guid.NewGuid())
            };

            _repository.Add(new List<Partner>() { _Newpartner });
            Assert.IsNotNull(_Newpartner.Id);
            Assert.AreNotEqual(_Newpartner.Id, 0);
        }

        [TestMethod]
        public void TestRead()
        {
            Partner _Newpartner = new Partner()
            {
                Name = String.Format("Test {0}", Guid.NewGuid())
            };
            _repository.Add(new List<Partner>() { _Newpartner });

            bool success = false;
            object[] filter = new object[1];
            filter[0] = new object[3] {"name", "=", _Newpartner.Name};

            IEnumerable<Partner> result = _repository.Read(filter);

            foreach(Partner partner in result)
            {
                if (partner.Name == _Newpartner.Name)
                {
                    success = true;
                }
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestWrite()
        {
            Partner _Newpartner = new Partner()
            {
                Name = String.Format("Test {0}", Guid.NewGuid())
            };
            _repository.Add(new List<Partner>() { _Newpartner });

            string street = "Gangs of new york";
            string name = _Newpartner.Name + "Last name";

            _Newpartner.Street = street;
            _Newpartner.Name = name;

            _repository.Update(new List<Partner>() { _Newpartner });

            bool success = false;
            object[] filter = new object[1];
            filter[0] = new object[3] { "id", "=", _Newpartner.Id };

            IEnumerable<Partner> result = _repository.Read(filter);

            foreach (Partner partner in result)
            {
                if (partner.Name == name && partner.Street == street)
                {
                    success = true;
                }
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestRemove()
        {
            Partner _Newpartner = new Partner()
            {
                Name = String.Format("Test {0}", Guid.NewGuid())
            };
            _repository.Add(new List<Partner>() { _Newpartner });

            _repository.Remove(new List<Partner>() { _Newpartner });

            bool success = true;
            object[] filter = new object[1];
            filter[0] = new object[3] { "name", "=", _Newpartner.Name};

            IEnumerable<Partner> result = _repository.Read(filter);

            foreach (Partner partner in result)
            {
                if (partner.Name == _Newpartner.Name)
                {
                    success = false;
                }
            }

            Assert.IsTrue(success);
        }
    }
}
