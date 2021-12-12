using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    bool canMove = true;
    bool leaving = false;
    public GameObject levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position -= new Vector3(20f / 3f * Time.deltaTime, 0, 0);
            if(transform.position.x <= 0 && !leaving)
            {
                transform.position = new Vector3(0, -5, 0);
                canMove = false;
                levelManager.GetComponent<LevelManagerBehavior>().AtHouse();
            }
            if(transform.position.x <= -20 && leaving)
            {
                levelManager.GetComponent<LevelManagerBehavior>().SpawnHouse();
                Destroy(gameObject);
            }
        }
    }
    public void ResumeMoving()
    {
        canMove = true;
        leaving = true;
    }
}
