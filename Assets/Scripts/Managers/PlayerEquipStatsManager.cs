using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipStatsManager : MonoBehaviour
{
    public Action UpdateStats;

    public float damage, atkSpeed, hp, def, speed, stamina;

    public void EquipItemStatsUpdate()
    {
        damage = 0;
        atkSpeed = 0;
        hp = 0;
        def = 0;
        speed = 0;
        stamina = 0;

        foreach (var item in Managers.UserData.playerEquipItemDatas)
        {
            switch(item.Key)
            {
                case ItemType.Weapon:
                    damage = item.Value.Atk;
                    atkSpeed = item.Value.AtkSpeed;
                    break;
                case ItemType.Armor:
                    hp = item.Value.Hp;
                    break;
                case ItemType.Shield:
                    def = item.Value.Def;
                    break;
                case ItemType.Boots:
                    speed = item.Value.Speed;
                    stamina = item.Value.Stamina;
                    break;
            }
        }

        UpdateStats?.Invoke();
    }
}
