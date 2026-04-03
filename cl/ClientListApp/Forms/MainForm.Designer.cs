namespace ClientListApp.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private ToolStrip toolStrip;
    private ToolStripButton btnAdd;
    private ToolStripButton btnEdit;
    private ToolStripButton btnDelete;
    private ToolStripSeparator sep;
    private ToolStripButton btnOrgs;
    private ToolStripSeparator sep2;
    private ToolStripLabel lblSearch;
    private ToolStripTextBox txtSearch;
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
        sep = new ToolStripSeparator();
        btnOrgs = new ToolStripButton();
        sep2 = new ToolStripSeparator();
        lblSearch = new ToolStripLabel();
        txtSearch = new ToolStripTextBox();
        grid = new DataGridView();

        toolStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        SuspendLayout();

        // toolStrip
        toolStrip.Items.AddRange(new ToolStripItem[] { btnAdd, btnEdit, btnDelete, sep, btnOrgs, sep2, lblSearch, txtSearch });
        toolStrip.Location = new Point(0, 0);
        toolStrip.Size = new Size(900, 25);
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

        // btnOrgs
        btnOrgs.Text = "Organizations";
        btnOrgs.Click += btnOrgs_Click;

        // lblSearch
        lblSearch.Text = "Search:";

        // txtSearch
        txtSearch.Size = new Size(180, 25);
        txtSearch.Name = "txtSearch";
        txtSearch.TextChanged += txtSearch_TextChanged;

        // grid
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.ReadOnly = true;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Dock = DockStyle.Fill;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        grid.TabIndex = 1;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = SystemColors.Window;
        grid.CellDoubleClick += grid_CellDoubleClick;
        grid.KeyDown += grid_KeyDown;

        // Columns
        var colNum        = new DataGridViewTextBoxColumn { Name = "colNum",        HeaderText = "#",            FillWeight = 5  };
        var colName       = new DataGridViewTextBoxColumn { Name = "colName",       HeaderText = "Name",         FillWeight = 35 };
        var colPhone      = new DataGridViewTextBoxColumn { Name = "colPhone",      HeaderText = "Phone",        FillWeight = 25 };
        var colProfession = new DataGridViewTextBoxColumn { Name = "colProfession", HeaderText = "Profession",   FillWeight = 20 };
        var colOrg        = new DataGridViewTextBoxColumn { Name = "colOrg",        HeaderText = "Organization", FillWeight = 10 };
        var colId         = new DataGridViewTextBoxColumn { Name = "colId",         HeaderText = "Id",           Visible = false };
        grid.Columns.AddRange(colNum, colName, colPhone, colProfession, colOrg, colId);

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 500);
        Controls.Add(grid);
        Controls.Add(toolStrip);
        MinimumSize = new Size(600, 300);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Client List";

        toolStrip.ResumeLayout(false);
        toolStrip.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
