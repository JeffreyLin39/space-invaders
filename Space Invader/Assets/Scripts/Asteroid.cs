using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject _explosion;

    void Start()
    {
        transform.position = new Vector3(0.45f, 4.09f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_explosion, new Vector3(0, 4.09f, 0), Quaternion.identity);            
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Player>().LoseLife();
            Instantiate(_explosion, new Vector3(0f, 4.09f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
