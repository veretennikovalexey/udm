using System.Runtime.InteropServices;

namespace ClientListApp;

public static class Theme
{
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    public static void EnableDarkTitleBar(Form form)
    {
        int value = 1;
        DwmSetWindowAttribute(form.Handle, 20, ref value, sizeof(int));
    }

    public static readonly Color Background    = ColorTranslator.FromHtml("#1E1E1E");
    public static readonly Color Surface       = ColorTranslator.FromHtml("#252526");
    public static readonly Color InputBg       = ColorTranslator.FromHtml("#3C3C3C");
    public static readonly Color Border        = ColorTranslator.FromHtml("#474747");
    public static readonly Color TextPrimary   = ColorTranslator.FromHtml("#D4D4D4");
    public static readonly Color TextSecondary = ColorTranslator.FromHtml("#9D9D9D");
    public static readonly Color Selection     = ColorTranslator.FromHtml("#094771");
    public static readonly Color Accent        = ColorTranslator.FromHtml("#007ACC");
    public static readonly Color ButtonBg      = ColorTranslator.FromHtml("#0E639C");
    public static readonly Color AltRow        = ColorTranslator.FromHtml("#252526");

    public static void ApplyToAll(Control root)
    {
        ApplyToControl(root);
        foreach (Control child in root.Controls)
            ApplyToAll(child);
    }

    private static void ApplyToControl(Control c)
    {
        switch (c)
        {
            case DataGridView dgv:
                ApplyToGrid(dgv);
                break;
            case ToolStrip ts:
                ApplyToToolStrip(ts);
                break;
            case Button btn:
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = ButtonBg;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Accent;
                break;
            case TextBox txt:
                txt.BackColor = InputBg;
                txt.ForeColor = TextPrimary;
                txt.BorderStyle = BorderStyle.FixedSingle;
                break;
            case ComboBox cmb:
                cmb.BackColor = InputBg;
                cmb.ForeColor = TextPrimary;
                cmb.FlatStyle = FlatStyle.Flat;
                break;
            case Label lbl:
                lbl.ForeColor = TextSecondary;
                lbl.BackColor = Color.Transparent;
                break;
            default:
                c.BackColor = Surface;
                c.ForeColor = TextPrimary;
                break;
        }
    }

    private static void ApplyToGrid(DataGridView g)
    {
        g.BackgroundColor = Background;
        g.GridColor = Border;
        g.BorderStyle = BorderStyle.None;
        g.EnableHeadersVisualStyles = false;

        g.DefaultCellStyle.BackColor = Background;
        g.DefaultCellStyle.ForeColor = TextPrimary;
        g.DefaultCellStyle.SelectionBackColor = Selection;
        g.DefaultCellStyle.SelectionForeColor = TextPrimary;

        g.AlternatingRowsDefaultCellStyle.BackColor = AltRow;
        g.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;
        g.AlternatingRowsDefaultCellStyle.SelectionBackColor = Selection;
        g.AlternatingRowsDefaultCellStyle.SelectionForeColor = TextPrimary;

        g.ColumnHeadersDefaultCellStyle.BackColor = Surface;
        g.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
        g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Surface;
        g.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextPrimary;

        g.RowHeadersDefaultCellStyle.BackColor = Surface;
        g.RowHeadersDefaultCellStyle.ForeColor = TextPrimary;
    }

    private static void ApplyToToolStrip(ToolStrip ts)
    {
        ts.BackColor = Surface;
        ts.ForeColor = TextPrimary;
        ts.Renderer = new DarkToolStripRenderer();
        foreach (ToolStripItem item in ts.Items)
        {
            item.ForeColor = TextPrimary;
            item.BackColor = Surface;
            if (item is ToolStripTextBox tstb)
            {
                tstb.BackColor = InputBg;
                tstb.ForeColor = TextPrimary;
                if (tstb.Control is TextBox tb)
                {
                    tb.BackColor = InputBg;
                    tb.ForeColor = TextPrimary;
                }
            }
        }
    }
}

public class DarkToolStripRenderer : ToolStripProfessionalRenderer
{
    public DarkToolStripRenderer() : base(new DarkColorTable()) { }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        using var brush = new SolidBrush(Theme.Surface);
        e.Graphics.FillRectangle(brush, e.AffectedBounds);
    }

    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        var item = e.Item;
        if (item.Selected || item.Pressed)
        {
            var color = item.Pressed ? Theme.Accent : Theme.Selection;
            using var brush = new SolidBrush(color);
            e.Graphics.FillRectangle(brush, new Rectangle(0, 0, item.Width, item.Height));
        }
    }

    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
        var rect = new Rectangle(3, e.Item.Height / 2, e.Item.Width - 6, 1);
        using var brush = new SolidBrush(Theme.Border);
        e.Graphics.FillRectangle(brush, rect);
    }
}

public class DarkColorTable : ProfessionalColorTable
{
    public override Color ToolStripGradientBegin => Theme.Surface;
    public override Color ToolStripGradientMiddle => Theme.Surface;
    public override Color ToolStripGradientEnd => Theme.Surface;
    public override Color ImageMarginGradientBegin => Theme.Surface;
    public override Color ImageMarginGradientMiddle => Theme.Surface;
    public override Color ImageMarginGradientEnd => Theme.Surface;
    public override Color SeparatorDark => Theme.Border;
    public override Color SeparatorLight => Theme.Border;
    public override Color ButtonSelectedGradientBegin => Theme.Selection;
    public override Color ButtonSelectedGradientMiddle => Theme.Selection;
    public override Color ButtonSelectedGradientEnd => Theme.Selection;
    public override Color ButtonSelectedBorder => Theme.Accent;
    public override Color ButtonSelectedHighlight => Theme.Selection;
    public override Color ButtonSelectedHighlightBorder => Theme.Accent;
    public override Color ButtonPressedGradientBegin => Theme.Accent;
    public override Color ButtonPressedGradientMiddle => Theme.Accent;
    public override Color ButtonPressedGradientEnd => Theme.Accent;
    public override Color ButtonPressedBorder => Theme.Accent;
    public override Color ButtonPressedHighlight => Theme.Accent;
    public override Color ButtonPressedHighlightBorder => Theme.Accent;
}
