using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove2D : MonoBehaviour
{
    new public Collider collider;

    private TouchManager touchManager;   //タッチマネージャー。
    public Slider slider;           //カメラの拡大率のスライダー。

    public float moveSpeed = 2.0f;    //カメラのスピード。
    new Camera camera;                  //カメラ。
    Vector3 position;               //座標。
    Vector3 posMax;                 //コートの最大座標。
    Vector3 posMin;                 //コートの最小座標。
    Vector3 boxSize;
    float ScreenAspect;             //アスペクト比。
    bool isMove = false;            //動いたかどうか。

    // Start is called before the first frame update
    void Start()
    {
        touchManager = TouchManager.GetInstance();
        camera = GetComponent<Camera>();
        position = camera.transform.position;

        posMax = collider.bounds.max;
        posMin = collider.bounds.min;
        boxSize = posMax - posMin;
        ScreenAspect = (float)Screen.height / Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();       //カメラの移動。
        CameraPosLimit();   //カメラの位置を制限。
    }

    void CameraMove()
    {
        isMove = false;
        if (touchManager.IsTouch())
        {
            if(touchManager.GetPhase() == TouchInfo.Moved)
            {
                float camSpeed = camera.orthographicSize;

                position.x += touchManager.GetDeltaPosInScreen().x * -moveSpeed * camSpeed;
                position.z += touchManager.GetDeltaPosInScreen().y * -moveSpeed * camSpeed;

                isMove = true;
            }
        }

    }

    void CameraPosLimit()
    {
        float cameraCenterY = camera.orthographicSize;
        float cameraCenterX = cameraCenterY / ScreenAspect;
        //xを範囲に収める。
        if (boxSize.x > cameraCenterX * 2.0f)
        {
            position.x = Mathf.Clamp(position.x, posMin.x + cameraCenterX, posMax.x - cameraCenterX);
        }
        else
        {
            position.x = collider.transform.position.x;
        }
        //zを範囲に収める。
        if(boxSize.z > cameraCenterY * 2.0f)
        {
            position.z = Mathf.Clamp(position.z, posMin.z + cameraCenterY, posMax.z - cameraCenterY);
        }
        else
        {
            position.z = collider.transform.position.z;
        }

        camera.transform.position = position;
    }

    public bool IsMoved()
    {
        return isMove;
    }
}
