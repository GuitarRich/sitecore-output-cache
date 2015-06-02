namespace Sitecore.SharedSource.OutputCache
{
	using System.Web;

	using Data.Items;

	public interface ICacheControlManager
	{
		void AddCacheControlHeaders(Item item, HttpContextWrapper httpContext);
	}
}
