using System;
using UnityEngine.UI;
using UnityEngine;

public class Castle : MonoBehaviour
{

	public static Castle Instance;


	[SerializeField] private Text citizensUIText;
	[SerializeField] private Text soldiersUIText;
	

	private int _citizens = 0;
	public int CitizensCount
	{
		get { return _citizens; }
	}

	private void Awake()
	{
		Instance = this;
		
		if (citizensUIText == null || soldiersUIText == null )
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

		citizensUIText.text = "Citizens: \n" +  _citizens;
	}

	public void UpdateSoldiersCount(int count)
	{
		soldiersUIText.text = "Soldiers: \n" +  count;
	}

}
