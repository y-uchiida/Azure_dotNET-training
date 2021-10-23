using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        /* コンストラクタにデータベースの接続情報を入れておく */
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /* Create のページで入力された内容を受け取るため、BindProperty 属性をつけておく */
        [BindProperty]
        public Book Book { get; set; }


        public void OnGet()
        {
        }

        /* POST リクエストを受けたときの動作を、OnPost() メソッドで記述する
         * その際、入力を受け取るために引数にBook オブジェクトを入れておく
         */
        public async Task<IActionResult> OnPost()
        {
            /* フォームから受け取った内容をバリデーションする */
            if (ModelState.IsValid)
            {
                /* 変更をキューにプッシュ */
                await _db.Books.AddAsync(Book);
                /* 保存処理 */
                await _db.SaveChangesAsync();
                /* 元の画面に戻る */
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
