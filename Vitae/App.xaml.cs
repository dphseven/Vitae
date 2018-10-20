namespace Vitae
{
    using Ninject;
    using System.Reflection;
    using System.Windows;
    using Vitae.View;

    public partial class App : Application
    {
        private IKernel kernel;

        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);

            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            MainWindow = kernel.Get<ContainerView>();
            MainWindow.Show();
        }
    }
}