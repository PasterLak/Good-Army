using UnityEngine;

public class Reset : MonoBehaviour
{
   
    public float TimerTime { get; set; }
     
    private void Update()
    {
        if (TimerTime > 0)
            TimerTime -= Time.deltaTime;

        if (TimerTime <= 0)
            gameObject.SetActive(false);
    }

}
