using UnityEngine;
using UnityEngine.UI;

public sealed class Army 
{
	public const byte MaxTroopsCount = 9;
	public int UnitsCount { get; private set; } = 0;
    
	public Image[] troopSprite;
	public Text[] troopCount;

	private Troop[] troops = new Troop[MaxTroopsCount]; // set get
	

	public void AddTroop( int count, byte unitID)
	{
		if(count <= 0)
			return;
		
		if (ThereAreEmptyTroops())
		{
			int emptySlot = GetEmptyTroop();
			
			troops[emptySlot] = new Troop(count, unitID);
			Castle.Instance.UpdateCitizensCount(-count);
			UnitsCount += count;
			
			UpdateUI(emptySlot, unitID);
		}
	
	}

	public void RemoveTroop(int count, int unitID)
	{
		// empty
	}

	public void Disband()
	{
		Castle.Instance.UpdateCitizensCount(UnitsCount);

		for (byte i = 0; i < troops.Length; i++)
		{
			if (troops[i] == null) continue;
			troopSprite[i].sprite = Resources.Load<Sprite>("empty");
			troopCount[i].text = string.Empty;
		}
	}
	

	private void UpdateUI(int slotID, byte unitID)
	{
		troopSprite[slotID].sprite = Resources.Load<Sprite>(UnitsData.GetUnit(unitID).Sprite);
		troopCount[slotID].text = troops[slotID].Count.ToString();
	}

	private bool ThereAreEmptyTroops()
	{
		for (byte i = 0; i < troops.Length; i++)
		{
			if (troops[i] == null)
				return true;
		}

		return false;
	}
	private int GetEmptyTroop()
	{
		for (byte i = 0; i < troops.Length; i++)
		{
			if (troops[i] == null)
				return i;
			
		}

		return 0;
	}
	
	
}
