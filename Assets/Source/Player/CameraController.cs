using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public ControlAxes MyAxes;

    float fSensHor = 1.0f;
    float fSensVer = 1.0f;

    float fMinVert = -45.0f;
    float fMaxVert = 45.0f;

    float fRotationX;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.MyPlayer.MyActivityState.Name == "IdleState")
        {
            // Horizontal Rotation
            if (MyAxes == ControlAxes.HORIZONTAL)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * fSensHor, 0);
            }
            // Vertical Rotation
            else if (MyAxes == ControlAxes.VERTICAL)
            {
                fRotationX -= Input.GetAxis("Mouse Y") * fSensVer;
                fRotationX = Mathf.Clamp(fRotationX, fMinVert, fMaxVert);

                float fRotationY = transform.localEulerAngles.y;
                transform.localEulerAngles = new Vector3(fRotationX, fRotationY, 0);
            }
            // Horizontal and Vertical Rotation
            else if (MyAxes == ControlAxes.HORIZONTAL_AND_VERTICAL)
            {
                fRotationX -= Input.GetAxis("Mouse Y") * fSensVer;
                fRotationX = Mathf.Clamp(fRotationX, fMinVert, fMaxVert);

                float fDelta = Input.GetAxis("Mouse X") * fSensHor;
                float fRotationY = transform.localEulerAngles.y + fDelta;

                transform.localEulerAngles = new Vector3(fRotationX, fRotationY, 0);
            }
        }
    }
}
