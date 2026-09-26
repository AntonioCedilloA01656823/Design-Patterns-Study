// Extra C# example of Repository (same user-store problem as example.ipynb).
// Run: paste into a console Program.cs (top-level statements, .NET 6+).

using System;
using System.Collections.Generic;
using System.Linq;

sealed record User(string Id, string Email, string Name);

interface IUserRepository
{
    void Add(User user);
    User? Get(string userId);
    IReadOnlyList<User> ListAll();
}

sealed class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _byId = new();

    public void Add(User user) => _byId[user.Id] = user;

    public User? Get(string userId) =>
        _byId.TryGetValue(userId, out var user) ? user : null;

    public IReadOnlyList<User> ListAll() => _byId.Values.ToList();
}

static string DisplayName(IUserRepository repo, string userId)
{
    var user = repo.Get(userId);
    return user is null ? "unknown user" : user.Name;
}

var repo = new InMemoryUserRepository();
repo.Add(new User("u1", "ada@example.com", "Ada Lovelace"));
repo.Add(new User("u2", "grace@example.com", "Grace Hopper"));

Console.WriteLine(DisplayName(repo, "u1"));
Console.WriteLine(string.Join(", ", repo.ListAll().Select(u => u.Email)));
Console.WriteLine(DisplayName(repo, "missing"));
