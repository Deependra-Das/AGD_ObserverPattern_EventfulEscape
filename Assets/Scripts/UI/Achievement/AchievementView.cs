using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private int PotionsRequiredToTrigger;
    [SerializeField] private AchievementScriptableObject keyMasterAchievement;
    [SerializeField] private AchievementScriptableObject sanitySaverAchievement;
    [SerializeField] private AchievementScriptableObject tormentedSurvivorAchievement;
    [SerializeField] private AchievementScriptableObject masterOfShadowsAchievement;

    [Header("Achievement Popup")]
    [SerializeField] private GameObject achievementPopup;
    [SerializeField] private TextMeshProUGUI achievementText;

    private Coroutine achievementCoroutine;

    private void OnEnable() 
    { 
        EventService.Instance.OnKeyPickedUp.AddListener(showKeyMasterAchievement);
        EventService.Instance.OnPotionDrinkEvent.AddListener(showSanitySaverAchievement);
    }
    private void OnDisable() 
    {
        EventService.Instance.OnKeyPickedUp.RemoveListener(showKeyMasterAchievement);
        EventService.Instance.OnPotionDrinkEvent.RemoveListener(showSanitySaverAchievement);
    }

    public void ShowAchievement(AchievementType type)
    {
        stopCoroutine(achievementCoroutine);
        switch (type)
        {
            case AchievementType.KEYMASTER:
                achievementCoroutine = StartCoroutine(setAchievement(keyMasterAchievement));
                break;
            case AchievementType.SANITY_SAVER:
                achievementCoroutine = StartCoroutine(setAchievement(sanitySaverAchievement));
                break;
        }
    }

    private IEnumerator setAchievement(AchievementScriptableObject achievement)
    {
        yield return new WaitForSeconds(achievement.WaitToTriggerDuration);
        showAchievmentPopup(achievement);

        yield return new WaitForSeconds(achievement.DisplayDuration);
        hideAchievmentPopup();
    }

    private void hideAchievmentPopup()
    {
        achievementText.SetText(string.Empty);
        achievementPopup.SetActive(false);
        stopCoroutine(achievementCoroutine);
    }
    private void showAchievmentPopup(AchievementScriptableObject achievement)
    {
        achievementText.SetText(achievement.Achievement);
        achievementPopup.SetActive(true);
    }
    private void stopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void showKeyMasterAchievement(int keys)
    {
        if(keys== keysRequiredToTrigger)
        {
            ShowAchievement((AchievementType.KEYMASTER));
            GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.AchievementUnlocked);
        }
    }

    private void showSanitySaverAchievement(int potionEffect, int potionsDrank)
    {
        Debug.Log(potionsDrank);
        if (potionsDrank == PotionsRequiredToTrigger)
        {
            ShowAchievement((AchievementType.SANITY_SAVER));
            GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.AchievementUnlocked);
        }
    }

    private void showAchievement(AchievementScriptableObject achievement)
    {
        stopCoroutine(achievementCoroutine);
        achievementCoroutine = StartCoroutine(setAchievement(achievement));
    }
}
