using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterLoader : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        LoadFirstCharacter();
    }

    void LoadFirstCharacter()
    {
        if (TeamManager.Instance == null)
        {
            Debug.LogError("TeamManager instance not found!");
            return;
        }

        CharData[] team = TeamManager.Instance.GetAssignedCharacters();

        if (team.Length == 0 || team[0] == null)
        {
            Debug.LogWarning("No character is leader");
            return;
        }

        ApplyCharacter(team[0]);
    }

    void ApplyCharacter(CharData data)
    {
        // Gán avatar đứng tạm
        if (data.avatar != null)
            spriteRenderer.sprite = data.avatar;

        // Gán animation
        OverrideAnimations(data);
    }

    void OverrideAnimations(CharData data)
    {
        AnimatorOverrideController overrideController =
            new AnimatorOverrideController(animator.runtimeAnimatorController);

        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        overrideController.GetOverrides(overrides);

        for (int i = 0; i < overrides.Count; i++)
        {
            var pair = overrides[i];
            AnimationClip newClip = pair.Value;

            switch (pair.Key.name)
            {
                case "Idle_Up": newClip = data.IdleUp; break;
                case "Idle_Down": newClip = data.IdleDown; break;
                case "Idle_Left": newClip = data.IdleLeft; break;
                case "Idle_Right": newClip = data.IdleRight; break;

                case "Walk_Up": newClip = data.walkUp; break;
                case "Walk_Down": newClip = data.walkDown; break;
                case "Walk_Left": newClip = data.walkLeft; break;
                case "Walk_Right": newClip = data.walkRight; break;
            }

            overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(pair.Key, newClip);
        }

        overrideController.ApplyOverrides(overrides);
        animator.runtimeAnimatorController = overrideController;
    }

    private void OnEnable()
    {
        if (TeamManager.Instance != null)
        {
            TeamManager.Instance.OnTeamChanged += LoadFirstCharacter;
        }
    }

    private void OnDisable()
    {
        if (TeamManager.Instance != null)
        {
            TeamManager.Instance.OnTeamChanged -= LoadFirstCharacter;
        }
    }

}
