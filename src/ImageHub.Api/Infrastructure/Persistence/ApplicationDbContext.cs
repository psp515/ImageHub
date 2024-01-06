using Microsoft.EntityFrameworkCore;

namespace ImageHub.Api.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
}
