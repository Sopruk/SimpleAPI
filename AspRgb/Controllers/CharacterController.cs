using Microsoft.AspNetCore.Mvc;
using AspRgb.Models;
using System.Collections.Generic;
using System.Linq;
using AspRgb.Services.Interfaces;
using System.Threading.Tasks;
using AspRgb.Dtos.Character;

namespace AspRgb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController:ControllerBase
    {
        public CharacterController(ICharacterServices caracterServices)
        {
            _caracterServices = caracterServices;
        }

        private ICharacterServices _caracterServices;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _caracterServices.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok( await _caracterServices.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostSingle(AddCharacterDto newCharacter)
        {
            return Ok(await _caracterServices.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<GetCharacterDto> response = await _caracterServices.UpdateCharacter(updateCharacter);
            if (response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await _caracterServices.DeleteCharacterById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
