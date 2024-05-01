using SharedKernel.Abstractions;
using SharedKernel.Helpers;

namespace SharedKernel.FileUploader.Models
{
    public class FileWriter : IFileWriter
    {
        public async Task<BaseResponse<UploadResultModel>> Upload(UploadModel model)
        {
            var extension = "." + model.File.FileName.Split('.')[model.File.FileName.Split('.').Length - 1];

            var folderPath = Path.Combine(model.UploadPath, model.Folder);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = !string.IsNullOrEmpty(model.FileName) ? $"{model.FileName}-{Guid.NewGuid()}{extension}" : $"{Guid.NewGuid()}{extension}";
            string path = Path.Combine(folderPath, fileName);

            using (var stream = File.Create(path))
            {
                await model.File.CopyToAsync(stream);
            }

            var docUrl = model.ReturnDomain + model.Folder.Replace("\\", "/") + "/" + fileName;

            var result = new UploadResultModel
            {
                Url = docUrl
            };

            return new BaseResponse<UploadResultModel>(result);
        }

        public async Task<BaseResponse<UploadResultModel>> Upload(UploadBase64Model model)
        {
            var file = FileHelper.Base64ToFormFile(model.Content, model.FileName, model.Extension);

            return await Upload(new UploadModel
            {
                File = file,
                FileName = model.FileName,
                Folder = model.Folder,
                ReturnDomain = model.ReturnDomain,
                UploadPath = model.UploadPath
            });
        }
    }
}
