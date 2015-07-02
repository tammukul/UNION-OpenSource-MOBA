using UnityEngine;
using System.Collections;

public class PhotonInstantiator : MonoBehaviour
{
    /// <summary>
    /// Photon manager prefab reference.
    /// </summary>
    [SerializeField]
    GameObject photonPrefab;

    /// <summary>
    /// Checks if photon manager exists. If not, instantiates a new one.
    /// </summary>
    void Awake()
    {
        if (MultiplayerRoomsManager.instance == null)
        {
            Instantiate(photonPrefab);
        }
    }
}