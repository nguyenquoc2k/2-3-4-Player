using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorFalling : MonoBehaviour
{
    public List<Color> listColor = new List<Color>();
    [SerializeField] private SpriteRenderer _rendererLeft, _rendererRight;
    public GridManager gridManager;
    public int idColor;

    public void SetColor()
    {
        int index = Random.Range(0, listColor.Count);
        var randomColor = listColor[index];
        idColor = index;
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