using System.Collections.Generic;
using NSwag.Examples;

namespace NSwagWithExamples.Models.Examples
{
    public class CityExample : IExampleProvider<City>
    {
        public City GetExample()
        {
            return new City()
            {
                Id = 5,
                Name = "Brno",
                People = new List<Person>()
                {
                    new Person() {Id = 1, FirstName = "Henry", LastName = "Cavill"},
                    new Person() {Id = 2, FirstName = "John", LastName = "Doe"}
                }
            };
        }
    }
}