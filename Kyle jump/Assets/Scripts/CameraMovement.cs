using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera cam;
    float widthOfScreen;
    float heightOfScreen;

    void Start()
    {
        cam = GetComponent<Camera>();
        heightOfScreen = cam.orthographicSize * 2;
        widthOfScreen = heightOfScreen * cam.aspect;
        Debug.Log(cam.aspect);
    }

    void Update()
    {
        transform.position = new Vector3(getCameraPosX(), getCameraPosY(), transform.position.z);
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
