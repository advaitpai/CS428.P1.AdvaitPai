using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Rotation : MonoBehaviour
{
    
    public GameObject ClassCube;
    public Light WhiteLight;
    public Light RedLight;
    int upsideDown;
    int mode;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Update",0f,1f);
        mode = 1;
        RedLight.enabled = false;
        if (Vector3.Dot(ClassCube.transform.up, Vector3.down) > 0) {
            upsideDown = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log("X: "+ClassCube.transform.localEulerAngles.x+"Y: "+ClassCube.transform.localEulerAngles.y+"Z: "+ClassCube.transform.localEulerAngles.z);
        //Debug.Log("X: "+ClassCube.transform.localEulerAngles.x);
        if (Vector3.Dot(ClassCube.transform.up, Vector3.down) > 0) {
            //updateText();
            upsideDown = 1;
        }
        
        if ((Vector3.Dot(ClassCube.transform.up, Vector3.down) < 0) & upsideDown == 1){
            //updateText();
            upsideDown = 0;
            Debug.Log("Flipped");
            mode = mode*-1;
        }
        if (mode == 1)
        {
            WhiteLight.enabled = true;
            RedLight.enabled = false;
        }
        else if (mode == -1)
        {
            WhiteLight.enabled = false;
            RedLight.enabled = true;
        }

    }
}
