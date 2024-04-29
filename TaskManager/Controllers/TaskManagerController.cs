using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.UseCases.CreateTask;
using TaskManager.Communication.Requests;
using TaskManager.Communication.Responses;
using TaskManager.Communication.SqlServer;

namespace TaskManager.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class TaskManagerController : ControllerBase
{
    private readonly Database _requestTaskListJson;
    public TaskManagerController(Database requestTaskListJson)
    {
        _requestTaskListJson = requestTaskListJson;
    }

    [HttpPost("criarTask")]
    [ProducesResponseType(typeof(ResponseNewTaskCreatedJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult NovaTask([FromBody] RequestTaskListJson requestTaskListJson)
    {
        _requestTaskListJson.Add(requestTaskListJson);
        _requestTaskListJson.SaveChanges();
        return Created(string.Empty, requestTaskListJson);
    }

    [HttpGet("buscarTodasTasks")]
    [ProducesResponseType(typeof(ResponseAllTasksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult BuscarTodasTasks()
    {
        return Ok(_requestTaskListJson.GerenciadorDeTarefas);
    }

    [HttpGet("buscarTaskPorId/{id}")]
    [ProducesResponseType(typeof(ResponseTaskByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult BuscarTaskPorId(int id)
    {
        var resultadoTaskporId = _requestTaskListJson.GerenciadorDeTarefas.Find(id);
        if (resultadoTaskporId == null)
        {
            return NotFound("Nenhuma task cadastrada com esse ID.");
        }
        return Ok(resultadoTaskporId);
    }

    [HttpPut("atualizarTask/{id}")]
    [ProducesResponseType(typeof(ResponseShortTaskJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult EditarTask(int id, [FromBody] RequestTaskListJson requestTaskListJson)
    {
        var resultadoTaskporId = _requestTaskListJson.GerenciadorDeTarefas.Find(id);
        if (resultadoTaskporId == null)
        {
            return NotFound("Nenhuma task cadastrada com esse ID.");
        }
        resultadoTaskporId.Name = requestTaskListJson.Name;
        resultadoTaskporId.Description = requestTaskListJson.Description;
        resultadoTaskporId.DueDate = requestTaskListJson.DueDate;
        resultadoTaskporId.Priority = requestTaskListJson.Priority;
        resultadoTaskporId.Status = requestTaskListJson.Status;
        _requestTaskListJson.SaveChanges();
        return Ok(resultadoTaskporId);
    }

    [HttpDelete("deletarTask/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletarTask(int id)
    {
        var resultadoTaskporId = _requestTaskListJson.GerenciadorDeTarefas.Find(id);
        if (resultadoTaskporId == null)
        {
            return NotFound("Nenhuma task cadastrada com esse ID.");
        }
        _requestTaskListJson.GerenciadorDeTarefas.Remove(resultadoTaskporId);
        _requestTaskListJson.SaveChanges();
        return NoContent();
    }
}
