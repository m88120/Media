namespace AllYouMedia.DataAccess.InfrastructureLayer
{
    using System.Web.Mvc;
    using Autofac;
    using AllYouMedia.DataAccess.DataLayer;
    using AllYouMedia.DataAccess.EntityLayer;
    using AllYouMedia.DataAccess.ServiceLayer;
    using AllYouMedia;
    using AllYouMedia.Models;
    using AllYouMedia.DataAccess.ServiceLayer.Interface;

    internal class DependencyConfigure
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(new Dependency(RegisterServices(builder)));
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).PropertiesAutowired();

            ////deal with your dependencies here
            builder.RegisterType<DataContext>().As<IDbContext>().InstancePerDependency();

            builder.RegisterGeneric(typeof(RepositoryService<>)).As(typeof(IRepository<>));
            builder.RegisterType(typeof(AspNetUserRepositoryService)).As(typeof(IRepository<AspNetUser>));
            builder.RegisterType(typeof(AspNetUserRoleRepositoryService)).As(typeof(IRepository<AspNetUserRole>));
            builder.RegisterType(typeof(AspNetRoleRepositoryService)).As(typeof(IRepository<AspNetRole>));
            builder.RegisterType<Microsoft.AspNet.Identity.EntityFramework.UserStore<AspNetUser, AspNetRole, long, AspNetUserLogin, AspNetUserRole, AspNetUserClaim>>().As(typeof(Microsoft.AspNet.Identity.IUserStore<AllYouMedia.Models.AspNetUser, long>));
            builder.RegisterType<DataContext>().As<System.Data.Entity.DbContext>();

            builder.RegisterType<AspNetUserService>().As<IAspNetUserService>();
            builder.RegisterType<AdminService>().As<IAdminService>();
            builder.RegisterType<CategoryTypeService>().As<ICategoryTypeService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<GenderService>().As<IGenderService>();
            builder.RegisterType<InstrumentService>().As<IInstrumentService>();
            builder.RegisterType<InstrumentSpecificationService>().As<IInstrumentSpecificationService>();
            //builder.RegisterType<role>().As<ICategoryService>();
            return
                builder.Build();
        }
    }
}