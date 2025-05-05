using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DropDownController : MonoBehaviour
{
    public SpownClient SpawnClient;

    public void Dropdown(int index)
    {

        switch (index)
        {
            case 0: SpawnClient.DropDownClient(SpownClient.ViewState.AllClient); break;
            case 1: SpawnClient.DropDownClient(SpownClient.ViewState.IsManager); break;
            case 2: SpawnClient.DropDownClient(SpownClient.ViewState.NonManager); break;
        
        }
        
       
    }
  
}
