# Accessing Azure Storage using System Managed Identity and Service Principal
This solution has 2 methods in program.cs:

# CreateBlockBlobAsync

1. Make sure you set service principal to the Azure blob as described in this Authorize access to data with a managed identity - Azure Storage | Microsoft Docs
 
Command from the above link: az ad sp create-for-rbac \
    --name <service-principal> \
    --role "Storage Blob Data Contributor" \
    --scopes /subscriptions/<subscription>/resourceGroups/<resource-group>/providers/Microsoft.Storage/storageAccounts/<storage-account>

2. Make sure you set environmental variables as described in the link.

Command from the above link: az ad sp create-for-rbac \
    --name <service-principal> \
    --role "Storage Blob Data Contributor" \
    --scopes /subscriptions/<subscription>/resourceGroups/<resource-group>/providers/Microsoft.Storage/storageAccounts/<storage-account>

3. uncomment method in the solution and provide required params to create a sample blob in the azure storage.

# CreateBlockBlobUsingPemCertAsync
For this we are going to Service Principal using self-signed certificate.
1. Open cloud shell https://ms.portal.azure.com/#cloudshell/
2. Run the following command which will generate Service principal with certificate:
az ad sp create-for-rbac --name ServicePrincipalName --create-cert
More details can be found here: https://docs.microsoft.com/en-us/cli/azure/create-an-azure-service-principal-azure-cli
3. In azure portal set permission to the azure storage using the above service principal. Please add roles “Contributor” and “Storage Blob Data Contributor”
4. uncomment method in the solution and provide required params to create a sample blob in the azure storage.