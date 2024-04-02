using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDB : MonoBehaviour
{
    private Dictionary<int, SkillData> _skills = new();
    // int���� _idŰ��(10001001)�� �־��ش�.

    public SkillDB() //_items�� SO�� �־��ִ� �ڵ� 
    {
        var res = Resources.Load<SkillDBSheet>("DB/SkillDBSheet"); // item Scriptable Object�� �ҷ���
        var skillsSo = UnityEngine.Object.Instantiate(res);               // �ε带 ���ְ� instantiate�� ����
        var entites = skillsSo.Entities;                      // Entities�� ������ ����, (Entities���� Scriptable Object�ȿ� ������� �ִ�)

        if (entites == null || entites.Count <= 0)
            return;

        var entityCount = entites.Count; //Length�ε� ��������� �����Ͱ� ���⶧���� Count�� ���
        for (int i = 0; i < entityCount; i++)  //for������ �����鼭 SO�� �ִ� �����͸� Dictionary�� �����صд�.
        {
            var skill = entites[i];

            if (_skills.ContainsKey(skill.Id))
                _skills[skill.Id] = skill;
            else
                _skills.Add(skill.Id, skill);

        }


    }
    public SkillData GetID(int id) // id�� ���� ���ϴ� �����ͷ� �����ϴ� �Լ�
    {
        if (_skills.ContainsKey(id))
            return _skills[id];

        return null;
    }
}
