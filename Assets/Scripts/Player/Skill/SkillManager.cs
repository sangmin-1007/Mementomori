using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.VR;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public SkillSO data;
    public int skillLevel;
    public SkillController skill_1;

    [SerializeField] private List<PlayerStats> statsModifier;
    private HealthSystem healthSystem;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;


    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.skillIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.skillName;

    }

    void OnEnable()
    {
        textLevel.text = "Lv." + skillLevel;

        switch(data.skillType)
        {
            case SkillSO.SkillType.Melee:
            case SkillSO.SkillType.Range:
                textDesc.text = string.Format(data.skillDesc, data.damages[Mathf.Min(4,skillLevel)], data.counts[Mathf.Min(4,skillLevel)]);
                break;
            case SkillSO.SkillType.Statup:
            case SkillSO.SkillType.AttackSkill:
            case SkillSO.SkillType.DefenseSkill:
            case SkillSO.SkillType.Heal:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
            default:
                textDesc.text = string.Format(data.skillDesc, data.damages[skillLevel]);
                break;
        }
        
    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            case SkillSO.SkillType.Melee:
            case SkillSO.SkillType.Range:
                if(skillLevel == 0)
                {
                    GameObject newWeapon = new GameObject();
                    skill_1 = newWeapon.AddComponent<SkillController>();
                    skill_1.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage = data.damages[skillLevel];
                    nextCount += data.counts[skillLevel];

                    skill_1.LevelUp(nextDamage,nextCount);
                }
                break;
            case SkillSO.SkillType.Statup:
                healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
                healthSystem.ChangeHealth(data.damages[skillLevel]);
                PlayerStatsHandler statsHandler = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
                foreach (PlayerStats stat in statsModifier)
                {
                    statsHandler.AddStatModifire(stat);
                }
                break;
            case SkillSO.SkillType.AttackSkill:
                PlayerStatsHandler statsHandler1 = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
                foreach (PlayerStats stat in statsModifier)
                {
                    statsHandler1.AddStatModifire(stat);
                }
                break;
            case SkillSO.SkillType.DefenseSkill:
                PlayerStatsHandler statsHandler2 = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
                foreach (PlayerStats stat in statsModifier)
                {
                    statsHandler2.AddStatModifire(stat);
                }
                break;
            case SkillSO.SkillType.Heal:
                healthSystem = Managers.GameSceneManager.Player.GetComponent<HealthSystem>();
                healthSystem.ChangeHealth(data.baseDamage);
                break;
            case SkillSO.SkillType.Money:
                Managers.UserData.acquisitionGold += (int)data.damages[skillLevel];
                break;
        }
        skillLevel++;
        if(skillLevel == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
        Time.timeScale = 1;
        Managers.UI_Manager.HideUI<UI_Skill>();
    }
}
