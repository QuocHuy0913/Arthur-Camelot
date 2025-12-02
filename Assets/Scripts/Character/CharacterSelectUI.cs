using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public Image avatarDisplay;
    public TMP_Text infoText;
    public Transform charListParent;
    public GameObject charItemPrefab;
    public CharData[] allCharacters;

    private TeamSlot currentSlot;
    private CharData currentSelected;

    public void Open(TeamSlot slot)
    {
        currentSlot = slot;
        currentSelected = slot.assignedChar;
        UpdatePreview();
        UIManager.Instance.OpenCharacterSelect();
    }

    public void Close()
    {
        UIManager.Instance.CloseCharacterSelect();
    }

    public void GenerateCharList()
    {
        foreach (Transform t in charListParent) Destroy(t.gameObject);
        foreach (CharData ch in allCharacters)
        {
            GameObject go = Instantiate(charItemPrefab, charListParent);
            CharListItem item = go.GetComponent<CharListItem>();
            item.Setup(ch, this);
        }
    }

    public void OnSelectChar(CharData charData)
    {
        currentSelected = charData;
        UpdatePreview();
    }

    void UpdatePreview()
    {
        if (currentSelected != null)
        {
            avatarDisplay.sprite = currentSelected.avatar;
            infoText.text = $"Name: {currentSelected.charName}\nHP: {currentSelected.hp}\nMP: {currentSelected.mp}\nDamage: {currentSelected.basicDamage}";
        }
        else
        {
            avatarDisplay.sprite = null;
            infoText.text = "No char selected";
        }
    }

    public void ConfirmSelection()
    {
        if (currentSlot != null && currentSelected != null)
        {
            currentSlot.AssignChar(currentSelected);
        }
        Close();
    }

    public void ConfirmClearSlot()
    {
        if (currentSlot != null)
        {
            currentSlot.Clear();
        }
        Close();
    }

    void Start()
    {
        GenerateCharList();
    }
}
