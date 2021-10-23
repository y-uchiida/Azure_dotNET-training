# memoranduma of youtube_tutotial
ハンズオン中に学んだこと、つまづいて調べたことなどのメモです

## ASP.NETの設定ファイルなど
- .csproj ... パッケージの依存関係などが入ってる
- Propaties/launchSettings.json ... 環境ごとの設定情報を記述しておき、開発環境やプロダクト環境など状況に合わせて切り替えできる
- appsettings.json ... アプリケーションの設定情報を記述する。appsettings.Development_._json のように、複数のファイルを作ることも可能

詳細は以下のページにもまとまっている
https://docs.microsoft.com/ja-jp/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1

## データベースにBook テーブル作るまでの手順
1. csproj にEntity Frameworkのパッケージを追加(GUIから設定可能、バージョンはASP.NET Core に合わせる、今回は3.1.x)
2. Modelフォルダを作って、`Book.cs` と`ApplicationDbContxt.cs` を作成
3. `appsettings.json` に、DBへの接続情報を追記。データベース名(`databese=...`)は任意に設定
4. `Startup.cs` の`ConfigureServices()` メソッドに、`services.AddDbContext`を追加
5. package manager console を開いて、マイグレーションを追加
`PM> add-migration create_books_table`
6. `Migrations` フォルダに、指定した名前のマイグレーションファイルがタイムスタンプ付きで生成される
7. package manager console で、マイグレーションを実行。データベースが存在しなければ作成処理を行う。その後、未実行のマイグレーションを処理する
`PM> update-database`