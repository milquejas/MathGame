using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Handle fadeout
 * Handle scene change states:
 * https://www.youtube.com/watch?v=OmobsXZSRKo
 * Handle Saving?
 * Check which device plays?
 * versioning [Major build number].[Minor build number].[Revision]
*/

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private string m_DeviceType;
    [SerializeField] private string version;

    [SerializeField] private FadeToBlack fadeOut;
    [SerializeField] private TMP_Text versionText;

    [SerializeField] private float fadeTime;
    void Start()
    {
        CheckDevice();
        versionText.text = version;
        LoadScene("IsometricMain");
    }

    private void Awake()
    {
        if (instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAndFadeout(sceneName));
    }

    private IEnumerator LoadSceneAndFadeout(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        // wait for fadout before scene change
        fadeOut.ToggleFadeToBlack(fadeTime);
        yield return new WaitForSeconds(fadeTime);

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            // could put loading screen things here

            yield return null;
        }
    }

    private string CheckDevice()
    {
        //Check if the device running this is a console
        if (SystemInfo.deviceType == DeviceType.Console)
        {
            //Change the text of the label
            m_DeviceType = "Console";
        }

        //Check if the device running this is a desktop
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            m_DeviceType = "Desktop";
        }

        //Check if the device running this is a handheld
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            m_DeviceType = "Handheld";
        }

        //Check if the device running this is unknown
        if (SystemInfo.deviceType == DeviceType.Unknown)
        {
            m_DeviceType = "Unknown";
        }
        return m_DeviceType;
    }
    
}
