using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {
    public Rigidbody2D rb2d;
    public float speed = 500f;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = new Vector2(0,0);
        float x = Input.GetAxis("Horizontal") * speed ;
        float y = Input.GetAxis("Vertical") * speed ;
        rb2d.AddForce(new Vector2(x, y));
       
	}
}
