using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameUtility 
{
    public static Unit FindNearUnit(this List<Unit> units, Vector3 pos, float range)
    {
        float min = range;
        Unit findUnit = null;
        for (int i = 0; i < units.Count; i++)
        {
            if(Vector3.Distance(units[i].transform.position,pos) <range + 1)
            {
                findUnit = units[i];
            }
        }

        return findUnit;
    }
}
