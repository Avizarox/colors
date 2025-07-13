using UnityEngine;

public class Sycling : MonoBehaviour
{
    private int Speed = 1080; //на сколько градусов вращается в секунду

    void Update()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
