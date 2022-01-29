using System.ComponentModel.DataAnnotations;

namespace BookList_MVC.Models
{
    public class Book
    {
        /* 主キーとしてId カラムを追加 */
        [Key]
        public int Id { get; set; }

        /* 必須カラムとしてName カラムを追加 */
        /* 必須カラムとしてName カラムを追加 */
        [Required]
        public string Name { get; set; }

        public string Author{ get; set; }
        public string ISBN { get; set; }
    }
}
