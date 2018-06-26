using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var hitPlayer = hit.GetComponent<Player>();
        if (hitPlayer != null)
        {
            // Subscribe and Publish model may be good here!
			float distance = Vector3.Distance(hitPlayer.transform.position, transform.position);
			float hurt = 15f/(distance);
            var combat = hit.GetComponent<Combat>();
            combat.TakeDamage(hurt);
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}






