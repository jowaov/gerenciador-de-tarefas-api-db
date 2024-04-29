using Microsoft.EntityFrameworkCore;
using TaskManager.Communication.Requests;

namespace TaskManager.Communication.SqlServer;
public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options)
    {

    }

    public DbSet<RequestTaskListJson> GerenciadorDeTarefas { get; set; }
}
