namespace SharedKernel.FileUploader.Models
{
    public class UploadBase64Model
    {
        public string Content { get; set; }
        public string Extension { get; set; }
        public string Folder { get; set; }
        public string FileName { get; set; }

        public string? UniqueName { get; set; }

        public string UploadPath { get; set; }
        public string ReturnDomain { get; set; }
    }
}
