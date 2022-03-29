using DevIO.Business.Models.Fornecedores;
using DevIO.Business.Models.Produtos;
using DevIO.Business.Models.Produtos.Services;
using DevIO.Infra.Data.Context;
using DevIO.Infra.Data.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace DevIO.AppMvc.App_Start
{
    public class DependecyInjectionConfig
    {
        public static void RegisterDIConfig()
        {
            var container = new Container();
            container.Options.DefaultLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        public static void InitializeContainer(Container container)
        {
            container.Register<AppDbContext>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
        }
    }
}