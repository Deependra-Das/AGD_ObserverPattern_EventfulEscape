using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    public void Interact()
    {   
        int currentKeys = GameService.Instance.GetPlayerController().KeysEquipped;
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);

        currentKeys++;
        EventService.Instance.OnKeyPickedUp.InvokeEvent(currentKeys);

        gameObject.SetActive(false);
    }
}
