using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    public Dictionary<Vector2, Tile> _tiles = new Dictionary<Vector2, Tile>();
    public List<Color> listColor = new List<Color>();
    public RandomColorFalling randomColorFalling;
    public Color defaultColor;

    void Awake()
    {
        Invoke("GenerateGrid", 3f);
    }

    public void HandleAfterRandomColor(int idColor)
    {
        List<Tile> tilesToRemove = new List<Tile>();

        foreach (var tile in _tiles.Values)
        {
            if (tile.GetComponent<Tile>().idTile == idColor)
            {
                // Nếu màu khớp, không làm gì cả
                tile.gameObject.tag = "Obstacle";
            }
            else
            {
                // Nếu màu không khớp, thêm Tile vào danh sách cần xóa
                tile.GetComponent<SpriteRenderer>().color = Color.black;
                tile.gameObject.tag = "Hole";
            }
        }


        Invoke("FillColor", 2f);
    }

    private void FillColor()
    {
        // Convert the color code string to a Color object
        Color color = defaultColor;

        // Loop through all the values in the _tiles dictionary
        foreach (Tile tile in _tiles.Values)
        {
            // Get the sprite renderer component of the tile
            SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();

            // Set the color of the sprite renderer to the new color
            spriteRenderer.color = color;
            tile.gameObject.tag = "Untagged";
        }

        Invoke("ChangeTileColors", 1f);
    }

    public void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x - 4.25f, y - 3.5f), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(transform);
                spawnedTile.SetPosition(new Vector3(x - 2.25f, y - 3));
                //var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                //spawnedTile.Init(isOffset);
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        Invoke("ChangeTileColors", 3f);
    }

    public void ChangeTileColors()
    {
        randomColorFalling.isSetColor = false;
        foreach (var tile in _tiles.Values)
        {
            int id = UnityEngine.Random.Range(0, listColor.Count);
            var randomColor = listColor[id];
            tile.SetColor(randomColor, id);
        }

        TimeController.Instances.SetTimeCountDown(3);
        Invoke("CallSetColor", 3f);
    }

    void CallSetColor()
    {
        randomColorFalling.SetColor();
    }
}