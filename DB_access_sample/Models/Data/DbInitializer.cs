namespace DB_access_sample.Models.Data
{
    /* データベースの初期化を行うクラス？ */
    public class DbInitializer
    {
        /* データベースが存在しない場合に実行される処理 Initialize 
         * これが最初に実行されると、データベースが作成される
         */
        public static void Initialize(MyDbContext context)
        {
            /* 引数で渡されるcontextそって、データベースを初期化する(DI) */
            context.Database.EnsureCreated();
        }
    }
}
