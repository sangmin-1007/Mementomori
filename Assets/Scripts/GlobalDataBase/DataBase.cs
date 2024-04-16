using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//외부에서 DataBase.item을 이용하면 ItemDB로 접근이가능, 거기서 Get을 치면 원하는 코드 실행가능
public class DataBase : MonoBehaviour
{
    private static ItemDB _item;

    public static ItemDB Item 
    {  
        get
        {
            if( _item == null )
                _item = new ItemDB();

            return _item;
        }
            
    }
}


