# Client List App — Windows EXE

## Goal
Build a native Windows desktop application to manage a simple client list (name + phone number).

## Tech Stack
- **Language:** C#
- **UI Framework:** WinForms (or WPF)
- **Data Storage:** Local JSON file (`clients.json`) via System.Text.Json
- **Target:** .NET 8, single-file self-contained EXE

## Core Features
1. Display all clients in a DataGridView / ListView
2. Add new client (Name + Phone)
3. Edit existing client (inline or via dialog)
4. Delete client (with confirmation dialog)
5. Search / filter clients by name or phone
6. Persist data automatically on every change (save to JSON)

## UI Layout
- Main window with a toolbar (Add / Edit / Delete / Search buttons)
- DataGridView showing columns: #, Name, Phone
- Modal dialog for Add / Edit with two input fields and validation

## Data Model
```csharp
public class Client
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
}
```

## File Structure