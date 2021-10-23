using Microsoft.EntityFrameworkCore;

namespace BookListSample_with_Rasor.Model
{
    public class ApplicationDbContext : DbContext
    {
        /* コンストラクタの引数に、Dbコンテキストを渡す */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        /* データベースに追加するBookモデルの情報を追加 */
        public DbSet<Book> Books { get; set; }
    }
}
