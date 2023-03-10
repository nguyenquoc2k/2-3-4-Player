using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class GameDemoManager : MonoBehaviour
{
    [SerializeField] private Transform gameDemo;
    [SerializeField] private GhostInGameDemoManager ghost;
    public static GameDemoManager Instances;
    public GameObject ghostPlayer1;
    public GameObject ghostPlayer2;
    public GameObject ghostPlayer3;
    public GameObject ghostPlayer4;
    public AstarPath astarPath;
    public GameObject Player1, Player2, Player3, Player4;
    public List<GameObject> listPlayer;
    [SerializeField] private Transform parentListPlayer;
    private Transform parentTransform;
    private bool joystick1Status, joystick2Status, joystick3Status, joystick4Status;
    public WallController wallController;
    private float scaleSpawn = 0.7f;

    void Awake()
    {
        Instances = this;
        Transform transform = GameObject.Find("PathFinding").transform;
        astarPath = transform.GetComponent<AstarPath>();
        ShowResultGameDemo.Instances.gameObject.SetActive(false);
        astarPath.Scan();
        Invoke("InitGhost", 2f);
        AddPlayerToList();
        CheckAmountPlayer();
        parentTransform = GameObject.Find("UIInGame").transform;
    }

    private void CheckAmountPlayer()
    {
        parentTransform = GameObject.Find("UIInGame").transform;
        foreach (Transform joystick in parentTransform)
        {
            if (joystick.gameObject.activeInHierarchy && joystick.name == "FixedJoystick")
            {
                Player2.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player2.GetComponent<AIPlayerController>().enabled = true;
                Player2.GetComponent<AIPath>().enabled = true;
                Player2.GetComponent<AIDestinationSetter>().enabled = true;
                Player2.GetComponent<Rigidbody2D>().freezeRotation = false;
                
                Player3.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player3.GetComponent<AIPlayerController>().enabled = true;
                Player3.GetComponent<AIPath>().enabled = true;
                Player3.GetComponent<AIDestinationSetter>().enabled = true;
                Player3.GetComponent<Rigidbody2D>().freezeRotation = false;
                
                Player4.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player4.GetComponent<AIPlayerController>().enabled = true;
                Player4.GetComponent<AIPath>().enabled = true;
                Player4.GetComponent<AIDestinationSetter>().enabled = true;
                Player4.GetComponent<Rigidbody2D>().freezeRotation = false;
                joystick1Status = true;
                
            }

            if (joystick.gameObject.activeInHierarchy && joystick.name == "FixedJoystick1")
            {
                Player2.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player2.GetComponent<AIPlayerController>().enabled = false;
                Player2.GetComponent<AIPath>().enabled = false;
                Player2.GetComponent<AIDestinationSetter>().enabled = false;
                Player2.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                Player3.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player3.GetComponent<AIPlayerController>().enabled = true;
                Player3.GetComponent<AIPath>().enabled = true;
                Player3.GetComponent<AIDestinationSetter>().enabled = true;
                Player3.GetComponent<Rigidbody2D>().freezeRotation = false;
                
                Player4.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player4.GetComponent<AIPlayerController>().enabled = true;
                Player4.GetComponent<AIPath>().enabled = true;
                Player4.GetComponent<AIDestinationSetter>().enabled = true;
                Player4.GetComponent<Rigidbody2D>().freezeRotation = false;
                joystick2Status = true;
            }

            if (joystick.gameObject.activeInHierarchy && joystick.name == "FixedJoystick2")
            {
                Player2.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player2.GetComponent<AIPlayerController>().enabled = false;
                Player2.GetComponent<AIPath>().enabled = false;
                Player2.GetComponent<AIDestinationSetter>().enabled = false;
                Player2.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                Player3.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player3.GetComponent<AIPlayerController>().enabled = false;
                Player3.GetComponent<AIPath>().enabled = false;
                Player3.GetComponent<AIDestinationSetter>().enabled = false;
                Player3.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                Player4.GetComponent<HumanCharacterGhostAttack>().enabled = false;
                Player4.GetComponent<AIPlayerController>().enabled = true;
                Player4.GetComponent<AIPath>().enabled = true;
                Player4.GetComponent<AIDestinationSetter>().enabled = true;
                Player4.GetComponent<Rigidbody2D>().freezeRotation = false;
                joystick3Status = true;
            }

            if (joystick.gameObject.activeInHierarchy && joystick.name == "FixedJoystick3")
            {
                Player2.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player2.GetComponent<AIPlayerController>().enabled = false;
                Player2.GetComponent<AIPath>().enabled = false;
                Player2.GetComponent<AIDestinationSetter>().enabled = false;
                Player2.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                Player3.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player3.GetComponent<AIPlayerController>().enabled = false;
                Player3.GetComponent<AIPath>().enabled = false;
                Player3.GetComponent<AIDestinationSetter>().enabled = false;
                Player3.GetComponent<Rigidbody2D>().freezeRotation = true;
                
                Player4.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                Player4.GetComponent<AIPlayerController>().enabled = false;
                Player4.GetComponent<AIPath>().enabled = false;
                Player4.GetComponent<AIDestinationSetter>().enabled = false;
                Player4.GetComponent<Rigidbody2D>().freezeRotation = true;
                joystick4Status = true;
            }
        }
    }

    private void AddPlayerToList()
    {
        listPlayer = new List<GameObject>(GetComponentsInChildren<Transform>().Length);
        foreach (Transform child in parentListPlayer.transform)
        {
            listPlayer.Add(child.gameObject);
        }
    }

    private void Update()
    {
        CheckAndRemovePlayer();
    }

    private void CheckAndRemovePlayer()
    {
        for (int i = listPlayer.Count - 1; i >= 0; i--)
        {
            if (listPlayer[i] == null)
            {
                listPlayer.RemoveAt(i);
            }
        }

        if (listPlayer.Count == 1)
        {
            if (wallController != null) wallController.DestroyWall();
            ShowResultGameDemo.Instances.gameObject.SetActive(true);
            ShowResultGameDemo.Instances.ShowResult(listPlayer[0].name);
        }
    }

    public void InitGhostAfterPlayerDie(string name, Vector3 spawn)
    {
        parentTransform = GameObject.Find("UIInGame").transform;
        if (name == "Player1")
        {
            ghostPlayer1 = Instantiate(ghostPlayer1, gameDemo);
            ghostPlayer1.transform.position = spawn;
            ghostPlayer1.transform.localScale = new Vector3(scaleSpawn, scaleSpawn, scaleSpawn);
            if (joystick1Status == true)
            {
                ghostPlayer1.GetComponent<GhostInGameDemoManager>().enabled = false;
                ghostPlayer1.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                ghostPlayer1.GetComponent<HumanCharacterGhostAttack>().AddJoystick();
            }
            else
            {
                ghostPlayer1.GetComponent<GhostInGameDemoManager>().enabled = true;
                ghostPlayer1.GetComponent<HumanCharacterGhostAttack>().enabled = false;
            }
        }
        else if (name == "Player2")
        {
            ghostPlayer2 = Instantiate(ghostPlayer2, gameDemo);
            ghostPlayer2.transform.position = spawn;
            ghostPlayer2.transform.localScale = new Vector3(scaleSpawn, scaleSpawn, scaleSpawn);
            if (joystick2Status == true)
            {
                ghostPlayer2.GetComponent<GhostInGameDemoManager>().enabled = false;
                ghostPlayer2.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                ghostPlayer2.GetComponent<HumanCharacterGhostAttack>().AddJoystick();
            }
            else
            {
                ghostPlayer2.GetComponent<GhostInGameDemoManager>().enabled = true;
                ghostPlayer2.GetComponent<HumanCharacterGhostAttack>().enabled = false;
            }
        }
        else if (name == "Player3")
        {
            ghostPlayer3 = Instantiate(ghostPlayer3, gameDemo);
            ghostPlayer3.transform.position = spawn;
            ghostPlayer3.transform.localScale = new Vector3(scaleSpawn, scaleSpawn, scaleSpawn);
            if (joystick3Status == true)
            {
                ghostPlayer3.GetComponent<GhostInGameDemoManager>().enabled = false;
                ghostPlayer3.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                ghostPlayer3.GetComponent<HumanCharacterGhostAttack>().AddJoystick();
            }
            else
            {
                ghostPlayer3.GetComponent<GhostInGameDemoManager>().enabled = true;
                ghostPlayer3.GetComponent<HumanCharacterGhostAttack>().enabled = false;
            }
        }
        else if (name == "Player4")
        {
            ghostPlayer4 = Instantiate(ghostPlayer4, gameDemo);
            ghostPlayer4.transform.position = spawn;
            ghostPlayer4.transform.localScale = new Vector3(scaleSpawn, scaleSpawn, scaleSpawn);
            if (joystick4Status == true)
            {
                ghostPlayer4.GetComponent<GhostInGameDemoManager>().enabled = false;
                ghostPlayer4.GetComponent<HumanCharacterGhostAttack>().enabled = true;
                ghostPlayer4.GetComponent<HumanCharacterGhostAttack>().AddJoystick();
            }
            else
            {
                ghostPlayer4.GetComponent<GhostInGameDemoManager>().enabled = true;
                ghostPlayer4.GetComponent<HumanCharacterGhostAttack>().enabled = false;
            }
        }
    }

    void InitGhost()
    {
        ghost = Instantiate(ghost, gameDemo);
        ghost.transform.position = Vector3.zero;
        ghost.transform.SetParent(parentListPlayer);
        ghost.transform.localScale = new Vector3(scaleSpawn, scaleSpawn, scaleSpawn);
        ghost.transform.localPosition = new Vector3(0, 0, 0);
    }
}