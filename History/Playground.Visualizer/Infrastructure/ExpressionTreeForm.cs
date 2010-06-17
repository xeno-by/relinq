using System;
using System.Drawing;
using System.Windows.Forms;

namespace Playground.Visualizer.Infrastructure
{
    public class TreeWindow : Form
    {
        private TextBox errorMessageBox;
        private TreeBrowser browser;
        private string errors;

        private void InitializeComponent()
        {
            this.errorMessageBox = new TextBox();


            this.SuspendLayout();

            // 
            // errorMessageBox
            // 
            this.errorMessageBox.Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right);
            this.errorMessageBox.Location = new Point(8, 8);
            this.errorMessageBox.Multiline = true;
            this.errorMessageBox.Name = "errorMessageBox";
            this.errorMessageBox.ReadOnly = true;
            this.errorMessageBox.ScrollBars = ScrollBars.Both;
            this.errorMessageBox.Size = new Size(280, 56);
            this.errorMessageBox.TabIndex = 1;
            this.errorMessageBox.TabStop = false;
            this.errorMessageBox.Text = this.errors;

            // 
            // browser
            // 
            this.browser.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left)
                    | AnchorStyles.Right);
            this.browser.Location = new Point(8, 72);
            this.browser.Size = new Size(280, 192);
            this.browser.TabIndex = 2;
            this.browser.ExpandAll();

            // 
            // TreeWindow
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(292, 266);
            this.Controls.AddRange(new Control[] {
                this.browser,
                this.errorMessageBox,
            });
            this.Name = "TreeWindow";
            this.Text = "Expression Tree Viewer";
            this.ResumeLayout(false);

            this.Size = new Size(600, 800);
        }

        public TreeWindow(TreeBrowser browser, string expression)
        {
            this.browser = browser;
            this.errors = expression;
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }
    }

    public class TreeBrowser : TreeView
    {
        public TreeBrowser() { }

        public void Add(TreeNode root)
        {
            Nodes.Add(root);
        }
    }
}