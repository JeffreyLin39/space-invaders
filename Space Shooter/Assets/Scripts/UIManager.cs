using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _Score;
    [SerializeField]
    private Sprite[] _Lives;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private Text _GameOver;
    [SerializeField]
    private Text _Restart;

    void Start()
    {
        _Score.text = "Score: " + 0; 
    }

    public void UpdateScore(int currentScore)
    {
        _Score.text = "Score: " + currentScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _Lives[currentLives];
    }

    public void GameOver()
    {
        _GameOver.gameObject.SetActive(true);
        _Restart.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
        while(true)
        {
            
            _GameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _GameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
