using ClientListApp.Models;
using ClientListApp.Services;

namespace ClientListApp.Forms;

public partial class MainForm : Form
{
    private readonly ClientService _service = new();
    private readonly OrganizationService _orgService = new();

    public MainForm()
    {
        InitializeComponent();
        Theme.ApplyToAll(this);
        Load += (_, _) => Theme.EnableDarkTitleBar(this);
        RefreshGrid();
    }

    private void RefreshGrid(string filter = "")
    {
        var clients = _service.GetAll();
        var filtered = string.IsNullOrWhiteSpace(filter)
            ? clients
            : clients.Where(c =>
                c.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                c.Profession.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                c.OrganizationKey.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

        grid.Rows.Clear();
        for (int i = 0; i < filtered.Count; i++)
        {
            var c = filtered[i];
            grid.Rows.Add(i + 1, c.Name, c.Phone, c.Profession, c.OrganizationKey, c.Id);
        }
    }

    private Client? SelectedClient()
    {
        if (grid.CurrentRow == null) return null;
        var id = (Guid)grid.CurrentRow.Cells["colId"].Value;
        return _service.GetAll().FirstOrDefault(c => c.Id == id);
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var dlg = new ClientDialog(_orgService);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _service.Add(new Client
            {
                Name = dlg.ClientName,
                Phone = dlg.ClientPhone,
                Profession = dlg.ClientProfession,
                OrganizationKey = dlg.ClientOrgKey
            });
            RefreshGrid(txtSearch.Text);
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var client = SelectedClient();
        if (client == null) { MessageBox.Show("Select a client first.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

        using var dlg = new ClientDialog(_orgService, client);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _service.Update(new Client
            {
                Id = client.Id,
                Name = dlg.ClientName,
                Phone = dlg.ClientPhone,
                Profession = dlg.ClientProfession,
                OrganizationKey = dlg.ClientOrgKey
            });
            RefreshGrid(txtSearch.Text);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var client = SelectedClient();
        if (client == null) { MessageBox.Show("Select a client first.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

        var confirm = MessageBox.Show($"Delete \"{client.Name}\"?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm == DialogResult.Yes)
        {
            _service.Delete(client.Id);
            RefreshGrid(txtSearch.Text);
        }
    }

    private void btnOrgs_Click(object sender, EventArgs e)
    {
        using var frm = new OrganizationsForm(_orgService, _service);
        frm.ShowDialog(this);
        RefreshGrid(txtSearch.Text);
    }

    private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEdit_Click(sender, e);
    }

    private void grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
            btnEdit_Click(sender, e);
        }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        RefreshGrid(txtSearch.Text);
    }
}
