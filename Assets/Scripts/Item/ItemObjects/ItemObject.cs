using UnityEngine;

public class ItemObject : MonoBehaviour 
{

    private ItemData item;

    public int itemId;

    private SpriteRenderer spriteRenderer;

    private Transform playerTransform;
    private float rootDistance = 1f;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerTransform = Managers.GameSceneManager.Player.GetComponent<Transform>();

       if( itemId != 0)
        {
            ItemSetting(itemId);

        }
    }

    private void Update()
    {
        float Distance = Vector3.Distance(transform.position, playerTransform.position);

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
            Managers.ItemObjectPool.DisableItem(gameObject);
        }
        else
        {
            Managers.ItemObjectPool.DisableItem(gameObject);
            Debug.Log("°æÇèÄ¡ È¹µæ");
            //ObjectPool
            //°æÇèÄ¡ 
        }
      
    }
}
