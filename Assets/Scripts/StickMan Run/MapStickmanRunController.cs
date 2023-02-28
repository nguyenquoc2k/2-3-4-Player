using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class MapStickmanRunController : MonoBehaviour
{
    public static MapStickmanRunController Instances;
    [SerializeField] private PolygonCollider2D confiner;
    [SerializeField] private Transform player1, parentListPlayer;
    [SerializeField] private GameObject player2, player3, player4;
    public List<Transform> listPlayers = new List<Transform>();
    public List<Transform> staticListPlayer = new List<Transform>();
    private float waitTime = 1f;
    private float elapsedTime = 0f;

    void Awake()
    {
        listPlayers.Clear();
        Instances = this;
        AddPlayToList();
        Invoke("AddPlayToList", 0.2f);
        Invoke("HandleComponentPlayer", 0.1f);
        //HandleComponentPlayer();
    }

    private void HandleComponentPlayer()
    {
        foreach (Transform button in ReverseGravityController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer1")
            {
                player2.GetComponent<StickmanController>().enabled = false;
                player2.transform.GetChild(0).gameObject.SetActive(true);
                player3.GetComponent<StickmanController>().enabled = false;
                player3.transform.GetChild(0).gameObject.SetActive(true);
                player4.GetComponent<StickmanController>().enabled = false;
                player4.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer2")
            {
                player2.GetComponent<StickmanController>().enabled = true;
                player2.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer3")
            {
                player3.GetComponent<StickmanController>().enabled = true;
                player3.transform.GetChild(0).gameObject.SetActive(false);
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer4")
            {
                player4.GetComponent<StickmanController>().enabled = true;
                player4.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void AddPlayToList()
    {
        foreach (var item in staticListPlayer)
        {
            if (!listPlayers.Contains(item))
            {
                listPlayers.Add(item);
            }
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= waitTime)
        {
            CameraStickmanRunController.Instances.GetComponent<CinemachineVirtualCamera>().Follow = null;
            CameraStickmanRunController.Instances.GetComponent<CinemachineVirtualCamera>().transform.position =
                new Vector3(
                    CameraStickmanRunController.Instances.GetComponent<CinemachineVirtualCamera>().transform
                        .position
                        .x + 4.25f * Time.deltaTime,
                    CameraStickmanRunController.Instances.GetComponent<CinemachineVirtualCamera>().transform
                        .position.y,
                    CameraStickmanRunController.Instances.GetComponent<CinemachineVirtualCamera>().transform
                        .position
                        .z);
        }
    }
}