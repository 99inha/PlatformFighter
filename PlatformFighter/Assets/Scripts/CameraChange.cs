using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
    private float sizeToDistanceRatio = 0.6f;
    private float height;
    private float width;
    [SerializeField] private float maxSize;

    [SerializeField] float upperLimit;
    [SerializeField] float lowerLimit;
    [SerializeField] float leftLimit;
    [SerializeField] float rightLimit;

    

    void Start()
    {
        camera = GetComponent<Camera>();
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;
        maxSize = getMaxSize();
    }

    // Update is called once per frame
    void Update()
    {
        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;
        camera.transform.position = calculateLocation();
        camera.orthographicSize = getCameraSize();
        fixBounds();
        if(camera.orthographicSize > maxSize)
        {
            camera.orthographicSize = maxSize;
        }

        Debug.Log(camera.aspect);

    }


    // fixBounds: this function make sure the camera do not go beyond the bounds set by
    //      the limits, use the drawGizmos function at the end to see the bounds
    void fixBounds()
    {

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
        float MaxX = 0;
        float MaxY = 0;
        float x = 0;
        float y = 0;
        for (int i = 0; i < players.Count - 1; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                if(MaxX < findXDistance(players[i].transform.position, players[j].transform.position))
                {
                    MaxX = findXDistance(players[i].transform.position, players[j].transform.position);
                    x = (players[i].transform.position.x + players[j].transform.position.x) / 2;
                }

                if (MaxY < findYDistance(players[i].transform.position, players[j].transform.position))
                {
                    MaxY = findYDistance(players[i].transform.position, players[j].transform.position);
                    y = (players[i].transform.position.y + players[j].transform.position.y) / 2;
                }
            }
        }

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
            for(int i = 0; i < players.Count - 1; i++)
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

    // findXDistance: finds the difference of the x value between two point
    float findXDistance(Vector3 p1, Vector3 p2)
    {
        return Math.Abs(p1.x - p2.x);
    }

    // findYDistance: finds the difference of the y value between two point
    float findYDistance(Vector3 p1, Vector3 p2)
    {
        return Math.Abs(p1.y - p2.y);

    }

    float getMaxSize()
    {
        float maxX = 0;
        float maxY = 0;
        // 2 * cameraSize * aspectRatio = the width of camera
        maxX = (rightLimit - leftLimit) / (2 * camera.aspect);

        // 2 * cameraSize = height of camera
        maxY = (upperLimit - lowerLimit) / 2;
        return Math.Min(maxX, maxY);
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
