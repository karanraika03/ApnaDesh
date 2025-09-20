using WebApiDomain;

namespace WebApiData.RoleRepo;

public interface IRoleRepository
{
    Task<Role> GetById(int id);
}
