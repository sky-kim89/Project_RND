using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProjecktExtnesions;
using DG.Tweening;
using TMPro;

public class General : Unit
{
    [SerializeField]
    private GameObject m_SoldierPrefab = null;

    private Soldier m_Soldier = null;

    private List<Skill> m_Skill = new List<Skill>();

    public void Init(SaveUnitData generalData, SaveUnitData soldierData = null)
    {
        Data = UnitRandomMachine.GetUnitData(generalData);
    }
}