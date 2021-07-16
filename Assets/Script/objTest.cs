using UnityEngine;
using System.Collections;

public class objTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var rig = GameTool.GetTheChildComponent<Rigidbody>(gameObject,"MyObj");
        rig.useGravity = true;
        var collider = GameTool.GetTheChildComponent<BoxCollider>(gameObject, "MyObj");
        collider.isTrigger = true;

        var rig2 = GameTool.AddTheChildComponent<Rigidbody>(gameObject,"obj");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
