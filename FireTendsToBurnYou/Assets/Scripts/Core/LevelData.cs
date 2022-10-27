using UnityEngine.UI;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] Text numberText;
    [HideInInspector] public int levelNum;
    [SerializeField] GameObject parentObject;

    public void setLevel(int level)
    {
        levelNum = level;
        numberText.text = levelNum.ToString();
    }

    public void changeActive(bool newActive)
    {
        parentObject.SetActive(newActive);
    }
}
