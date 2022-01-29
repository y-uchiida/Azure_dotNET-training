
using DB_access_sample.Models;
using System.Linq;

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


            if (context.Organizations.Any())
            {
                /* context.Organizations が空ではない場合、このまま終了 */
                return;   // DB has been seeded
            }

            /* Organizationテーブルの初期データを設定
             * 営業部、開発部などの部門データを作っておく
             */
            var orgs = new Organization[]
            {
                new Organization {Name = "営業部"},
                new Organization {Name = "開発部"},
                new Organization {Name = "経理部"},
                new Organization {Name = "広報部"},
                new Organization {Name = "人事部"},
                new Organization {Name = "経営企画部"},
                new Organization {Name = "総務部"},
            };

            /* 作った配列データを、contextに順番に追加 */
            foreach (var o in orgs)
            {
                context.Organizations.Add(o);
            }
            /* contextを保存する */
            context.SaveChanges();

            /* 同じ要領で、ユーザー情報の作成 */
            var users = new UserInfo[]
            {
                new UserInfo {Name = "ポチ", OrganizationId = orgs.First(o => o.Name == "営業部").Id},
                new UserInfo {Name = "タマ", OrganizationId = orgs.First(o => o.Name == "総務部").Id},
                new UserInfo {Name = "クロ", OrganizationId = orgs.First(o => o.Name == "広報部").Id},
                new UserInfo {Name = "タロ", OrganizationId = orgs.First(o => o.Name == "経理部").Id},
                new UserInfo {Name = "ハチ", OrganizationId = orgs.First(o => o.Name == "開発部").Id},
                new UserInfo {Name = "パトラッシュ", OrganizationId = orgs.First(o => o.Name == "人事部").Id},
                new UserInfo {Name = "ラッシー", OrganizationId = orgs.First(o => o.Name == "営業部").Id},
                new UserInfo {Name = "ロンドン", OrganizationId = orgs.First(o => o.Name == "広報部").Id},
            };

            /* ループでcontextへ追加 */
            foreach (var u in users)
            {
                context.UserInfoes.Add(u);
            }
            /* contextを保存する */
            context.SaveChanges();
        }
    }
}
