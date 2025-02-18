using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    [Header("Wall")]
    [SerializeField] GameObject topWallDoor;
    [SerializeField] GameObject bottomWallDoor;
    [SerializeField] GameObject leftWallDoor;
    [SerializeField] GameObject rightWallDoor;

    [Header("Wall path")]
    [SerializeField] GameObject topPath;
    [SerializeField] GameObject bottomPath;
    [SerializeField] GameObject leftPath;
    [SerializeField] GameObject rightPath;

    public Vector2Int RoomIndex { get; set; }

    public void OpenDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            topDoor.SetActive(true);
            topPath.SetActive(true);
            //topWallDoor.SetActive(false);
            topWallDoor.GetComponent<TilemapCollider2D>().enabled = false;
        }

        if (direction == Vector2Int.down)
        {
            bottomDoor.SetActive(true);
            bottomPath.SetActive(true);
            //bottomWallDoor.SetActive(false);
            bottomWallDoor.GetComponent<TilemapCollider2D>().enabled = false;
        }

        if (direction == Vector2Int.left)
        {
            leftDoor.SetActive(true);
            leftPath.SetActive(true);
            //leftWallDoor.SetActive(false);
            leftWallDoor.GetComponent<TilemapCollider2D>().enabled = false;
        }

        if (direction == Vector2Int.right)
        {
            rightDoor.SetActive(true);
            rightPath.SetActive(true);
            //rightWallDoor.SetActive(false);
            rightWallDoor.GetComponent<TilemapCollider2D>().enabled = false;
        }

        Physics2D.SyncTransforms();
    }
}
