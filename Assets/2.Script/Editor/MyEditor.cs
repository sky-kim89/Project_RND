using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditor : Editor {

    [MenuItem("MyShortcutsKey/SetActive %e")]
    public static void SetActive()
    {
        foreach (GameObject obj in Selection.objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    [MenuItem("MyShortcutsKey/SavePrefab %w")]
    public static void SavePrefab()
    {
        foreach (GameObject obj in Selection.objects)
        {
            var instanceRoot = PrefabUtility.FindRootGameObjectWithSameParentPrefab(obj);
            var targetPrefab = UnityEditor.PrefabUtility.GetPrefabParent(instanceRoot);

            PrefabUtility.ReplacePrefab(
                    instanceRoot,
                    targetPrefab,
                    ReplacePrefabOptions.ConnectToPrefab
                    );
        }
    }

    [MenuItem("MyShortcutsKey/TimeScaleSpeedUp %t")]
    public static void TimeScale()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 5;
        }
    }
}
