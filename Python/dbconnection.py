# https://docs.microsoft.com/en-us/azure/developer/python/configure-local-development-environment?tabs=cmd

## AADTokenCredentials for multi-factor authentication
from msrestazure.azure_active_directory import AADTokenCredentials
import adal, uuid, time, datetime, pem, pyodbc, struct

def authenticate_client_cert():
    """
    Authenticate using service principal w/ cert.
    """
    authority_host_uri = 'https://login.microsoftonline.com'
    tenant = '798d7834-694a-41b4-b6cb-e5448f079f6b'
    authority_uri = authority_host_uri + '/' + tenant
    #resource_uri = 'https://management.core.windows.net/'
    resource_uri = 'https://database.windows.net/'
    application_id = '8cbd0efa-37d2-4e39-9fad-e8757079b23c'
    certs = pem.parse_file("PipelineCloudPythonExample_privatenopass.pem")
    client_cert = str(certs[0])
    #print(str(certs[0]))
    client_cert_thumbprint = 'D5C67AFFACF1B5BD2087500B777F97F05E7C6FA0'

    context = adal.AuthenticationContext(authority_uri, api_version=None)

    mgmt_token = context.acquire_token_with_client_certificate(resource_uri, application_id, client_cert, client_cert_thumbprint)
    credentials = AADTokenCredentials(mgmt_token, application_id)
    #print(mgmt_token)
    return mgmt_token
    
    #return credentials
#print(dir(authenticate_client_cert().token.values))
authDict = authenticate_client_cert()
#print(a["accessToken"])
print("Your Token Will Expire on: " + authDict["expiresOn"])
print("Your Token Expires in: " + str(authDict["expiresIn"]))
rawToken = authDict["accessToken"]
token = bytearray(rawToken.encode());
exptoken = b'';
for i in token:
    exptoken += bytes({i});
    exptoken += bytes(1);

tokenstruct = struct.pack('=i', len(exptoken)) + exptoken;
connstr = 'Driver={ODBC Driver 17 for SQL Server};Server=esv30ddbms001.database.windows.net;Database=PipelineCloudExampleDatabase;'
conn = pyodbc.connect(connstr, attrs_before = { 1256:tokenstruct });
cursor = conn.cursor()
cursor.execute("SELECT @@version")
print((cursor.fetchone())[0])


