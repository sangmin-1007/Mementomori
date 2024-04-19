using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Shield,
        Boots,
        Consume
    }

    public enum ItemGrade
    {
        Normal,
        Rare,
        Unique,
        Legend,
        God
         

    }

    public class Colors
    {
        public static readonly Color[] ItemGrade = new[]
       {
            new Color(0.7f,0.7f,0.7f),
            new Color(12/255f,23/255f,217/255f,255/255f),
            new Color(152/255f,6/255f,255/255f,255/255f),
            new Color(255f,140/255f,0,255/255f),
            new Color(255,255,0,255/255f)
        };
    }
}
