using Ninject;
using System.Reflection;

namespace Vitae
{
    public class VitaeNinjectKernel : StandardKernel
    {
        public VitaeNinjectKernel() : base()
        {
            this.Load(Assembly.GetExecutingAssembly());
        }
    }
}