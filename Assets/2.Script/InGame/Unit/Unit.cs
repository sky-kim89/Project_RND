using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProjecktExtnesions;
using DG.Tweening;
using TMPro;
public enum eUnitActionType
{
    None = -1,
    Attack = 0,
    Hit = 1,
    Hiting = 2,
    Move = 3,
    Die = 4,
    Kill = 5,
    Start = 6,
    Skilling = 7,
    Dieing = 9

}

public enum eJobType
{
    Center = -1,
    Jobless = 0,
    Warrior = 1,
    Archer = 2,
    Magician = 3,
    Shield = 4,

    List = 5
}

public enum eGradeType
{
    Common = 1,
    UnCommon = 2,
    Rare = 3,
    Unique = 4,
    Epic = 5,

    List = 6
}

public enum eTribe
{
    Human = 0,
    Elf = 1,
    Dwarf = 2,

    List = 3

}

[System.Serializable]
public class UnitData
{
    public eGradeType Grade = eGradeType.Common;
    public eTribe Tribe = eTribe.Human;
    //종족. 필요.
    public string Name = string.Empty;

    public int Power = 1;
    public int Stamina = 1;
    public int Normal = 1;
    public int Level = 1;

    public int AddLevel = 1;
    public int UpGrade = 0;

    public int Count = 2;
    public int MaxCount = 2;

    public int DamageDrainage = 1000;

    public int Exp = 0;

    public float AttackRange = 1;
    public float AttackSpeed = 1.0f;
    public float Speed = 1;

    public int AP { get { return (int)((float)(Power + (int)Grade) * (1.0f + (float)Level * (0.1f + (float)Grade * 0.01f)) * (1f + (float)Grade * 0.1f)); } }
    public int HP { get { return (int)((float)(Stamina + (int)Grade) * (1.0f + (float)Level * (0.1f + (float)Grade * 0.01f)) * (10 + (int)Grade)) + 100; } }
    public int GP { get { return (int)((float)(Normal + (int)Grade) * (1.0f + (float)Level * (0.1f + (float)Grade * 0.01f)) * (1f + (float)Grade * 0.1f)); } }
    public int DP
    {
        get
        {
            return (int)((Level * 2) * ((float)Grade * 0.1f + 1));
        }
    }

    //public void Init(SaveUnitData data)
    //{
    //    UnitData UnitData = UnitRandomMachine.GetUnitData(data);
    //    Grade = UnitData.Grade;
    //    Tribe = UnitData.Tribe;
    //    Name = UnitData.Name;

    //    Power = UnitData.Power;
    //    Stamina = UnitData.Stamina;
    //    Normal = UnitData.Normal;
    //    MaxCount = UnitData.MaxCount;
    //    //AddLevel = UnitData.AddLevel;
    //    DamageDrainage = UnitData.DamageDrainage;
    //    Exp = UnitData.Exp;
    //    AttackRange = UnitData.AttackRange;
    //    AttackSpeed = UnitData.AttackSpeed;
    //    Speed = UnitData.Speed;
    //}

    public SaveUnitData GetSaveData()
    {
        SaveUnitData saveUnitData = new SaveUnitData();
        saveUnitData.Name = Name;
        saveUnitData.AddLevel = AddLevel;
        saveUnitData.UpGrade = UpGrade;
        return saveUnitData;
    }

}

public class Unit : MonoBase
{
    public UnitData Data = null;
    [SerializeField]
    protected Transform m_Target = null;

    [SerializeField]
    protected UnitStateUI m_UnitStateUI = null;

    public eGradeType Grade = eGradeType.Common;
    public eTribe Tribe = eTribe.Human;

    public string Name = string.Empty;

    public int DamageDrainage = 1000;

    public int Exp = 0;

    public float AttackRange = 1;
    public float AttackSpeed = 1.0f;
    public float Speed = 1;

    public int MaxHP = 110;
    public int HP = 110;
    public int AP = 1;

    public bool isEnemy = false;

    public bool IsAttackCoolTime = false;

    public eUnitActionType ActionType = eUnitActionType.Start;


