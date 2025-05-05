using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
public class ClientPopup : MonoBehaviour
{
    public static ClientPopup instance;

    public GameObject popupPanel;
    public TMP_Text nameText;
    public TMP_Text pointText;
    public TMP_Text addressText;

    private void Awake()
    {
        instance = this;
        popupPanel.SetActive(false);
    }

    public void ShowPopup(string name, int points, string address)
    {
       
        nameText.text = "Name: " + name;
        pointText.text = "Points: " + points;
        addressText.text = "Address: " + address;

        popupPanel.SetActive(true);
        popupPanel.transform.localScale = Vector3.zero;
        popupPanel.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);

        Invoke("ClosePopup", 3f);
    }


    public void ClosePopup()
    {
        popupPanel.transform.DOScale(0f, 0.3f).OnComplete(() =>
        {
            popupPanel.SetActive(false);
        });
    }
}
