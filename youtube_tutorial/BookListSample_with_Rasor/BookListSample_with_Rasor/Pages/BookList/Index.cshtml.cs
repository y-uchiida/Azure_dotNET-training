using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        /* ApplicationDbContextに、データベースにアクセスするための設定を格納しておく */
        private readonly ApplicationDbContext _db;

        /* コンストラクタを作成、引数としてデータベースの設定情報(ApplicationDbContext)を受け取る */
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /*
         * Books テーブルのレコードを取得してfetchするために、Enumerableをメンバとして作成している？
         * IEnumerable<T> インターフェイス
         * 指定した型のコレクションに対する単純な反復処理をサポートする列挙子を公開します
         * 型パラメーター T: 列挙するオブジェクトの型。この型パラメーターは共変です。 つまり、指定した型、または強い派生型のいずれかを使用することができます。
         */
        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Books.ToListAsync();
        }
    }
}
