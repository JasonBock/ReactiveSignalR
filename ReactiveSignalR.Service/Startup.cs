using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace ReactiveSignalR.Service
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var signalRConfig = new HubConfiguration();
			var httpConfig = new HttpConfiguration();
			httpConfig.Routes.MapHttpRoute(
				 name: "DefaultApi",
				 routeTemplate: "api/{controller}/{id}",
				 defaults: new { id = RouteParameter.Optional }
			);

			var builder = new ContainerBuilder();
			builder.RegisterHubs(Assembly.GetExecutingAssembly()).SingleInstance();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			var container = builder.Build();

			httpConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			signalRConfig.Resolver = new AutofacDependencyResolver(container);

			app.UseWebApi(httpConfig);
			app.MapSignalR(signalRConfig);


			var options = new FileServerOptions
			{
				EnableDirectoryBrowsing = false,
				EnableDefaultFiles = false,
				FileSystem = new PhysicalFileSystem("Content")
			};

			app.UseFileServer(true);
		}
	}
}
