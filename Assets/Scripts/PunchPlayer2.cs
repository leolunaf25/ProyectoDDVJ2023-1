using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchPlayer2 : MonoBehaviour
{
    public float damageAmount = 10f;

    private void Update()
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();

        if (Gamepad.all[1].buttonWest.isPressed)
        {
            StartCoroutine(DisableColliderForSeconds(capsuleCollider, 1f));

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player1"))
        {
            // Obtener el script del jugador que recibe el golpe
            ToralController playerHealth = col.gameObject.GetComponent<ToralController>();

            // Disminuir la vida del jugador
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Hay colisoon2222");
        }
    }

    IEnumerator DisableColliderForSeconds(CapsuleCollider collider, float seconds)
    {
        collider.enabled = true;

        yield return new WaitForSeconds(seconds);

        collider.enabled = false;
    }
}