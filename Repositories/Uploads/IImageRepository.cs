using coreAPI.Models.Domain;

namespace coreAPI.Repositories.Uploads
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}