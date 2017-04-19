using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.DIManager
{
    /// <summary>
    /// IoC provider interface
    /// </summary>
    /// <typeparam name="TContainer">the type of IoC container</typeparam>
    public interface IDIProvider<TBuilder, TContainer> : IDisposable where TBuilder : class
    {
        void PreBuild(Action<TBuilder> builderDelegate);
        void Build();
        void AfterBuild(Action<TContainer> containerDelegate);
        TContainer GetContainer();
    }
}
