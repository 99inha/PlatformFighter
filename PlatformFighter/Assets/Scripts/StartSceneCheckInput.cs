using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCheckInput : MonoBehaviour
{
    public SceneChange sceneChanger;
    public SoundManager soundMana;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            soundMana.playSound("start");
            sceneChanger.loadScene();
        }
    }
}
