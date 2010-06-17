//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Playground.V2.DataContexts;
using Playground.Visualizer.Infrastructure;
using System.Linq;
using Relinq.V2.Linq.Preprocessing;

namespace Playground.Visualizer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            showVisualizerButton_Click(this, e);
            Visible = false;
        }

        private void showVisualizerButton_Click(object sender, EventArgs eventArgs)
        {
            var ctx = new TestClientDataContext();

            int x = 2;
            var some = from c in ctx.Companies
                       where c.Employees.Count == x * x
                       select new { c.Name };
            var expr = Simplifier.Simplify(some.Expression);

            var host = new VisualizerDevelopmentHost(
                expr,
                typeof(ExpressionTreeVisualizer),
                typeof(ExpressionTreeVisualizerObjectSource));
            host.ShowVisualizer(this);
        }
    }
}