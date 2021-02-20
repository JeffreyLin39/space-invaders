using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    private Player _player;
    private Player player;
    private Animator _anim;
    private AudioSource _AudioSource;
    [SerializeField]
    private AudioClip _explosionSound;

    void Start(){
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        if (_anim == null) Debug.LogError("anim is null");
        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null) Debug.LogError("_laserSource is null");
        else _AudioSource.clip = _explosionSound;
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.9f) transform.position = new Vector3(UnityEngine.Random.Range(-9.8f, 9.8f), 7.3f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            player = other.GetComponent<Player>();
            transform.GetComponent<BoxCollider2D>().enabled = false;
            if (player != null) _player.LoseLife();
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _AudioSource.Play();
            Destroy(this.gameObject, 1f);
        }

        if (other.tag == "Laser")
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            if (_player != null) _player.updateScore(10);
            Destroy(other.gameObject);
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            _AudioSource.Play();
            Destroy(this.gameObject, 1f);
        }
    }
}
