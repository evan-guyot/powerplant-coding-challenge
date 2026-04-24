using PowerPlant.Models;
using PowerPlant.Services.DTOs;

namespace PowerPlant.Services.Tests
{
    public class ProductionPlanServiceTests
    {
        [Fact]
        public void Calculate_ExamplePayload_ReturnsExpectedResponse()
        {
            // Arrange
            var service = new ProductionPlanService();

            var plan = new ProductionPlanDto // Example payload : https://github.com/evan-guyot/powerplant-coding-challenge/blob/master/example_payloads/payload3.json
            {
                Load = 910,
                Fuels = new FuelsDto
                {
                    Gas = 13.4,
                    Kerosine = 50.8,
                    Co2 = 20,
                    Wind = 60
                },
                PowerPlants = new List<PowerPlantDto>
                {
                    new() { Name = "gasfiredbig1",           Type = PowerPlantType.GasFired,     Efficiency = 0.53, Pmin = 100, Pmax = 460 },
                    new() { Name = "gasfiredbig2",           Type = PowerPlantType.GasFired,     Efficiency = 0.53, Pmin = 100, Pmax = 460 },
                    new() { Name = "gasfiredsomewhatsmaller",Type = PowerPlantType.GasFired,     Efficiency = 0.37, Pmin = 40,  Pmax = 210 },
                    new() { Name = "tj1",                    Type = PowerPlantType.Turbojet,     Efficiency = 0.3,  Pmin = 0,   Pmax = 16  },
                    new() { Name = "windpark1",              Type = PowerPlantType.WindTurbine,  Efficiency = 1,    Pmin = 0,   Pmax = 150 },
                    new() { Name = "windpark2",              Type = PowerPlantType.WindTurbine,  Efficiency = 1,    Pmin = 0,   Pmax = 36  }
                }
            };

            var expected = new List<PowerPlantAllocationDto> // Expected response : https://github.com/evan-guyot/powerplant-coding-challenge/blob/master/example_payloads/response3.json
            {
                new() { Name = "windpark1",               P = 90.0  },
                new() { Name = "windpark2",               P = 21.6  },
                new() { Name = "gasfiredbig1",            P = 460.0 },
                new() { Name = "gasfiredbig2",            P = 338.4 },
                new() { Name = "gasfiredsomewhatsmaller", P = 0.0   },
                new() { Name = "tj1",                     P = 0.0   }
            };

            // Act
            var result = service.Calculate(plan);

            // Assert
            Assert.Equal(expected.Count, result.Count);

            foreach (var expectedAllocation in expected)
            {
                var actual = result.SingleOrDefault(r => r.Name == expectedAllocation.Name);
                Assert.NotNull(actual);
                Assert.Equal(expectedAllocation.P, actual.P);
            }
        }
    }
}
