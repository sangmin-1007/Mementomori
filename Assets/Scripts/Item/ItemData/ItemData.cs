using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{

  
    [SerializeField] private int _id;
  //[SerializeField] private ItemGrade _grade;
   // [SerializeField] private ItemType _type;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _spritePath;
    [SerializeField] private float _atk;
    [SerializeField] private float _atkSpeed;
    [SerializeField] private float _hp;
    [SerializeField] private float _def;
    [SerializeField] private float _speed;
    [SerializeField] private float _stamina;


    public int Id => _id;
   // public ItemGrade Grade => _grade;
//public ItemType Type => _type;
    public string Name => _name;
    public string Description => _description;

    public string SpritePath => _spritePath;

        

    public float Atk=> _atk;

    public float AtkSpeed => _atkSpeed;
    public float Hp => _hp;
    public float Def => _def;
    public float Speed => _speed;
    public float Stamina => _stamina;

    private Sprite _sprite;

    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
               _sprite = Resources.Load<Sprite>(SpritePath);
            return _sprite;
        }
    }




}
