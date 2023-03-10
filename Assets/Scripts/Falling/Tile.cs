using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour {
    public int idTile;
    [SerializeField] private SpriteRenderer _renderer;
    private Vector2 position;
    public void SetColor(Color color, int id)
    {
        idTile = id;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
    // Thiết lập vị trí của Tile
    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
    public Vector2 GetPosition()
    {
        return position;
    }
}