using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Controllers
{

    /* コントローラで処理するURLのルートを設定 */
    [Route("api/Book")]

    /* BookControllerを、APIコントローラとして設定 */
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

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
            return Json(new {success = true, message = "Delete successful"});
        }
    }
}
