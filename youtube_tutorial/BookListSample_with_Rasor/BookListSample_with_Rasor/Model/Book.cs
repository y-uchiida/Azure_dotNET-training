using System.ComponentModel.DataAnnotations;

namespace BookListSample_with_Rasor.Model
{
    public class Book
    {
        /* Books テーブルの主キーとして、Id カラムを指定 */
        [Key]
        public int Id { get; set; }

        /* Books テーブルの必須カラムとして、Name を設定 */
        [Required]
        public string Name { get; set; }

        /* Author は必須カラムにならなない(生成されるマイグレーションのファイルで、nullable: trueになる) */
        public string Author { get; set; }
    }
}
