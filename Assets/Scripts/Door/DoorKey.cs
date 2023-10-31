using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public int ID;
    BoxCollider collider;
    Bounds bounds;
    Bounds shrunkBounds;

    void Start()
    {
        /*
        collider = GetComponent<BoxCollider>();
        bounds = collider.bounds;
        shrunkBounds = new Bounds(
            new Vector3(bounds.center.x, bounds.center.y, bounds.center.z + (bounds.center.z * 0.5f)),
            new Vector3(bounds.size.x, bounds.size.y, bounds.size.z * 0.5f)
        );
        */
    }

    public void ToggleCollider(bool state)
    {
        /*
        if (state) // ""Enable"" collider
        {
            collider.center = bounds.center;
            collider.size = bounds.size;
        }
        else // Smaller collider for when in door lock
        {
            collider.center = shrunkBounds.center;
            collider.size = shrunkBounds.size;
        }
        */
    }
}
