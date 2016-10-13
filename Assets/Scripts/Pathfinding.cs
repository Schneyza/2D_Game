using UnityEngine;
using System.Collections;

public class Pathfinding : MonoBehaviour
{

    public TileMap collisionMap;

    // Use this for initialization
    void Start()
    {
        int width = (int)collisionMap.mapSize.x;
        int height = (int)collisionMap.mapSize.y;
        int[,] map = new int[width, height];
        for (int r = 0; r < width - 1; r++)
        {
            for (int c = 0; c < height - 1; c++)
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
    }



    // Update is called once per frame
    void Update()
    {

    }
}
