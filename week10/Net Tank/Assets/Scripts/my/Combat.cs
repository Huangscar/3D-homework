using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Combat :  NetworkBehaviour 
{
    public const float maxHealth = 100f;

	public Slider m_Slider;                             
    public Image m_FillImage;                           
    public Color m_FullHealthColor = Color.green;       
    public Color m_ZeroHealthColor = Color.red;

    [SyncVar]
    public float health = maxHealth;

    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;
        Debug.Log(health);
        health -= amount;
		RpcSetHealthUI();
        if (health <= 0)
        {
            //health = maxHealth;
            Destroy(gameObject);
            // called on the server, will be invoked on the clients
            //RpcRespawn();
        }
    }
    [ClientRpc]
	private void RpcSetHealthUI ()
    {
        m_Slider.value = health;
		Debug.Log(health);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}
