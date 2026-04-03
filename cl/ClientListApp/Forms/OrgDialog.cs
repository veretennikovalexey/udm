using ClientListApp.Models;
using ClientListApp.Services;

namespace ClientListApp.Forms;

public partial class OrgDialog : Form
{
    private readonly List<Organization> _existing;
    private readonly string? _editingKey;

    public string OrgKey => txtKey.Text.Trim();
    public string OrgName => txtName.Text.Trim();

    public OrgDialog(List<Organization> existing, Organization? org = null)
    {
        InitializeComponent();
        Theme.ApplyToAll(this);
        Load += (_, _) => Theme.EnableDarkTitleBar(this);
        _existing = existing;
        _editingKey = org?.Key;

        if (org != null)
        {
            Text = "Edit Organization";
            txtKey.Text = org.Key;
            txtKey.Enabled = false; // key is immutable on edit
            txtName.Text = org.Name;
        }
    }

    private void txtKey_TextChanged(object sender, EventArgs e)
    {
        // Strip non-letters and force lowercase, preserve cursor position
        var pos = txtKey.SelectionStart;
        var cleaned = new string(txtKey.Text.Where(char.IsLetter).ToArray()).ToLower();
        if (cleaned.Length > 3) cleaned = cleaned[..3];
        if (txtKey.Text != cleaned)
        {
            txtKey.Text = cleaned;
            txtKey.SelectionStart = Math.Min(pos, cleaned.Length);
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        var key = txtKey.Text.Trim();
        var name = txtName.Text.Trim();

        if (key.Length != 3 || !key.All(char.IsLetter))
        {
            MessageBox.Show("Key must be exactly 3 letters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtKey.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtName.Focus();
            return;
        }
        // Uniqueness check (skip self when editing)
        if (_editingKey == null && _existing.Any(o => o.Key == key))
        {
            MessageBox.Show($"Key \"{key}\" already exists.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtKey.Focus();
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }
}
