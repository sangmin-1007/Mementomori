using DG.Tweening;
using DG.Tweening.Core.Easing;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : UI_Base<UI_Loading>
{
    [Header("■ CanvasGroup")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("■ Image")]
    [SerializeField] private Image progressBar;

    string loadSceneName;

    public Text GameTipText;

    public string[] GameTips;

    public override void OnEnable()
    {
        base.OnEnable();

        loadSceneName = Managers.UI_Manager.sceneName;

        LoadScene();
    }

    private void Awake()
    {
        GameTips = new string[]
       {
            "게임 팁: NPC상점은 환생일이 갱신될때마다 초기화됩니다.",
            "게임 팁: 창고를 이용해서 장비들을 보관할 수 있습니다.",
            "게임 팁: 아이템의 등급이 높을수록 더 높은 능력치를 가집니다.",
            "게임 팁: 아이템의 등급은 노멀 > 레어 > 유니크 > 레전드 순입니다.",
            "게임 팁: 아이템의 등급은 동일하나 S등급의 장비가 제일 높은 효율을 자랑합니다."

       };
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(LoadSceneProcess());
        StartCoroutine(ShowRandomGameTip());
    }

    private IEnumerator ShowRandomGameTip()
    {
        string[] gameTips = GameTips;
        for (int i = 0; i < 5; i++)
        {
            int randomIndex =Random.Range(0, gameTips.Length);
            string randomGameTip = gameTips[randomIndex];

            GameTipText.text = randomGameTip;
           
            yield return new WaitForSeconds(5f);
        }
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
            CloseUI();
            canvasGroup.alpha = 0f;
            yield break;
        }
    }
}
