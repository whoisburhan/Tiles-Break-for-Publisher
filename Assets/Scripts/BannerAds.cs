using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class BannerAds : MonoBehaviour
{
#if UNITY_ANDROID
    const string gameID = "3643881";
#endif
#if UNITY_IOS
    const string gameID = "3643880";
#endif

    string placement = "bannerAds";

    bool testMode = false;

    void Start()
    {
        if(!Advertisement.isInitialized)
            Advertisement.Initialize(gameID, testMode);

        StartCoroutine(ShowBannerWhenInitialized());
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placement);
        /*if (Advertisement.Banner.isLoaded)
        {
            Debug.Log("YES ITS LOADED");
            Advertisement.Banner.Show(placement);
        }*/
    }
}
