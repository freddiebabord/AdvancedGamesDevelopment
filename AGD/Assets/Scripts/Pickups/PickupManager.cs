using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PickupManager : NetworkBehaviour
{

    public GameObject powerBoostPickup;
    public GameObject speedBoostPickup;
    public GameObject weaponRechargePickup;

	// Use this for initialization
	void OnEnable ()
	{
	    SceneManager.sceneLoaded += LoadLevelHandle;
	}

    void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadLevelHandle;
    }
    
    void LoadLevelHandle(Scene scene, LoadSceneMode mode)
    {
        if(!isServer)
            return;
        
        PickupBaseHandler[] pubh = FindObjectsOfType<PickupBaseHandler>();
        for (var i = 0; i < pubh.Length; i++)
        {
            switch (pubh[i].pickupType)
            {
                case PickupBaseHandler.PickupType.PowerBoost:
                    InstantiatePickup(pubh[i].transform, powerBoostPickup);
                    break;
                case PickupBaseHandler.PickupType.SpeedBoost:
                    InstantiatePickup(pubh[i].transform, speedBoostPickup);
                    break;
                case PickupBaseHandler.PickupType.WeaponRecharge:
                    InstantiatePickup(pubh[i].transform, weaponRechargePickup);
                    break;
            }
        }
    }

    void InstantiatePickup(Transform trans, GameObject prefab)
    {
        GameObject newPickup = (GameObject) Instantiate(prefab, trans.position += (Vector3.up * 1.5f), trans.rotation);
        NetworkServer.Spawn(newPickup);
    }
}
