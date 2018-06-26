using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    public GameObject bulletPrefab;
	//public Camera cam;

    private float offsetX;
    private float x;
    private float y;

	//private Vector3 position;
	//private Vector3 forward;

	//private Quaternion rotation;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

	void Start() {
		
	}

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 20;
			//position = transform.position;
			//forward = transform.forward;
			
			//transform.Translate(0, 0, 0.1f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * -20;
			//position = transform.position;
			//forward = transform.forward;
			//transform.Translate(0, 0, -0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

        if (Input.GetMouseButtonUp(1))
        {
            CmdFire();
        }

        offsetX = Input.GetAxis("Mouse X") * 2.0f;
        x = transform.localEulerAngles.y + offsetX * 5;
        y = transform.localEulerAngles.x;
        transform.localEulerAngles = new Vector3(y, x, 0);
		Vector3 position = new Vector3(transform.position.x, 20, transform.position.z);
		Camera.main.transform.position = position;
		
		//rotation = transform.rotation;
		//forward = transform.forward;
		//position = transform.position;
    }



    [Command]
    void CmdFire()
    {
        // This [Command] code is run on the server!

        // create the bullet object locally

		Debug.Log(transform.position.x);

        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + 2*transform.forward, transform.rotation
            );
        //bullet.transform.eulerAngles = new Vector3(y, x, 0);
		

		Vector3 v = transform.forward * 30;
		v.y = 0;

       bullet.GetComponent<Rigidbody>().velocity = v;

        // spawn the bullet on the clients
        NetworkServer.Spawn(bullet);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(bullet, 10.0f);
    }
}

