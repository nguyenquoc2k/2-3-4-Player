using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    
    private void OnBecameInvisible()
    {
      GetComponent<StickmanController>().PlayerDeath();
    }
    
}