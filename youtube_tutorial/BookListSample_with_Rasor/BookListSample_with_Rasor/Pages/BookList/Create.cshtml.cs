using BookListSample_with_Rasor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace BookListSample_with_Rasor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        /* �R���X�g���N�^�Ƀf�[�^�x�[�X�̐ڑ��������Ă��� */
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /* Create �̃y�[�W�œ��͂��ꂽ���e���󂯎�邽�߁ABindProperty ���������Ă��� */
        [BindProperty]
        public Book Book { get; set; }


        public void OnGet()
        {
        }

        /* POST ���N�G�X�g���󂯂��Ƃ��̓�����AOnPost() ���\�b�h�ŋL�q����
         * ���̍ہA���͂��󂯎�邽�߂Ɉ�����Book �I�u�W�F�N�g�����Ă���
         */
        public async Task<IActionResult> OnPost()
        {
            /* �t�H�[������󂯎�������e���o���f�[�V�������� */
            if (ModelState.IsValid)
            {
                /* �ύX���L���[�Ƀv�b�V�� */
                await _db.Books.AddAsync(Book);
                /* �ۑ����� */
                await _db.SaveChangesAsync();
                /* ���̉�ʂɖ߂� */
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
