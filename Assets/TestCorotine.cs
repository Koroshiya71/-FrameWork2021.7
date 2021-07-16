using UnityEngine;
using System.Collections;
using GameCore;
public class TestCorotine : MonoBehaviour {



	void Start () 
    {
        CoroutineController.Instance.StartCoroutine(Print());
	}
	void Show()
    {
        Debug.Log(123);
    }
	void Update () 
    {
        Debug.Log(1);
	}
    IEnumerator Print()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(1);
        Debug.Log(2);
        yield return new WaitForSeconds(1); 
        Debug.Log(3);
        yield return new WaitForSeconds(1); 
        Debug.Log(4);
        yield return new WaitForSeconds(1);
    }
}
