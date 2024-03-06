using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPlay;
    [SerializeField] private GameObject menuLoading;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private int levelLoad;

    public void PlayGame()
    {
     menuPlay.SetActive(false);
        menuLoading.SetActive(true);
        StartCoroutine(LoadLevelAsync(levelLoad));
    }

    IEnumerator LoadLevelAsync(int levelLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelLoad);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
