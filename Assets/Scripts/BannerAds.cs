using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class BannerAds : MonoBehaviour
{
#if UNITY_ANDROID
    string gameID = "3643881";
#endif
#if UNITY_IOS
    string gameID = "3791504";
#endif

    string placement = "bannerAds";

    bool testMode = false;

    IEnumerator Start()
    {
        Advertisement.Initialize(gameID, testMode);

        while (!Advertisement.IsReady(placement))
            yield return null;
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placement);

    }
}
