using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void horseTrot()
    {
        anim.Play("Armature|Trot Cycle");
    }

    public void horseWalk()
    {
        anim.Play("Armature|Walk Cycle");
    }

    public void horseStandIdle()
    {
        anim.Play("Idle");
    }

}
