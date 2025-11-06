using UnityEngine;

public class Player : MonoBehaviour
{

    Vector2 movement;

    [SerializeField]
    float playerSpeed;

    [SerializeField]
    LayerMask interactableMask;

    IInteractable interactable;

    CharacterController character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        LookForInteractable();
    }

    public void SetWalkDirection(Vector2 direction)
    {
        movement = direction;
    }

    void Walk()
    {
        Vector3 speed = new Vector3(movement.x, 0, movement.y);
        speed = Camera.main.transform.rotation * speed;
        speed.y = 0;
        speed.Normalize();
        speed = speed * playerSpeed;
        character.SimpleMove(speed);
    }

    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    public void ReturnItem()
    {
        if (interactable != null && interactable.Returnable)
        {
            interactable.Return();
        }
    }
    
    void LookForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f, interactableMask))
        {
            if (interactable == null || interactable != hit.collider.GetComponent<IInteractable>())
            {
                interactable = hit.collider.GetComponent<IInteractable>();
            }
        }
        else
        {
            interactable = null;
        }
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2f);
    }
}
