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

        // Verificar colisões entre o jogador e a posição desejada da câmera
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit, collisionLayers))
        {
            // Ajustar a posição para o ponto de colisão
            desiredPosition = hit.point;
        }

        
        transform.position = desiredPosition;

        // Manter a câmera olhando para o jogador
        transform.LookAt(target.position + Vector3.up * height);
    }
}
