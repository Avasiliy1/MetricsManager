using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager
{
    public class ValuesHolder
    {
        public ValuesHolder()
        {
            _repository = new List<WeatherForecast>();
        }
        private List<WeatherForecast> _repository;
        internal void Add(WeatherForecast input)
        {
            if (!_repository.Any(a => a.Date == input.Date))
            {
                _repository.Add(input);
            }
            else
            {
                throw new Exception("На указанную дату температура уже указана, для изменения воспользуетесь методам Update!");
            }
        }

        internal WeatherForecast[] Get(DateTime startDate, DateTime endDate)
        {
            return _repository.Where(a => a.Date >= startDate && a.Date <= endDate).ToArray();
        }

        internal void Update(WeatherForecast input)
        {
            WeatherForecast weatherForecast = _repository.Where(a => a.Date == input.Date).FirstOrDefault();

            if (weatherForecast == null)
            {
                throw new Exception("На указанную дату запись не существует!");
            }

            weatherForecast.Summary = input.Summary;
            weatherForecast.TemperatureC = input.TemperatureC;
        }

        internal void Delete(DateTime startDate, DateTime endDate)
        {

            foreach (WeatherForecast weatherForecast in _repository.Where(a => a.Date >= startDate && a.Date <= endDate).ToList())
            {
                _repository.Remove(weatherForecast);
            }
        }
    }
}