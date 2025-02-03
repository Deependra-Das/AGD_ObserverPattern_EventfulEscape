using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject", order = 1)]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("Player Movement:")]
    public float raycastLength;
    public float jumpForce;
    public float sensitivity;
    public float walkSpeed = 0.50f, sprintSpeed = 0.85f;
    public float rotationLimit = 0.5f;
    public int KeysEquipped = 0;
    public int PotionsDrank = 0;
}
