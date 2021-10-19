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