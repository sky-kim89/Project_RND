using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUnitData
{
    public string Name = string.Empty;

    public int AddLevel = 1;
    public int UpGrade = 0;

    public int AddCount = 0;
}

public class UserInfo
{
    const string FILE_NAME = "UserInfo";
    protected static UserInfo _instance;
    public static UserInfo Instance
    {
        set
        {
            _instance = value;
        }
        get
        {
            //if (_instance == null)
            //{
            //    string jsonData = SecurityPlayerPrefs.GetString(FILE_NAME, string.Empty);
            //    if (string.IsNullOrEmpty(jsonData) == false)
            //    {
            //        _instance = XOR.XOREncryption.FromString<UserInfo>(jsonData);
            //    }

            if (_instance == null)
            {
                _instance = new UserInfo();
            }
            //}

            return _instance;
        }
    }

    public List<SaveUnitData> MyGeneralNames = new List<SaveUnitData>();
    public List<SaveUnitData> MyUnitNames = new List<SaveUnitData>();

    public int MyGold = 0;
    public int MyCash = 0;

    public int MyLevel = 0;


    public void SaveData()
    {

    }
}
