using ClientListApp.Models;
using ClientListApp.Services;

namespace ClientListApp.Forms;

public partial class OrganizationsForm : Form
{
    private readonly OrganizationService _orgService;
    private readonly ClientService _clientService;

    public OrganizationsForm(OrganizationService orgService, ClientService clientService)
    {
        InitializeComponent();
        Theme.ApplyToAll(this);
        Load += (_, _) => Theme.EnableDarkTitleBar(this);
        _orgService = orgService;
        _clientService = clientService;
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        grid.Rows.Clear();
        foreach (var org in _orgService.GetAll())
            grid.Rows.Add(org.Key, org.Name);
    }

    private Organization? SelectedOrg()
    {
        if (grid.CurrentRow == null) return null;
        var key = grid.CurrentRow.Cells["colKey"].Value as string;
        return _orgService.GetByKey(key ?? "");
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var dlg = new OrgDialog(_orgService.GetAll());
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _orgService.Add(new Organization { Key = dlg.OrgKey, Name = dlg.OrgName });
            RefreshGrid();
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var org = SelectedOrg();
        if (org == null) { MessageBox.Show("Select an organization first.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

        using var dlg = new OrgDialog(_orgService.GetAll(), org);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            _orgService.Update(new Organization { Key = org.Key, Name = dlg.OrgName });
            RefreshGrid();
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var org = SelectedOrg();
        if (org == null) { MessageBox.Show("Select an organization first.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

        var inUse = _clientService.GetAll().Any(c => c.OrganizationKey == org.Key);
        if (inUse)
        {
            MessageBox.Show(
                $"Cannot delete \"{org.Name}\" — it is assigned to one or more clients.",
                "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var confirm = MessageBox.Show($"Delete organization \"{org.Name}\" ({org.Key})?", "Confirm Delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirm == DialogResult.Yes)
        {
            _orgService.Delete(org.Key);
            RefreshGrid();
        }
    }
}
