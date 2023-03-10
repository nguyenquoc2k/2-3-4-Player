using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFlappyBird : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            if (col.GetComponent<BotFlappyBird>() != null)
                FlappyBirdControllerMap.Instances.CheckPlayerDeath(col.GetComponent<BotFlappyBird>().indexPlayer);
            else if (col.GetComponent<BirdController>() != null)
                FlappyBirdControllerMap.Instances.CheckPlayerDeath(col.GetComponent<BirdController>().indexPlayer);
        }
    }
}
