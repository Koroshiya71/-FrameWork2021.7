using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameCore;
public class TestSend : MonoBehaviour {

    Button sendButton;
	// Use this for initialization
	void Start () {
        sendButton = GetComponent<Button>();
        sendButton.onClick.AddListener(SendMessage);
	}
	
    void SendMessage()
    {
        int coin = 991125;
        EventDispatcher.TriggerEvent<int>(E_MessageType.TestMsg,coin);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
