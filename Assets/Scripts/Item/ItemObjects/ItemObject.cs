using UnityEngine;

public class ItemObject : MonoBehaviour 
{

    private ItemData item;

    public int itemId;

    private SpriteRenderer spriteRenderer;

    public Transform PlayerTransform;
    private float rootDistance = 2f;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

       if( itemId != 0)
        {
            ItemSetting(itemId);

        }
    }

    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, PlayerTransform.position);

        if( Distance <= rootDistance )
        {
            Interaction();
        }
    }


    public void ItemSetting(int id)
    {
       item = DataBase.Item.Get(id);
        spriteRenderer.sprite = item.Sprite;
    }

    public void Interaction()
    {
        if(item.Type != Constants.ItemType.Consume)
        {
            Managers.DataManager.AddItem(item);
            Inventory.instance.AddItem(Managers.DataManager.playerItemData[0]);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //ObjectPool
            //°æÇèÄ¡ 
        }
      
    }
}
