using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;
using System.Collections;

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
            GetItem();
            StartCoroutine(GetItemTextCourtine());

        }

    }
    // �ڷ�ƾ �ڵ� �߰� �ʿ�

    private IEnumerator GetItemTextCourtine()
    {
        promptText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        promptText.gameObject.SetActive(false);
        yield break;
    }

    private void SetPromptText() // [space] ����ֱ� ��ȣ�ۿ� ǥ�� �ؽ�Ʈ
    {
        promptText.gameObject.SetActive(true);

    }

    private void ClosePromptText() // [space] ����ֱ� ��ȣ�ۿ� ǥ�� �ؽ�Ʈ
    {
        promptText.gameObject.SetActive(false);

    }

    public void GetItem()  // �����̽�Ű, ȹ����  �������� 
    {

        curInteractable.OnInteract();
        curInteractGameobject = null;
        curInteractable = null;
        promptText.gameObject.SetActive(false);



    }
}
