using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickMaster.Models;

namespace QuickMaster.Controllers
{
    public class BooksController : Controller
    {
        private readonly MyContext _context;

        /* コンストラクタで、コンテキスト（データベース接続情報）を注入 */
        public BooksController(MyContext context)
        {
            _context = context;
        }

        // GET: Books
        /* アクションメソッドにasync 修飾子をつけて、返り値の型をTask<IActionResult>にすることで、
         * データベースからのレコード取得を非同期的に行われるようにする
         */
        public async Task<IActionResult> Index()
        {
            /* データベースにアクセス氏、取得した結果をリストにする
             * await 式は、非同期処理を示すためのもので、メソッドに async 修飾子を付けておくことで
             * 利用できるようになる
             */
            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        /* URLからパラメータid を取得するため、アクションメソッドの引数に指定
         * なお、今回はURLパラメータを取得しているが、同様の方法でGETストリングやPOSTのデータも取得できる
         * GETストリングにもURLパラメータにも同名のものがあったらどうなるんだろう？？
         */
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /* 引数id を条件にして、データベース内を検索
             */
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                /* 該当するレコードがなかった場合エラー(404 Not Found) */
                return NotFound();
            }

            /* 取得したbook オブジェクトをviewに渡す */
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Publisher,Sample")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Publisher,Sample")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
