using Microsoft.EntityFrameworkCore;

namespace BookList_MVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        /* コンストラクタを作成し、初期化時に外部からDbContext を受け取れるようにしておく */
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /* データベースとの接続するためのオブジェクトをメンバに持たせる */
        public DbSet<Book> Books {  get; set; }
    }
}
