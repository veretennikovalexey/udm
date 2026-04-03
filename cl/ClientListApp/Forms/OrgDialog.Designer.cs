namespace ClientListApp.Forms;

partial class OrgDialog
{
    private System.ComponentModel.IContainer components = null;

    private Label lblKey;
    private Label lblName;
    private TextBox txtKey;
    private TextBox txtName;
    private Button btnOk;
    private Button btnCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblKey = new Label();
        lblName = new Label();
        txtKey = new TextBox();
        txtName = new TextBox();
        btnOk = new Button();
        btnCancel = new Button();
        SuspendLayout();

        // lblKey
        lblKey.AutoSize = true;
        lblKey.Location = new Point(12, 15);
        lblKey.Text = "Key (3 letters):";

        // txtKey
        txtKey.Location = new Point(120, 12);
        txtKey.Size = new Size(60, 23);
        txtKey.MaxLength = 3;
        txtKey.TabIndex = 0;
        txtKey.TextChanged += txtKey_TextChanged;

        // lblName
        lblName.AutoSize = true;
        lblName.Location = new Point(12, 50);
        lblName.Text = "Name:";

        // txtName
        txtName.Location = new Point(120, 47);
        txtName.Size = new Size(194, 23);
        txtName.TabIndex = 1;

        // btnOk
        btnOk.Location = new Point(158, 85);
        btnOk.Size = new Size(75, 28);
        btnOk.Text = "OK";
        btnOk.TabIndex = 2;
        btnOk.Click += btnOk_Click;

        // btnCancel
        btnCancel.Location = new Point(239, 85);
        btnCancel.Size = new Size(75, 28);
        btnCancel.Text = "Cancel";
        btnCancel.TabIndex = 3;
        btnCancel.DialogResult = DialogResult.Cancel;

        // Form
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(328, 125);
        Controls.Add(lblKey);
        Controls.Add(txtKey);
        Controls.Add(lblName);
        Controls.Add(txtName);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Add Organization";
        ResumeLayout(false);
        PerformLayout();
    }
}
