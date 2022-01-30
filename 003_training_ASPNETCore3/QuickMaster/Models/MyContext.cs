/* nuget パッケージマネージャからEntityFrameworkをインストールしておく（v3系） */
using Microsoft.EntityFrameworkCore;

namespace QuickMaster.Models
{
    /* DbContext を継承して、Entityframeworkの機能を利用できるようにする */
    public class MyContext : DbContext
    {
        /* コンストラクタ、引数としてcontextに対する設定情報を受け取る */
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        {
        }

        /* モデルに対するアクセサメソッドを追加 */
        public DbSet<Book> Book { get; set; }

        /* Book モデル以外にも利用するモデルが有る場合、同じ用にアクセサを追加する */
    }
}
