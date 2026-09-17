using UnityEngine;

public class Move2point : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 1.0f;

    private float t = 0f;
    private bool goingToB = true;

    void Update()
    {
        // Update the interpolation value
        t += Time.deltaTime * speed * (goingToB ? 1 : -1);

        // Clamp t between 0 and 1
        t = Mathf.Clamp01(t);

        // Move the object
        transform.position = Vector3.Lerp(pointA, pointB, t);

        // Switch direction when reaching either point
        if (t >= 1f) goingToB = false;
        else if (t <= 0f) goingToB = true;
    }
}
