using SharedKernel.Abstractions;

namespace SharedKernel.FileUploader.Models
{
    public interface IFileWriter
    {
        Task<BaseResponse<UploadResultModel>> Upload(UploadModel model);
        Task<BaseResponse<UploadResultModel>> Upload(UploadBase64Model model);
    }
}
