using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkBroadcastList : NetworkDiscovery {

    [Header("Find A Game Section")]
    public RectTransform ipHolderContectRect;
    public Button templateButton;
    public List<string> networkAddresses = new List<string>();
    public float minWidth = 350.0f;
	bool init = false;

    public void OnEnable()
    {
		if(!ipHolderContectRect)
			ipHolderContectRect = GameObject.Find("IPContentButtonsList").GetComponent<RectTransform>();
		if(!templateButton)
			templateButton = GameObject.Find("IPButtonTemplate").GetComponent<Button>();
        templateButton.gameObject.SetActive(false);
        Initialize();
        StartAsClient();
		init = true;
    }

	public void OnDisable()
	{
		if(init)
			StopBroadcast ();
		init = false;
	}

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!networkAddresses.Contains(fromAddress))
        {
            networkAddresses.Add(fromAddress);
            GameObject newButton = Instantiate(templateButton.gameObject);
            newButton.GetComponentInChildren<Text>().text = fromAddress;
            newButton.GetComponent<Button>().onClick.AddListener(() => ConnectToServer(fromAddress));
            newButton.transform.SetParent(ipHolderContectRect);
            newButton.SetActive(true);
            if ((ipHolderContectRect.sizeDelta.x / networkAddresses.Count) < minWidth)
                ipHolderContectRect.sizeDelta.Set(networkAddresses.Count * minWidth, ipHolderContectRect.sizeDelta.y);
        }
    }

    public void StartAsHost()
    {
        if(this.running)
            StopBroadcast();
        Initialize();
        StartAsServer();
    }

    public void ConnectToServer(string ipAddress)
    {
        NetManager.singleton.networkAddress = ipAddress;
        NetManager.singleton.StartClient();
    }
}
