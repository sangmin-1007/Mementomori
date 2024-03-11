using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{

  
    [SerializeField] private int _id;
    [SerializeField] private string _grade;
    [SerializeField] private string _type;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _spritePath;
    [SerializeField] private float _value1;
    [SerializeField] private float _value2;
    [SerializeField] private float _value3;
    [SerializeField] private float _value4;
    [SerializeField] private float _value5;
    [SerializeField] private float _value6;


    public int Id => _id;
    public string Grade => _grade;
    public string Type => _type;
    public string Name => _name;
    public string Description => _description;

    public string SpritePath => _spritePath;
    public float Value1=> _value1;

    public float Value2 => _value2;
    public float Value3 => _value3;
    public float Value4 => _value4;
    public float Value5 => _value5;
    public float Value6 => _value6;

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
