using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] WeatherForecast input)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(new Error { Message = "Ошибка в передаваемых параметрах!" });
            }
            try
            {
                _holder.Add(input);
            }
            catch (Exception E)
            {
                return Unauthorized(new Error { Message = "Не удалось добавить, по причине: " + E.Message });
            }
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime? startDate, DateTime? endDate = null)
        {
            return Ok(_holder.Get(startDate ?? DateTime.MinValue, endDate ?? DateTime.MaxValue));
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] WeatherForecast input)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(new Error { Message = "Ошибка в передаваемых параметрах!" });
            }
            try
            {
                _holder.Update(input);
            }
            catch (Exception E)
            {
                return Unauthorized(new Error { Message = "Не удалось обновить, по причине: " + E.Message });
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime? startDate, DateTime? endDate = null)
        {
            if (startDate == null)
            {
                return Unauthorized(new Error { Message = "Не указана StartDate !" });
            }
            try
            {
                _holder.Delete(startDate.Value, endDate ?? startDate.Value);
            }
            catch (Exception E)
            {
                return Unauthorized(new Error { Message = "Не удалось удалить, по причине: " + E.Message });
            }
            return Ok();
        }
    }
}
