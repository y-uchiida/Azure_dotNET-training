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

    }
}
