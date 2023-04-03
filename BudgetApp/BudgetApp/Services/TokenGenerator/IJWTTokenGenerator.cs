using BudgetApp.Models;
using System.Security.Claims;

namespace BudgetApp.Services.TokenGenerator
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(User user);
    }
}
