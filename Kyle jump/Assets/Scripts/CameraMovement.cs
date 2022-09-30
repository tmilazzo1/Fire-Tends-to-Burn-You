using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera cam;
    Vector3 oldCamPos;
    float widthOfScreen;
    float heightOfScreen;

    void Start()
    {
        cam = GetComponent<Camera>();
        heightOfScreen = cam.orthographicSize * 2;
        widthOfScreen = heightOfScreen * cam.aspect;
        GameManager.Instance.changeLevelData();
        Debug.Log(cam.aspect);
    }

    void Update()
    {
        transform.position = new Vector3(getCameraPosX(), getCameraPosY(), transform.position.z);
        if(oldCamPos != transform.position)
        {
            oldCamPos = transform.position;
            GameManager.Instance.changeLevelData();
        }
    }

    float getCameraPosX()
    {
        int screensToMove = (int)((getPlayerPosition().x + widthOfScreen / 2 * getNegativeMultiplierX()) / widthOfScreen);
        return screensToMove * widthOfScreen;
    }

    float getCameraPosY()
    {
        int screensToMove = (int)((getPlayerPosition().y + heightOfScreen / 2 * getNegativeMultiplierY()) / heightOfScreen);
        return screensToMove * heightOfScreen;
    }

    int getNegativeMultiplierX()
    {
        if(getPlayerPosition().x > 0) {
            return 1;
        }else
        {
            return -1;
        }
    }

    int getNegativeMultiplierY()
    {
        if (getPlayerPosition().y > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    Vector3 getPlayerPosition()
    {
        if(Player.Instance)
        {
            return Player.Instance.transform.position;
        }else
        {
            return GameManager.Instance.getTempPlayerPosition();
        }
    }
}
