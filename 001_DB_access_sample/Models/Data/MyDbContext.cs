/* データベースコンテキスト
 * データベースの作成とテーブルの作成を行うためのファイル
 * Larvelでいえば、migrationファイルみたいなもの
 */

using Microsoft.EntityFrameworkCore;
using DB_access_sample.Models;

namespace DB_access_sample.Models.Data
{
    public class MyDbContext : DbContext
    {
        /* コンストラクタ */
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        /* ユーザー情報のテーブルを作成する？ */
        public DbSet<UserInfo> UserInfoes { get; set; }

        /* 組織情報のテーブルを作成する？ */
        public DbSet<Organization> Organizations { get; set; }
    }
}
