using ClientListApp.Models;

namespace ClientListApp.Forms;

partial class ClientDialog
{
    private System.ComponentModel.IContainer components = null;

    private Label lblName;
    private Label lblPhone;
    private Label lblProfession;
    private Label lblOrg;
    private TextBox txtName;
    private TextBox txtPhone;
    private TextBox txtProfession;
    private ComboBox cmbOrg;
    private Button btnOk;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblName = new Label();
        lblPhone = new Label();
        lblProfession = new Label();
        lblOrg = new Label();
        txtName = new TextBox();
        txtPhone = new TextBox();
        txtProfession = new TextBox();
        cmbOrg = new ComboBox();
        btnOk = new Button();
        btnCancel = new Button();
        SuspendLayout();

        // lblName
        lblName.AutoSize = true;
        lblName.Location = new Point(12, 15);
        lblName.Text = "Name:";

        // txtName
        txtName.Location = new Point(110, 12);
        txtName.Size = new Size(440, 23);
        txtName.TabIndex = 0;

        // lblPhone
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(12, 50);
        lblPhone.Text = "Phone:";

        // txtPhone
        txtPhone.Location = new Point(110, 47);
        txtPhone.Size = new Size(440, 23);
        txtPhone.TabIndex = 1;

        // lblProfession
        lblProfession.AutoSize = true;
        lblProfession.Location = new Point(12, 85);
        lblProfession.Text = "Profession:";

        // txtProfession
        txtProfession.Location = new Point(110, 82);
        txtProfession.Size = new Size(440, 23);
        txtProfession.MaxLength = 100;
        txtProfession.TabIndex = 2;

        // lblOrg
        lblOrg.AutoSize = true;
        lblOrg.Location = new Point(12, 120);
        lblOrg.Text = "Organization:";

        // cmbOrg
        cmbOrg.Location = new Point(110, 117);
        cmbOrg.Size = new Size(440, 23);
        cmbOrg.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbOrg.TabIndex = 3;

        // btnOk
        btnOk.Location = new Point(394, 158);
        btnOk.Size = new Size(75, 28);
        btnOk.Text = "OK";
        btnOk.TabIndex = 4;
        btnOk.Click += btnOk_Click;

        // btnCancel
        btnCancel.Location = new Point(475, 158);
        btnCancel.Size = new Size(75, 28);
        btnCancel.Text = "Cancel";
        btnCancel.TabIndex = 5;
        btnCancel.DialogResult = DialogResult.Cancel;

        // Form
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(564, 198);
        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(lblPhone);
        Controls.Add(txtPhone);
        Controls.Add(lblProfession);
        Controls.Add(txtProfession);
        Controls.Add(lblOrg);
        Controls.Add(cmbOrg);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Add Client";
        ResumeLayout(false);
        PerformLayout();
    }
}
