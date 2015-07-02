using UnityEngine;
using System.Collections;

public class DeactivateAnimator : MonoBehaviour
{
 
    /// <summary>
    /// Deacticates animator component
    /// </summary>
    public void Deactivate()
    {
        this.GetComponent<Animator>().enabled = false;
    }
}