using System.Threading.Tasks;
using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //create
                return Page();
            }

            //update
            Book = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {

                /* ID��0... �ǉ��f�[�^�̏ꍇ */
                if (Book.Id == 0)
                {
                    _db.Books.Add(Book);
                }
                /* 0�ȊO�̒l... �����f�[�^�̏ꍇ */
                else
                {
                    _db.Books.Update(Book);
                }

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

    }
}