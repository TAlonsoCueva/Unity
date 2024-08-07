using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float mouseX;
    float mouseY;
    public float camSpeed;
    public Transform spine;
    public Vector3 offset;
    public Vector3 offset2;
    private void Awake()
    {
        player = FindObjectOfType<Swat>().transform;   
    }
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        print(mouseX+ "" + "" + mouseY);
        mouseY = Input.GetAxis("Mouse Y");
    }
    private void LateUpdate()
    {
        transform.position = player.position + new Vector3(0, 1.5f, 0) + offset + offset2;
        transform.eulerAngles = transform.eulerAngles + (new Vector3(mouseY * -1, mouseX, 0) * camSpeed * Time.deltaTime);
        player.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
        spine.eulerAngles = new Vector3(spine.eulerAngles.x + this.transform.eulerAngles.x,spine.eulerAngles.y, spine.eulerAngles.z );
    }
}
