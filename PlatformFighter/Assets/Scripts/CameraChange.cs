using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> players;


    const float MINSIZE = 4f;

    // prviate
    private Camera camera;
    private float sizeToDistanceRatio = 0.5f;

    void Start()
    {
        camera = GetComponent<Camera>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = calculateLocation();
        camera.orthographicSize = getCameraSize();
    }


    Vector3 calculateLocation()
    {
        float x = 0;
        float y = 0;
        foreach (GameObject game in players)
        {
            x += game.transform.position.x;
            y += game.transform.position.y;
        }
        x = x / players.Count;
        y = y / players.Count;

        return new Vector3(x, y, -10);
    }

    float getCameraSize()
    {
        float maxDis = 0;
        if(players.Count <= 2)
        {
            maxDis = findDistance(players[0].transform.position, players[1].transform.position);
        }
        else
        {
            for(int i = 0; i < players.Count; i++)
            {
                for(int j = i + 1; j < players.Count; j++)
                {
                    maxDis = Math.Max(maxDis,
                        findDistance(players[i].transform.position, players[j].transform.position));
                }
            }
        }

        maxDis = sizeToDistanceRatio * maxDis;

        if(maxDis < MINSIZE)
        {
            maxDis = MINSIZE;
        }



        return maxDis;
    }

    float findDistance(Vector3 p1, Vector3 p2)
    {
        float xDiff = p1.x - p2.x;
        float yDiff = p1.y - p2.y;
        double ans = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        return (float)ans;
    }
}
