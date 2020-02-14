using System.Collections.Generic;
using UnityEngine;

public static class UnitsData
{
	private static readonly Dictionary<byte, Unit> _types = new Dictionary<byte, Unit>()
	{
		{0, new Spearman(0,50, 15,"spearman")},
		{1, new Swordsman(1,85,22,"swordsman")},
		{2, new Archer(2,20,10,"archer")}
	};
	
	public static byte TypesCount 
	{
		get { return (byte)_types.Count;  }
	}

	public static Unit GetUnit(byte unitID)  
	{
		
		if (unitID > TypesCount)
		{
			unitID = (byte)(TypesCount-1);
			Debug.LogError("Out of range exception!");
		}
		return _types[unitID];
	}


}
