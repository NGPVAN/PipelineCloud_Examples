# PipelineCloud_Examples

PURPOSE
The sample code provided in this package shows how to connect to an Azure SQL Database using Azure Active Directory application token authentication for Python developers.

CONTENTS
1) Python\dbconnection.py is sample Python code.
2) Python\PipelineCloudPythonExample_privatenopass.pem is a private certificate.  This will be provided to you.
3) Python\PipelineCloudPythonExample_public.cer is a public certificate.   This will be provided to you.
4) Python\requirements.txt is a list of the required code libraries used in dbconnection.py.


ABOUT Python\dbconnection.py
The sample code includes a mix of variable and static parameters.

Static
  - authority_host_uri = 'https://login.microsoftonline.com'
  - tenant = '798d7834-694a-41b4-b6cb-e5448f079f6b'
  - Server=esv30ddbms001.database.windows.net
  - resource_uri = 'https://database.windows.net/'

Variable
  - application_id = '8cbd0efa-37d2-4e39-9fad-e8757079b23c'.  This will be provided to you.
  - certs = pem.parse_file("PipelineCloudPythonExample_privatenopass.pem").  The private certificate will be provided to you; this must match the file name.
  - client_cert_thumbprint = 'E21FC4884D4893CD3DDB9838BBA8842E7A4B9957'.  The public certificate will be provided to you; the thumbprint is inside of it.
  - Database=TestDb01.   This will be provided to you.




