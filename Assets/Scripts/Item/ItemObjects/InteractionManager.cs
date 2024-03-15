using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public interface IInteractable  //�������̽�
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Item")) // ���̾ "interactable"�� �����۰� �浹���� ���
        {
            curInteractGameobject = collision.gameObject;
            curInteractable = curInteractGameobject.GetComponent<IInteractable>();
            SetPromptText();
        }

    }
    void OnTriggerExit2D(Collider2D collision) //  ���̾ "interactable"�� �����۰� �浹���� ���
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText() // [space] ����ֱ� ��ȣ�ۿ� ǥ�� �ؽ�Ʈ
    {
        promptText.gameObject.SetActive(true);

    }

    public void OnGetItem(InputValue value)  // �����̽�Ű, ȹ����  �������� 
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
