using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = fadePanel.color;
        c.a = 1f;
        fadePanel.color = c;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = 1f - (elapsed / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        c.a = 0f;
        fadePanel.color = c;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float elapsed = 0f;
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = elapsed / fadeDuration;
            fadePanel.color = c;
            yield return null;
        }

        c.a = 1f;
        fadePanel.color = c;
        SceneManager.LoadScene(sceneName);
    }
}