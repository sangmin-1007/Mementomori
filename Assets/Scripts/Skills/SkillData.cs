using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillData
{


    [SerializeField] private int _id;
    [SerializeField] private int _level;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _spritePath;
    [SerializeField] private float _power;
    [SerializeField] private float _atkSpeed;
    [SerializeField] private float _def;
    [SerializeField] private float _movespeed;
    [SerializeField] private float _maxStamina;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _curHealth;
    [SerializeField] private int _objectDamage;
    [SerializeField] private int _objectSpeed;
    [SerializeField] private int _objectNum;



    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public string SpritePath => _spritePath;
    public float Power => _power;
    public float AtkSpeed => _atkSpeed;
    public float Def => _def;
    public float Movespeed => _movespeed;
    public float MaxStanmina => _maxStamina;
    public float MaxHealth => _maxHealth;
    public float CurHealth => _curHealth;
    public float ObjectDamage => _objectDamage;
    public float ObjectSpeed => _objectSpeed;
    public float ObjectNum => _objectNum;


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