using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //public GameObject leadPlayer;
    public List<Transform> followerCharacters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform current in followerCharacters)
        {
            Vector3 yTransform = new Vector3(transform.position.x, current.position.y, transform.position.z);
            current.LookAt(yTransform);
        }
    }
}
