using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Level2Gnome level2Gnome;
    public GameObject gnomeRB;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        level2Gnome = FindObjectOfType<Level2Gnome>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(gnomeRB.transform.position.x, transform.position.y, transform.position.z);
        if (level2Gnome.moveRight1)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else if(level2Gnome.moveLeft1)
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
