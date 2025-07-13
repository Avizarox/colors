using System.Collections;
using UnityEngine;

public class DozaTaker : MonoBehaviour
{
    private SpriteRenderer _char;
    public enum colors { red, blue }

    public static Item lastPickedItem;

    private Coroutine dozaTimerCoroutine;
    public float dozaDuration = 5f;

    private void Start()
    {
        _char = GetComponent<SpriteRenderer>();
    }

    public class Item
    {
        public string Name { get; }
        public colors Color { get; }
        public Sprite Icon { get; }

        public Item(string name, colors color, Sprite icon)
        {
            Name = name;
            Color = color;
            Icon = icon;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Item newItem = GetItemInfo(other.gameObject);
            lastPickedItem = newItem;

            if (dozaTimerCoroutine != null)
            {
                StopCoroutine(dozaTimerCoroutine);
            }
            dozaTimerCoroutine = StartCoroutine(DozaDurationTimer());

            Destroy(other.gameObject);

            if (newItem.Color == colors.red) _char.color = Color.red;
            else if (newItem.Color == colors.blue) _char.color = Color.blue;

            Debug.Log("Picked: " + newItem.Color);
        }
    }

    Item GetItemInfo(GameObject itemObject)
    {

        string itemName = itemObject.name.Replace("(Clone)", "").Trim();
        colors color = colors.red;
        Sprite icon = itemObject.GetComponent<SpriteRenderer>()?.sprite;

        if (itemName.Contains("blue_doza"))
        {
            color = colors.blue;
        }
        else if (itemName.Contains("red_doza"))
        {
            color = colors.red;
        }

        return new Item(itemName, color, icon);
    }

    public Item GetLastPickedItem()
    {
        return lastPickedItem;
    }

    IEnumerator DozaDurationTimer()
    {
        yield return new WaitForSeconds(dozaDuration);

        lastPickedItem = null;
        _char.color = Color.white;
        Debug.Log("Доза закончилась");
    }
}
