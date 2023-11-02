
using gerenciadorDeTarefas.Context;
using gerenciadorDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;

namespace gerenciadorDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;
        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound();
            }
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Titulo = tarefa.Titulo;
            _context.SaveChanges();
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("ObterTodos")]
        public IActionResult GetAllTarefas()
        {
            Tarefa[] tarefas = _context.Tarefas.ToArray();
            return Ok(tarefas);
        }
        [HttpGet("ObterPorTitulo")]
        public IActionResult GetByTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(t => t.Titulo == titulo).ToList();
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult GetByData(DateTime data)
        {
            var tarefa = _context.Tarefas.Find(data);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult GetByStatus(EnumStatusTarefa status)
        {

            var tarefa = _context.Tarefas.Where(t => t.Status == status).ToList();
            if (tarefa.Count == 0)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            var novaTarefa = _context.Add(tarefa);
            _context.SaveChanges();
            return Ok(tarefa);
        }
    }
}