using UnityEngine;
using System.Collections;
using GameCore;
public class MonoTool : UnitySingleton<MonoTool>
{
    private int num = 777;
	// Use this for initialization


    public void print()
    {
        Debug.Log(num);
    }
	void Start () {
        print();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
