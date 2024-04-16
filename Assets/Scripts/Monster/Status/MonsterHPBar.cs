using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
    GameObject hpBarPrf;
    Canvas canvas;
    RectTransform hpBar;
    Slider hpBarSlider;
    PlayerStatsHandler hpMax;
    HealthSystem hp;

    public float height = 1f;

    private void Awake()
    {
        hpBarPrf = Resources.Load<GameObject>("Prefabs/Monster/HP_barBG");
        canvas = GetComponentInChildren<Canvas>();
        hpBar = Instantiate(hpBarPrf, canvas.transform).GetComponent<RectTransform>();
        hpBarSlider = GetComponentInChildren<Slider>();
        hpMax = GetComponent<PlayerStatsHandler>();
        hp = GetComponent<HealthSystem>();
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;

        hpBarSlider.value = hp.CurrentHealth / hpMax.CurrentStates.maxHealth;
    }
}
