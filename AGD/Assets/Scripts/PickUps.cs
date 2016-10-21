using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using System;

public class PickUps : MonoBehaviour {
    Color[] colours = { Color.red, Color.green, Color.blue };

    Color temp;

    enum ActivePower { Speed, Power, Weapon, None};
    ActivePower active = ActivePower.None;

    public bool power = false;

    string power_name = "";

    Text power_obtained;
    Slider timer;

    Radar radar;

    void Start()
    {
        power_obtained = GameObject.FindGameObjectWithTag("Text").gameObject.GetComponent<Text>();
        timer = FindObjectOfType<Slider>();

        radar = gameObject.GetComponent<Radar>();
    }

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
                gameObject.GetComponent<NetworkedFirstPersonController>().m_Multiplier = 1.0f;
            }
            timer.value -= 1;

            if (power_name == "Speed Boost" && active != ActivePower.Speed)
            {
                gameObject.GetComponent<NetworkedFirstPersonController>().m_Multiplier = 2.0f;
                active = ActivePower.Speed;
            }
            else if(power_name == "Power Boost" && active != ActivePower.Power)
            {
                Debug.Log("I know you hate this Freddie");
                active = ActivePower.Power;
            }
            else if (power_name == "Weapon Recharge" && active != ActivePower.Weapon)
            {
                Debug.Log("That's why I'm doing it haha!");
                active = ActivePower.Weapon;
            }

            power_obtained.text = power_name + " Obtained";
            power_obtained.color = temp;

        }
        else
        {
            power_obtained.text = "No power";
            active = ActivePower.None; 
        }

    }

	public void Triggered(Collider other)
    {
        if (power)
        {
            return;
        }
        else
        {
            if (other.gameObject.GetComponent<PickUpBase>() != null)
            {
                if (other.gameObject.name == "Speed Boost")
                {
                    temp = colours[0];
                }
                else if (other.gameObject.name == "Power Boost")
                {
                    temp = colours[1];

                }
                else if (other.gameObject.name == "Weapon Recharge")
                {
                    temp = colours[2];
                }

                power_name = other.gameObject.name;

                power = true;

            }
        }
    }
}
