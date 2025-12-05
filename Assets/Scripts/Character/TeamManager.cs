using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TeamSaveData
{
    public string[] charGUIDs;
}

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance { get; private set; }
    public event Action OnTeamChanged;
    public TeamSlot[] slots;
    public CharData[] allCharacters;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public CharData[] GetAssignedCharacters()
    {
        List<CharData> list = new List<CharData>();

        foreach (var slot in slots)
        {
            if (slot.assignedChar != null)
                list.Add(slot.assignedChar);
        }
        return list.ToArray();
    }

    public void SetCharInSlot(int index, CharData c)
    {
        slots[index].assignedChar = c; // GÁN LẠI TEAM CHÍNH XÁC
        SaveTeam();
        OnTeamChanged?.Invoke();
    }

    public void SaveTeam()
    {
        TeamSaveData data = new TeamSaveData();
        data.charGUIDs = new string[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            data.charGUIDs[i] = slots[i].assignedChar ? slots[i].assignedChar.name : "";
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("saved_team", json);
        PlayerPrefs.Save();
        Debug.Log("Team saved: " + json);
    }

    public void LoadTeam()
    {
        if (!PlayerPrefs.HasKey("saved_team")) return;
        string json = PlayerPrefs.GetString("saved_team");
        TeamSaveData data = JsonUtility.FromJson<TeamSaveData>(json);

        for (int i = 0; i < slots.Length && i < data.charGUIDs.Length; i++)
        {
            string id = data.charGUIDs[i];
            if (string.IsNullOrEmpty(id))
            {
                slots[i].AssignChar(null);
            }
            else
            {
                CharData found = FindCharByName(id);
                slots[i].AssignChar(found);
            }

        }
    }

    private CharData FindCharByName(string name)
    {
        if (allCharacters == null) return null;
        foreach (var c in allCharacters)
        {
            if (c != null && c.name == name) return c;
        }
        return null;
    }

    private void Start()
    {
        LoadTeam();
    }

    
}
