using Cowboy.API.Dtos;
using Cowboy.API.Entities;
using Cowboy.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Cowboy.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CowboyController : ControllerBase
    {
        private readonly ICowboyService _cowboyService;

        public CowboyController(ICowboyService cowboyService)
        {
            _cowboyService = cowboyService;
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<CowboyEntity>), 200)]
        public async Task<IEnumerable<CowboyEntity>> ListCowboys()
        {
            var result = await _cowboyService.ListAsync();
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CowboyEntity), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindCowboyById(int id)
        {
            var result = await _cowboyService.FindByIdAsync(id);
            if (!result.Success)
            {
                return Problem(result.Message);
            }

            return Ok(result.Cowboy);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CowboyEntity), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveCowboy([FromBody] CowboyDto value)
        {
            var result = await _cowboyService.SaveAsync(value);
            if (!result.Success)
            {
                return Problem(result.Message);
            }

            return CreatedAtAction(nameof(FindCowboyById), new { id = result.Cowboy.Id }, result.Cowboy);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCowboy(int id, JsonPatchDocument<CowboyEntity> cowboyUpdates)
        {
            var existingCowboy = await _cowboyService.FindByIdAsync(id);
            if (!existingCowboy.Success)
            {
                return Problem(existingCowboy.Message);
            }

            cowboyUpdates.ApplyTo(existingCowboy.Cowboy);
            var result = await _cowboyService.UpdateAsync(existingCowboy.Cowboy);
            if (result)
                return NoContent();
            return Problem("Internal server error");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(bool), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _cowboyService.DeleteAsync(id);
            if (!result.Success)
            {
                return Problem(result.Message);
            }

            return NoContent();
        }

        [HttpPost("Shoot/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CowboyEntity), 200)]
        public async Task<IActionResult> ShootGunAsync(int id)
        {
            var result = await _cowboyService.ShootAsync(id);
            if (!result.Success)
            {
                return Problem(result.Message);
            }

            return Ok(result.Cowboy);
        }

        [HttpPost("Reload/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CowboyEntity), 200)]
        public async Task<IActionResult> ReloadGunAsync(int id)
        {
            var result = await _cowboyService.ReloadAsync(id);
            if (!result.Success)
            {
                return Problem(result.Message);
            }

            return Ok(result.Cowboy);
        }

        [HttpPost("shootout")]
        [Produces("application/json")]
        public async Task<IActionResult> ShootoutAsync(int FirstCowboyId, int SecondCowboyId)
        {
            return Ok(await _cowboyService.ShootoutAsync(FirstCowboyId, SecondCowboyId));
        }
    }
}
