using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Exploration;

namespace Relinq.Helpers.Ogre.Exploration
{
    public interface IExplorer : ITransitionFactory<Vertex, Edge>
    {
        Func<Object, IEnumerable<Pair<String, Object>>> Logic { get; set; }
    }
}