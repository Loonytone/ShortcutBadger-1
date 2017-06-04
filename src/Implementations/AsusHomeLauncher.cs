using System.Collections.Generic;
using Android.Content;
using Xamarin.ShortcutBadger.Infrastructure;
using Xamarin.ShortcutBadger.Utils;

namespace Xamarin.ShortcutBadger.Implementations
{
	/**
	 * @author leolin
	 * https://github.com/leolin310148/ShortcutBadger/blob/master/ShortcutBadger/src/main/java/me/leolin/shortcutbadger/impl/AsusHomeBadger.java
	 */
	internal class AsusHomeLauncher : IShortcutBadger
	{
		private const string IntentAction = "android.intent.action.BADGE_COUNT_UPDATE";
		private const string IntentExtraBadgeCount = "badge_count";
		private const string IntentExtraPackageName = "badge_count_package_name";
		private const string IntentExtraActivityName = "badge_count_class_name";
		private const string IntentExtraBadgeVipCount = "badge_vip_count";

		#region IShortcutBadger implementation

		public void ExecuteBadge(Context context, ComponentName componentName, int badgeCount)
		{
			var intent = new Intent(IntentAction);
			intent.PutExtra(IntentExtraBadgeCount, badgeCount);
			intent.PutExtra(IntentExtraPackageName, componentName.PackageName);
			intent.PutExtra(IntentExtraActivityName, componentName.ClassName);
			intent.PutExtra(IntentExtraBadgeVipCount, 0);

			if (BroadcastHelper.CanResolveBroadcast(context, intent))
			{
				context.SendBroadcast(intent);
			}
			else
			{
				throw new ShortcutBadgeException("unable to resolve intent: " + intent);
			}
		}
		
		public IEnumerable<string> SupportLaunchers => new[] {"com.asus.launcher"};

		#endregion
	}
}