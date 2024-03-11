using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{

    [SerializeField] private int _itemType;
    [SerializeField] private int _id;
    [SerializeField] private string _grade;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _stringPath;
    [SerializeField] private float _value;

    public int Type => _itemType;
    public int Id => _id;
    public string Grade => _grade;
    public string Name => _name;
    public string Description => _description;
    public float Value => _value;
        
    
        

}
