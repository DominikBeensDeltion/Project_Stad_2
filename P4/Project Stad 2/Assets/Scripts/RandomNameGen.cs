using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNameGen : MonoBehaviour {
    public List<string> prefix = new List<string>();
    public List<string> middle = new List<string>();
    public List<string> suffix = new List<string>();

    public string randomName;


    public string Name()
    {
        int one = Random.Range(0, prefix.Count);
        int two = Random.Range(0, middle.Count);
        int three = Random.Range(0, middle.Count);
        string pref = prefix[one];
        string mid =  middle[two];
        string suf =  suffix[three];

        randomName = pref + " " + mid + " " + suf;

        return randomName;

    }
}
