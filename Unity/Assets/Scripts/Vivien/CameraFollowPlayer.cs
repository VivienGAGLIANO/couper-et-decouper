using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Player player;
    [SerializeField][Range(0, 0.5f)] private float cameraFollowSpeed = 0.3f;
    public float zoom = 1f;
    [SerializeField] private float zoomStrength = 2f;
    public Vector3 offset = new Vector3(0, 4f, -8f);

    private void Awake()
    {
        player = GameManager.instance.player;
    }
    private void Start()
    {
        
        transform.position = player.transform.position + zoom * offset;
    }

    private void Update()
    {
        Vector3 finalPosition = player.transform.position + zoom * offset;
        transform.position = Vector3.Lerp(transform.position, finalPosition, cameraFollowSpeed);
        transform.LookAt(player.transform);

        zoom -= Input.mouseScrollDelta.y * Time.deltaTime * zoomStrength;
    }
}
