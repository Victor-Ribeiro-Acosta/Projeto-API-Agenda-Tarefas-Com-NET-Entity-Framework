using Context;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace CRUDTarefaController
{
[ApiController]
[Route("Agenda/[controller]")]
    public class CRUDTarefa : ControllerBase
    {
        private readonly TarefaContext _context;

        public CRUDTarefa(TarefaContext context)
        {
            _context = context;
        }

        [HttpPost("Registrar")]
        public IActionResult Create(Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("Tarefa inválida!");
            }
            else if (tarefa.Data == DateTime.MinValue)
            {
                return BadRequest("Data inválida!");
            }
            _context.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }


        [HttpGet("Listar")]
        public IActionResult GetTarefas()
        {
            var tarefas = _context.Tarefas.ToList();
            if (tarefas == null)
            {
                return NotFound("Não há tarefas registradas");
            }
            return Ok(tarefas);
        }

        
        [HttpGet("PesquisarId/{Id}")]
        public IActionResult GetId(int Id)
        {
            Tarefa tarefa = _context.Tarefas.Find(Id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada!");
            }
            return Ok(tarefa);
        }

        
        [HttpGet("PesquisarTitulo/{Titulo}")]
        public IActionResult GetTitulo(string Titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo == Titulo);
            if (tarefas == null)
            {
                return NotFound("Não Há tarefas com esse título!");
            }
            return Ok(tarefas);
        }


        [HttpGet("PesquisarStatus/{Status}")]
        public IActionResult GetStatus(StatusTarefa Status)
        {
            var tarefas = _context.Tarefas.Where(x => x.Status == Status);
            if (tarefas == null)
            {
                return NotFound("Não há tarefas nesse Status!");
            }
            return Ok(tarefas);
        }
    

    [HttpGet("PesquisarData/{Data}")]
        public IActionResult GetData(DateTime Data)
        {
            var tarefas = _context.Tarefas.Where(x => x.Data == Data.Date);
            if (tarefas == null)
            {
                return NotFound("Não Há tarefas Registradas nessa data!");
            }
            return Ok(tarefas);
        }


        [HttpPut("Atualizar")]
        public IActionResult PutTarefa(int Id,Tarefa tarefa)
        {
            var tarefaDB = _context.Tarefas.Find(Id);
            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada!");
            }
            tarefaDB.Titulo = tarefa.Titulo;
            tarefaDB.Descricao = tarefa.Descricao;
            tarefaDB.Data = tarefa.Data;
            tarefaDB.Status = tarefa.Status;
            _context.Tarefas.Update(tarefaDB);
            _context.SaveChanges();
            return Ok(tarefa);
        }


        [HttpDelete("DeletarTarefa/{Id}")]
        public IActionResult Delete(int Id)
        {
            var tarefa = _context.Tarefas.Find(Id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Remove(tarefa);
            _context.SaveChanges();
            return NoContent();
        }
    }
}