using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
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
			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				 name: "DefaultApi",
				 routeTemplate: "api/{controller}/{id}",
				 defaults: new { id = RouteParameter.Optional }
			);

			var builder = new ContainerBuilder();
			builder.RegisterHubs(Assembly.GetExecutingAssembly()).SingleInstance();
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			var container = builder.Build();

			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			signalRConfig.Resolver = new AutofacDependencyResolver(container);

			app.UseAutofacMiddleware(container);
			app.UseAutofacWebApi(config);
			app.UseWebApi(config);
			app.MapSignalR(signalRConfig);
		}
	}
}
