# PipelineCloud_Examples


- [PipelineCloud_Examples](#pipelinecloud_examples)
  - [Python](#python)
    - [CONTENTS](#contents)
    - [ABOUT](#about)
      - [Python\dbconnection.py](#pythondbconnectionpy)
      - [Static](#static)
      - [Variable](#variable)

*****
The sample code provided in this package shows how to connect to an Azure SQL Database using Azure Active Directory application token authentication for Python developers.

## Python
### CONTENTS

1. ExampleSingleUseSecretContent.txt is an example of the information recweieved through single use secret when the account is created.
2. Python\dbconnection.py is sample Python code.
3. PipelineCloudPythonExample_full.pem is a constructed certificate in pem format created from the information sent by a single use secret when the account was setup.  The command to convert the certificate to the proper form for use with this example is:

   ```openssl rsa -in PipelineCloudPythonExample_full.pem -out PipelineCloudPythonExample_privatenopass.pem```
4. Python\PipelineCloudPythonExample_privatenopass.pem is a private certificate.
5. Python\requirements.txt is a list of the required code libraries used in dbconnection.py.


### ABOUT 
#### Python\dbconnection.py
The sample code includes a mix of variable and static parameters.

#### Static
  * ```authority_host_uri = 'https://login.microsoftonline.com'```
  * ```tenant = '798d7834-694a-41b4-b6cb-e5448f079f6b'```
  * ```Server=esv30ddbms001.database.windows.net```
  * ```resource_uri = 'https://database.windows.net/'```

#### Variable
  * ```application_id = '8cbd0efa-37d2-4e39-9fad-e8757079b23c'```  This will be provided to you.
  * ```certs = pem.parse_file("PipelineCloudPythonExample_privatenopass.pem")```  The private certificate will be provided to you; this must match the file name.
  * ```client_cert_thumbprint = 'D5C67AFFACF1B5BD2087500B777F97F05E7C6FA0'```  The private certificate will be provided to you; the thumbprint is inside of it.
  * ```Database=PipelineCloudExampleDatabase```   This will be provided to you.




