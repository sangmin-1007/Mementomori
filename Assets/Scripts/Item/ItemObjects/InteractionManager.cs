using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

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
            SetPromptText();
        }

    }
    void OnTriggerExit2D(Collider2D collision) //  레이어가 "interactable"인 아이템과 충돌했을 경우
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText() // [space] 집어넣기 상호작용 표시 텍스트
    {
        promptText.gameObject.SetActive(true);

    }

    public void OnGetItem(InputValue value)  // 스페이스키, 획득이  눌렸을때 
    {
        if (value.isPressed)
        {
            curInteractable.OnInteract();
            curInteractGameobject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);


        }
    }
}
