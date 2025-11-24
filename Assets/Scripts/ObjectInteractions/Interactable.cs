

public interface IInteractable
{
    bool Returnable{ get; set; }
    bool Interactable{ get; set; }
    public void Interact();

    public void Return();
}
