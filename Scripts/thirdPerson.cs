using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPerson : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        gameObject.transform.position = new Vector3(0.46f, 3.8f, target.transform.position.z - 5);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(0.46f, 3.8f, target.transform.position.z - 5);
    }
}
