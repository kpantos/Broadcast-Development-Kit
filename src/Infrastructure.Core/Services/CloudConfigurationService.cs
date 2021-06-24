using Application.Common.Config;
using Application.Interfaces.Common;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Core.Services
{
    public class CloudConfigurationService : ICloudConfigurationService
    {
        private readonly CloudConfigSettings configSettings;

        public CloudConfigurationService(CloudConfigSettings configSettings)
        {
            this.configSettings = configSettings;
        }
        public Stream GetCertificate()
        {
            var certificateUri = GetFileBlobStorageUri(this.configSettings.CertificateFileName);
            var certificateStream = GetFileFromBlobStorageAsStreamtAsync(certificateUri).Result;

            return certificateStream;
        }
        public async Task<Stream> GetCertificateAsync()
        {
            var certificateUri = GetFileBlobStorageUri(this.configSettings.CertificateFileName);
            var certificateStream = await GetFileFromBlobStorageAsStreamtAsync(certificateUri);

            return certificateStream;
        }

        public Stream GetAppSettingsAsStream()
        {
            var appSettingsUri = GetFileBlobStorageUri(this.configSettings.AppSettingsFileName);
            var appSettingsStream = GetFileFromBlobStorageAsStreamtAsync(appSettingsUri).Result;

            return appSettingsStream;
        }

        public async Task<Stream> GetAppSettingsAsStreamAsync()
        {
            var appSettingsUri = GetFileBlobStorageUri(this.configSettings.AppSettingsFileName);
            var appSettingsStream = await GetFileFromBlobStorageAsStreamtAsync(appSettingsUri);

            return appSettingsStream;
        }

        private static async Task<Stream> GetFileFromBlobStorageAsStreamtAsync(string uri)
        {
            using (var client = new HttpClient())
            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                var response = await client.SendAsync(httpRequestMessage);

                var responseStream = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // TODO: Handle this scenario
                }

                return responseStream;
            }
        }
        private string GetFileBlobStorageUri(string fileName)
        {
            var uri = $"https://{this.configSettings.StorageAccountName}.blob.core.windows.net/{this.configSettings.BlobContainerName}/{fileName}{this.configSettings.SasToken}";
            return uri;
        }
    }
}
