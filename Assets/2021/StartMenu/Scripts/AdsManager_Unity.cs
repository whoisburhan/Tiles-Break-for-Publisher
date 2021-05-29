using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace GS.CommonStuff
{
    public class AdsManager_Unity : MonoBehaviour, IUnityAdsListener
    {
        public static AdsManager_Unity Instance { get; private set; }

        public static event Action OnRewardGiving;
        public static event Action OnRewardCancel;
        public static event Action OnRewardError;

        private bool testMode = false;

        public const string REWARDED_VIDEO_PLACEMENT = "rewardedVideo";
        public const string NORMAL_VIDEO_PLACEMENT = "video";

        #if UNITY_ANDROID
        private const string GAME_ID = "3643881";
#endif
#if UNITY_IOS
        private const string GAME_ID = "3643880";
#endif


        private void Awake()
        {          
            if (Instance != null)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            Advertisement.AddListener(this);

            if (!Advertisement.isInitialized)
                Advertisement.Initialize(GAME_ID, testMode);
        }

        public bool IsReady(string placementId)
        {
            return Advertisement.IsReady(placementId);
        }

        public void ShowRewardedVideo()
        {
            if (Advertisement.IsReady(REWARDED_VIDEO_PLACEMENT))
            {
                Advertisement.Show(REWARDED_VIDEO_PLACEMENT);
            }
        }

        public void OnUnityAdsReady(string placementId)
        {

        }

        public void OnUnityAdsDidError(string message)
        {

        }

        public void OnUnityAdsDidStart(string placementId)
        {

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                OnRewardGiving?.Invoke();
            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
                OnRewardCancel?.Invoke();
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
                OnRewardError?.Invoke();
            }
        }

        public void OnDestroy()
        {
            Advertisement.RemoveListener(this);
        }
    }
}
