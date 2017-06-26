using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escc.Umbraco.MediaFileSavedIncorrectlyChecker
{
    internal class AzureBlobStorageFileSystem : IFileSystem
    {
        private readonly CloudBlobContainer _container;
        private readonly string _connectionString;

        public AzureBlobStorageFileSystem(string connectionString)
        {
            _connectionString = connectionString;
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            _container = cloudBlobClient.GetContainerReference("media");
        }

        public IEnumerable<string> GetFiles(Folder folder)
        {
            if (_container == null) return new string[0];

            IEnumerable<IListBlobItem> blobs = _container.ListBlobs(folder.Path, true);

            var files = new List<string>();
            foreach (var blob in blobs)
            {
                files.Add(blob.Uri.AbsolutePath);
            }

            return files;
        }

    public IEnumerable<Folder> GetFolders()
        {
            if (_container == null) return new Folder[0];

            CloudBlobDirectory directory = _container.GetDirectoryReference(String.Empty);

            IEnumerable<IListBlobItem> blobs = directory.ListBlobs().Where(blob => blob is CloudBlobDirectory).ToList();

            // Always get last segment for media sub folder simulation. E.g 1001, 1002
            return blobs.Cast<CloudBlobDirectory>().Select(cd => new Folder() { Path = cd.Prefix.TrimEnd('/') });
        }
    }
}