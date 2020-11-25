using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class Game : MonoBehaviour
{
	public static Game Instance;

	[SerializeField] private InputField inputField;
	[Header("UI Buttons")]
	[SerializeField] private Button buttonCreateArmy;
	[SerializeField] private Button buttonDisbandArmy;
	[SerializeField] private Button buttonAttack;
	[SerializeField] private Button buttonRestart;
	[Header("Army UI Objects")]
	[SerializeField] private Image[] troopSprite;
	[SerializeField] private Text[] troopCount;
	
	private Army _army;
	private int _size = 0;


	private void Awake ()
	{
		if (Instance == null)
			Instance = this;
		
	   if (inputField == null || buttonCreateArmy == null || buttonDisbandArmy == null ||
			buttonAttack == null || buttonRestart == null)
		{
			Debug.LogError("Object reference is null!");
		}
	   else
	   {
		   inputField.contentType = InputField.ContentType.IntegerNumber;
		   inputField.onEndEdit.AddListener(SetArmySize);
		   
		   buttonCreateArmy.onClick.AddListener(CreateRandomArmy);
		   
		   buttonDisbandArmy.onClick.AddListener(DisbandArmy);
		   buttonDisbandArmy.gameObject.SetActive(false);
		   
		   buttonAttack.onClick.AddListener(Attack);
		   buttonAttack.gameObject.SetActive(false);
		   
		   buttonRestart.onClick.AddListener(RestartGame);
		 
	   }
	  
	}


	public void SetArmySize(string text)
	{
		var i = text != "" ? int.Parse(text) : 0;
	   
		if (i >= UnitsData.TypesCount)
		{
			if (i <= Castle.Instance.CitizensCount)
			{
				_size = i;
			}
			else
			{
				_size = Castle.Instance.CitizensCount;
			}
			inputField.text = _size.ToString();
		}
		else
		{
			_size = UnitsData.TypesCount;
			inputField.text = _size.ToString();
		}
	   
	}

	private void CreateRandomArmy()
	{
        if(_size < UnitsData.TypesCount) return;
		
		int part = _size / UnitsData.TypesCount;
		int remainder = _size - (part * UnitsData.TypesCount);
	   
		int[] parts = new int[UnitsData.TypesCount];
		int random = 0;
	   
		for (int i = 0; i < parts.Length; i++)
		{
			parts[i] += part;
		   
			random = Random.Range(-part / 2, part / 2);
			parts[i] += random;

			if (i + 1 < parts.Length) 
				parts[i+1] -= random;
			else  
				parts[i-1] -= random;
		  
		}
	   
		parts[ Random.Range(0, UnitsData.TypesCount ) ] += remainder;

		_army = new Army(troopCount, troopSprite);
		
		buttonAttack.gameObject.SetActive(true);

		for (byte i = 0; i < UnitsData.TypesCount; i++)
		{
			if(parts[i] > 0)
				_army.AddTroop(parts[i], i);
		}
		
		Castle.Instance.UpdateSoldiersCount(_army.UnitsCount);
		
	    inputField.text = "0";
	    _size = 0;
	    
		buttonCreateArmy.gameObject.SetActive(false);
		buttonDisbandArmy.gameObject.SetActive(true);

	}
	
	
	public void Attack()
	{
		if(_army == null) return;
		Fortress.Instance.SetDamage( Random.Range(5, 25 + _army.UnitsCount / 10) );
	}

	public void DisbandArmy()
	{
		if (_army == null) return;
	   
		_army.Disband();
		_army = null;
		
		Castle.Instance.UpdateSoldiersCount(0);
		
		buttonCreateArmy.gameObject.SetActive(true);
		buttonDisbandArmy.gameObject.SetActive(false);
		buttonAttack.gameObject.SetActive(false);

	}

	public byte GetUnitIDInArmy(byte slotID, out bool error)
	{
		if (_army == null)
		{
			error = true;
			return 0;
		}
		
		
		if(slotID >= Army.MaxTroopsCount) 
		{
				error = true;
				return 0;
		}
		if(_army.Troops[slotID] == null) 
		{
			error = true;
			return 0;
		}

		error = false;
		return _army.Troops[slotID].UnitID;

	}

	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
