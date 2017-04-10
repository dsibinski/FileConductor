using System.Reflection;
using Ninject;

namespace FileConductor.Ninject
{
    public class IoC
    {
        private static readonly StandardKernel Kernel = new StandardKernel();

        static IoC()
        {
            Kernel.Load(Assembly.GetExecutingAssembly());
        }

        public static T Resolve<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
