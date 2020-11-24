using UnityEngine;
using UnityEngine.UI;

public sealed class Fortress : MonoBehaviour
{
    public static Fortress Instance;
    public int Hp { get; private set; } = 1000;
    
    [Header("UI Objects")] 
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private GameObject _victoryWindow;
    [SerializeField] private GameObject _gold;
    [SerializeField] private Button _closeVictoryWindow;
    [SerializeField] private Image _face;
    [SerializeField] private Text[] _texts;
    [Header("Other")]
    [SerializeField] private Animator _animator;
    
    private string[] _messages = {"Please no!", "Stop that!", "Give up!",
        "I will defeat you!", "There is no turning back!", "Show me what you are capable of!",
        "And it's all?", "Stop pressing the button!", "Victory will be mine!", "My army is stronger!"};

    private int _lastRandomMessage = 0;
    private int _lastRandomText = 0;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (_victoryWindow != null || _closeVictoryWindow != null ||
            _hpSlider != null || _gold != null || _face != null)
        {

            _hpSlider.maxValue = Hp;
            _hpSlider.value = Hp;
            
            _closeVictoryWindow.onClick.AddListener(CloseVictoryWindow);
            
            _gold.SetActive(false);
            _victoryWindow.SetActive(false);
            
        }
        else
        {
            Debug.LogError("Object reference is null!");
        }
       
        
    }

    public void SetDamage (int damage)
    {
        if (Hp - damage > 0)
        {
            Hp -= damage;
            _animator.Play("fortressHit");
            ShowMessage();
        }
        else
        {
            Hp = 0;
            Destroy();
        }

        UpdateUI();

    }

    private void ShowMessage()
    {
        restart :
        var i = Random.Range(0, _texts.Length);
        if (i == _lastRandomText) 
            goto restart;
       
        start :
        var r = Random.Range(0, _messages.Length);
        if (r == _lastRandomMessage) 
            goto start;
        
        _texts[i].text = _messages[r];
        _texts[i].gameObject.SetActive(true);
        
        var founded = _texts[i].TryGetComponent(out Reset reset);
        if (founded) reset.time = Random.Range(0.5f, 1.2f);
        
        _lastRandomMessage = r;
        _lastRandomText = i;

    }

    private void UpdateUI()
    {
        _hpSlider.value = Hp;
    }

    private void Destroy()
    {
        _face.sprite = Resources.Load<Sprite>("happyFace");
        
        _gold.SetActive(true);
        _victoryWindow.SetActive(true);
    }

    public void CloseVictoryWindow()
    {
        _victoryWindow.SetActive(false);
    }
   
}
