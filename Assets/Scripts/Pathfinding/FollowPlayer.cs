﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour
{

    public TileMap collisionMap;
    public GameObject player;
    public Sprite pathSprite;
    public int followThreshold;
    public float speed = 50;

    private Vector3 nextWaypoint = Vector3.back;
    private List<Vector3> waypoints;
    private Search search;
    private Graph graph;
    private int width;
    private int height;
    private Vector3 playerPos;
    // Use this for initialization
    void Start()
    {
        collisionMap = GameObject.FindWithTag("CollisionMap").GetComponent<TileMap>();
        width = (int)collisionMap.mapSize.x;
        height = (int)collisionMap.mapSize.y;
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

        graph = new Graph(map);
        search = new Search(graph);
        waypoints = new List<Vector3>();

        calculatePathToPlayer();
    }

    void OnEnable()
    {
        //Object Pooling requires this to be again, after the player has died
        player = GameObject.FindWithTag("Player");
    }

    public int posToIndex(Vector3 pos)
    {
        float posX = pos.x;
        posX -= collisionMap.transform.position.x;  //correction if map is not at the origin
        float posY = pos.y;
        posY -= collisionMap.transform.position.y;  //correction if map is not at the origin
        int tileX = (int)(posX / collisionMap.tileSize.x);
        int tileY = (int)(-posY / collisionMap.tileSize.y);
        return (tileX + tileY * (int)collisionMap.mapSize.x);
    }

    public Vector3 indexToPos(int index)
    {
        float posX = (index % (width) * collisionMap.tileSize.x) + collisionMap.tileSize.x / 2;
        posX += collisionMap.transform.position.x; //correction if map is not at the origin
        float posY = ((index / -(int)collisionMap.mapSize.y) * collisionMap.tileSize.y) - collisionMap.tileSize.y / 2;
        posY += collisionMap.transform.position.y; //correction if map is not at the origin
        return new Vector3(posX, posY, 0);
    }

    public void calculatePathToPlayer()
    {
        if (player != null)
        {
            int startPos;
            if (waypoints.Count > 0)
            {
                startPos = posToIndex(waypoints[waypoints.Count - 1]);
            }
            else
            {
                startPos = posToIndex(transform.position);
            }

            int playerGridPos = posToIndex(player.transform.position);
            search.Start(graph.nodes[startPos], graph.nodes[playerGridPos]);

            while (!search.finished)
            {
                search.Step();
            }

            if (waypoints.Count > 0)
            {
                foreach (Vector3 point in convertPath(search.path))
                {
                    if (point != waypoints[waypoints.Count - 1])
                    {
                        waypoints.Add(point);
                    }
                }
            }
            else
            {
                waypoints = convertPath(search.path);
            }
            nextWaypoint = Vector3.back;
            //foreach (var tile in search.path)
            //{
            //    GameObject go = new GameObject("Path_Step_" + tile.label);
            //    //go.transform.SetParent(transform);
            //    int index = Int32.Parse(tile.label);
            //    int posX = index % width * (int)collisionMap.tileSize.x + (int)collisionMap.tileSize.x / 2;
            //    int posY = index / width * (int)collisionMap.tileSize.y + (int)collisionMap.tileSize.y / 2;
            //    go.transform.position = new Vector3(posX, -posY, 0);
            //    go.AddComponent<SpriteRenderer>();
            //    go.GetComponent<SpriteRenderer>().sprite = pathSprite;
            //}

            search.finished = false;
            playerPos = Vector3.back;

        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            checkLos();
        }
    }

    List<Vector3> convertPath(List<Node> list)
    {
        List<Vector3> path = new List<Vector3>();
        for (int i = 0; i < list.Count; i++)
        {
            path.Add(indexToPos(Int32.Parse(list[i].label)));
        }

        return path;
    }

    public void walk()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
        if (transform.position == nextWaypoint)
        {
            waypoints.Remove(waypoints[0]);
            if (waypoints.Count > 0)
            {
                nextWaypoint = waypoints[0];
            }
        }
    }

    void updatePathMovement()
    {
        //set the new playerposition if it is not set yet
        if (playerPos == Vector3.back)
        {
            playerPos = player.transform.position;
        }

        //if the player has moved a certain distance, update the path
        if (Mathf.Abs(Vector3.Distance(playerPos, player.transform.position)) > followThreshold)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            calculatePathToPlayer();
        }

        //continue walking along the path if the is not reached yet
        if (waypoints.Count > 0)
        {
            if (nextWaypoint == Vector3.back)
            {
                nextWaypoint = waypoints[0];
            }
            walk();
        }
    }



    void checkLos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        //Debug.DrawRay(transform.position, player.transform.position - transform.position);
        if (hit.transform.tag == "Player")
        {
            if (waypoints.Count > 0)
            {
                resetPath();
            }
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            updatePathMovement();
        }
    }

    void resetPath()
    {
        waypoints.Clear();
        nextWaypoint = Vector3.back;
    }

}
