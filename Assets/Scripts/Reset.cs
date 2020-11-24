using UnityEngine;

public class Reset : MonoBehaviour
{
   
    public float time = 0;

    private void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        if (time <= 0) gameObject.SetActive(false);
    }
}
