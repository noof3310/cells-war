using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDie : MonoBehaviour
{
    Animator animator;
    int count = 5000;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DEAD")){
            Destroy(gameObject);
        }
        else if (count == 0 ){
            animator.SetBool("isDie", true);
        }

        count--;
    }
}
