using System;
using Relinq.Helpers.Ogre.Exploration;

namespace Relinq.Helpers.Ogre.Rendering
{
    public class Renderer<T>
    {
        public T Context { get; set; }
        public Action<T, IObjectGraph> Logic { get; set; }

        public void Render(IObjectGraph graph)
        {
            Logic(Context, graph);
        }
    }
}