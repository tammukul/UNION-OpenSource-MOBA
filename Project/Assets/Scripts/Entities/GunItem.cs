using UnityEngine;
using System.Collections;

public class GunItem : MonoBehaviour
{
    /// <summary>
    /// Type of gun the drop will give.
    /// </summary>
    public GunTypes myType { get; private set; }

    /// <summary>
    /// Bullet lifetime. Used to prevent player who dropped it to pick it up again, before dying.
    /// </summary>
    float lifetime;

    /// <summary>
    /// Initializes drop, with given position and type.
    /// </summary>
    /// <param name="type">Gun type.</param>
    /// <param name="position">Position to create.</param>
    public void Initialize(GunTypes type,Vector3 position)
    {
        lifetime = 0;
        this.transform.position = position;
        myType = type;
    }

    /// <summary>
    /// Resets the item. Used when a player catches the item.
    /// </summary>
    public void ResetMe()
    {
        myType = GunTypes.None;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Update method. Just adds time to lifetime.
    /// </summary>
    void Update()
    {
        lifetime += Time.deltaTime;
    }

    /// <summary>
    /// Collision method; if possible, gives the item to the player.
    /// </summary>
    /// <param name="target">Player who collided.</param>
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player" && lifetime > 0.2f)
        {
            GunDropsManager.instance.PickUp(this, target.GetComponent<PlayerManager>());
        }
    }
}