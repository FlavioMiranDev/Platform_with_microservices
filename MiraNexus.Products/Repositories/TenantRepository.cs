using MiraNexus.Products.Data;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Repositories;

public class TenantRepository(AppDbContext context) : Repository<Tenant>(context)
{
}
