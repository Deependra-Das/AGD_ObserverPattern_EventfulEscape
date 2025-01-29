using UnityEngine;

[CreateAssetMenu(fileName = "AchievementScriptableObject", menuName = "ScriptableObjects/AchievementScriptableObject", order = 3)]

public partial class AchievementScriptableObject : ScriptableObject
{
    public AchievementType AchievementType;
    public string Achievement;
    public int DisplayDuration;
    public int WaitToTriggerDuration = 0;
}