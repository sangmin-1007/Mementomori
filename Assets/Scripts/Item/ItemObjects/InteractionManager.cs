using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Collections;

public interface IInteractable  //인터페이스
{ 
    void OnInteract();
}
public class InteractionManager : MonoBehaviour
{


    public LayerMask layerMask;
    private GameObject curInteractGameobject;
    private IInteractable curInteractable;

    public Text promptText;



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item")) // 레이어가 "interactable"인 아이템과 충돌했을 경우
        {
            curInteractGameobject = collision.gameObject;
            curInteractable = curInteractGameobject.GetComponent<IInteractable>();
            GetItem();
            StartCoroutine(GetItemTextCourtine());

        }

    }
    // 코루틴 코드 추가 필요

    private IEnumerator GetItemTextCourtine()
    {
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        promptText.gameObject.SetActive(false);
        yield break;
    }

    private void SetPromptText() // [space] 집어넣기 상호작용 표시 텍스트
    {
        promptText.gameObject.SetActive(true);

    }

    private void ClosePromptText() // [space] 집어넣기 상호작용 표시 텍스트
    {
        promptText.gameObject.SetActive(false);

    }

    public void GetItem()  // 스페이스키, 획득이  눌렸을때 
    {

        curInteractable.OnInteract();
        curInteractGameobject = null;
        curInteractable = null;
        promptText.gameObject.SetActive(false);



    }
}
