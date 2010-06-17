using System;
using System.Collections.Generic;
using QuickGraph.Algorithms.Exploration;

namespace Relinq.Helpers.Ogre.Exploration
{
    public interface IPostprocessor : ITransitionFactory<Vertex, Edge>
    {
        Func<Vertex, IObjectGraph, IEnumerable<Edge>> Logic { get; set; }
    }
}