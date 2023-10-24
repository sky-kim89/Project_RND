using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//그냥 타임메니져를 수정해서 쓰자...;
public class DelayAction : Singleton<DelayAction>
{
    public class DelayActionData
    {
        public float Time;
        public Action Action;

        public DelayActionData(float time, Action action)
        {
            Time = time;
            Action = action;
        }
    }

    private Dictionary<UnityEngine.Object, DelayActionData> m_ActionList = new Dictionary<UnityEngine.Object, DelayActionData>();
    private List<UnityEngine.Object> m_RemoveActionList = new List<UnityEngine.Object>();
    
    public void AddDelayAction(float time, UnityEngine.Object caster, Action action)
    {
        m_ActionList.Add(caster, new DelayActionData(time, action));
    }

    public void RemoveDelayAction(UnityEngine.Object caster)
    {
        m_ActionList.Remove(caster);
    }

    private void Update()
    {
        var entry = m_ActionList.GetEnumerator();

        while(entry.MoveNext())
        {
            DelayActionData temp = entry.Current.Value;
            temp.Time -= Time.deltaTime;

            if(temp.Time < 0)
            {
                temp.Action();
                m_RemoveActionList.Add(entry.Current.Key);
            }
        }

        for(int i = 0; i < m_RemoveActionList.Count; i++)
        {
            m_ActionList.Remove(m_RemoveActionList[i]);
        }
    }
}