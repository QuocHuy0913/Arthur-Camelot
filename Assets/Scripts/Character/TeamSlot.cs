using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TeamSlot : MonoBehaviour
{
    public Image avatarImage;
    public CharData assignedChar;
    public CharacterSelectUI characterSelectUI;
    public int slotIndex;

    public void OnClickSlot()
    {
        characterSelectUI.Open(this);
    }

    public void AssignChar(CharData newChar)
    {
        assignedChar = newChar;
        UpdateUI();
        TeamManager.Instance.SetCharInSlot(slotIndex, newChar);
    }

    public void Clear()
    {
        assignedChar = null;
        UpdateUI();
        TeamManager.Instance.SetCharInSlot(slotIndex, null);
    }

    public void UpdateUI()
    {
        if (assignedChar != null)
        {
            avatarImage.sprite = assignedChar.avatar;
            avatarImage.color = Color.white;
        }
        else
        {
            avatarImage.sprite = null;
            avatarImage.color = new Color(1,1,1,0);
        }
    }

    void Start()
    {
        UpdateUI();
    }
}
