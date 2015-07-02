using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWorldLifebar : MonoBehaviour
{

    /// <summary>
    /// Name of the player
    /// </summary>
    [SerializeField]
    Text playerName;

    /// <summary>
    /// Image of player's lifebar.
    /// </summary>
    [SerializeField]
    Image playerLifebar;

    /// <summary>
    /// Rect transform of player's lifebar
    /// </summary>
    RectTransform myTransform;

    /// <summary>
    /// Owner of the player.
    /// </summary>
    PlayerManager playerOwner;

	/// <summary>
	/// Reference to the scale of the screen, based on the 1920x1080 layout.
	/// </summary>
	float screenScale;

    public bool hasOwner { get; private set; }

    public void Initialize(PlayerManager owner)
    {
		screenScale = Screen.width / 1920f;

        playerName.text = owner.playerPlayFabName;
        playerOwner = owner;
        playerOwner.myLifebar = this;

        myTransform = this.transform as RectTransform;

        hasOwner = true;
    }

    void Update()
    {
		if (Camera.main != null)
		{
			if (Camera.main.enabled && hasOwner)
			{
				Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerOwner.lifebarWorldPoint.position);
				screenPosition /= screenScale;
				myTransform.anchoredPosition = new Vector2(screenPosition.x, screenPosition.y);
			}
		}
    }

    public void UpdateLifebarFill(float actualHP, float totalHP)
    {
        playerLifebar.fillAmount = actualHP / totalHP;
    }
}