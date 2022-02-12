/* DisplayName 属性を利用するため追加で読み込みしておく */
using System.ComponentModel;

/* 独自バリデーションの作成のために読み込んでおく */
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickMaster.Models
{
    /* Book テーブルに対応するデータモデルとして、Book クラスを定義
     * 特別な継承関係を持たないプレーンなクラス（POCO: Plain Old Clr Object）
     * CLR... Common Language Runtime
     * 特別なクラスやインターフェイスの継承／実装を行っていないプレーンな.NETクラス（のオブジェクト）のこと
     * 
     * モデルクラスとして利用されるための規約（設計上のルール）は以下
     * クラス名はテーブル名と同一であること(Cake PHP やらLaravelのように、モデル名単数系-テーブル名複数系ではなく、完全に同名)
     * プロパティは、対応するテーブルのカラム名と同一であること
     * プライマリキー名は「Id」 であること
     * 
     * コンテキストクラスを通じて、データベースと連結して利用できるようになる
     * コンテキストクラスも、Models オブジェクト内に作成する
     */
    public class Book : IValidatableObject /* 独自バリデーションのため、IValidatableObject を実装(implements) */
    {
        public int Id { get; set; }

        /* [DisplayName("項目表示名")] で、ビューに表示する項目名称を設定 */
        [DisplayName("書名")]
        /* [Required] 属性で、必須入力の項目として指定する(ErrorMassage 内の{0}は表示名(DisplayName)を表す) */
        [Required(ErrorMessage = "{0}は必須です")]
        public string Title { get; set; }

        [DisplayName("価格")]
        /* [Range(min, max)] 属性で値の範囲を指定する */
        [Range(0, 5000, ErrorMessage = "{0} は {1}~{2}円の範囲で指定してください")]
        public int Price { get; set; }

        [DisplayName("出版社")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字以内で入力してください")]
        public string Publisher { get; set; }

        [DisplayName("配布サンプル")]
        public bool Sample { get; set; }
        
        /* 競合検出を行えるように、RowVersionカラムを追加
         * データベースのテーブル定義にも反映させるため、Visual Studioのパッケージマネージャーコマンドを実行する
         * PM> Add-Migration AddRowVersionToBook
         * PM> Update-Database
         * ---
         * RowVersionカラムは、更新の際に自動的にインクリメントされる
         * データ取得時のRowVersionと、更新時のRowVersionの値を比較することで、他の更新処理が行われたかどうかを検知することができる
         */
        [Timestamp] public byte[]
        RowVersion { get; set; }

        /* 独自バリデーション設定を作成
         * IEnumerable 型を返すため、yield return を使ってValidationResultを返す
         * これによって反復処理可能な ValidationResultsのコレクションにする
         * このバリデーションは、プロパティの属性として定義した条件と違ってJavaScriptによる検証はされない
         * リクエスト時にサーバ側でバリデーションしてエラーを検出する
         */
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Publisher == "フリー文庫" && this.Price > 0)
            {
                yield return new ValidationResult("フリー文庫の価格は０円でなければなりません。");
            }
        }
    }
}
