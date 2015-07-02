using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour
{
    /// <summary>
    /// Animator reference.
    /// </summary>
    [SerializeField]
    Animator animator;

    /// <summary>
    /// Plays idle animation.
    /// </summary>
    public void PlayIdle()
    {
        animator.SetBool("idle", true);
        animator.SetLayerWeight(1, 0);
        //animator.SetLayerWeight(2, 0);
        //animator.SetLayerWeight(3, 0);
        //animator.SetLayerWeight(4, 0);
    }

    /// <summary>
    /// Plays walk animation.
    /// </summary>
    /// <param name="myRotation">Player rotation.</param>
    public void PlayWalk(Quaternion myRotation)
    {
        animator.SetBool("idle", false);

        animator.SetLayerWeight(1, 1);
        animator.SetFloat("angle", 0.5f);
        //animator.SetFloat("angle", myRotation.eulerAngles.y / 360f);
        

        /*float sin = Mathf.Sin((myRotation.eulerAngles.y) * Mathf.Deg2Rad);
        float cos = Mathf.Cos((myRotation.eulerAngles.y) * Mathf.Deg2Rad);

        if (cos < 0)
        {
            animator.SetLayerWeight(1, Mathf.Abs(cos));
            animator.SetLayerWeight(2, 0);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, Mathf.Abs(cos));
        }

        if (sin > 0)
        {
            animator.SetLayerWeight(3, Mathf.Abs(sin));
            animator.SetLayerWeight(4, 0);
        }
        else
        {
            animator.SetLayerWeight(3, 0);
            animator.SetLayerWeight(4, Mathf.Abs(sin));
        }*/
    }

    /// <summary>
    /// Plays shoot animation.
    /// </summary>
    public void PlayShoot()
    {
        animator.SetBool("idle", false);
        animator.SetBool("shoot", true);
    }

    /// <summary>
    /// Plays die animation.
    /// </summary>
    public void PlayDie()
    {
        animator.SetBool("idle", false);
        animator.SetLayerWeight(1, 0);
        animator.SetBool("death", true);
    }
}