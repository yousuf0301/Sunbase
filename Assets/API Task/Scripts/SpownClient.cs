using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static SpownClient;

public class SpownClient : MonoBehaviour
{
    public GameObject _clientPrefab;
    public Transform _spawnPosClient;
    public Transform _spawnPosPoint;
   
    public ViewState viewState;
    public enum ViewState
    {
        IsManager,
        NonManager,
        AllClient
    }

    public Dictionary<bool, GameObject> clientsByRole = new Dictionary<bool, GameObject>();

    public List<GameObject> AllClient;
    public List<GameObject> manager;
    public List<GameObject> nonManager;

    public static SpownClient instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
   public void InvokClient()
    {
        RootObject root = GetData.instance.Root;
        
        for(int i = 0; i<root.clients.Count;i++)
        {
            GameObject client = Instantiate(_clientPrefab, _spawnPosClient);
            GameObject point = Instantiate(_clientPrefab, _spawnPosPoint);
            client.name = "Client" + i;
            CreateClient(root, client, i , point);
            AllClient.Add(client);
            AllClient.Add(point);
            if(root.clients[i].isManager)
            {
                manager.Add(client);
                manager.Add(point);
            }
            else
            {
                nonManager.Add(client);
                nonManager.Add(point);

            }
        }
    }

    void CreateClient(RootObject root, GameObject prefab, int i, GameObject point)
    {
        
        Transform labelText = prefab.transform.GetChild(0);  
        Transform pointText = point.transform.GetChild(0);  
        point.GetComponent<Button>().enabled = false;
      
        string clientId = root.clients[i].id.ToString();

      
        Data clientData = null;
        if (root.data.ContainsKey(clientId))
        {
            clientData = root.data[clientId];
        }

        // Extract info
        string prefabLabel = root.clients[i].label;  // What shows on the prefab
        string displayName = clientData != null ? clientData.name : prefabLabel;  // Real name for popup
        string address = clientData != null ? clientData.address : "No Address";
        int points = clientData != null ? clientData.points : 0;

        // Update prefab UI
        labelText.GetComponent<Text>().text = prefabLabel;
        pointText.GetComponent<Text>().text = "Point: " + points.ToString();

        // Setup popup to show the correct name from Data
        prefab.GetComponent<Button>().onClick.AddListener(() =>
        {
            ClientPopup.instance.ShowPopup(displayName, points, address);
        });
    }




    public void DropDownClient(ViewState viewState)
    {
        

        switch (viewState)
        {
            case ViewState.AllClient:
                foreach (GameObject list in AllClient)
                {
                        list.SetActive(true);
                }
                break;

            case ViewState.IsManager:
                foreach (GameObject list in nonManager)
                {
                    list.SetActive(false);
                }
                foreach (GameObject list in manager)
                {
                    list.SetActive(true);
                }

                break;

            case ViewState.NonManager:

                foreach (GameObject list in manager)
                {
                    list.SetActive(false);
                }
                foreach (GameObject list in nonManager)
                {
                    list.SetActive(true);
                }
                break;
        }
    }

}
