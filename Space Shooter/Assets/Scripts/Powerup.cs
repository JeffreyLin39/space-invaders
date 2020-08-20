using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5;
    private Player player;
    [SerializeField]
    private int powerupID;
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _powerupSound;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.8f) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            if (player!= null)
            {
                AudioSource.PlayClipAtPoint(_powerupSound, transform.position);
                switch (powerupID)
                {
                    case 0:            
                        player.TripleShot();
                        break;
                    case 1:
                        player.SpeedBoost();
                        break;
                    case 2:
                        player.ShieldPowerup();
                        break;
                }
            }
            gameObject.SetActive(false);
            Destroy(this.gameObject, 0.75f);
        }
    }
}
