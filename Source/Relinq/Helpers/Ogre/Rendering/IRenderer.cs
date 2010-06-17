using Relinq.Helpers.Ogre.Exploration;

namespace Relinq.Helpers.Ogre.Rendering
{
    public interface IRenderer<T>
    {
        T Context { get; set; }
        void Render(IObjectGraph graph);
    }
}