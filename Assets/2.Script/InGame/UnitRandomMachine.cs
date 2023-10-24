    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using MyTable;

public class UnitRandomMachine
{
    private enum eSpecialty
    {
        Good = 2,
        Ok = 1,
        No = 0,

        List = 3
    }

    static public UnitData GetSkillData(SaveUnitData saveUnitData)
    {
        Random.InitState(Random.Range(int.MinValue, int.MaxValue));

        UnitData data = new UnitData();
        data.Name = saveUnitData.Name;

        int seed = NameToSeed(data.Name);

        Random.InitState(seed);
        List<Skill> skill = new List<Skill>();

        return data;
    }

    static public UnitData GetUnitData(SaveUnitData saveUnitData)
    {
        Random.InitState(Random.Range(int.MinValue, int.MaxValue));

        UnitData data = new UnitData();
        data.Name = saveUnitData.Name;

        int seed = NameToSeed(data.Name);

        Random.InitState(seed);

        int index = Random.Range(1, (int)eGradeType.List);
        data.Grade = (eGradeType)index;
        index = Random.Range(1, (int)eTribe.List);
        data.Tribe = (eTribe)index;

        data.MaxCount = Random.Range(5, 10);
        data.MaxCount *= (int)data.Grade + data.UpGrade;

        data.Count = Random.Range(2, 4);
        data.Count += (int)data.Grade + data.UpGrade;
        data.Count += saveUnitData.AddCount;

        SettingStats(data);

        return data;
    }

    static public UnitData NewUnitData()
    {
        Random.InitState(Random.Range(int.MinValue, int.MaxValue));

        UnitData data = new UnitData();
        int index = Random.Range(0, Table.NameTables.Length);
        index = index - index % 3;
        data.Name = Table.NameTables[index];

        int seed = NameToSeed(data.Name);

        Random.InitState(seed);

        index = Random.Range(1, (int)eGradeType.List);
        data.Grade = (eGradeType)index;

        SettingStats(data);

        //index = Random.Range(1, (int)eJobType.List);
        //data.JobType = (eJobType)index;

        //index = Random.Range(1, (int)eSizeType.List);
        //data.Size = (eSizeType)index;

        //index = Random.Range(0, Table.BobyColors.Length);
        //data.UnitColors[0] = Table.BobyColors[index]; 

        //index = Random.Range(0, Table.HeadColors.Length);
        //data.UnitColors[1] = Table.HeadColors[index];

        //index = Random.Range(0, Table.HairColors.Length);
        //data.UnitColors[2] = Table.HairColors[index];

        //index = Random.Range(0, Table.EyeRColors.Length);
        //data.UnitColors[3] = Table.EyeRColors[index];
        //data.UnitColors[4] = Table.EyeLColors[index];

        //data.Skills = new ISkill[2];
        //data.Skills[0] = SkillTable.GetRandomPassiveSkill();
        //data.Skills[1] = SkillTable.GetRandomActiveSkill();
        return data;
    }

    static private void SettingStats(UnitData data)
    {
        int index = 0;

        bool isGood = false;

        index = Random.Range(0, (int)eSpecialty.List);

        data.Power = GetStats((int)data.Grade, (eSpecialty)index);
        if ((eSpecialty)index == eSpecialty.Good)
        {
            index = Random.Range(0, (int)eSpecialty.Good);
            isGood = true;
        }
        else
        {
            index = Random.Range(0, (int)eSpecialty.Good);
        }

        data.Stamina = GetStats((int)data.Grade, (eSpecialty)index);
        if ((eSpecialty)index == eSpecialty.Good || isGood)
        {
            index = Random.Range(0, (int)eSpecialty.Good);
        }
        else
        {
            index = (int)eSpecialty.Good;
        }

        data.Normal = GetStats((int)data.Grade, (eSpecialty)index);
    }

    static private int GetStats(int grade, eSpecialty specialty)
    {
        switch (specialty)
        {
            case eSpecialty.Good:
                return Random.Range((int)grade * 2, 20);
            case eSpecialty.Ok:
                return Random.Range((int)grade * 2, (int)grade * 4);
            case eSpecialty.No:
                return Random.Range(1, (int)grade * 4);

        }

        return Random.Range((int)grade * 2, (int)grade * 4);
    }

    static private int NameToSeed(string name)
    {
        int seed = 0;
        int prev = 0;

        foreach (int i in name)
        {
            if (prev == 0)
                seed += i;
            else
                seed += prev * i + prev + i;

            prev = i;
        }
        return seed;
    }
}
