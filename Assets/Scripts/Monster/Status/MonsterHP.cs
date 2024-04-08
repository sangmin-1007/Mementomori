using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    GameObject prfHP;
    GameObject canvas;

    RectTransform hpBar;

    public float height = 1.7f;

    private void Awake()
    {
        prfHP = Resources.Load<GameObject>("Prefabs/Monster/HP_barBG");
    }

    // Start is called before the first frame update
    void Start()
    {
        hpBar = Instantiate(prfHP, canvas.transform).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
    }
}
