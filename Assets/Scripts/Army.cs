using UnityEngine;
using UnityEngine.UI;

public sealed class Army
{
	public const byte MaxTroopsCount = 9;
	public int UnitsCount { get; private set; } = 0;

	private Image[] _troopSprite;
	private Text[] _troopCount;
	private Troop[] _troops = new Troop[MaxTroopsCount]; 
	
	public Troop[] Troops
	{
		get { return _troops; }	
	}

	public Army (Text[] troopCount, Image[] troopSprite)
    {
		_troopCount = troopCount;
		_troopSprite = troopSprite;
    }

	public void AddTroop( int count, byte unitID)
	{
		if(count <= 0)
			return;
		
		if (ThereAreEmptyTroops())
		{
			int emptySlot = GetEmptyTroop();
			
			_troops[emptySlot] = new Troop(count, unitID);
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

		for (byte i = 0; i < _troops.Length; i++)
		{
			if (_troops[i] == null) continue;
			_troopSprite[i].sprite = Resources.Load<Sprite>("empty");
			_troopCount[i].text = string.Empty;
		}
	}

	private void UpdateUI(int slotID, byte unitID)
	{
		_troopSprite[slotID].sprite = Resources.Load<Sprite>(UnitsData.GetUnit(unitID).Sprite);
		_troopCount[slotID].text = _troops[slotID].Count.ToString();
	}

	private bool ThereAreEmptyTroops()
	{
		for (byte i = 0; i < _troops.Length; i++)
		{
			if (_troops[i] == null)
				return true;
		}

		return false;
	}

	private int GetEmptyTroop()
	{
		for (byte i = 0; i < _troops.Length; i++)
		{
			if (_troops[i] == null)
				return i;
			
		}

		return 0;
	}
	
	
}
