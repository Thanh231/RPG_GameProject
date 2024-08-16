using UnityEngine;
public static class Prefabs
{
    public static string playerData
    {
        get => PlayerPrefs.GetString(ContantTest.playerData);
        set => PlayerPrefs.SetString(ContantTest.playerData, value);
    }
}
