using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameController : MonoBehaviour
{
    public List<GameObject> listGame = new List<GameObject>();
    [SerializeField] private Transform gamePlaying;
    public void OnClickGame(int index)
    {
        HandleAfterSlectGame(index);
    }

    void HandleAfterSlectGame(int index)
    {
       Instantiate(listGame[index], gamePlaying);
       if (ListGameManager.Instances!=null)ListGameManager.Instances.uiSelectGame.SetActive(false);
    }
}
