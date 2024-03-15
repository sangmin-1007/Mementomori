using UnityEngine;

public class ItemObject : MonoBehaviour , IInteractable
{
  

 


    private ItemData item;

    public int itemId;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

       if( itemId != 0)
        {
            ItemSetting(itemId);

        }
    }


    public void ItemSetting(int id)
    {
       item = DataBase.Item.Get(id);
        spriteRenderer.sprite = item.Sprite;
    }

    public void OnInteract()
    {
        
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
