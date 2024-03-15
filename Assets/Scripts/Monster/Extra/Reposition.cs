//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Reposition : MonoBehaviour
//{
//    Collider2D coll;

//    private void Awake()
//    {
//        coll = GetComponent<Collider2D>();
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        Vector3 playerPos = SpawnManager.instance.player.transform.position;
//        Vector3 myPos = transform.position;
//        float diffX = Mathf.Abs(playerPos.x - myPos.x);
//        float diffY = Mathf.Abs(playerPos.y - myPos.y);

//        Vector3 playerDir = SpawnManager.instance.player.inputVec;
//        float dirX = playerDir.x < 0 ? -1 : 1;
//        float dirY = playerDir.y < 0 ? -1 : 1;

//        if (collision.tag == "Player" && coll.enabled)
//        {
//            transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
//        }
//    }
//}
