/* DisplayName 属性を利用するため追加で読み込みしておく */
using System.ComponentModel;

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
    public class Book
    {
        public int Id { get; set; }

        /* [DisplayName("項目表示名")] で、ビューに表示する項目名称を設定 */
        [DisplayName("書名")]
        public string Title { get; set; }

        [DisplayName("価格")]
        public int Price { get; set; }

        [DisplayName("出版社")]
        public string Publisher { get; set; }

        [DisplayName("配布サンプル")]
        public bool Sample { get; set; }
    }
}
