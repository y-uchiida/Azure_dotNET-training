# memoranduma of DB_access_sample
ハンズオン中につまづいたことなどのメモです

## Azure への発行(publish) ができない
作成したデータをAzureにデプロイする作業を、VS上で自動でやってくれる機能があってとても便利  
しかし、資料通りにソリューションエクスプローラで右クリックしても「発行」の項目が表示されない  
よく確認したところ、「フォルダ」としてデータを開いているとできないらしい  
メニューの「ファイル」から　開く > プロジェクト/ソリューション を選択して、プロジェクトとして読み込ませないといけない  

## MVCのディレクトリがない
うっかり、MVCのないASP.NET プロジェクトを選択してしまうと発生する  

## Usingsqlserver メソッドがないというエラー
Startup.cs にデータベースコンテキストへの参照を追加する変更を行った際、下記のエラーが表示された
```
'DbContextOptionsBuilder' does not contain a definition for 'UseSqlServer' and no accessible extension method 'UseSqlServer' accepting a first argument of type 'DbContextOptionsBuilder' could be found (are you missing a using directive or an assembly reference?)	DB_access_sample	C:\Users\y-uchiida\Documents\develop\Azure_dotNET-training\DB_access_sample\Startup.cs	33	Active
```
調べたところ、SqlServerのためのパッケージをインストールする必要があるらしい  
掲載されていた通り、PM コンソールで以下のコマンドを実行
```
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
```
エラーが消えた。新しくプロジェクトを作ると、SQL用のパッケージは含まれていないので、別途インストールが必要になるらしい

## LINQ とはなにか
「統合言語クエリ」...ってなんだ？なおさらわからない  
いくつかの検索結果を見てみると、
`LINQというのは、こういったリストのようなデータの集まりに対して何らかの処理をすること`  
https://tech.drecom.co.jp/ac2019-knowing-linq-essential-csharp/

`LINQ は foreach のパワーアップ版`  
https://qiita.com/nskydiving/items/c9c47c1e48ea365f8995

みたいな説明があった。なんとなくわかった気がする。  
データの集合の各要素を操作するための仕組みで、扱えるデータ集合の中に、  
データベースのレコードが含まれるので、SQLを使わなくてもデータベースの操作ができるということかな...

## Scaffold の作成
GUIからポチポチで作れる  
Controllerフォルダを右クリックしてAdd > Controller から、MVC controller with Entity Framework を選択し、  
設定ウィンドウからスカフォールドを作りたいテーブルやデータソースを選ぶだけ  
あとは勝手にVisualStudioが作ってくれる  
`Microsoft.VisualStudio.Web.CodeGeneration.Design` パッケージがないと、エラーになる  
何も考えずにインストールコマンドを実行したら、 .NET 5 対応したバージョンをインストールしようとして依存関係エラーになった
```
$ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 3.1.5
```
このバージョン指定でやったら、.NET 3.1ベースのプロジェクトでもOKだった

次も同じようなエラーで、`microsoft.bcl.asyncinterfaces` がないとかいった内容だったので、

```
$ dotnet add package Microsoft.Bcl.AsyncInterfaces
```
これでインストールを実行  
バージョン指定しなかったら`5.0.0` が入ってきちゃったんだけど大丈夫かな...


とりあえずそのままにして再度実行したら、もうひとつエラーが出てきた  
`the database provider attempted to register an imrementation of the IRelationalTypeMappingSource service ...` みたいな内容  
調べたところ、Entity Frameworkのバージョンがあってない場合に出てくるらしい
参照したページと同じように、.csproj ファイルを見てみると、
```
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.11" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="3.1.20" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="3.1.20">
```
こんな感じに、Core だけ `5.0.11` になっていたので、 `3.1.20` に合わせてみた  
スカフォールド生成をもう一度実行を試してみたら、今度は成功した  
3.1はそろそろ古いのかな...パッケージの依存性管理で苦労するとは。。。  

`Ctrl +F5` で、デバッグなしビルド & サーバ起動してブラウザ表示  
URLに`/Organizations` をつけてみたら、作成したい部署名が一覧表示された  

同じ要領でUserInfoについてもスカフォールドを生成。今度はエラーもなく完了。

## まとめ
- 強いこだわりがなければ、VS CodeよりもVisual Studioの方が楽できそう
- パッケージのバージョン依存、.NETについて不慣れなので勘が働かなくて解決に時間がかかる
- あと、ASP.NET Core 自体の構成の理解が深まっていない。スカフォールドで生成された内容をきちんと読み解けるようにならないと