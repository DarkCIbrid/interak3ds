using UnityEngine;

public class TargetTest : MonoBehaviour
{
    public float disappearDuration = 2f; // koliko sekundi objekt nestane
    private Renderer objectRenderer;
    private bool isHidden = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHidden)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    StartCoroutine(HideTemporarily());
                }
            }
        }
    }

    System.Collections.IEnumerator HideTemporarily()
    {
        isHidden = true;
        objectRenderer.enabled = false;
        yield return new WaitForSeconds(disappearDuration);
        objectRenderer.enabled = true;
        isHidden = false;
    }
}
