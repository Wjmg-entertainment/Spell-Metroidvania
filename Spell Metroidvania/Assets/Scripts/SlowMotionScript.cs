using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Time.timeScale = .25f;
        }else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Time.timeScale = 1f;
        }
    }
}
