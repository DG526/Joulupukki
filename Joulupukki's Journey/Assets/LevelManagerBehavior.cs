using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerBehavior : MonoBehaviour
{
    public GameObject houseType, house, dialoguePanel;
    public Image face;
    public Button btnGive, btnLeave, btnContinue;
    public Text dialogue, timer;
    float timeLeft = 160;
    int currentPerson = 0;
    public Sprite[] faces = new Sprite[6];
    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtHouse()
    {
        LeaveHouse();
    }
    public void LeaveHouse()
    {
        house.GetComponent<HouseBehavior>().ResumeMoving();

    }
    public void SpawnHouse()
    {
        house = Instantiate(houseType, new Vector3(20, -5, 0), new Quaternion());
        house.GetComponent<HouseBehavior>().levelManager = gameObject;
    }
}
