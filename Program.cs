namespace AzureStroageManagedIdentitySample
{
    using Azure;
    using Azure.Identity;
    using Azure.Storage.Blobs;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            //https://docs.microsoft.com/en-us/azure/storage/common/storage-auth-aad-msi 

            // Please set the following environmment variables before trying this example for non-cert based authentication
            // AZURE_CLIENT_ID	The app ID for the service principal
            // AZURE_TENANT_ID The service principal's Azure AD tenant ID
            // AZURE_CLIENT_SECRET The password generated for the service principal

            // Uncomment the following and supply correct inputs to validate this.
            // CreateBlockBlobAsync("your account name", "your container name", "your blob name").Wait();
            
            // PLEASE READ THE FOLLOWING BEFORE YOU TRY IT.....
            // 

            CreateBlockBlobUsingPemCertAsync("skarridmsteststore", "certcontainer", "certblob", "72f988bf-86f1-41af-91ab-2d7cd011db47", "a8ca46b2-1778-4354-be4a-fa946c6517d2", @"C:\goodones\tmp4sssy5vj.pem").Wait();
        }

        async static Task CreateBlockBlobUsingPemCertAsync(
            string accountName, 
            string containerName, 
            string blobName,
            string tenantId,
            string clientId,
            string certPath)
        {
            // Construct the blob container endpoint from the arguments.
            string containerEndpoint = string.Format("https://{0}.blob.core.windows.net/{1}",
                                                        accountName,
                                                        containerName);

            ClientCertificateCredential clientCertificateCredential = new ClientCertificateCredential(tenantId, clientId, certPath);

            // Get a credential and create a client object for the blob container.
            BlobContainerClient containerClient = new BlobContainerClient(new Uri(containerEndpoint),
                                                                            clientCertificateCredential);

            try
            {
                // Create the container if it does not exist.
                await containerClient.CreateIfNotExistsAsync();

                // Upload text to a new block blob.
                string blobContents = "This is a block blob.";
                byte[] byteArray = Encoding.ASCII.GetBytes(blobContents);

                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    await containerClient.UploadBlobAsync(blobName, stream);
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        async static Task CreateBlockBlobAsync(string accountName, string containerName, string blobName)
        {
            // Construct the blob container endpoint from the arguments.
            string containerEndpoint = string.Format("https://{0}.blob.core.windows.net/{1}",
                                                        accountName,
                                                        containerName);

            // Get a credential and create a client object for the blob container.
            BlobContainerClient containerClient = new BlobContainerClient(new Uri(containerEndpoint),
                                                                            new DefaultAzureCredential());

            try
            {
                // Create the container if it does not exist.
                await containerClient.CreateIfNotExistsAsync();

                // Upload text to a new block blob.
                string blobContents = "This is a block blob.";
                byte[] byteArray = Encoding.ASCII.GetBytes(blobContents);

                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    await containerClient.UploadBlobAsync(blobName, stream);
                }
            }
            catch (RequestFailedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}
