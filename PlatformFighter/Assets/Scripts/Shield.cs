using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Vector3 scale;
    const float SHIELDDURATION = 2f;
    const float RECHARGETIME = 4f;
    float currentTime = 2f;
    float ratio = 1f;
     
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        ratio = currentTime / SHIELDDURATION;
        transform.localScale = new Vector3(scale.x * ratio, scale.y * ratio, scale.z * ratio);
    }
}
