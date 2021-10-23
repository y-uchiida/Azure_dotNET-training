# memoranduma of youtube_tutotial
ハンズオン中に学んだこと、つまづいて調べたことなどのメモです

## ASP.NETの設定ファイルなど
- .csproj ... パッケージの依存関係などが入ってる
- Propaties/launchSettings.json ... 環境ごとの設定情報を記述しておき、開発環境やプロダクト環境など状況に合わせて切り替えできる
- appsettings.json ... アプリケーションの設定情報を記述する。appsettings.Development_._json のように、複数のファイルを作ることも可能

詳細は以下のページにもまとまっている
https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1

## ASP.NETのフォルダ構成とURLのマッピング
ここで作っているBookListアプリはMVCプロジェクトではないので、URLマッピングの情報が存在しないっぽい  
代わりにルールが決まっている
- ドメインルートへのアクセスは、`pages/index.cshtml` が開く
- ファイル名が指定されていない場合は、`index.cshtml` をインデックスページとして開く
- ドメインにパスをつけてアクセスされた場合は、`pages/` に該当するcshtmlファイルがあるかを探す
- 上記ルールでフォルダ内にcshtmlファイルが存在しない場合は、同名のフォルダを探す

pagesディレクトリから対象のファイルを探してパスを作るには、`asp-page` のtagヘルパーを利用する  
リンクを貼る場合はこんな感じ  
`<a asp-page="subfolder/file">example.com/subfolder/page</a>`  
この場合は、`Pages/subfolder/page.cshtml` または `Pages/subfolder/page/index.cshtml` へのリンクが表示される

## データベースにBook テーブル作るまでの手順
1. csproj にEntity Frameworkのパッケージを追加
　GUIから設定可能、バージョンはASP.NET Core に合わせる、今回は3.1.x
2. Modelフォルダを作って、`Book.cs` と`ApplicationDbContxt.cs` を作成
3. `appsettings.json` に、DBへの接続情報を追記。データベース名(`databese=...`)は任意に設定
4. `Startup.cs` の`ConfigureServices()` メソッドに、`services.AddDbContext`を追加
5. package manager console を開いて、マイグレーションを追加
　`PM> add-migration create_books_table`
6. `Migrations` フォルダに、指定した名前のマイグレーションファイルがタイムスタンプ付きで生成される
7. package manager console で、マイグレーションを実行
　データベースが存在しなければ作成処理を行う。その後、未実行のマイグレーションを処理する  
　`PM> update-database`

## データベースの更新
1. 対象のモデルファイルに、変更を加える  
カラムに当たるメンバ変数を、削除したり追加したり、変更したり
2. マイグレーションファイルを追加する  
  package manager console を開いて、マイグレーションを追加  
  例はISBNのカラムを追加する場合のコマンド例  
    ```
	PM> add-migration add_ISBN_to_Book_model
	```
3. マイグレーションファイルを確認
　指定した名称でマイグレーションファイルが生成されるので、内容を確認。
　意図通りの変更になっていればOK
4. マイグレーションを実行
　`update-database` で未実行のマイグレーションが動作する

## データベースからレコードを取り出すまで
1. Pagesフォルダ内に、対象となる`cshtml` ファイル(Razor Page)を作る(今回は`BookList/index.cshtml`)
Visual Studioの機能を使うとはやいので、`Pages` フォルダを右クリックしてAdd > Razor Page を選択  
2. `index.cshtml.cs` にコードを追加
    - `ApplicationDbContext` をプライベートメンバに含めて、データベース接続情報を扱えるようにする
    - ページのオブジェクトのコンストラクタを作成、上記のApplidationDbContext を引数で受け取る(DI)
    - データベースから取得したデータをループで扱えるように、`Inumerable<テーブルモデル>` をメンバ宣言する
    - `OnGet()` メソッドで、データベースからBookテーブルのレコードを取得する内容を追加
3. `index.cshtml` を編集、OnGet()メソッドで取得したデータベースからの応答を使って一覧を表示
    - `@if` ディレクティブで、レコードが存在しているか判定
    - 存在している場合、`@foreach` ディレクティブで各レコードごとの値を取り出して表示する
    - 存在していない場合、データがない旨をメッセージ表示

## モデルへのデータ追加
1. データ追加するためのフォームを表示するRazor Page ファイルを作成  
  今回は`Pages/BookList/Create.cshtml`
2. cshtml.cs を編集
    - ApplicationDbContext をプライベートメンバに追加
    - コンストラクタで、ApplicationDbContext を受け取るように設定(DI)
    - Bookオブジェクトをメンバに追加  
    フォームで入力された内容とBookオブジェクトの内容を関連付けるため、`BindPromerty` をつける
    - Postリクエスト時の処理として、`OnPost()` メソッドを追加
	`ModelState.IsValid` を使って、入力内容をバリデーション
	`_db.Books.AddAsync()` で、オブジェクトの変更をキューに追加する  
	`_db.SaveChangesdAsync()`で、保存処理を実行  
	成功したら`redirectToPage("Index")` で元の画面に戻る
3. .cshtmlファイルにフォームを記述
    - 通常通りのform に、タグヘルパーを組み合わせて利用する
	- 入力に問題があった場合、エラーメッセージを表示する`asp-validation-summary="ModelOnly"`
	- 各入力値のエラー情報を表示する`asp-validation-for="[column_name]"` など