using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class PickUps : NetworkBehaviour {
    Color[] colours = { Color.red, Color.green, Color.blue };

    Color temp;

    enum PowerName { UnlimitedStamina, WeaponRecharge, PowerBoost, NullifyFear, None };
    PowerName pow = PowerName.None;

    public enum ActivePower { Speed, Power, Weapon, Nullify, None };
    public ActivePower active = ActivePower.None;

    public bool power = false;

    string power_name = "";

    // GameObject the_text;
   // public Text power_obtained;
    public Slider timer;
    public Slider stamina;
    

    public float cooldown = 5.0f;
    public bool cooloff = false;
	public float powerupDuration = 5;

    GameObject the_pickup;


    void Update()
    {
         if (!isLocalPlayer)
             return;
        if (active != ActivePower.Speed && !cooloff)
        {
            if (gameObject.GetComponent<NetworkedThirdPersonUserControl>().running && stamina.value != stamina.minValue)
            {
                stamina.value -= 1.0f;
            }
            else if (gameObject.GetComponent<NetworkedThirdPersonUserControl>().running && stamina.value <= stamina.minValue)
            {
                gameObject.GetComponent<NetworkedThirdPersonUserControl>().allowRunning = false;
                stamina.value += 1.0f;
                cooloff = true;
            }
            else if (!gameObject.GetComponent<NetworkedThirdPersonUserControl>().running && stamina.value != stamina.maxValue)
            {
                stamina.value += 1.0f;
            }
        }
        else if(cooloff)
        {
            gameObject.GetComponent<NetworkedThirdPersonUserControl>().allowRunning = false;

            cooldown -= Time.deltaTime;
            stamina.value += 1.0f;

            if(cooldown <= 0.0f)
            {
                cooldown = 5.0f;
                cooloff = false;
                gameObject.GetComponent<NetworkedThirdPersonUserControl>().allowRunning = true;

            }
        }

       
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
            }
			timer.value -= 1 * Time.deltaTime;


            if (pow == PowerName.UnlimitedStamina && active != ActivePower.Speed)
            {
                the_pickup.GetComponent<UnlimitedStamina>().Triggered();
                active = ActivePower.Speed;
            }
            else if(pow == PowerName.PowerBoost && active != ActivePower.Power)
            {
                the_pickup.GetComponent<PowerBoost>().Triggered();
				if(!weaponOutputDoubled)
					StartCoroutine(DoubleWeaponOutput(gameObject.GetComponent<NetworkedThirdPersonCharacter>()));
				active = ActivePower.Power;
            }
            else if (pow == PowerName.WeaponRecharge && active != ActivePower.Weapon)
            {
                the_pickup.GetComponent<WeaponRecharge>().Triggered();
				gameObject.GetComponent<NetworkedThirdPersonCharacter> ().currentWeaponTime = 0.0f;
                active = ActivePower.Weapon;
            }
            else if (pow == PowerName.NullifyFear && active != ActivePower.Nullify)
            {
                the_pickup.GetComponent<NullifyFear>().Triggered();
                active = ActivePower.Nullify;
            }

            //power_obtained.text = power_name;
            var cb = timer.colors;
            cb.normalColor = temp;
            timer.colors = cb;

        }
        else
        {
           // power_obtained.text = "No power";
            active = ActivePower.None; 
        }

    }
	bool weaponOutputDoubled = false;
	IEnumerator DoubleWeaponOutput(NetworkedThirdPersonCharacter player)
	{
		weaponOutputDoubled = true;
		player.damagePerSecond *= 2;
		yield return new WaitForSeconds (powerupDuration);
		player.damagePerSecond /= 2;
		weaponOutputDoubled = false;
	}

	void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
            return;

        if (power || other.gameObject.GetComponent<PickUpBase>() == null)
        {
            return;
        }
        else 
        {
            if (other.gameObject.GetComponent<PickUpBase>() != null)
            {
                if (other.gameObject.GetComponent<UnlimitedStamina>() != null)
                {
                    temp = colours[0];
                    pow = PowerName.UnlimitedStamina;

                }
                else if (other.gameObject.GetComponent<PowerBoost>() != null)
                {
                    temp = colours[1];
                    pow = PowerName.PowerBoost;

                }
                else if (other.gameObject.GetComponent<WeaponRecharge>() != null)
                {
                    temp = colours[2];
                    pow = PowerName.WeaponRecharge;

                }
                else if (other.gameObject.GetComponent<NullifyFear>() != null)
                {
                    temp = colours[3];
                    pow = PowerName.NullifyFear;

                }

                other.gameObject.GetComponent<PickUpBase>().player = gameObject;

                the_pickup = other.gameObject;

                power_name = other.gameObject.name;

                power = true;

            }
        }
    }
}
