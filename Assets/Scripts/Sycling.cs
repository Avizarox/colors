using UnityEngine;

public class Sycling : MonoBehaviour
{
    private int Speed = 1080; //�� ������� �������� ��������� � �������

    void Update()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime);
    }
}
