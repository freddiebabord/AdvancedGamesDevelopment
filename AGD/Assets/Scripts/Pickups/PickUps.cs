using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class PickUps : NetworkBehaviour {
    Color[] colours = { Color.red, Color.green, Color.blue, Color.cyan };

    Color temp;

    enum PowerName { UnlimitedStamina, WeaponRecharge, PowerBoost, NullifyFear, None };
    PowerName pow = PowerName.None;

    public enum ActivePower { Speed, Power, Weapon, Nullify, None};
    public ActivePower active = ActivePower.None;

    public bool power = false;

    string power_name = "";
    string remove = "(Clone)";

    Text power_obtained;
    Slider timer;
    Slider fear;
    public Slider stamina;

    public float cooldown = 5.0f;
    public bool cooloff = false;

    GameObject the_pickup;

    float radius = 5f;

    EnemyBase[] enemies;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        power_obtained = GameObject.Find("PowerupText").GetComponent<Text>();

        power_obtained.transform.position = new Vector3(390, 220, 0);

        power_obtained.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    

        power_obtained.horizontalOverflow = HorizontalWrapMode.Overflow;
        power_obtained.verticalOverflow = VerticalWrapMode.Overflow;

        power_obtained.fontSize = 32;

        Slider[] get_sliders = FindObjectsOfType<Slider>();

        for(int i = 0; i < get_sliders.Length; i++)
        {
            if (get_sliders[i].name == "Stamina")
                stamina = get_sliders[i];
            else if (get_sliders[i].name == "Slider")
                timer = get_sliders[i];
            else if (get_sliders[i].name == "Fear")
                fear = get_sliders[i];
        }

        enemies = FindObjectsOfType<EnemyBase>();

    }

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
            timer.value -= 1;


            if (pow == PowerName.UnlimitedStamina && active != ActivePower.Speed)
            {
                the_pickup.GetComponent<UnlimitedStamina>().Triggered();
                active = ActivePower.Speed;
            }
            else if(pow == PowerName.PowerBoost && active != ActivePower.Power)
            {
                the_pickup.GetComponent<PowerBoost>().Triggered();
                active = ActivePower.Power;
            }
            else if (pow == PowerName.WeaponRecharge && active != ActivePower.Weapon)
            {
                the_pickup.GetComponent<WeaponRecharge>().Triggered();
                active = ActivePower.Weapon;
            }
            else if(pow == PowerName.NullifyFear && active != ActivePower.Nullify)
            {
                the_pickup.GetComponent<NullifyFear>().Triggered();
                active = ActivePower.Nullify;
            }

            power_name = power_name.Replace(remove, "");
            power_obtained.text = power_name;
            power_obtained.color = temp;

        }
        else
        {
            power_obtained.text = "No power";
            active = ActivePower.None; 
        }

    }

    void EnemiesCloseForFear()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if(Vector3.Distance(gameObject.transform.position, enemies[i].gameObject.transform.position) < radius)
            {
                if(!fear.gameObject.activeInHierarchy)
                {
                    fear.gameObject.SetActive(true);
                }
            }
        }
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
                else if(other.gameObject.GetComponent<NullifyFear>() != null)
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
