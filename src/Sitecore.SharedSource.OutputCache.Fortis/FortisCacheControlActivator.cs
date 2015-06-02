namespace Sitecore.SharedSource.OutputCache.Fortis
{
	using Microsoft.Practices.ServiceLocation;

	public class FortisCacheControlActivator : CacheControlActivator
	{
		public override ICacheControlManager CreateCacheControlManager()
		{
			return ServiceLocator.Current.GetInstance<ICacheControlManager>();
		}
	}
}
