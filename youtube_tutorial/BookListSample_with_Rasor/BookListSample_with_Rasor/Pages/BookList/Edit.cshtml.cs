using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Pages.BookList
{
    public class EditModel : PageModel
    {
        /* データベースコンテキストを保持する */
        private readonly ApplicationDbContext _db;

        /* コンストラクタでデータベースコンテキストを受け取って保存(DI) */
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /* フォームから受け取った値で処理を行うので、BindPropertyをつけておく */
        [BindProperty]
        public Book Book { get; set; }



        public async Task OnGet(int id)
        {
            /* データベースから、指定されたIDのレコードを取得する(async/await を用いた非同期アクション) */
            Book = await _db.Books.FindAsync(id);
        }

        /* POST リクエストを受けたときの動作を、OnPost() メソッドで記述する
         * 処理が終わったら一覧ページに遷移させるため、 Page()やRedirectToPage()をreturnする
         * そのため、Task<IActionResult> で、型を規定する
         */
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                /* 更新するレコードを、Bookモデルのオブジェクトとして取得 */
                var BookFromDb = await _db.Books.FindAsync(Book.Id);

                /* Name, ISBN, Author の値を、それぞれフォームから受け取った値(Book オブジェクト)で更新 */
                BookFromDb.Name = Book.Name;
                BookFromDb.ISBN = Book.ISBN;
                BookFromDb.Author = Book.Author;

                /* 変更した内容を保存 */
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
