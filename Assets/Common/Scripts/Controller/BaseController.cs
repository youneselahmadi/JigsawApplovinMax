using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;

public class BaseController : MonoBehaviour {
    public GameObject gameMaster;
    public string sceneName;
    public Music.Type music = Music.Type.None;
    protected int numofEnterScene;

    protected virtual void Awake()
    {
        if (GameMaster.instance == null && gameMaster != null)
            Instantiate(gameMaster);
        
        iTween.dimensionMode = CommonConst.ITWEEN_MODE;
        CPlayerPrefs.useRijndael(CommonConst.ENCRYPTION_PREFS);

        numofEnterScene = CUtils.IncreaseNumofEnterScene(sceneName);
    }

    protected virtual void Start()
    {
        CPlayerPrefs.Save();
        JobWorker.instance.onEnterScene?.Invoke(sceneName);
        CUtils.CloseBannerAd();

#if UNITY_WSA && !UNITY_EDITOR
        StartCoroutine(SavePrefs());
#endif
        Music.instance.Play(music);
    }

    public virtual void OnApplicationPause(bool pause)
    {
        CPlayerPrefs.Save();
        if (pause == false)
        {
            Timer.Schedule(this, 0.3f, () =>
            {
              //  CUtils.ShowInterstitialAd();
            });
        }
    }

    private IEnumerator SavePrefs()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            CPlayerPrefs.Save();
        }
    }
}
