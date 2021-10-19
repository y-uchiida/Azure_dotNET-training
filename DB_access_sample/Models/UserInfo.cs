namespace DB_access_sample.Models
{
    public class UserInfo
    {
        /* ユーザーの識別番号 Id
         * (int 型の Id カラムが、自動で主キーになる)
         */
        public int Id {  get; set; }

        /* ユーザー名を表す Name */
        public string Name { get; set; }

        /* 組織IDを外部キーとして持つ OganizationId */
        public int OganizationId { get; set; }

        /* リレーションの設定？ */
        public Organization Organization { get; set; }
    }
}
