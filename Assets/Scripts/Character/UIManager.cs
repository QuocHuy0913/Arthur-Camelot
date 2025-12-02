using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject teamSelectUI;
    public GameObject characterSelectUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void OpenTeamUI()
    {
        teamSelectUI.SetActive(true);
    }

    public void CloseTeamUI()
    {
        teamSelectUI.SetActive(false);
    }
    public void OpenCharacterSelect()
    {
        characterSelectUI.SetActive(true);
    }

    public void CloseCharacterSelect()
    {
        characterSelectUI.SetActive(false);
    }
}
