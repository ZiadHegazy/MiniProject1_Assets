using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Manager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static bool muted;
        public static GameObject[] objects = new GameObject[5];
        public GameObject blueOrb;
        static int factor = 0;
        public static bool startWithMenu = true;
        public GameObject greenOrb;
        public GameObject redOrb;
        public GameObject obstacle;
        public GameObject player;
        public GameObject pauseMenu;
        public GameObject gameOver;
        public GameObject titleMenu;
        public GameObject optionsMenu;
        public int first = 0;
        public static ArrayList permutations = new ArrayList();
        public static ArrayList created = new ArrayList();
        public AudioClip background;
        public AudioClip quietBackground;
        public static bool paused = false;
        public GameObject scoreCanvas;
        public  void Resume()
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            GetComponent<AudioSource>().clip= background;
            
            GetComponent<AudioSource>().Play();
            
            
            
        }
        
        public void Mute(bool isOn)
        {
            muted=isOn;
            
            if (muted)
            {
                AudioListener.volume = 0.0f;
            }
            else
            {
                AudioListener.volume = 1.0f;
            }
        }
        public void Quit()
        {
            Application.Quit();
        }

        public void ToMainMenu()
        {
            startWithMenu = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        public void ToOption()
        {
            optionsMenu.SetActive(true);
            titleMenu.SetActive(false);
            optionsMenu.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Toggle>().isOn = muted;
        }
        public void PlayGame()
        {
            paused = false;
            Time.timeScale = 1;
            titleMenu.SetActive(false);
            scoreCanvas.SetActive(true);
            GetComponent<AudioSource>().clip = background;
            GetComponent<AudioSource>().Play();



        }
        
        public void Restart()
        {
            paused = false;
            startWithMenu = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            
        }
        
        public static void ShuffleArrayList(ArrayList list)
        {
            System.Random rng = new System.Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);

                // Swap the elements at positions k and n
                object temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }

        public int CountObstacles(GameObject[] elements)
        {
            int count = 0;
            foreach (var element in elements)
            {
                if (element != null && element == obstacle)
                {
                    count++;
                }
            }
            return count;
        }
        public int CountOrb(GameObject[] elements)
        {
            int count = 0;
            foreach (var element in elements)
            {
                if (element != null && (element == blueOrb || element == greenOrb || element == redOrb))
                {
                    count++;
                }
            }
            return count;
        }
        void Start()
        {
            if (startWithMenu)
            {

            Time.timeScale = 0;
            GetComponent<AudioSource>().clip = quietBackground;
            GetComponent<AudioSource>().Play();
            titleMenu.SetActive(true);
            }
            else
            {
                PlayGame();
                
            }
            factor = 0;
            objects[0] = blueOrb;
            objects[1] = greenOrb;
            objects[2] = redOrb;
            objects[3] = obstacle;
            objects[4] = null;
            for (int i = 0; i < objects.Length; i++)
            {
                for (int j = 0; j < objects.Length; j++)
                {
                    for (int k = 0; k < objects.Length; k++)
                    {
                        GameObject[] permutation = { objects[i], objects[j], objects[k] };


                        if (CountObstacles(permutation) <= 2 && CountOrb(permutation) <= 2)
                        {
                            permutations.Add(permutation);
                        }
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if(muted)
            {
                GetComponent<AudioSource>().Pause();
            }
            else
            {
                GetComponent<AudioSource>().UnPause();
            }
            if(Input.GetKeyDown(KeyCode.Escape) && !paused && !titleMenu.activeInHierarchy && !optionsMenu.activeInHierarchy && !gameOver.activeInHierarchy  )
            {
                paused = true;
                first = 0;
                pauseMenu.SetActive(true);
                gameOver.SetActive(false);
                optionsMenu.SetActive(false);
                titleMenu.SetActive(false);
                Time.timeScale = 0;

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            {
                
                Resume();

            }
            if (paused && first==0)
            {
                GetComponent<AudioSource>().clip = quietBackground;
                GetComponent <AudioSource>().Play();
                first = 1;
                //GetComponent<AudioSource>().Play();
            }
            else
            {
                
            }
            ShuffleArrayList(permutations);

            for (int i = 0; i < permutations.Count; i++)

            {
                GameObject[] arr = (GameObject[])permutations[i];
                if (created.Count < 1 * permutations.Count)
                {
                    if (arr[0] != null)
                    {

                        GameObject object1 = Instantiate(arr[0], new Vector3(-1.5f, 0.85f, player.transform.position.z + 10 + 25 * factor), Quaternion.identity);
                        created.Add(object1);
                    }
                    if (arr[1] != null)
                    {

                        GameObject object2 = Instantiate(arr[1], new Vector3(0.5f, 0.85f, player.transform.position.z + 10 + 25 * factor), Quaternion.identity);
                        created.Add(object2);
                    }
                    if (arr[2] != null)
                    {

                        GameObject object3 = Instantiate(arr[2], new Vector3(2.5f, 0.85f, player.transform.position.z + 10 + 25 * factor), Quaternion.identity);
                        created.Add(object3);
                    }
                    factor += 1;

                }


            }
            for (int i = 0; i < created.Count; i++)
            {
                
                if (created[i] != null && !((GameObject)(created[i])).IsDestroyed()  && ((GameObject)(created[i])).transform.position.z <= player.transform.position.z - 5)
                {
                    Debug.Log(((GameObject)(created[i])).tag);
                    Destroy(((GameObject)(created[i])).gameObject);
                    created.RemoveAt(i);
                }

            }


        }
    }
}