using BookList_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookList_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; } 

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Book = new Book();
            if (id == null) {
                /* IDが渡されていない場合、新規追加の処理 */
                return View(Book);
            }

            /* ID が渡ってきた場合、更新処理 */
            Book = _db.Books.FirstOrDefault(u => u.Id == id);
            if (Book == null)
            {
                /* データベースから、対象のIDのレコードが見つからなかった == 不正なIDの入力 */
                return NotFound();
            }
            return View();
        }

        [HttpPost] /* POST リクエストを処理する */
        [ValidateAntiForgeryToken] /* CSRF のトークンを使う */
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _db.Books.Add(Book);
                }
                else { 
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Book);
        }

        /* API 呼び出し時の処理として設定(region) */
        #region API Calls

        /* Get リクエストに対するAPIとして設定 */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        /* Delete リクエストに対するAPIとして設定 */
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            /* 削除対象のBook オブジェクトを取得する */
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            /* 削除処理をキューに追加 */
            _db.Books.Remove(bookFromDb);
            /* 削除実行 */
            await _db.SaveChangesAsync();
            /* 削除処理が終わったらメッセージを返す */
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
