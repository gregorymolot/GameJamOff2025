using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    bool Returnable{ get; set; }
    public void Interact();

    public void Return();
}
