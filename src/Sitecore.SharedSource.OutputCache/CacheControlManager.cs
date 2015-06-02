namespace Sitecore.SharedSource.OutputCache
{
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using System.Web;

	using Configuration;

	using Diagnostics;

	using Item = Sitecore.Data.Items.Item;

	public class CacheControlManager : ICacheControlManager
	{
		public void AddCacheControlHeaders(Item item, HttpContextWrapper httpContext)
		{
			Assert.IsNotNull(item, "item");
			Assert.IsNotNull(httpContext, "httpContext");


			int secondsToCache;
			int.TryParse(item[Data.FieldIds.OutputCacheMaxAge], out secondsToCache);

			var lastUpdated = item.Statistics.Updated;
			if (lastUpdated > DateTime.Now)
			{
				lastUpdated = DateTime.Now;
			}

			if (secondsToCache == 0)
			{
				// Calculate the max age base on the last updated date of the item
				var timeSinceLastUpdate = DateTime.Now - lastUpdated;
				if (timeSinceLastUpdate.TotalHours < 4)
				{
					// Cache for a 5 minutes
					secondsToCache = Settings.GetIntSetting("SharedSource.OutputCache.TTL.LessThan4HoursOld", 300);
				}
				else if (timeSinceLastUpdate.TotalHours < 24)
				{
					// Cache for 1 hour
					secondsToCache = Settings.GetIntSetting("SharedSource.OutputCache.TTL.LessThan24HoursOld", 3600);
				}
				else
				{
					// over 24 hours, cache for 14 days
					secondsToCache = Settings.GetIntSetting("SharedSource.OutputCache.TTL.MoreThan24HoursOld", 1902600);
				}
			}

			var delta = new TimeSpan(0, 0, 0, secondsToCache);

			// Set the response headers
			httpContext.Response.AddHeader("Vary", "Accept-Encoding, User-Agent");
			httpContext.Response.Cache.SetMaxAge(delta);
			httpContext.Response.Cache.SetCacheability(HttpCacheability.Public);

			Tracer.Info("Adding Http header to indicate last modification.", "Date: " + lastUpdated + ".");
			httpContext.Response.Cache.SetLastModified(lastUpdated);
			var etag = GetMd5Sum(item.Statistics.Updated.ToString("O"));
			httpContext.Response.AddHeader("Etag", string.Format("\"{0}\"", etag));
		}

		// Create an md5 sum string of this string
		private static string GetMd5Sum(string str)
		{
			// First we need to convert the string into bytes, which
			// means using a text encoder.
			var enc = Encoding.Unicode.GetEncoder();

			// Create a buffer large enough to hold the string
			var unicodeText = new byte[str.Length * 2];
			enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

			// Now that we have a byte array we can ask the CSP to hash it
			MD5 md5 = new MD5CryptoServiceProvider();
			var result = md5.ComputeHash(unicodeText);

			// Build the final string by converting each byte
			// into hex and appending it to a StringBuilder
			var sb = new StringBuilder();
			for (var i = 0; i < result.Length; i++)
			{
				sb.Append(result[i].ToString("X2"));
			}

			// And return it
			return sb.ToString();
		}
	}
}