    public virtual void InitData(UnitData data)
    {
        Data = data ;
        Name = Data.Name;
        HP = Data.HP;
        MaxHP = HP;
        AP = Data.AP;
        Speed = Data.Speed;

        AttackRange = Data.AttackRange;
        AttackSpeed = Data.AttackSpeed;
        DamageDrainage = Data.DamageDrainage;

        Grade = Data.Grade + Data.UpGrade;
        Tribe = Data.Tribe;

        //transform.Activate();

        m_UnitStateUI.SetEnemy(isEnemy);
    }

    public virtual void Attack()
    {
        if (ActionType != eUnitActionType.Hiting && IsAttackCoolTime.IsFalse())
        {
            Unit target = m_Target.GetComponent<Unit>();
            ActionType = eUnitActionType.Attack;

            target.Hit(CreateDamage());

            DelayAction(AttackSpeed, () =>
            {
                IsAttackCoolTime = false;
                ActionType = eUnitActionType.None;
            });
        }
    }

    public virtual Damage CreateDamage()
    {
        int ap = (int)((float)AP * (float)DamageDrainage * 0.001f);
        return new Damage(this, ap);
    }

    //피격
    public virtual void Hit(Damage damage)
    {
        if (ActionType != eUnitActionType.Dieing)
        {
            ActionType = eUnitActionType.Hit;

            HP -= damage.DamagePoint;
            float hp = (float)HP / (float)MaxHP;

            m_UnitStateUI.SetHP(hp);
            m_UnitStateUI.Hit(damage.DamagePoint);
            if (HP <= 0)
            {
                Die(damage.Unit);
            }
        }
    }
    public virtual void Hit_End(float time)
    {
        DelayAction(time, () =>
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            ActionType = eUnitActionType.Hit;
        });
    }

    //죽음
    public virtual void Die(Unit unit)
    {
        if (unit.IsNotNull())
        {
            unit.Kill(this);
        }

        gameObject.DisableCollider();

        ActionType = eUnitActionType.Dieing;

        //Debug.Log("Die : " + Data.Name);
        DelayAction(0.5f, () =>
        {
            ActionType = eUnitActionType.Die;
            gameObject.Deactivate();
            m_UnitStateUI.Deactivate();
        });
    }

    //죽임
    public virtual void Kill(Unit unit)
    {
        ActionType = eUnitActionType.Kill;
    }

    //이동
    public virtual void Move(float speed)
    {
        if (ActionType != eUnitActionType.Hiting && ActionType != eUnitActionType.Attack && ActionType != eUnitActionType.Skilling)
        {
            if (m_Target.IsNull())
            {
                if (ActionType != eUnitActionType.Move)
                {
                    ActionType = eUnitActionType.Move;
                }

                transform.rotation = Quaternion.identity;
            }
            else
            {
                Quaternion temp = Quaternion.LookRotation(transform.position - m_Target.transform.position);
                transform.rotation = Quaternion.Euler(0, temp.eulerAngles.y, 0);
            }

            transform.Translate(Vector3.back * Time.deltaTime * speed); //전진
        }
    }

    public virtual void Respawn()
    {
        gameObject.EnableCollider();
        m_UnitStateUI.Respawn();
        InitData(Data);
    }

    private void OnEnable()
    {
        ActionType = eUnitActionType.Start;
    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        if (isEnemy)
        {
            transform.tag = "Enemy";
        }
        else
        {
            transform.tag = "Unit";
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Target = FindTarget();

        if (InUnitToAttackRange())
        {
            Attack();
        }
        else
        {
            Move(Speed);
        }
    }

    protected virtual Transform FindTarget()
    {
        Unit target = null;
        if (isEnemy)
        {
            target = InGameManager.Instance.UserUnits.FindNearUnit(transform.position, AttackRange);
        }
        else
        {
            target = InGameManager.Instance.EnemyUnits.FindNearUnit(transform.position, AttackRange);
        }

        return target.IsNotNull() ? target.transform : null;
    }

    protected virtual bool InUnitToAttackRange()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, -transform.forward, AttackRange);
        //if (Physics.Raycast(transform.position, -transform.forward, out hit, Data.AttackRange))
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Unit unit = hits[i].transform.GetComponent<Unit>();
                if (unit.IsNotNull() && isEnemy != unit.isEnemy)
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected virtual void BuffActives()
    {
        //for(int i = 0; i < m_Buffs.Count; i++)
        //{

        //}
    }
}

