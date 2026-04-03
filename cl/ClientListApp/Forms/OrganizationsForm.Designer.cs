namespace ClientListApp.Forms;

partial class OrganizationsForm
{
    private System.ComponentModel.IContainer components = null;

    private ToolStrip toolStrip;
    private ToolStripButton btnAdd;
    private ToolStripButton btnEdit;
    private ToolStripButton btnDelete;
    private DataGridView grid;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        toolStrip = new ToolStrip();
        btnAdd = new ToolStripButton();
        btnEdit = new ToolStripButton();
        btnDelete = new ToolStripButton();
        grid = new DataGridView();

        toolStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        // toolStrip
        toolStrip.Items.AddRange(new ToolStripItem[] { btnAdd, btnEdit, btnDelete });
        toolStrip.Location = new Point(0, 0);
        toolStrip.Size = new Size(500, 25);
        toolStrip.TabIndex = 0;

        // btnAdd
        btnAdd.Text = "Add";
        btnAdd.Click += btnAdd_Click;

        // btnEdit
        btnEdit.Text = "Edit";
        btnEdit.Click += btnEdit_Click;

        // btnDelete
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;

        // grid
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Dock = DockStyle.Fill;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = SystemColors.Window;
        grid.TabIndex = 1;

        var colKey = new DataGridViewTextBoxColumn { Name = "colKey", HeaderText = "Key", FillWeight = 20 };
        var colName = new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Name", FillWeight = 80 };
        grid.Columns.AddRange(colKey, colName);

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 350);
        Controls.Add(grid);
        Controls.Add(toolStrip);
        MinimumSize = new Size(400, 250);
        StartPosition = FormStartPosition.CenterParent;
        Text = "Organizations";

        toolStrip.ResumeLayout(false);
        toolStrip.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
