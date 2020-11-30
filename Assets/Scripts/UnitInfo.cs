using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  
    [SerializeField] private byte _slotID = 0;

    private static GameObject _infoWindow;
    private static RectTransform _infoWindowRect;
    private static Text _infoText;
   
    private void Awake()
    {

        if (_infoWindow == null)
        {
            _infoWindow = Instantiate((Resources.Load<GameObject>("InfoWindow")));
            _infoWindow.transform.SetParent(GameObject.Find("Canvas").transform);
            _infoWindowRect = _infoWindow.GetComponent<RectTransform>();
            _infoWindow.SetActive(false);
        }
           
        if(_infoText == null && _infoWindow != null)
            _infoText = _infoWindow.transform.Find("Text").GetComponent<Text>();
        
    }

    private void Update()
    {
        if (_infoWindow.activeSelf)
        {
            _infoWindowRect.position = new Vector3( 
                Input.mousePosition.x + _infoWindowRect.rect.width / 2, 
                Input.mousePosition.y + _infoWindowRect.rect.height / 2,
                   Input.mousePosition.z);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        byte _unitID = Game.Instance.GetUnitIDInArmy(_slotID, out bool error);
        
        if (error == false)
        {
            _infoWindow.SetActive(true);
            _infoText.text = UnitsData.GetUnit(_unitID).ToString();
        }
          
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
       
        _infoWindow.SetActive(false);
    }
    
    
}
