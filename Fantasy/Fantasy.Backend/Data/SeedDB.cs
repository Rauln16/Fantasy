using Fantasy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class SeedDB
{
    private readonly DataContext _context;

    public SeedDB(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
        await CheckTeamsAsync();
    }

    private async Task CheckTeamsAsync()
    {
        if (!_context.Teams.Any())
        {
            foreach (var country in _context.Countries)
            {
                _context.Teams.Add(new Team { Name = country.Name, Country = country! });
                if (country.Name == "Spain")
                {
                    _context.Teams.Add(new Team { Name = "Real Madrid", Country = country });
                    _context.Teams.Add(new Team { Name = "FC Barcelona", Country = country });
                    _context.Teams.Add(new Team { Name = "Atlético de Madrid", Country = country });
                    _context.Teams.Add(new Team { Name = "Sevilla FC", Country = country });
                    _context.Teams.Add(new Team { Name = "Real Betis", Country = country });
                    _context.Teams.Add(new Team { Name = "Real Sociedad", Country = country });
                    _context.Teams.Add(new Team { Name = "Villarreal CF", Country = country });
                    _context.Teams.Add(new Team { Name = "Athletic Club", Country = country });
                    _context.Teams.Add(new Team { Name = "Valencia CF", Country = country });
                    _context.Teams.Add(new Team { Name = "CA Osasuna", Country = country });
                    _context.Teams.Add(new Team { Name = "RC Celta de Vigo", Country = country });
                    _context.Teams.Add(new Team { Name = "Rayo Vallecano", Country = country });
                    _context.Teams.Add(new Team { Name = "Deportivo Alavés", Country = country });
                    _context.Teams.Add(new Team { Name = "RCD Espanyol", Country = country });
                    _context.Teams.Add(new Team { Name = "UD Las Palmas", Country = country });
                    _context.Teams.Add(new Team { Name = "Getafe CF", Country = country });
                    _context.Teams.Add(new Team { Name = "RCD Mallorca", Country = country });
                    _context.Teams.Add(new Team { Name = "CD Leganés", Country = country });
                    _context.Teams.Add(new Team { Name = "Real Valladolid", Country = country });
                    _context.Teams.Add(new Team { Name = "Girona FC", Country = country });
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.Countries.Any())
        {
            var countriesSqlScript = File.ReadAllText("Data\\Countries.sql");
            await _context.Database.ExecuteSqlRawAsync(countriesSqlScript);
        }
    }
}