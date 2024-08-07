﻿using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Services
{
    public class CloudStorage : ICloudStorage
    {
        private readonly StorageClient _storageClient;
        private const string BucketName = "dentistclinic-prn221.appspot.com";
        public CloudStorage(StorageClient storageClient)
        {
            _storageClient = storageClient;
        }

        public async Task<string> UploadFile(IFormFile file, string filePath)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var objectName = $"{filePath}/{file.FileName}";
            var blob = await _storageClient.UploadObjectAsync(BucketName, objectName, file.ContentType, stream);
            return $"https://storage.googleapis.com/{BucketName}/{objectName}";
        }
    }
}
