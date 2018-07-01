using UnityEngine;

public class JsonData
{
    public PersonalData[] party = null;
}

public class PersonalData
{
    public string   author       = string.Empty;
    public string   sex          = string.Empty;
    public string   name         = string.Empty;
    public long     y            = 0;
    public long     speed        = 0;
    public BulletArr[] bulletArr = null;
}

[System.Serializable]
public class BulletArr
{
    public string   type       = string.Empty;
    public long     speed      = 0;
    public long     color      = 0;
    public long     degree     = 0;
    public long     radius     = 0;
    public long     rectWidth  = 0;
    public long     rectHeight = 0;
}