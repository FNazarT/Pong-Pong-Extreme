using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mousePosition;
    [SerializeField] private Camera twoDimensionalCamera;
    [SerializeField] private GameObject horizontalPaddle;
    [SerializeField] private GameObject verticalPaddle;
    [SerializeField] LayerMask horizontalLayerMask;
    [SerializeField] LayerMask verticalLayerMask;
    public int score = 0;

    void FixedUpdate()
    {
        // Check floor and ceiling
        RaycastHit2D hitFloors = Physics2D.Raycast(transform.position, transform.right, 20, horizontalLayerMask);
        Debug.DrawRay(transform.position, transform.right*20, Color.green);

        //If something was hit, the RaycastHit2D.collider will not be null.
        if (hitFloors.collider != null)
        {
            horizontalPaddle.transform.position = hitFloors.point;
        }

        // Check walls
        RaycastHit2D hitWalls = Physics2D.Raycast(transform.position, transform.right, 20, verticalLayerMask);
        Debug.DrawRay(transform.position, transform.right * 20, Color.red);

        //If something was hit, the RaycastHit2D.collider will not be null.
        if (hitWalls.collider != null)
        {
            verticalPaddle.transform.position = hitWalls.point;
        }

        // Rotate to mouse
        mousePosition = Input.mousePosition;

        Vector3 objectPos = twoDimensionalCamera.WorldToScreenPoint(transform.position);
        mousePosition.x -= objectPos.x;
        mousePosition.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
