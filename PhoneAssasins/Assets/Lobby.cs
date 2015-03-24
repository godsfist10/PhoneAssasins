using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Amazon;

public class Lobby : MonoBehaviour 
{
    public bool isHost = false;


    public void startGame( int numberOfPlayers)
    {
        List<int> targetList = new List<int>();
        List<int> randomBag = new List<int>();

        for(int i = 0; i < numberOfPlayers; i++)
        {
            randomBag.Add(i);
       
        }

        for(int i = 0; i < numberOfPlayers; i++)
        {
            int random = Random.Range(0, randomBag.Count - 1);
           
            targetList.Add(randomBag[random]);
            randomBag.RemoveAt(random);
        }
    }

    public void Start()
    {
        startGame(5);
    }

}
