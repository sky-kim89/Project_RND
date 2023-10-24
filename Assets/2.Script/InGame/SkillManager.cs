using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSkill : Skill
{
    //아이콘
    //발동 이팩트

    //스킬의 랜덤 범위값.
    int MinValue = 0;
    int MaxValue = 100;
}

[System.Serializable]
public class ActiveSkill : Skill
{
    public float CoolTime = 5.0f;
    public bool isCool = false;

    //스킬 발동
    public override void Action(Unit target)
    {
        //액티브는 하나만 갖고 있을 것이기 때문에 가능 할 듯...?
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
    //네임
    public string SkillName = string.Empty;
    //설명
    public string SkillExplanation = string.Empty;

    //아이콘
    public Sprite Icon = null;
    //발동 이팩트
    public GameObject Effect = null;

    //스킬의 랜덤 범위값.
    public int MinValue = 0;
    public int MaxValue = 100;

    //시전자
    public Unit Caster = null;

    //스킬 발동
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
