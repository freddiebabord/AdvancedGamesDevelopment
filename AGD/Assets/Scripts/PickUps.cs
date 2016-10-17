using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUps : MonoBehaviour {

    bool power = false;

    public Text power_obtained;
    public Slider timer;

    void Update()
    {
        if (power && !timer.gameObject.activeInHierarchy)
        {
            timer.gameObject.SetActive(true);
            timer.value = 100;
        }
        else if (!power && timer.gameObject.activeInHierarchy)
            timer.gameObject.SetActive(false);

        if(timer.gameObject.activeInHierarchy)
        {
            if (timer.value <= 0)
            {
                power = false;
                return;
            }
            timer.value -= 1;
        }
        else
        {
            power_obtained.text = "";
        }

    }

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            power_obtained.text = other.gameObject.name + " Obtained";
            power = true;
            Destroy(other.gameObject);
        }
            
    }
}
