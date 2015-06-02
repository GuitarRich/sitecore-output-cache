namespace Sitecore.SharedSource.OutputCache
{
	using System.Web;

	using Sitecore.Data.Items;

	public interface ICacheControlManager
	{
		void AddCacheControlHeaders(Item item, HttpContextWrapper httpContext);
	}
}
