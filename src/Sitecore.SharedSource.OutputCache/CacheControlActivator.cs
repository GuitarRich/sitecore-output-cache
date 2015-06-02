namespace Sitecore.SharedSource.OutputCache
{
	using System;

	public class CacheControlActivator
	{
		private static CacheControlActivator current = new CacheControlActivator();

		public static CacheControlActivator Current
		{
			get { return current; }
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				current = value;
			}
		}

		public virtual ICacheControlManager CreateCacheControlManager()
		{
			return new CacheControlManager();
		}
	}
}
