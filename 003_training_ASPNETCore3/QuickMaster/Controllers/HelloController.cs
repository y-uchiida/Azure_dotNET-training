using Microsoft.AspNetCore.Mvc;

namespace QuickMaster.Controllers
{
    /* コントローラクラスを継承したHelloControllerクラスを作成 */
    public class HelloController : Controller
    {
        /* index アクションメソッド
         * /hello, または /hello/index で表示される
         * デフォルト設定がStartup.csのConfigureに記述されている
         * 例外的なルーティングを設定したい場合はこれに追記をする
         * アクションメソッドの返り値となるIActionResult オブジェクトには、
         * レスポンスデータとして含めるべき情報をまとめて格納できる
         * IActionResultを返すメソッドもいろいろあり、用途に応じて使い分けることで
         * 適切なレスポンスデータをかんたんに生成できる
         */
        public IActionResult Index()
        {
            /* IActionResult を実装したContentResultを返す Content() メソッド
             * ContentResult はレスポンスデータに指定したテキストを出力する
             */
            return Content("<h1>hello, ASP.Net Core 3!!</h1>");
        }

        public IActionResult Greet()
        {
            /* ViewBag オブジェクトは、Viewに引き渡されるデータを格納することができるオブジェクト
             * ViewBag.[変数名] = [値] の形式で記述できる
             */
            ViewBag.Message = "Hello from Action Method to Razor template!";

            /* View() メソッドは、指定したビュー（Razor テンプレートなど）を出力する
             * 引数を指定しなかった場合は、そのコントローラとアクションメソッド名から該当するテンプレートを呼び出す
             * /Views/[コントローラー名]/[アクション名].cshtml
             * 今回であれば、 /Views/Hello/Greet.cshtml がデフォルトで呼び出される
             */
            return View();
        }

    }
}
