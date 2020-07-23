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

    [SerializeField] float upperLimit;
    [SerializeField] float lowerLimit;
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;

    void Start()
    {
        camera = GetComponent<Camera>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = calculateLocation();
        camera.orthographicSize = getCameraSize();
        fixBounds();

    }


    // fixBounds: this function make sure the camera do not go beyond the bounds set by
    //      the limits, use the drawGizmos function at the end to see the bounds
    void fixBounds()
    {
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;
        Vector3 vec = camera.transform.position;

        // left and right bounds
        if(camera.transform.position.x + (width / 2) > rightLimit 
            && camera.transform.position.x - (width / 2) > leftLimit)
        {   // the right side is out of bounds
            vec.x = rightLimit - (width / 2);
        }
        else if (camera.transform.position.x + (width / 2) < rightLimit
            && camera.transform.position.x - (width / 2) < leftLimit)
        {   // the left side is out of bounds
            vec.x = leftLimit + (width / 2);
        }
        else if (camera.transform.position.x + (width / 2) > rightLimit
            && camera.transform.position.x - (width / 2) < leftLimit)
        {
            vec.x = (leftLimit + rightLimit) / 2;
        }

        // upper and lower bounds
        if(camera.transform.position.y + (height / 2) > upperLimit
            && camera.transform.position.y - (height / 2) > lowerLimit)
        {   // the top side is out of bounds
            vec.y = upperLimit - (height / 2);
        }
        else if(camera.transform.position.y + (height / 2) < upperLimit
            && camera.transform.position.y - (height / 2) < lowerLimit)
        {   // the bottom side is out of bounds
            vec.y = lowerLimit + (height / 2);
        }
        else if (camera.transform.position.y + (height / 2) > upperLimit
            && camera.transform.position.y - (height / 2) < lowerLimit)
        {
            vec.y = (upperLimit + lowerLimit) / 2;
        }

        camera.transform.position = vec;
    }



    // calculateLocation: calcualtes where the camera's location should be,
    //      it's the mean value of all item's location in the players list
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



    // getCameraSize: this function finds the max distance between any two items' location
    //      in the player list then returns a value to adjust the camera's size
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



    // findDistance: finds the 2d distance between 2 Vector3 points, the z coordinate is not used
    float findDistance(Vector3 p1, Vector3 p2)
    {
        float xDiff = p1.x - p2.x;
        float yDiff = p1.y - p2.y;
        double ans = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        return (float)ans;
    }


    // this unity function helps see where the camera bounds should be
    private void OnDrawGizmos()
    {
        Gizmos.color = new UnityEngine.Color(255, 0, 0, 1f);

        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(rightLimit, upperLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(leftLimit, lowerLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, lowerLimit), new Vector2(rightLimit, lowerLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, upperLimit), new Vector2(rightLimit, lowerLimit));
    }
}
