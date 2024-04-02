using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDB : MonoBehaviour
{
    private Dictionary<int, SkillData> _skills = new();
    // int에는 _id키값(10001001)를 넣어준다.

    public SkillDB() //_items에 SO를 넣어주는 코드 
    {
        var res = Resources.Load<SkillDBSheet>("DB/SkillDBSheet"); // item Scriptable Object를 불러옴
        var skillsSo = UnityEngine.Object.Instantiate(res);               // 로드를 해주고 instantiate로 생성
        var entites = skillsSo.Entities;                      // Entities에 접근을 해줌, (Entities에는 Scriptable Object안에 내용들이 있다)

        if (entites == null || entites.Count <= 0)
            return;

        var entityCount = entites.Count; //Length로도 사용하지만 데이터가 많기때문에 Count로 사용
        for (int i = 0; i < entityCount; i++)  //for문으로 돌리면서 SO에 있는 데이터를 Dictionary에 저장해둔다.
        {
            var skill = entites[i];

            if (_skills.ContainsKey(skill.Id))
                _skills[skill.Id] = skill;
            else
                _skills.Add(skill.Id, skill);

        }


    }
    public SkillData GetID(int id) // id를 통해 원하는 데이터로 접근하는 함수
    {
        if (_skills.ContainsKey(id))
            return _skills[id];

        return null;
    }
}
