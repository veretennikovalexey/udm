using System.Text.Json;
using ClientListApp.Models;

namespace ClientListApp.Services;

public class ClientService
{
    private readonly string _filePath;
    private List<Client> _clients;

    public ClientService()
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "clients.json");
        _clients = Load();
    }

    public List<Client> GetAll() => _clients;

    public void Add(Client client)
    {
        _clients.Add(client);
        Save();
    }

    public void Update(Client updated)
    {
        var index = _clients.FindIndex(c => c.Id == updated.Id);
        if (index >= 0)
        {
            _clients[index] = updated;
            Save();
        }
    }

    public void Delete(Guid id)
    {
        _clients.RemoveAll(c => c.Id == id);
        Save();
    }

    private List<Client> Load()
    {
        if (!File.Exists(_filePath)) return new List<Client>();
        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Client>>(json) ?? new List<Client>();
        }
        catch
        {
            return new List<Client>();
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_clients, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
