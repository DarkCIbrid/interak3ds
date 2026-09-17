using UnityEngine;
using TMPro;
using UnityEngine.Audio;


public class PlayerCam : MonoBehaviour
{
    public float sensX = 400;
    public float sensY = 400;

    public Transform orient;
    float xRotation;
    float yRotation;


    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {

        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // apply rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orient.rotation = Quaternion.Euler(0, yRotation, 0);


    }


    public int GetDisplayVolume()
    {
        return Mathf.RoundToInt(audioSource.volume * 100);
    }
}
