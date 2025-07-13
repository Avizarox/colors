using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject Item;
    private bool inPosition;
    private float timer;
    public float timerDuration = 5f;
    void Start()
    {
        inPosition = true;
        timer = timerDuration;
        Instantiate(Item, this.transform);
    }

    void Update()
    {
        if (inPosition == false)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    Instantiate(Item, this.transform);
                    inPosition = true;
                    timer = timerDuration;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            inPosition = false;
        }
    }
}
