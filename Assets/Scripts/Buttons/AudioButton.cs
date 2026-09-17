using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class AudioButton : MonoBehaviour
{
    public float interactionDistance = 5f;
    [SerializeField] private float resetTime = 3f;


    [SerializeField] private bool changeSound = false;
    [SerializeField] private bool changeSound3D = false;
    [Header("Audio Settings")]
    public AudioMixer audioMixer;           // Assign your AudioMixer here
    public AudioSource audioSource;         // Assign the AudioSource with 3D sound
    public int changeAmount = 1;
    private int volume = 100;



    public bool isActive = false;
    private Camera playerCamera;





    private Coroutine resetCoroutine;

    public string defaultTextSens;
    public string defaultTextRender;

    public TMP_Text worldTextSens;
    public TMP_Text worldTextRender;

    public TMP_Text worldAudio3D;
    public TMP_Text worldAudioVolume;
    public string defaultAudio3D;
    public string defaultAudioVolume;
    public PlayerCam pcam;

    void Start()
    {
        playerCamera = Camera.main;

        if(worldTextSens!=null)
            defaultTextSens = worldTextSens.text;
        if(worldTextRender!=null)
            defaultTextRender = worldTextRender.text;

        playerCamera = Camera.main;
        if (worldAudio3D != null)
            defaultAudio3D = worldAudio3D.text;
        if (worldAudioVolume != null)
            defaultAudioVolume = worldAudioVolume.text;
        if (audioSource != null)
            volume = Mathf.RoundToInt(audioSource.volume * 100);


    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {


                if (Input.GetKeyDown(KeyCode.E))
                {
                    isActive = true;
                    if (changeSound)
                    {
                        ChangeVolume(changeAmount);
                    }
                    if (changeSound3D)
                    {
                        if(audioSource.spatialBlend == 1f)
                            audioSource.spatialBlend = 0f;
                        else
                        audioSource.spatialBlend = 1f;
                    }






                    if (resetCoroutine != null)
                        StopCoroutine(resetCoroutine);

                    resetCoroutine = StartCoroutine(ResetButton());
                }
            }
        }


        if(pcam!=null)
            worldTextSens.text = defaultTextSens + pcam.sensX;
        if (worldTextRender!=null)
            worldTextRender.text = defaultTextRender + playerCamera.farClipPlane;
        if (worldAudio3D != null)
        {
            if (audioSource.spatialBlend == 1)
                worldAudio3D.text = defaultAudio3D + " On";
            else
                worldAudio3D.text = defaultAudio3D + " Off";
        }


        if(worldAudioVolume != null)
            worldAudioVolume.text = defaultAudioVolume + GetDisplayVolume();
    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(resetTime);

        isActive = false;


    }


    void ChangeVolume(int delta)
    {
        int currentVolume = Mathf.RoundToInt(audioSource.volume * 100);
        int newVolume = Mathf.Clamp(currentVolume + delta, 0, 100);
        SetVolume(newVolume);
    }

    void SetVolume(int newVolume)
    {
        if (audioSource)
            audioSource.volume = newVolume / 100f;
    }

    public void Toggle3DSound(bool enable3D)
    {
        if (audioSource != null)
        {
            audioSource.spatialBlend = enable3D ? 1f : 0f;
        }
    }
    public int GetDisplayVolume()
    {
        return Mathf.RoundToInt(audioSource.volume * 100);
    }


}
