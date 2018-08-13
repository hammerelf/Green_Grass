using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float x;
    public float y;

	// Use this for initialization
	void Start () {
        //x = transform.position.x;
        //y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //currentPlayer.transform.position.Set(currentHex.transform.position.x, currentHex.transform.position.y, -1);
            //changePosition(currentHex.transform.position.x, currentHex.transform.position.y);
        }
    }

    public void changePosition(float newX, float newY) {
        //transform.position.Set(newX, newY, -1);
    }
}
