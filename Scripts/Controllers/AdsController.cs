#if UNITY_ADS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Toolkit;

namespace Toolkit.Controllers
{
	[AddComponentMenu ("Toolkit/Controllers/AdsController")]
	public class AdsController : Singleton<AdsController>
	{
		public const string placementIdVideo = "video";
		public const string placementIdRewardedVideo = "rewardedVideo";

		public int rewardCoins = 200;

		public void Show (string placementId, Action<ShowResult> resultCallback)
		{
			ShowOptions options = new ShowOptions ();
			options.resultCallback = resultCallback;

			Advertisement.Show (placementId, options);
		}

		public void ShowVideo (Action<ShowResult> resultCallback)
		{
			Show (placementIdVideo, resultCallback);
		}

		public void ShowRewardedVideo (Action<ShowResult> resultCallback)
		{
			Show (placementIdRewardedVideo, resultCallback);
		}

		public bool IsReady (string placementId)
		{
			return Advertisement.IsReady (placementId);
		}

		public bool IsVideoReady ()
		{
			return IsReady (placementIdVideo);
		}

		public bool IsRewardedVideoReady ()
		{
			return IsReady (placementIdRewardedVideo);
		}
	}
}
#endif
