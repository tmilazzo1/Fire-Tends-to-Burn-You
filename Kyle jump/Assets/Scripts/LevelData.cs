using UnityEngine.UI;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] Text numberText;
    [HideInInspector] public int levelNum;

    public void setLevel(int level)
    {
        levelNum = level;
        numberText.text = levelNum.ToString();
    }
}
