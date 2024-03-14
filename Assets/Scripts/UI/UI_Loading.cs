using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : UI_Base<UI_Loading>
{
    [Header("бс CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("бс Image")]
    [SerializeField] private Image progressBar;

    string loadSceneName;

    public override void OnEnable()
    {
        base.OnEnable();

        loadSceneName = Managers.UI_Manager.sceneName;

        LoadScene();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime * 0.5f;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if(progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == loadSceneName)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            StartCoroutine(Fade(false));
        }
    }

    private IEnumerator Fade(bool isFadein)
    {
        if (isFadein)
        {
            yield return null;
            canvasGroup.DOFade(1f, 1f);
        }
        else
        {
            yield return null;
            canvasGroup.DOFade(0f, 1f);
        }

        yield return new WaitForSeconds(2f);
        if (!isFadein)
        {
            CloseUI();
            yield break;
        }
    }
}
