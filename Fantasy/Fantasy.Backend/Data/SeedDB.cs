using Fantasy.Backend.UnitOfWork.Interfaces;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class SeedDB
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDB(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountriesAsync();
        await CheckTeamsAsync();
        await CheckRolesAsync();
        await CheckUserAsync("Raul", "Nieto", "raulnietogarcia16@gmail.com", "622136742", UserType.Admin);
    }

    private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Name == "España");
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Country = country!,
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "123456");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
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