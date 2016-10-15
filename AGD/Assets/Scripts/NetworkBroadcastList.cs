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
    public List<string> networkAdresses = new List<string>();
    public float minWidth = 350.0f;

    public void Start()
    {
        ipHolderContectRect = GameObject.Find("IPContentButtonsList").GetComponent<RectTransform>();
        templateButton = GameObject.Find("IPButtonTemplate").GetComponent<Button>();
        templateButton.gameObject.SetActive(false);
        Initialize();
        StartAsClient();
    }


    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!networkAdresses.Contains(fromAddress))
        {
            networkAdresses.Add(fromAddress);
            GameObject newButton = Instantiate(templateButton.gameObject);
            newButton.GetComponentInChildren<Text>().text = fromAddress;
            newButton.GetComponent<Button>().onClick.AddListener(() => ConnectToServer(fromAddress));
            newButton.transform.SetParent(ipHolderContectRect);
            newButton.SetActive(true);
            if ((ipHolderContectRect.sizeDelta.x / networkAdresses.Count) < minWidth)
                ipHolderContectRect.sizeDelta.Set(networkAdresses.Count * minWidth, ipHolderContectRect.sizeDelta.y);
        }
    }

    public void StartAsHost()
    {
        if(this.running)
            StopBroadcast();
        Initialize();
        StartAsServer();
    }

    public void ConnectToServer(string ipAdress)
    {
        NetManager.singleton.networkAddress = ipAdress;
        NetManager.singleton.StartClient();
    }
}
