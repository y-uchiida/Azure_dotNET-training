using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        /* Delete ボタンクリック時に、asp-page-handler でDeleteを指定しているので、
         * OnPostDelete() が利用できる
         */
        public async Task<IActionResult> OnPostDelete(int id)
        {
            /* 削除対象のIDでデータベースからレコードを取得し、存在していれば削除する */
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                /* IDで検索した結果がnull の場合、削除するデータがない */
                return NotFound();
            }

            /* 対象レコードを削除して、元の画面に戻る */
            _db.Books.Remove(book); /* 削除処理をキューへ登録 */
            await _db.SaveChangesAsync(); /* データベースへの変更を反映 */

            return RedirectToAction("Index");
        }
    }
}
