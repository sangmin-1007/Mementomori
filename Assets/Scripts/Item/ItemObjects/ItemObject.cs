using UnityEngine;
using UnityEngine.UIElements;

public class ItemObject : MonoBehaviour 
{

    private ItemData item;

    public int itemId;

    private SpriteRenderer spriteRenderer;

    private Transform playerTransform;
    private PlayerStatsHandler playerStats;
    private float rootDistance = 0.5f;
    private float magnetDistance = 3f;

    private float moveSpeed;

    private Level level;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerTransform = Managers.GameSceneManager.Player.GetComponent<Transform>();
        playerStats = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
        level = Managers.GameSceneManager.Player.GetComponent<Level>();

        moveSpeed = (playerStats.CurrentStates.speed + 2f);

       if ( itemId != 0)
        {
            ItemSetting(itemId);

        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float Distance = Vector3.Distance(transform.position, playerTransform.position);

        if( Distance <= rootDistance )
        {
            Interaction();
        }

        if (Distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }


    public void ItemSetting(int id)
    {
       item = DataBase.Item.GetID(id);
        spriteRenderer.sprite = item.Sprite;
    }

    public void Interaction()
    {
        if(item.Type != Constants.ItemType.Consume)
        {
            Managers.UserData.AddItem(item);
            //Inventory.instance.AddItem(Managers.DataManager.playerItemData[0]);
            Managers.ItemObjectPool.DisableItem(gameObject);
        }
        else
        {
            Managers.ItemObjectPool.DisableItem(gameObject);
            //Debug.Log("경험치 획득");
            Debug.Log($"경험치 : {level.expriecne}, 레벨 : {level.level}");
            level.IncreaseExprience(50);
            //level.IncreaseExprience(item.Exp);
            //ObjectPool
            //경험치 
        }
      
    }
}
