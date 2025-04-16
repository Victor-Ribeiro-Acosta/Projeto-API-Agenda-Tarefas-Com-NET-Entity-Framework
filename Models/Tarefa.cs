
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Models
{
    [Table("tarefas")]
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName ="date")]
        public DateTime Data { get; set; }
        public StatusTarefa Status { get; set; }
    }
}