using NXWalks.API.Models.Domains;

namespace NXWalks.API.Repositories
{
    public interface FileUploadRepository
    {
        Task<Image> UploadFile(Image image);
    }
}
