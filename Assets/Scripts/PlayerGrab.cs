using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrab : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private float radius = 4;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Collider2D selfCollider;

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("ITS BEING PERFORMED");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (Collider2D collider in colliders)
            {
                if (collider == selfCollider) continue;


                if (collider.TryGetComponent(out PlayerMovement player))
                {
                    player.grab(gameObject.transform.position);
                }
            }
        }

        if(context.canceled)
        {
            print("ITS FINISHED");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (Collider2D collider in colliders)
            {
                if (collider == selfCollider) continue;


                if (collider.TryGetComponent(out PlayerMovement player))
                {
                    //while (context.performed) 
                    //{
                    player.ungrab();
                    //}
                }
            }

        }
        
    }
}
