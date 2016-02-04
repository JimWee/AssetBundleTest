using UnityEngine;
using System.Collections;

public class TestLog : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
