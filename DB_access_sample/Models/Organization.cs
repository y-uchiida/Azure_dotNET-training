/* Modelクラスを生成、雛形はVSが自動で作ってくれる */

namespace DB_access_sample.Models
{
    /* 組織情報を扱うためのモデル、Oganization */
    public class Organization
    {
        /* 固有の識別番号をセットするId カラム
         * ※ Id という名前の int 型プロパティは自動的にテーブルのキーとして認識される
         */
        public int Id {  get; set; }

        /* 組織名を表すカラム Name */
        public string Name { get; set; }
    }
}
