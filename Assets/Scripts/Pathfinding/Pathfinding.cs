using UnityEngine;
using System.Collections;
using System;

public class Pathfinding : MonoBehaviour
{

    public TileMap collisionMap;
    public Sprite pathSprite;

    // Use this for initialization
    void Start()
    {
        int width = (int)collisionMap.mapSize.x;
        int height = (int)collisionMap.mapSize.y;
        int[,] map = new int[width, height];
        for (int r = 0; r < width; r++)
        {
            for (int c = 0; c < height; c++)
            {
                map[r, c] = 0;
                Transform tile;
                if ((tile = collisionMap.tiles.transform.Find("tile_" + (r * width + c).ToString())) != null)
                {
                    if (tile.GetComponent<BoxCollider2D>() != null)
                        map[r, c] = 1;
                }
            }
        }

        var graph = new Graph(map);

        var search = new Search(graph);
        search.Start(graph.nodes[62], graph.nodes[291]);

        while (!search.finished)
        {
            search.Step();
        }
        print("Search done. Path length " + search.path.Count + " iterations " + search.iterations);

        foreach(var tile in search.path)
        {
            GameObject go = new GameObject("Path_Step_" + tile.label);
            int index = Int32.Parse(tile.label);
            int posX = index%width * (int)collisionMap.tileSize.x + (int)collisionMap.tileSize.x/2;
            int posY = index / width * (int)collisionMap.tileSize.y + (int)collisionMap.tileSize.y/2;
            go.transform.position = new Vector3(posX, -posY, 0);
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = pathSprite;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
