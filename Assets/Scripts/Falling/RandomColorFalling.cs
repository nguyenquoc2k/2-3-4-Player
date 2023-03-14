using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorFalling : MonoBehaviour
{
    public List<Color> listColor = new List<Color>();
    [SerializeField] private SpriteRenderer _rendererLeft, _rendererRight;
    public GridManager gridManager;
    public int idColor;
    public bool isSetColor = false;
    public static event Action RandomTile;
    public void SetColor()
    {
        int index = Random.Range(0, listColor.Count);
        var randomColor = listColor[index];
        idColor = index;
        isSetColor = true;
        RandomTile?.Invoke();
        _rendererLeft.color = randomColor;
        _rendererRight.color = randomColor;
        TimeController.Instances.SetTimeCountDown(5);
        Invoke("CallCheckAndRemoveGrid", 5f);
        
    }

    void CallGridManager()
    {
        gridManager.ChangeTileColors();
    }

    void CallCheckAndRemoveGrid()
    {
        gridManager.HandleAfterRandomColor(idColor);
    }
}