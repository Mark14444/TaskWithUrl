
using Inforce.Service.Dto.UrlDtos;

namespace Inforce.Service.Services.Interfaces
{
    public interface IUrlService
    {
        Task<UrlDto> GetUrlByShortName(string shortUrl); 
        Task AddAsync(CreateShortUrlDto entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UrlDto>> GetAllUrlsAsync();
        Task<UrlDto> GetUrlByIdAsync(int id);
    }
}
