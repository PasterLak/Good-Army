using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private byte _slot_ID = 0;

    private static GameObject infoWindow;
    private static RectTransform infoWindowRect;
    private static Text infoText;
   
    private void Awake()
    {

        if (infoWindow == null)
        {
            infoWindow = Instantiate((Resources.Load<GameObject>("InfoWindow")));
            infoWindow.transform.SetParent(GameObject.Find("Canvas").transform);
            infoWindowRect = infoWindow.GetComponent<RectTransform>();
            infoWindow.SetActive(false);
        }
           
        if(infoText == null && infoWindow != null)
            infoText = infoWindow.transform.Find("Text").GetComponent<Text>();
        
    }

    private void Update()
    {
        if (infoWindow.activeSelf)
        {
            infoWindowRect.position = new Vector3( 
                Input.mousePosition.x + infoWindowRect.rect.width / 2, 
                Input.mousePosition.y + infoWindowRect.rect.height / 2,
                   Input.mousePosition.z);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        byte _unitID = Game.Instance.GetUnitIDInArmy(_slot_ID, out var error);
        
        if (error == false)
        {
            infoWindow.gameObject.SetActive(true);
            infoText.text = UnitsData.GetUnit(_unitID).ToString();
        }
          
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
       
        infoWindow.gameObject.SetActive(false);
    }
    
    
}
