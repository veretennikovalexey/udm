using ClientListApp.Models;
using ClientListApp.Services;

namespace ClientListApp.Forms;

public partial class ClientDialog : Form
{
    public string ClientName => txtName.Text.Trim();
    public string ClientPhone => txtPhone.Text.Trim();
    public string ClientProfession => txtProfession.Text.Trim();
    public string ClientOrgKey => (cmbOrg.SelectedValue as string) ?? "";

    public ClientDialog(OrganizationService orgService, Client? existing = null)
    {
        InitializeComponent();
        Theme.ApplyToAll(this);
        Load += (_, _) => Theme.EnableDarkTitleBar(this);

        // Populate ComboBox: blank entry first, then all orgs
        var orgs = orgService.GetAll();
        cmbOrg.DataSource = new[] { new Organization { Key = "", Name = "" } }.Concat(orgs).ToList();
        cmbOrg.DisplayMember = "Name";
        cmbOrg.ValueMember = "Key";

        if (existing != null)
        {
            Text = "Edit Client";
            txtName.Text = existing.Name;
            txtPhone.Text = existing.Phone;
            txtProfession.Text = existing.Profession;
            cmbOrg.SelectedValue = existing.OrganizationKey;
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            MessageBox.Show("Phone is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPhone.Focus();
            return;
        }
        if (txtProfession.Text.Trim().Length > 100)
        {
            MessageBox.Show("Profession must be 100 characters or fewer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProfession.Focus();
            return;
        }
        DialogResult = DialogResult.OK;
        Close();
    }
}
