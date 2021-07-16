using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GameTool.SetString("developer1", "Koroshiya");

        //GameTool.SetInt("developer", 71);

        //Debug.Log(GameTool.GetString("developer1") + GameTool.GetInt("developer"));
        DataController.Instance.LoadAllCfg();
        string name = DataController.Instance.ReadCfg("Name", 2, DataController.Instance.dicTest);
        Debug.Log(name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
