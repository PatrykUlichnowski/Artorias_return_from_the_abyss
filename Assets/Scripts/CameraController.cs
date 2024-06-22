using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    private float lookAhead;

    private void Update()
    {
        //camera that follow player on the X axis + gives a bit of space ahead so that player can see further
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //if a player faces left the localscale is -1 so lookahead have to be replaced
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}
