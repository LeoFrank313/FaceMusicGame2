using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.    
    public bool mainTrack,supriseTrack,noseTrack,eyeCloseTrack;
    public bool GameStart;
    public GameObject[] faces;

    private void Start()
    {
        mainTrack = false;
        supriseTrack = false;
        noseTrack = false;
        eyeCloseTrack = false;
        GameStart = false;
    }
    //Awake is always called before any Start functions
    void Awake()
    {
        
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }


}
