using System.Net.Sockets;
using GamesApi.Data;
using GamesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
   [HttpGet]
   public ActionResult<List<Game>> GetAll()
   {
      return Ok(GamesStore.Games);
   }

   [HttpGet("favourites")]
   public ActionResult<List<Game>> GetFavourites()
   {
      var favourites = GamesStore.Games.Where(g => g.IsFavourite).ToList();
      return Ok(favourites);
   }

   [HttpGet("{id}")]
   public ActionResult<Game> GetById(int id)
   {
      var game = GamesStore.Games.FirstOrDefault(g => g.Id == id);
      if (game is null)
      {
         return NotFound(new { message = $"Игра с id={id} не найдена" });
      }
      return Ok(game);
   }

   [HttpPost]
   public ActionResult<Game> Create([FromBody] Game game)
   {
      if (string.IsNullOrWhiteSpace(game.Title))
      {
         return BadRequest(new { message = "Название игры не может быть пустым" });
      }

      game.Id = GamesStore.NextId();
      GamesStore.Games.Add(game);
      return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
   }

   [HttpDelete("{id}")]
   public ActionResult Delete(int id)
   {
      var game = GamesStore.Games.FirstOrDefault(g => g.Id == id);
      if (game is null)
      {
         return NotFound(new { message = $"Игра с id={id} не найдена" });
      }
      GamesStore.Games.Remove(game);
      return NoContent();
   }

   [HttpPut("{id}")]
   public ActionResult<Game> Update(int id, [FromBody] Game update)
   {
      var game = GamesStore.Games.FirstOrDefault(g => g.Id == id);
      if (game is null){
         return NotFound(new { message = $"Игра с id={id} не найдена" });
      }
      game.Title = update.Title;
      game.Genre = update.Genre;
      game.ReleaseYear = update.ReleaseYear;
      return Ok(game);
   }
}