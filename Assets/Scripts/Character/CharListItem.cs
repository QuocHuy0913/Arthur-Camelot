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

    public void Setup(CharData charData, CharacterSelectUI ui)
    {
        data = charData;
        selectUI = ui;
        avatarImage.sprite = data.avatar;
        nameText.text = data.charName;
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        selectUI.OnSelectChar(data);
    }
}
