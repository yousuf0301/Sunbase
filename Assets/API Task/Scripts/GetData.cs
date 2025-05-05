using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static SpownClient;

public class GetData : MonoBehaviour
{
    public string Response;
    private RootObject root;
    public static GetData instance;
    public RootObject Root
    {
        get
        {
            return root;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        StartCoroutine(GetData_Coroutine());

    }
    void Start()
    {
    }

    IEnumerator GetData_Coroutine()
    {
        string url = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);

             
                root = JsonConvert.DeserializeObject<RootObject>(request.downloadHandler.text);

                Debug.Log("Client count: " + root.clients.Count);
                Debug.Log("Label: " + root.label);
            }
        }

        SpownClient.instance.InvokClient();
        SpownClient.instance.DropDownClient(SpownClient.instance.viewState);
        

    }
}

[System.Serializable]
public class RootObject
{
    public List<Client> clients;
    public Dictionary<string, Data> data;
    public string label;
}

[System.Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[System.Serializable]
public class Data
{
    public string address;
    public string name;
    public int points;
}