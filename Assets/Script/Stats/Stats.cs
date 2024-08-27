using System.Collections.Generic;

[System.Serializable]
public class Stats 
{
    public int baseValue;

    public List<int> modify;

    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int item in modify)
        {
            finalValue += item;
        }
        return finalValue;
    }
    public void AddModify(int _modify)
    {
        modify.Add(_modify);
    }
    public void RemoveModify(int _modify)
    {
        modify.Remove(_modify);
    }
    public void SetDefaltValue(int _value)
    {
        baseValue = _value;
    }
}
