using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StartMovie : UI_Base<UI_StartMovie>
{
    [SerializeField] private RawImage startMovie;

    public override void OnEnable()
    {
        base.OnEnable();

    }

    private void Start()
    {
        StartCoroutine(StartMovie());
    }

    private IEnumerator StartMovie()
    {
        startMovie.DOFade(1f, 2f);
        yield return new WaitForSeconds(34f);

        startMovie.DOFade(0f, 2f);

        yield return new WaitForSeconds(2f);

        Managers.UI_Manager.ShowLoadingUI("LobbyScene");
    }

    public void OnDisable()
    {
        DestroyUI();
    }
}
