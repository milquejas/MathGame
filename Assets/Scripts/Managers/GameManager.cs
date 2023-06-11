using System;
using System.Collections;
using TMPro;
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
    public static GameManager GameManagerInstance;

    private string m_DeviceType;
    private IEnumerator coroutine = null;

    [SerializeField] private string version;

    [SerializeField] private FadeToBlack fadeOut;
    [SerializeField] private TMP_Text versionText;

    [SerializeField] private float fadeTime;
    [Header("Keeps spawn locations permanent in isometric scene...")]
    [SerializeField] private LevelSpawnSO spawnPoint;
    void Start()
    {
        CheckDevice();
        versionText.text = version;
    }

    //temp 
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LoadScene("IsometricMain");
        }
    }

    private void Awake()
    {
        if (GameManagerInstance is not null)
        {
            Destroy(gameObject);
            return;
        }
        
        GameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Nulls on disable to keep domain reload off :)
    private void OnDisable()
    {
        GameManagerInstance = null;
    }

    public void LoadScene(string sceneName)
    {
        if (coroutine is not null) return;

        coroutine = LoadSceneAndFadeout(sceneName);
        StartCoroutine(coroutine);
    }

    private IEnumerator LoadSceneAndFadeout(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        // wait for fadout before scene change
        fadeOut.ToggleFadeToBlack(fadeTime);

        yield return new WaitForSeconds(fadeTime + 0.2f);

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;

                fadeOut.ToggleFadeToBlack(fadeTime);
            }
            // could put loading screen things here

            // set to null so level swap can happen again
            coroutine = null;

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
