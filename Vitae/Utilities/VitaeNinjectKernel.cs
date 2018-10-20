using Ninject;
using System.Reflection;

namespace Vitae
{
    public class VitaeNinjectKernel : StandardKernel, IKernel
    {
        public VitaeNinjectKernel() : base()
        {
            this.Load(Assembly.GetExecutingAssembly());
        }
    }
}