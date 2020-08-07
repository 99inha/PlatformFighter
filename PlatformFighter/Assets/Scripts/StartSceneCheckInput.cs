using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCheckInput : MonoBehaviour
{
    public SceneChange sceneChanger;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            sceneChanger.loadScene();
        }
    }
}
