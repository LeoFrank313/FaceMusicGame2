using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Affdex;
using UnityEngine.UI;

public class MusicController : ImageResultsListener
{
    public AudioClip[] source;
    public float currentSmile;
    public float currentSadness;
    public float currentSuprise;
    public float currentEyeClose;
    public float currentNose;
    public Text currentText;
    public float addScore,minusScore;
    public GameObject[] bars;
    public GameObject bg;
    float pitchScore = 0,supriseScore = 0,eyeScore = 0,noseScore = 0;

    public override void onFaceFound(float timestamp, int faceId)
    {
        Debug.Log("Found the face");
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        Debug.Log("Lose the face");
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
        foreach (KeyValuePair<int, Face> pair in faces)
        {
            int FaceId = pair.Key;  // The Face Unique Id.
            Face face = pair.Value;    // Instance of the face class containing emotions, and facial expression values.

            //Retrieve the Emotions Scores
            face.Expressions.TryGetValue(Expressions.Smile, out currentSmile);
            face.Expressions.TryGetValue(Expressions.MouthOpen, out currentSuprise);
            face.Emotions.TryGetValue(Emotions.Sadness, out currentSadness);
            face.Expressions.TryGetValue(Expressions.EyeClosure, out currentEyeClose);
            face.Expressions.TryGetValue(Expressions.NoseWrinkle, out currentNose);



            //Retrieve the Interocular distance, the distance between two outer eye corners.
            //currentInterocularDistance = face.Measurements.interOcularDistance;


            //Retrieve the coordinates of the facial landmarks (face feature points)
            //featurePointsList = face.FeaturePoints;
        }
    }

    private void Update()
    {
        currentText.text = "Current smile = " + currentSmile + " Current suprise = " + currentSuprise + " Current sadness = " + currentSadness + " Current eye = " + currentEyeClose;
        barControll();


    }
    private void FixedUpdate()
    {
        if (GameManager.instance.GameStart)
        {


            if (GameManager.instance.mainTrack)
            {
                activeTrack();
            }
            if (currentSmile > 90 || currentSadness > 50)
            {
                pitchControll();
            }
            else
            {
                if (pitchScore > 0)
                {
                    pitchScore -= minusScore;
                }
                if (pitchScore < 0)
                {
                    pitchScore += minusScore;
                }
                Debug.Log(pitchScore);
            }
        }
        

    }
    void pitchControll()
    {
        if (!GameManager.instance.mainTrack)
        {
            GameManager.instance.mainTrack = true;
            AudioManager.instance.startPlay();
            AudioManager.instance.playMainMusic();
            bg.SetActive(false);
            Debug.Log("main track on, game start");
        }
        else
        {
            if (currentSmile > 90)
            {
                pitchScore += addScore;

                Debug.Log(pitchScore);
            }
            if (currentSadness > 30)
            {
                pitchScore -= addScore;

                Debug.Log(pitchScore);
            }
        }


       
    }
    void activeTrack()
    {
        if (currentSuprise > 90)
        {
            if (!GameManager.instance.supriseTrack)
            {
                if (AudioManager.instance.supriseSource.mute)
                {
                    Debug.Log("suprise track on");
                    supriseScore = 100;
                    GameManager.instance.supriseTrack = true;

                    AudioManager.instance.playSupriseMusic();
                    //surpriseSheet.layer = LayerMask.NameToLayer("Default");
                    //surpriseSheet.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
            else
            {
                if (supriseScore <= 80)
                {
                    supriseScore += 20;
                }
                
            }
        }
        if(currentSuprise < 90)
        {
            if (supriseScore > 0)
            {
                supriseScore -= 2;
            }
        }

        if (currentEyeClose > 90)
        {
            if (!GameManager.instance.eyeCloseTrack)
            {
                if (AudioManager.instance.eyeCloseSource.mute)
                {
                    eyeScore = 100;
                    Debug.Log("eye track on");
                    GameManager.instance.eyeCloseTrack = true;
                    AudioManager.instance.playeyeMusic();
                }

            }
            else
            {
                if (eyeScore <= 80)
                {
                    eyeScore += 40;
                }
                
            }
        }
        if (currentEyeClose < 90)
        {
            if (eyeScore > 0)
            {
                eyeScore -= 2;
            }
        }

        if (currentNose > 90)
        {
            if (!GameManager.instance.noseTrack)
            {
                if (AudioManager.instance.noseSouce.mute)
                {
                    noseScore = 100;
                    Debug.Log("nose track on");
                    GameManager.instance.eyeCloseTrack = true;
                    AudioManager.instance.playeyeMusic();
                }
                
            }
            else
            {
                if (noseScore <= 80)
                {
                    noseScore += 20;
                }
                
            }
        }
        if (currentNose < 90)
        {
            if (noseScore > 0)
            {
                noseScore -= 2;
            }
        }
    }

    void barControll()
    {
        //0-9 suprise nose eye, smile 123, sad 123
        bars[0].GetComponent<Image>().fillAmount = supriseScore / 100;
        bars[1].GetComponent<Image>().fillAmount = noseScore / 100;
        bars[2].GetComponent<Image>().fillAmount = eyeScore / 100;
        bars[3].GetComponent<Image>().fillAmount = (pitchScore-66) / 33;
        bars[4].GetComponent<Image>().fillAmount = (pitchScore-33) / 33;
        bars[5].GetComponent<Image>().fillAmount = pitchScore / 33;
        bars[6].GetComponent<Image>().fillAmount = pitchScore / -33;
        bars[7].GetComponent<Image>().fillAmount = (pitchScore+33) / -33;
        bars[8].GetComponent<Image>().fillAmount = (pitchScore+66) / -33;
        if (pitchScore >= 0 && pitchScore < 33)
        {
            AudioManager.instance.pitchChange(1.0f);
        }
        if (pitchScore > 33 && pitchScore < 66)
        {
            AudioManager.instance.pitchChange(1.2f);
        }
        if (pitchScore > 66)
        {
            AudioManager.instance.pitchChange(1.5f);
        }
        if (pitchScore < 0 && pitchScore > -33)
        {
            AudioManager.instance.pitchChange(1.0f);
        }
        if (pitchScore < -33 && pitchScore > -66)
        {
            AudioManager.instance.pitchChange(0.8f);
        }
        if (pitchScore < -66)
        {
            AudioManager.instance.pitchChange(0.7f);
        }
        AudioManager.instance.volumeChange(supriseScore / 100, noseScore / 100, eyeScore / 100);
    }



}
