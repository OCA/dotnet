using CookComputing.XmlRpc;
using OdooRpcWrapper.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace OdooRpcWrapper
{
    public partial class OdooAPI : IOdooAPI
    {
        private const string STR_Id = "id";
        private readonly OdooConnectionCredentials _credentials;
        private readonly WebProxy _networkProxy;
        private readonly RemoteCertificateValidationCallback _certificateValidationFunction;
        private readonly List<Type> _validModels;
        private IOdooObjectRpc _objectRpc;
        private OdooLoginResult _loginResult = OdooLoginResult.Unknown;
        
        
        public OdooAPI(OdooConnectionCredentials credentials, WebProxy networkProxy = null,
            RemoteCertificateValidationCallback certificateValidationFunction = null)
        {
            _certificateValidationFunction = certificateValidationFunction;
            _networkProxy = networkProxy;
            _credentials = credentials;
            _validModels = new List<Type>();
        }

        public OdooLoginResult Login()
        {
            IOdooCommonRpc loginRpc = XmlRpcProxyGen.Create<IOdooCommonRpc>();
            loginRpc.Url = _credentials.CommonUrl;

            if (_networkProxy != null)
            {
                loginRpc.Proxy = _networkProxy;
            }

            if (_certificateValidationFunction != null)
            {
                ServicePointManager.ServerCertificateValidationCallback = _certificateValidationFunction;
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = StandardValidationFunction;
            }

            // Log in and get user id
            object result = null;
            try
            {
                result = loginRpc.login(_credentials.DbName, _credentials.DbUser, _credentials.DbPassword);

                if (result is bool)
                {
                    // Login failed!
                    _credentials.UserId = -1;
                    _loginResult = OdooLoginResult.InvalidCredentials;
                }
                else if (result is int)
                {
                    _credentials.UserId = (int)result;

                    // Create proxy for Object operations
                    _objectRpc = XmlRpcProxyGen.Create<IOdooObjectRpc>();
                    _objectRpc.Url = _credentials.ObjectUrl;

                    _loginResult = OdooLoginResult.Success;
                }
                else
                {
                    _loginResult = OdooLoginResult.Unknown;
                }
            }
            catch (UriFormatException uriEx)
            {
                Console.WriteLine(uriEx.StackTrace);
                _loginResult = OdooLoginResult.InvalidUri;
            }
            catch (XmlRpcInvalidXmlRpcException invEx)
            {
                Console.WriteLine(invEx.StackTrace);
                _loginResult = OdooLoginResult.InvalidDatabase;
            }
            catch(Exception other)
            {
                Console.WriteLine(other.StackTrace);
                _loginResult = OdooLoginResult.Unknown;
            }

            return _loginResult;
        }

        private bool StandardValidationFunction(object sender, X509Certificate certificate, 
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void CheckLogin()
        {
            if(_loginResult != OdooLoginResult.Success)
            {
                throw new OdooLoginException();
            }
        }

        private void CheckModel(Type type)
        {
            if (!_validModels.Contains(type))
            {
                if (ValidateModelType(type))
                {
                    _validModels.Add(type);
                }
            }
        }
        
        // TODO: cleanup/test! 
        public bool Execute_Workflow(string model, string action, int id)
        {
            CheckLogin();

            return _objectRpc.exec_workflow(_credentials.DbName, _credentials.UserId, 
                                            _credentials.DbPassword, model, action, id);
        }
    }
}
