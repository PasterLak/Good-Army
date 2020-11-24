using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class Game : MonoBehaviour
{
	public static Game Instance;

	[SerializeField] private InputField _inputField;
	[Header("UI Buttons")]
	[SerializeField] private Button _buttonCreateArmy;
	[SerializeField] private Button _buttonDisbandArmy;
	[SerializeField] private Button _buttonAttack;
	[SerializeField] private Button _buttonRestart;
	[Header("Army UI Objects")]
	[SerializeField] private Image[] _troopSprite;
	[SerializeField] private Text[] _troopCount;
	
	private Army _army;
	private int _size = 0;


	private void Awake ()
	{
		if (Instance == null)
			Instance = this;
		
	   if (_inputField != null || _buttonCreateArmy != null || _buttonDisbandArmy != null || _buttonAttack != null || _buttonRestart != null)
	   {
		   _inputField.contentType = InputField.ContentType.IntegerNumber;
		   _inputField.onEndEdit.AddListener(SetArmySize);
		   
		   _buttonCreateArmy.onClick.AddListener(CreateRandomArmy);
		   
		   _buttonDisbandArmy.onClick.AddListener(DisbandArmy);
		   _buttonDisbandArmy.gameObject.SetActive(false);
		   
		   _buttonAttack.onClick.AddListener(Attack);
		   _buttonAttack.gameObject.SetActive(false);
		   
		   _buttonRestart.onClick.AddListener(RestartGame);
		 
	   }
	   else
	   {
		   Debug.LogError("Object reference is null!");
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
			_inputField.text = _size.ToString();
		}
		else
		{
			_size = UnitsData.TypesCount;
			_inputField.text = _size.ToString();
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

		_army = new Army(_troopCount, _troopSprite);
		
		_buttonAttack.gameObject.SetActive(true);

		for (byte i = 0; i < UnitsData.TypesCount; i++)
		{
			if(parts[i] > 0)
				_army.AddTroop(parts[i], i);
		}
		
		Castle.Instance.UpdateSoldiersCount(_army.UnitsCount);
		
	    _inputField.text = "0";
	    _size = 0;
	    
		_buttonCreateArmy.gameObject.SetActive(false);
		_buttonDisbandArmy.gameObject.SetActive(true);

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
		
		_buttonCreateArmy.gameObject.SetActive(true);
		_buttonDisbandArmy.gameObject.SetActive(false);
		_buttonAttack.gameObject.SetActive(false);

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
