using System;
using UnityEngine.UI;
using UnityEngine;

public sealed class Castle : MonoBehaviour
{

	public static Castle Instance;

	[SerializeField] private Text _citizensUIText;
	[SerializeField] private Text _soldiersUIText;
	

	private int _citizens = 0;

	public int CitizensCount
	{
		get { return _citizens; }
	}

	private void Awake()
	{
		if(Instance == null)
		Instance = this;
		
		if (_citizensUIText == null || _soldiersUIText == null )
		{
			Debug.LogError("Object reference is null!");
		}
	}

	private void Start ()
	{
		UpdateCitizensCount(800);
		UpdateSoldiersCount(0);
	}

	public void UpdateCitizensCount(int count)
	{
		_citizens += count;
		
		if (_citizens < 0) _citizens = 0;

		_citizensUIText.text = "Citizens: \n" +  _citizens;
	}

	public void UpdateSoldiersCount(int count)
	{
		_soldiersUIText.text = "Soldiers: \n" +  count;
	}

}
