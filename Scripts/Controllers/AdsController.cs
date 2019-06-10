#if UNITY_ADS
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Toolkit;

namespace Toolkit.Controllers
{
    [AddComponentMenu("Toolkit/Controllers/AdsController")]
    public class AdsController : Singleton<AdsController>
    {
        public const string PlacementIdVideo = "video";
        public const string PlacementIdRewardedVideo = "rewardedVideo";

        public int rewardCoins = 200;

        public void Show(string placementId, Action<ShowResult> resultCallback)
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = resultCallback;

            Advertisement.Show(placementId, options);
        }

        public void ShowVideo(Action<ShowResult> resultCallback)
        {
            Show(PlacementIdVideo, resultCallback);
        }

        public void ShowRewardedVideo(Action<ShowResult> resultCallback)
        {
            Show(PlacementIdRewardedVideo, resultCallback);
        }

        public bool IsReady(string placementId)
        {
            return Advertisement.IsReady(placementId);
        }

        public bool IsVideoReady()
        {
            return IsReady(PlacementIdVideo);
        }

        public bool IsRewardedVideoReady()
        {
            return IsReady(PlacementIdRewardedVideo);
        }
    }
}
#endif
