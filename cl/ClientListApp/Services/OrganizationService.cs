using System.Text.Json;
using ClientListApp.Models;

namespace ClientListApp.Services;

public class OrganizationService
{
    private readonly string _filePath;
    private List<Organization> _orgs;

    public OrganizationService()
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "organizations.json");
        _orgs = Load();
    }

    public List<Organization> GetAll() => _orgs;

    public Organization? GetByKey(string key) =>
        _orgs.FirstOrDefault(o => o.Key == key);

    public void Add(Organization org)
    {
        _orgs.Add(org);
        Save();
    }

    public void Update(Organization updated)
    {
        var index = _orgs.FindIndex(o => o.Key == updated.Key);
        if (index >= 0)
        {
            _orgs[index] = updated;
            Save();
        }
    }

    public void Delete(string key)
    {
        _orgs.RemoveAll(o => o.Key == key);
        Save();
    }

    private List<Organization> Load()
    {
        if (!File.Exists(_filePath))
        {
            var seed = new List<Organization>
            {
                new() { Key = "acm", Name = "Acme Corp" },
                new() { Key = "glb", Name = "Global Inc" },
                new() { Key = "krl", Name = "Karlin Ltd" }
            };
            var seedJson = JsonSerializer.Serialize(seed, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, seedJson);
            return seed;
        }
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Organization>>(json) ?? new List<Organization>();
        }
        catch
        {
            return new List<Organization>();
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_orgs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
