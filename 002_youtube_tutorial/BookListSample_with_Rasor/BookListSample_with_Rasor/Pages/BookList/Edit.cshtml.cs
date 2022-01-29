using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Pages.BookList
{
    public class EditModel : PageModel
    {
        /* �f�[�^�x�[�X�R���e�L�X�g��ێ����� */
        private readonly ApplicationDbContext _db;

        /* �R���X�g���N�^�Ńf�[�^�x�[�X�R���e�L�X�g���󂯎���ĕۑ�(DI) */
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /* �t�H�[������󂯎�����l�ŏ������s���̂ŁABindProperty�����Ă��� */
        [BindProperty]
        public Book Book { get; set; }



        public async Task OnGet(int id)
        {
            /* �f�[�^�x�[�X����A�w�肳�ꂽID�̃��R�[�h���擾����(async/await ��p�����񓯊��A�N�V����) */
            Book = await _db.Books.FindAsync(id);
        }

        /* POST ���N�G�X�g���󂯂��Ƃ��̓�����AOnPost() ���\�b�h�ŋL�q����
         * �������I�������ꗗ�y�[�W�ɑJ�ڂ����邽�߁A Page()��RedirectToPage()��return����
         * ���̂��߁ATask<IActionResult> �ŁA�^���K�肷��
         */
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                /* �X�V���郌�R�[�h���ABook���f���̃I�u�W�F�N�g�Ƃ��Ď擾 */
                var BookFromDb = await _db.Books.FindAsync(Book.Id);

                /* Name, ISBN, Author �̒l���A���ꂼ��t�H�[������󂯎�����l(Book �I�u�W�F�N�g)�ōX�V */
                BookFromDb.Name = Book.Name;
                BookFromDb.ISBN = Book.ISBN;
                BookFromDb.Author = Book.Author;

                /* �ύX�������e��ۑ� */
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
