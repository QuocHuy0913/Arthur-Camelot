using System.Linq;
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
        GenerateCharList();   // <-- bổ sung dòng này
        UpdatePreview();
        UIManager.Instance.OpenCharacterSelect();
    }
    public void Close() { UIManager.Instance.CloseCharacterSelect(); }

    public void GenerateCharList()
    {
        foreach (Transform t in charListParent) Destroy(t.gameObject);
        CharData[] usedChars = TeamManager.Instance.GetAssignedCharacters();
        foreach (CharData ch in allCharacters)
        {
            GameObject go = Instantiate(charItemPrefab, charListParent);
            CharListItem item = go.GetComponent<CharListItem>();
            bool isUsedByOtherSlot = usedChars.Contains(ch) && ch != currentSlot.assignedChar;
            item.Setup(ch, this, isUsedByOtherSlot);
        }
    }

    public void OnSelectChar(CharData charData)
    {
        CharData[] usedChars = TeamManager.Instance.GetAssignedCharacters();

        if (usedChars.Contains(charData) && charData != currentSlot.assignedChar)
        {
            Debug.Log("Character already assigned to another slot.");
            return; // Không chọn được char đã dùng ở slot khác
        }

        if (currentSelected == charData)
            currentSelected = null;
        else
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
        if (currentSlot != null)
        { // Nếu chọn char → gán vào team 
            if (currentSelected != null)
            { currentSlot.AssignChar(currentSelected); }
            else
            {
                currentSlot.Clear();
            }
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
