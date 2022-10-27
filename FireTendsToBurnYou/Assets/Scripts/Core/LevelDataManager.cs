using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<LevelData>().setLevel(i+1);
        }
    }
}
