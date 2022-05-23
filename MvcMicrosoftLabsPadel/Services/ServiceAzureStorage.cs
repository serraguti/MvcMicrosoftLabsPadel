using Azure.Storage.Blobs;

namespace MvcMicrosoftLabsPadel.Services
{
    public class ServiceAzureStorage
    {
        //NECESITAMOS UN CLIENTE DE BLOBCONTAINER PARA ACCEDER 
        //A LA SUBIDA DE BLOBS
        private BlobContainerClient containerClient;

        public ServiceAzureStorage(IConfiguration configuration)
        {
            string azureKey =
                configuration.GetValue<string>("AzureStorage:AzureStorageKey");
            string containerName =
                configuration.GetValue<string>("AzureStorage:ContainerName");
            BlobServiceClient blobServiceClient =
                new BlobServiceClient(azureKey);
            this.containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        //EXISTEN VARIAS FORMAS PARA SUBIR FICHEROS BLOBS A AZURE
        //VAMOS A UTILIZAR LA FORMA QUE GENERE EXCEPCION SI YA EXISTE EL FICHERO.
        public async Task UploadBlobAsync(string blobName, Stream stream)
        {
            await this.containerClient.UploadBlobAsync(blobName, stream);
        }
    }
}
