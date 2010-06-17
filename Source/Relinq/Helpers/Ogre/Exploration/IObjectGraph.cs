using QuickGraph;

namespace Relinq.Helpers.Ogre.Exploration
{
    public interface IObjectGraph : IImplicitGraph<Vertex, Edge>
    {
        Vertex Root { get; }
    }
}