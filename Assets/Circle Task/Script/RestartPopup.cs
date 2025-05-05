using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartPopup : MonoBehaviour
{
    public static RestartPopup instance;

    public GameObject restartPanel;
    void Start()
    {
        instance = this;
    }

    public void ShowPopup()
    {

        restartPanel.SetActive(true);
        restartPanel.transform.localScale = Vector3.zero;
        restartPanel.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);

        Invoke("ClosePopup", 3f);
    }

    public void ClosePopup()
    {
        restartPanel.transform.DOScale(0f, 0.3f).OnComplete(() =>
        {
            restartPanel.SetActive(false);
        });
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void LoadApiScene()
    {

        SceneManager.LoadScene(1);

    }
    
}
