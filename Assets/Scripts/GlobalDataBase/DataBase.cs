using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ܺο��� DataBase.item�� �̿��ϸ� ItemDB�� �����̰���, �ű⼭ Get�� ġ�� ���ϴ� �ڵ� ���డ��
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


