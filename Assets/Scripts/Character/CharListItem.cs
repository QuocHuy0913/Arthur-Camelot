using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharListItem : MonoBehaviour
{
    public Image avatarImage;
    public TMP_Text nameText;
    private CharData data;
    private CharacterSelectUI selectUI;
    public Button button;

    public void Setup(CharData charData, CharacterSelectUI ui, bool isLocked)
    {
        data = charData;
        selectUI = ui;
        avatarImage.sprite = data.avatar;
        nameText.text = data.charName;

        if (isLocked)
        {
            button.interactable = false;
            avatarImage.color = new Color(1, 1, 1, 0.7f);
            nameText.color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            button.interactable = true;
            avatarImage.color = Color.white;
            nameText.color = Color.white;

            button.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        selectUI.OnSelectChar(data);
    }
}
