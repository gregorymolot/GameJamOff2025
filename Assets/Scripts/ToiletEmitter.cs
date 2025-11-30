using UnityEngine;

public class ToiletEmitter : MonoBehaviour, IInteractable
{
    public bool Returnable { get => false; set => Returnable = false; }
    public bool Interactable { get => interactable; set => interactable = value; }
    private bool interactable;
    SphereSoundEmitter sphereSound;

    GameObject topOfToilet;

    public void Interact()
    {
        UIManager.Instance.ShowFailedText("Hmm... that toilet didn't sound normal...");
        if (sphereSound == null)
        {
            sphereSound = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 8f, 1f, Room.Bathroom, 15f);
        }
        else
        {
            sphereSound.EndSound();
            sphereSound = SphereSoundEmitterManager.Instance.SpawnSoundEmitter(transform.position, 8f, 1f, Room.Bathroom, 15f);
        }
    }

    public void Return()
    {
    }
}
