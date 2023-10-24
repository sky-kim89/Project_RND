using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UnitPrefab = null;

    private List<Unit> m_Units = new List<Unit>();

    private UnitData m_Data = null;

    private int m_Count { get { return m_Data.Count; } }


    public void Init(SaveUnitData data)
    {
        m_Data = UnitRandomMachine.GetUnitData(data);
        for (int i = 0; i < m_Data.Count; i++)
        {
            ObjectPool.Instance.GetObject(m_UnitPrefab, transform);
        }
    }
}
