using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DB_access_sample.Models.Data; /* データベースコンテキストを参照するため追加 */
using Microsoft.Extensions.DependencyInjection; /* CreateScope() メソッドを利用するために必要 */

namespace DB_access_sample
{
    public class Program
    {
        /* プログラムのエントリーポイント */
        public static void Main(string[] args)
        {
            /* 初期設定だと、すぐにRun() するようになっているので、これを修正する
             * CreateHostBuilder(args).Build().Run();
             */

            /* ホストのオブジェクトを作っておき、処理が終わったらRunする */
            var host = CreateHostBuilder(args).Build();

            /* データベースの初期化処理を設定 */
            using (var scope = host.Services.CreateScope()) 
            {
                var services = scope.ServiceProvider;
                try
                {
                    /* MyDbContextのデータを取得して、それをInitialize に渡す(DI) */
                    var context = services.GetRequiredService<MyDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                { 
                    /* 例外処理、ログの保存だけ行う */
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
