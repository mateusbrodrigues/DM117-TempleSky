using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float distance = 5.0f; 
    public float height = 2.0f; 
    public LayerMask collisionLayers; // Camadas das paredes

    void LateUpdate()
    {
        if (target == null)
            return;

       
        Vector3 desiredPosition = target.position + Vector3.up * height - target.forward * distance;

        // Verificar colis�es entre o jogador e a posi��o desejada da c�mera
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, collisionLayers))
        {
            // Ajustar a posi��o para o ponto de colis�o
            desiredPosition = hit.point;
        }

        
        transform.position = desiredPosition;

        // Manter a c�mera olhando para o jogador
        transform.LookAt(target.position + Vector3.up * height);
    }
}
