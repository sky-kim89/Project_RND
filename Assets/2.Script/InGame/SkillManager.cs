using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSkill : Skill
{
    //������
    //�ߵ� ����Ʈ

    //��ų�� ���� ������.
    int MinValue = 0;
    int MaxValue = 100;
}

[System.Serializable]
public class ActiveSkill : Skill
{
    public float CoolTime = 5.0f;
    public bool isCool = false;

    //��ų �ߵ�
    public override void Action(Unit target)
    {
        //��Ƽ��� �ϳ��� ���� ���� ���̱� ������ ���� �� ��...?
        isCool = true;
        DelayAction.Instance.AddDelayAction(CoolTime, Caster, () =>
        {
            isCool = false;
        });
    }

}

[System.Serializable]
public class Skill
{
    //����
    public string SkillName = string.Empty;
    //����
    public string SkillExplanation = string.Empty;

    //������
    public Sprite Icon = null;
    //�ߵ� ����Ʈ
    public GameObject Effect = null;

    //��ų�� ���� ������.
    public int MinValue = 0;
    public int MaxValue = 100;

    //������
    public Unit Caster = null;

    //��ų �ߵ�
    public virtual void Action(Unit target)
    {

    }
}

public class SaveSkillData
{
    public int Value;
}


public class SkillManager : MonoBehaviour
{

}
