using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    bool IsInteracting{ get; set; }
    public void Interact();

    public void Return();
}
