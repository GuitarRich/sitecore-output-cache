namespace Sitecore.SharedSource.OutputCache.Pipelines.RequestBegin
{
	using System.Web;

	using Diagnostics;

	using Mvc.Pipelines.Request.RequestBegin;

	public class CacheControlProcessor : RequestBeginProcessor
	{
		public override void Process(RequestBeginArgs args)
		{
			Assert.ArgumentNotNull(args, "args");
			var processor = CacheControlActivator.Current.CreateCacheControlManager();
			
			Assert.ArgumentNotNull(processor, "CacheControlManager");
			processor.AddCacheControlHeaders(args.PageContext.Item, (HttpContextWrapper)args.RequestContext.HttpContext);
		}
	}
}