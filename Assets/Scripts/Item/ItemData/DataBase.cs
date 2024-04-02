using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ܺο��� DataBase.item�� �̿��ϸ� ItemDB�� �����̰���, �ű⼭ Get�� ġ�� ���ϴ� �ڵ� ���డ��
public class DataBase : MonoBehaviour
{
    private static ItemDB _item;
    private static SkillDB _skill;
    public static ItemDB Item 
    {  
        get
        {
            if( _item == null )
                _item = new ItemDB();

            return _item;
        }
            
    }

    public static SkillDB Skill
    {
        get
        {
            if (_skill == null)
                _skill = new SkillDB();

            return _skill;
        }

    }
}


