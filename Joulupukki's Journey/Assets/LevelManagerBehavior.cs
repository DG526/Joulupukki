using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagerBehavior : MonoBehaviour
{
    public GameObject houseType, house, dialoguePanel;
    public Image face;
    public Button btnGive, btnLeave, btnContinue;
    public Text continueText, dialogue, timer, pointCounter;
    float timeLeft = 90;
    int currentPerson = 0, points = 0;
    public Sprite[] faces = new Sprite[6];
    bool choseGive = false, canStop = true, stopped = false;
    public GameObject scoreDisplay;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
        scoreDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)(timeLeft / 60);
        int seconds = Mathf.CeilToInt(timeLeft % 60);
        if(seconds > 9)
            timer.text = minutes + ":" + seconds;
        else
            timer.text = minutes + ":0" + seconds;
        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Max(0, timeLeft);
        if (canStop && timeLeft == 0 && !stopped)
        {
            DisplayScore();
            stopped = true;
        }
    }

    public void AtHouse()
    {
        canStop = false;
        choseGive = false;
        ChoosePerson();
        dialoguePanel.SetActive(true);
        btnGive.gameObject.SetActive(true);
        btnLeave.gameObject.SetActive(true);
        btnContinue.gameObject.SetActive(false);
        switch (currentPerson)
        {
            case 1:
            case 2:
                dialogue.text = "“Hello Joulupukki!”";
                break;
            case 3:
            case 4:
                dialogue.text = "“Hyvää joulua!”";
                break;
            case 5:
                int grumpyChoice = Random.Range(0, 2);
                if (grumpyChoice == 0)
                    dialogue.text = "“Wrong house.”";
                else
                    dialogue.text = "“What do you want?”";
                break;
            case 6:
                int OldChoice = Random.Range(0, 2);
                if (OldChoice == 0)
                    dialogue.text = "“Oh, if it isn’t Joulupukki!”";
                else
                    dialogue.text = "“Is it already Christmas?”";
                break;
        }
    }
    public void ChoosePerson()
    {
        bool isNew = true;
        do
        {
            int randnum = Random.Range(1, 9);
            int choice = 0;
            switch (randnum)
            {
                case 1:
                case 2:
                    choice = 1;
                    break;
                case 3:
                case 4:
                    choice = 2;
                    break;
                case 5:
                    choice = 3;
                    break;
                case 6:
                    choice = 4;
                    break;
                case 7:
                    choice = 5;
                    break;
                case 8:
                    choice = 6;
                    break;
            }
            if (choice == currentPerson)
                isNew = false;
            else
            {
                isNew = true;
                currentPerson = choice;
            }
        } while (!isNew);
    }
    public void ChooseGive()
    {
        choseGive = true;
        btnGive.gameObject.SetActive(false);
        btnLeave.gameObject.SetActive(false);
        btnContinue.gameObject.SetActive(true);
        switch (currentPerson)
        {
            case 1:
            case 3:
                dialogue.text = "“I’ve been good!”";
                continueText.text = "[Give present]";
                break;
            case 2:
            case 4:
                dialogue.text = "“I’ve been well-behaved!”";
                continueText.text = "[Give present]";
                break;
            case 5:
                dialogue.text = "“Bah, humbug.”";
                continueText.text = "[Give present anyway]";
                break;
            case 6:
                dialogue.text = "“Oh, my grandkids aren’t here right now, sorry.”";
                continueText.text = "[Leave]";
                break;
        }
    }
    public void ChooseLeave()
    {
        btnGive.gameObject.SetActive(false);
        btnLeave.gameObject.SetActive(false);
        btnContinue.gameObject.SetActive(true);
        continueText.text = "[Leave]";
        switch (currentPerson)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                dialogue.text = "“Huh? Wait, don’t go!”";
                break;
            case 5:
                dialogue.text = "“Get lost.”";
                break;
            case 6:
                dialogue.text = "“Of course. There are plenty of children waiting.”";
                break;
        }
    }
    public void LeaveHouse()
    {
        if (choseGive)
        {
            if (currentPerson <= 4)
                points += 10;
            if (currentPerson == 5)
                points -= 15;
        }
        else
        {
            if (currentPerson <= 4)
                points -= 5;
        }
        pointCounter.text = "" + points;
        dialoguePanel.SetActive(false);
        house.GetComponent<HouseBehavior>().ResumeMoving();
    }
    public void SpawnHouse()
    {
        house = Instantiate(houseType, new Vector3(20, -5, 0), new Quaternion());
        house.GetComponent<HouseBehavior>().levelManager = gameObject;
        canStop = true;
    }
    void DisplayScore()
    {
        house.GetComponent<HouseBehavior>().Freeze();
        scoreDisplay.SetActive(true);
        score.text += "" + points;
    }
}
