using Microsoft.EntityFrameworkCore;

namespace MyTodo.Data;
public class PracticeContext : DbContext
{
    public DbSet<TaskTemplate> Todo { get; set; }
    public PracticeContext(DbContextOptions<PracticeContext> options) : base(options)
    {
    }
}
