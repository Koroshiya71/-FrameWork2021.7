using UnityEngine;
using System.Collections;
using GameCore;

public class TestMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventDispatcher.AddListener<int>(E_MessageType.TestMsg, Show);
	
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void Show(int num)
    {
        Debug.Log("监听到测试消息"+num);
    }
}
