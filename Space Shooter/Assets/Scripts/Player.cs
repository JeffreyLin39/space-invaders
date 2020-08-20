using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleshotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool tripleShot = false;
    [SerializeField]
    private float Speedboost = 3.5f;
    [SerializeField]
    private bool Shield = false;
    [SerializeField]
    private int _Score = 0;
    [SerializeField]
    private GameObject[] _hurt;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private AudioClip _laser;
    private AudioSource _laserSource;    
    [SerializeField]
    private AudioClip _explosion;
    private SpawnManager spawnManager;
    private UIManager UIManager;
    private GameManager gameManager;
    private Animator _anim;
    void Start()
    {
        //Starting pos
        transform.position = new Vector3(0, 0, 0);
        _laserSource = GetComponent<AudioSource>();
        if (_laserSource == null) Debug.LogError("_laserSource is null");
        else _laserSource.clip = _laser;
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (spawnManager == null) Debug.LogError("Spawn manager is null");        
        UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (UIManager == null) Debug.LogError("UIManager is null");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("GameManager is null");
        _anim = GetComponent<Animator>();
        if (_anim == null) Debug.LogError("anim is null");
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space)) FireLaser();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 5.5f), 0);
        if (transform.position.x >= 11.25f) transform.position = new Vector3(-11.25f, transform.position.y, 0);
        else if (transform.position.x <= -11.25f) transform.position = new Vector3(11.25f, transform.position.y, 0);
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (tripleShot == true) Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity);
        else Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.15f, 0), Quaternion.identity);
        _laserSource.Play();
    }

    public void LoseLife()
    {
        if (Shield)
        {
            _shieldPrefab.SetActive(false);
            Shield = false;
            return;
        }
        _lives -= 1;
        UIManager.UpdateLives(_lives);
        if(_lives == 2)
        {
            _hurt[Random.Range(0, 2)].SetActive(true);
        }else if(_lives == 1)
        {
            _hurt[0].SetActive(true);
            _hurt[1].SetActive(true);
        }
        else if (_lives < 1)
        {
            _anim.SetTrigger("OnPlayerDeath");
            _laserSource.clip = _explosion;
            _laserSource.Play();
            _hurt[0].SetActive(false);
            _hurt[1].SetActive(false);
            _thruster.SetActive(false);
            _speed = 0f;
            gameManager.GameOver();
            UIManager.GameOver();
            spawnManager.playerDeath();
            Destroy(this.gameObject, 2.3f);
        } 
    }

    public void TripleShot()
    {
        tripleShot = true;
        StartCoroutine(TripleShotCoolDown());
    }

    IEnumerator TripleShotCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        tripleShot = false;
    }

    public void SpeedBoost()
    {         
        _speed += Speedboost;
        StartCoroutine(SpeedboostCoolDown());
    }

    IEnumerator SpeedboostCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed -= Speedboost;
    }

    public void ShieldPowerup()
    {
        _shieldPrefab.SetActive(true);
        Shield = true;
    }

    public void updateScore(int newScore)
    {
        _Score += newScore;
        UIManager.UpdateScore(_Score);
    }

}
