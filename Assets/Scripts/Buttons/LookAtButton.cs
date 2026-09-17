using System.Collections;
using TMPro;
using UnityEngine;

public class LookAtButtonShader : MonoBehaviour
{
    public float interactionDistance = 5f;
    [SerializeField] private float resetTime = 3f;


    [SerializeField] private int tQuality = 0;
    [SerializeField] private bool changeSensitivity = false;
    [SerializeField] private bool changeRenderDistance = false;
    [SerializeField] private bool changeTexture = false;
    [SerializeField] private float changeAmount = 10f;
    public string defaultTextQuality;
    public TMP_Text worldTextQuality;
    public bool isActive = false;
    private Camera playerCamera;
    private PlayerCam pcsettings;
    private Coroutine resetCoroutine;

    void Start()
    {
        playerCamera = Camera.main;
        pcsettings = playerCamera.GetComponent<PlayerCam>();
        if(worldTextQuality != null )
            defaultTextQuality = worldTextQuality.text;
    }

    void Update()
    {
        if (worldTextQuality != null)
            worldTextQuality.text = defaultTextQuality+tQuality;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isActive = true;
                    if (changeSensitivity)
                    {
                        pcsettings.sensX += changeAmount;
                        pcsettings.sensY += changeAmount;
                    }
                    else if (changeRenderDistance)
                    {
                        if(playerCamera.farClipPlane+changeAmount<1001 && playerCamera.farClipPlane + changeAmount>10)
                            playerCamera.farClipPlane += changeAmount;
                    }
                    else if (changeTexture)
                    {
                        tQuality++;
                        if (tQuality > 2)
                            tQuality = 0;

                        switch (tQuality)
                        {
                            case 0:
                                QualitySettings.SetQualityLevel( 0, true);
                                break;
                            case 1:
                                QualitySettings.SetQualityLevel(1, true);
                                break;
                            case 2:
                                QualitySettings.SetQualityLevel(2, true);
                                break;
                        }


                    }

                    if (resetCoroutine != null)
                        StopCoroutine(resetCoroutine);

                    resetCoroutine = StartCoroutine(ResetButton());
                }
            }
        }


    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(resetTime);

        isActive = false;

    }
}