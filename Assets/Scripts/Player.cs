using UnityEngine;

public class Player : MonoBehaviour
{

    Vector2 movement;

    [SerializeField]
    float playerSpeed;

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
        Debug.Log(character.velocity.magnitude);
    }
}
