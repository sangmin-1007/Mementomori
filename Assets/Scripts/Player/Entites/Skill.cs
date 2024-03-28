using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData data;
    public int skillLevel;
    //public Weapon weapon;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + skillLevel;
    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            case SkillData.SkillType.Statup:
                break;
            case SkillData.SkillType.Addskill:
                break;
        }
        skillLevel++;
        if(skillLevel == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
