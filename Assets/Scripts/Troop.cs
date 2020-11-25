
[System.Serializable]
public sealed class Troop
{
    public const int MaxSize = 120;
    public int Count { get; set; }
    public byte UnitID { get; set; }

    public Troop(int count, byte unitID)
    {
        Count = count;
        UnitID = unitID;
    }

}
