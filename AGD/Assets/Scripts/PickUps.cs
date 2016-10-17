using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;


public class PickUps : MonoBehaviour {
    Color[] colours = { Color.red, Color.green, Color.blue };

    bool power = false;

    string power_name = "";

    public Text power_obtained;
    public Slider timer;

    void Update()
    {

        if (power && !timer.gameObject.activeInHierarchy)
        {
            timer.gameObject.SetActive(true);
            timer.value = 300;
        }
        else if (!power && timer.gameObject.activeInHierarchy)
            timer.gameObject.SetActive(false);

        if(timer.gameObject.activeInHierarchy)
        {
            if (timer.value <= 0)
            {
                power = false;
                gameObject.GetComponent<FirstPersonController>().m_Multiplier = 1.0f;
                return;
            }
            timer.value -= 1;

            if (power_name == "Speed Boost")
            {
                gameObject.GetComponent<FirstPersonController>().m_Multiplier = 2.0f;
            }
            else if(power_name == "Power Boost")
            {
                Debug.Log("I know you hate this Freddie");
            }
            else if (power_name == "Weapon Recharge")
            {
                Debug.Log("That's why I'm doing it haha!");
            }
        }
        else
        {
            power_obtained.text = "";
        }

    }

	void OnTriggerEnter(Collider other)
    {
        if (power)
        {
            return;
        }
        else
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                if (other.gameObject.name == "Speed Boost")
                {
                    power_obtained.color = colours[0];
                    power_name = other.gameObject.name;
                }
                else if (other.gameObject.name == "Power Boost")
                {
                    power_obtained.color = colours[1];
                    power_name = other.gameObject.name;

                }
                else if (other.gameObject.name == "Weapon Recharge")
                {
                    power_obtained.color = colours[2];
                    power_name = other.gameObject.name;
                }

                power_obtained.text = other.gameObject.name + " Obtained";
                power = true;
                Destroy(other.gameObject);
            }
        }
    }
}
