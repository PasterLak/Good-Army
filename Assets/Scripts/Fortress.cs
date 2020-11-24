using UnityEngine;
using UnityEngine.UI;

public sealed class Fortress : MonoBehaviour
{
    public static Fortress Instance;
    public int Hp { get; private set; } = 1000;
    
    [Header("UI Objects")] 
    [SerializeField] private Slider HpSlider;
    [SerializeField] private GameObject VictoryWindow;
    [SerializeField] private GameObject gold;
    [SerializeField] private Button closeVictoryWindow;
    [SerializeField] private Image face;
    [SerializeField] private Text[] texts;
    [Header("Other")]
    [SerializeField] private Animator animator;
    
    private string[] messages = {"Please no!", "Stop that!", "Give up!",
        "I will defeat you!", "There is no turning back!", "Show me what you are capable of!",
        "And it's all?", "Stop pressing the button!", "Victory will be mine!", "My army is stronger!"};

    private int _lastRandomMessage = 0;
    private int _lastRandomText = 0;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (VictoryWindow != null || closeVictoryWindow != null ||
            HpSlider != null || gold != null || face != null)
        {

            HpSlider.maxValue = Hp;
            HpSlider.value = Hp;
            
            closeVictoryWindow.onClick.AddListener(CloseVictoryWindow);
            
            gold.SetActive(false);
            VictoryWindow.SetActive(false);
            
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
            animator.Play("fortressHit");
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
        var i = Random.Range(0, texts.Length);
        if (i == _lastRandomText) 
            goto restart;
       
        start :
        var r = Random.Range(0, messages.Length);
        if (r == _lastRandomMessage) 
            goto start;
        
        texts[i].text = messages[r];
        texts[i].gameObject.SetActive(true);
        
        var founded = texts[i].TryGetComponent(out Reset reset);
        if (founded) reset.time = Random.Range(0.5f, 1.2f);
        
        _lastRandomMessage = r;
        _lastRandomText = i;

    }

    private void UpdateUI()
    {
        HpSlider.value = Hp;
    }

    private void Destroy()
    {
        face.sprite = Resources.Load<Sprite>("happyFace");
        
        gold.SetActive(true);
        VictoryWindow.SetActive(true);
    }

    public void CloseVictoryWindow()
    {
        VictoryWindow.SetActive(false);
    }
   
}
