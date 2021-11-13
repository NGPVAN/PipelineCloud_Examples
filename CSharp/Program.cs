using System;

namespace CSharp
{
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System.Data.SqlClient;


    class Program
    {
        const string applicationIdentity = "<insert application key here>";
        const string applicationCertificateThumbprint = "<insert certificate thumbprint here>";
        static void Main(string[] args)
        {
            //Console.WriteLine("Starting .net Core Pipeline Cloud Example");
            #region Process public and private in separate files below .net 5
            //string[] privateKeyFileContents = File.ReadAllLines("PipelineCloudCSharpExample_privatenopass.pem");
            //Range range = 1..((privateKeyFileContents.Length) - 1);
            //string pemData = System.String.Join("", privateKeyFileContents[range]);
            //Console.WriteLine(pemData + Environment.NewLine);
            //var privateKeyBytes = Convert.FromBase64String(pemData);
            //var key = RSA.Create();
            //key.ImportPkcs8PrivateKey(privateKeyBytes, out _);
            //Console.WriteLine((key.GetType()));
            //byte[] certificateKeyFileContents = File.ReadAllBytes("PipelineCloudCSharpExample_public.cer");
            //var authCertificate = new X509Certificate2(certificateKeyFileContents);
            //var authCertificateWithPrivateKey = authCertificate.CopyWithPrivateKey(key);
            #endregion
            #region Process public and private in separate files .net 5+
            //var authCertificateWithPrivateKey = X509Certificate2.CreateFromPemFile("PipelineCloudCSharpExample_public.pem", "PipelineCloudCSharpExample_privatenopass.pem");
            #endregion
            
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, certThumbPrint, false);
            certStore.Close();
            var authCertificateWithPrivateKey = certCollection[0];

            var clientCredential = new ClientAssertionCertificate(applicationIdentity, authCertificateWithPrivateKey);

            var authContext = new AuthenticationContext("https://login.windows.net/798d7834-694a-41b4-b6cb-e5448f079f6b");
            var authResult = authContext.AcquireTokenAsync("https://database.windows.net/", clientCredential);
            int x = 0;
            while (x < 20 && (authResult.Result == null))
            {
                Console.WriteLine(x);
                System.Threading.Thread.Sleep(1000);
                x++;
            }
            Console.WriteLine(authResult.Result.AccessToken);

            var sqlConn = new System.Data.SqlClient.SqlConnection();
            sqlConn.ConnectionString = "Data Source=esv30ddbms001.database.windows.net;Initial Catalog=VAN_Pipeline_VQA_DevBasic;Connect Timeout=30";
            sqlConn.AccessToken = authResult.Result.AccessToken;
            sqlConn.Open();
            string sqlQuery = "select @@version";
            var command = new SqlCommand(sqlQuery, sqlConn);
            var result = command.ExecuteScalar();
            sqlConn.Close();
            Console.WriteLine(result);


        }
    }
}
