using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchPlayer1 : MonoBehaviour
{
    public float damageAmount = 10f;

    private void Update()
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();

        if (/*Gamepad.all[0].buttonWest.isPressed || */Input.GetKey(KeyCode.G))
        {
            StartCoroutine(DisableColliderForSeconds(capsuleCollider, 1f));

        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player2"))
        {
            // Obtener el script del jugador que recibe el golpe
            Player2 playerHealth = col.gameObject.GetComponent<Player2>();

            // Disminuir la vida del jugador
            playerHealth.TakeDamage(damageAmount);
            //Debug.Log("Hay colisoon");
        }
    }

    IEnumerator DisableColliderForSeconds(CapsuleCollider collider, float seconds)
    {
        collider.enabled = true;

        yield return new WaitForSeconds(seconds);

        collider.enabled = false;
    }
}
