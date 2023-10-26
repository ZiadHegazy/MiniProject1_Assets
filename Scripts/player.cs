using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts;
using System.Threading;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public Color color;
    public int form = 0;
    public bool isShieldActive=false;
    public bool isMultiplier=false;
    public GameObject ground;
    public GameObject ground2;
    public static GameObject[] newGrounds = new GameObject[3];
    public int redScore = 5;
    public int greenScore = 0;
    public int blueScore = 0;
    public int Score = 0;
    public GameObject gameOver;
    public TextMeshProUGUI redScore1 ;
    public TextMeshProUGUI greenScore1 ;
    public TextMeshProUGUI blueScore1;
    public TextMeshProUGUI Score1 ;
    public Material redMaterial;
    public Material greenMaterial;
    public Material greenMaterial2;
    public Material blueMaterial;
    public Material whiteMaterial;
    public AudioClip collision;
    public AudioClip usePower;
    public AudioClip wrongAction;
    public AudioClip collect;
    public AudioClip switchForm;
    public Manager Manager;

    
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * 12.0f*Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (Manager.muted)
        {
            GetComponent<AudioSource>().Pause();
        }
        else
        {
            GetComponent<AudioSource>().UnPause();
        }

        Score1.text = "Score: " + Score;
        redScore1.text = "Red Score: " + redScore;
        blueScore1.text = "Blue Score: " + blueScore;
        greenScore1.text = "Green Score: " + greenScore;
        
        GetComponent<Rigidbody>().velocity = Vector3.forward * 12.0f;
        if (isShieldActive)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x!=-1.5f)
        {
            transform.Translate(new Vector3(-2, 0, 0));
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x == -1.5f)
        {
            GetComponent<AudioSource>().clip = wrongAction;

            GetComponent<AudioSource>().Play();
            

        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x!=2.5f)
        {
            transform.Translate(new Vector3(2, 0, 0));
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x == 2.5f)
        {
            GetComponent<AudioSource>().clip = wrongAction;
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyDown(KeyCode.J) && redScore == 5)
        {
            if (form != 1)
            {
                GetComponent<AudioSource>().clip = switchForm;
                GetComponent<AudioSource>().Play();
                redScore -= 1;
                form = 1;
                redScore1.text = "Red Score: " + redScore;
                isShieldActive = false;
                isMultiplier = false;
            }
            else
            {
                GetComponent<AudioSource>().clip = wrongAction;
                GetComponent<AudioSource>().Play();
            }


        }
        if (Input.GetKeyDown(KeyCode.K) && greenScore == 5)
        {
            if(form != 2)
            {
                GetComponent<AudioSource>().clip = switchForm;
                GetComponent<AudioSource>().Play();
                greenScore -= 1;
            form = 2;
            greenScore1.text = "Green Score: " + greenScore;
            isShieldActive = false;
            isMultiplier = false;
            }
            else
            {
                GetComponent<AudioSource>().clip = wrongAction;
                GetComponent<AudioSource>().Play();
            }


        }
        if (Input.GetKeyDown(KeyCode.L) && blueScore == 5)
        {
            GetComponent<AudioSource>().clip = switchForm;
            GetComponent<AudioSource>().Play();
            blueScore -= 1;
            form = 3;
            blueScore1.text = "Blue Score: " + blueScore;
            isShieldActive = false;
            isMultiplier = false;


        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (form)
            {
                case 0:
                    GetComponent<AudioSource>().clip = wrongAction;
                    GetComponent<AudioSource>().Play();
                    break;

                case 1:
                    GetComponent<AudioSource>().clip = usePower;
                    GetComponent<AudioSource>().Play();
                    GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
                    for(int i=0;i<allGameObjects.Length;i++)
                    {
                        double objectZ = allGameObjects[i].gameObject.transform.position.z;
                        double playerZ = gameObject.transform.position.z;
  
                        if (allGameObjects[i].gameObject.CompareTag("obstacle") && allGameObjects[i].gameObject.transform.position.z>=gameObject.transform.position.z &&
                            allGameObjects[i].gameObject.transform.position.z <= gameObject.transform.position.z + 35)
                        {
                            

                            Destroy(allGameObjects[i].gameObject);
                        }
                       
                    }
                    redScore -= 1;
                    if (redScore == 0)
                    {
                        form = 0;

                    }
                    break;
                case 2:
                    if (greenScore == 1)
                    {
                        greenScore = 0;
                        form = 0;
                        isMultiplier = false;
                    }
                    else
                    {
                        if (!isMultiplier)
                        {
                            GetComponent<AudioSource>().clip = usePower;
                            GetComponent<AudioSource>().Play();
                            isMultiplier = true;
                            greenScore--;

                        }
                        else
                        {
                            GetComponent<AudioSource>().clip = wrongAction;
                            GetComponent<AudioSource>().Play();
                        }
                    }
                    break;
                case 3:
                    if (blueScore == 1)
                    {
                        blueScore = 0;
                        form = 0;
                        isShieldActive = false;
                    }
                    else
                    {
                        if(!isShieldActive)
                        {
                            GetComponent<AudioSource>().clip = usePower;
                            GetComponent<AudioSource>().Play();
                            isShieldActive = true;
                            blueScore--;
                        }
                        else
                        {
                            GetComponent<AudioSource>().clip = wrongAction;
                            GetComponent<AudioSource>().Play();
                        }
                    }

                    break;

            }
        }
        if(Input.GetKeyDown(KeyCode.I)) {
            if (redScore >= 5)
            {
                redScore = 5;
            }
            else
            {
                redScore += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (greenScore >= 5)
            {
                greenScore = 5;
            }
            else
            {
                greenScore += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (blueScore >= 5)
            {
                blueScore = 5;
            }
            else
            {
                blueScore += 1;
            }
        }
        switch (form)
        {

            case 1:
                GetComponent<MeshRenderer>().material=redMaterial;
                break;
            case 2:
                if (isMultiplier)
                {

                GetComponent<MeshRenderer>().material = greenMaterial2;
                }
                else
                {
                    GetComponent<MeshRenderer>().material = greenMaterial;
                }
                break;
            case 3:
                GetComponent<MeshRenderer>().material = blueMaterial;
                break;
            default:
                GetComponent<MeshRenderer>().material = whiteMaterial;
                break;


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground2"))
        {
            //Debug.Log(ground.transform.localScale.z);

            ground.transform.position = new Vector3(0.57f, 0, other.transform.position.z + ground.transform.localScale.z);

        }
        if (other.CompareTag("ground"))
        {
            //Debug.Log(ground.transform.localScale.z);

            ground2.transform.position=new Vector3(0.57f,0,other.transform.position.z+ground.transform.localScale.z);


        }if (other.CompareTag("obstacle"))
        {
            GetComponent<AudioSource>().clip = collision;
            GetComponent<AudioSource>().Play();
            if (form == 0)
            {
                Debug.Log("aaa");
                gameOver.SetActive(true);
                Time.timeScale = 0;
                gameOver.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Final Score: " + Score;
                Manager.GetComponent<AudioSource>().clip = Manager.quietBackground;
                GetComponent<AudioSource>().Play();
                // Destroy each game object
                //foreach (GameObject obj in allGameObjects)
                //{
                //    if (obj != Camera.main.gameObject && obj != gameObject) // Exclude the current object (the one with this script)
                //    {
                //        Destroy(obj);
                //    }
                //}
            }
            else
            {
                if(form==3 && isShieldActive)
                {
                    isShieldActive = false;
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    Destroy(other.gameObject);

                }
                else if (form==3 && !isShieldActive)

                {
                    form = 0;
                    Destroy(other.gameObject);
                }
                else
                {
                    form = 0;
                    Destroy(other.gameObject);
                }
            }
        } if (other.CompareTag("redOrb") && !gameObject.CompareTag("shield"))
        {
            GetComponent<AudioSource>().clip = collect;
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            if (redScore >= 5)
            {
                redScore = 5;
            }
            else
            {
                if (form != 1)
                {

                redScore += 1;
                }
                
            }
            Score += 1;
            if (form == 1)
            {
                Score += 1;
            }
            if (form == 2 && isMultiplier)
            {
                Score += 4;
                redScore += 1;
                if (redScore >= 5)
                {
                    redScore = 5;
                }
                isMultiplier = false;

            }
            Score1.text = "Score: " + Score;
            redScore1.text = "Red Score: " + redScore;
            

        }
         if (other.CompareTag("blueOrb") && !gameObject.CompareTag("shield"))
        {
            GetComponent<AudioSource>().clip = collect;
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            if (blueScore >= 5)
            {
                blueScore = 5;
            }
            else
            {
                if (form != 3)
                {

                blueScore += 1;
                }
            }
            Score += 1;
            if (form == 3)
            {
                Score += 1;
            }
            if(form==2 && isMultiplier)
            {
                Score += 4;
                blueScore += 1;
                if (blueScore >= 5)
                {
                    blueScore = 5;
                }
                isMultiplier = false;

            }
            Score1.text = "Score: " + Score;

            blueScore1.text = "Blue Score: " + blueScore;

        }
         if (other.CompareTag("greenOrb") && !gameObject.CompareTag("shield"))
        {
            GetComponent<AudioSource>().clip = collect;
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            if (greenScore >= 5)
            {
                greenScore = 5;
            }
            else
            {
                if (form != 2)
                {

                greenScore += 1;
                }
            }
           
            if(form == 2 && isMultiplier)
            {
                Score += 10;
                isMultiplier = false;

            }
            else
            {
                if (form == 2 && !isMultiplier)
                {

                Score += 2;
                }
                else
                {
                    Score += 1;
                }
            }
            
            Score1.text = "Score: " + Score;

            greenScore1.text = "Green Score: " + greenScore;
        }
    }
}
