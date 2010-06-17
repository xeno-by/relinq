using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Playground.Visualizer.Infrastructure;

[assembly: DebuggerVisualizer(typeof(ExpressionTreeVisualizer), typeof(ExpressionTreeVisualizerObjectSource), Target = typeof(Expression), Description = "Expression Tree Visualizer")]

namespace Playground.Visualizer.Infrastructure
{
    public class ExpressionTreeVisualizer : DialogDebuggerVisualizer
    {
        private IDialogVisualizerService modalService;

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {

            modalService = windowService;
            if (modalService == null)
                throw new NotSupportedException("This debugger does not support modal visualizers");

            ExpressionTreeContainer container = (ExpressionTreeContainer)objectProvider.GetObject();
            TreeBrowser browser = new TreeBrowser();
            browser.Add(container.Tree);
            TreeWindow treeForm = new TreeWindow(browser, container.Expression);
            modalService.ShowDialog(treeForm);
        }
    }
}