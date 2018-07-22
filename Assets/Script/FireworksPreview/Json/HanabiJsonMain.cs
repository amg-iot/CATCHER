using UnityEngine;

/**
* JSON：花火メイン.
*/
public class JsonData
{
    public PersonalData[] party = null;
}

/**
* JSON：お子様情報.
*/
public class PersonalData
{
    // お子様名
    public string   author       = string.Empty;
    // 性別
    public string   sex          = string.Empty;
    // 花火名
    public string   name         = string.Empty;
    // // Y座標
    // public long     y            = 0;
    // // スピード
    // public long     speed        = 0;
    // 作成花火データ配列
    public BulletArr[] bulletArr = null;
}

/**
* JSON：作成花火データ情報.
*/
public class BulletArr
{
    // 花火タイプ
    public string   type       = string.Empty;
    // // スピード
    // public long     speed      = 0;
    // 色
    public long     color      = 0;
    // 角度
    public long     degree     = 0;
    // 半径
    public long     radius     = 0;
    // // 幅
    // public long     rectWidth  = 0;
    // // 高さ
    // public long     rectHeight = 0;
}