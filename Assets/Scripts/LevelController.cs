using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private int _level;
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private GameObject menuCanvas;
    public Slider progressBar;

    void Start()
    {
        _level = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartGame()
    {
        StartCoroutine(StartLoad(_level + 1));
        menuCanvas.SetActive(false);
        _loaderCanvas.SetActive(true);
    }

    IEnumerator StartLoad(int level)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        while (!asyncOperation.isDone)
        {
            progressBar.value = asyncOperation.progress;
            yield return null;
        }
    }
}