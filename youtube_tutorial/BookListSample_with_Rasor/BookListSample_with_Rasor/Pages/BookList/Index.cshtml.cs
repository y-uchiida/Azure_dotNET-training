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
        /* ApplicationDbContext�ɁA�f�[�^�x�[�X�ɃA�N�Z�X���邽�߂̐ݒ���i�[���Ă��� */
        private readonly ApplicationDbContext _db;

        /* �R���X�g���N�^���쐬�A�����Ƃ��ăf�[�^�x�[�X�̐ݒ���(ApplicationDbContext)���󂯎�� */
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        /*
         * Books �e�[�u���̃��R�[�h���擾����fetch���邽�߂ɁAEnumerable�������o�Ƃ��č쐬���Ă���H
         * IEnumerable<T> �C���^�[�t�F�C�X
         * �w�肵���^�̃R���N�V�����ɑ΂���P���Ȕ����������T�|�[�g����񋓎q�����J���܂�
         * �^�p�����[�^�[ T: �񋓂���I�u�W�F�N�g�̌^�B���̌^�p�����[�^�[�͋��ςł��B �܂�A�w�肵���^�A�܂��͋����h���^�̂����ꂩ���g�p���邱�Ƃ��ł��܂��B
         */
        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _db.Books.ToListAsync();
        }
    }
}
