using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject victory, lose;

    private void Awake()
    {
        Instance = this;
    }

    public void OnVictory()
    {
        victory.gameObject.SetActive(true);
    }
    public void OnLose()
    {
        lose.gameObject.SetActive(true);
    }
}
