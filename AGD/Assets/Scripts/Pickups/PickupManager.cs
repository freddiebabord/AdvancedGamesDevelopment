using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PickupManager : NetworkBehaviour
{

    public GameObject powerBoostPickup;
    public GameObject speedBoostPickup;
    public GameObject weaponRechargePickup;
    public GameObject nullifyFearPickup;   

	// Use this for initialization
	void OnEnable ()
	{
	    SceneManager.sceneLoaded += LoadLevelHandle;
	}

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadLevelHandle;
    }

    private PickupBaseHandler[] pubh;
    void LoadLevelHandle(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0 || !isServer)
            return;

        pubh = FindObjectsOfType<PickupBaseHandler>();
        for (var i = 0; i < pubh.Length; i++)
        {
            switch (pubh[i].pickupType)
            {
                case PickupType.PowerBoost:
                    InstantiatePickup(pubh[i].transform, powerBoostPickup, PickupType.PowerBoost);
                    break;
                case PickupType.SpeedBoost:
                    InstantiatePickup(pubh[i].transform, speedBoostPickup, PickupType.SpeedBoost);
                    break;
                case PickupType.WeaponRecharge:
                    InstantiatePickup(pubh[i].transform, weaponRechargePickup, PickupType.WeaponRecharge);
                    break;
                case PickupType.NullifyFear:
                    InstantiatePickup(pubh[i].transform, nullifyFearPickup, PickupType.NullifyFear);
                    break;
            }
        }
    }

    void InstantiatePickup(Transform trans, GameObject prefab, PickupType type)
    {
        Vector3 spawnPos = trans.position + (Vector3.up*0.75f);
        GameObject newPickup = (GameObject) Instantiate(prefab, spawnPos, trans.rotation);
        newPickup.GetComponent<PickUpBase>().pickupType = type;
        NetworkServer.Spawn(newPickup);
    }
}
