using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Spellcaster : MonoBehaviour
{
    public Piano piano;
    public List<Spell> spells = new List<Spell>();
    public List<int> notelist = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        spells.Add(ScriptableObject.CreateInstance<Spell>());
        spells.Add(ScriptableObject.CreateInstance<Fireball>());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void keypressed(int key) 
    {
        notelist.Add(key);

        foreach (Spell spell in spells)
        {

            List<int> spellcode = StringToIntList(spell.spellcode);
            string a = "";
            List<int> b = notelist.Skip(Mathf.Max(0, notelist.Count - spellcode.Count)).ToList();
            foreach (int note in b) 
            {
                a+= note;
            }
            //Debug.Log(a);
            //List<int> lastNElements = notelist.Skip(Mathf.Max(0, notelist.Count - spellcode.Count)).ToList();
            //Debug.Log(a);
            string bString = string.Join("", b.Select(x => x.ToString()).ToArray());
            if (a == spell.spellcode)
            {
                spell.spellcast();
                //Debug.Log("spellcast");
            }
        }
        if (notelist.Count > 5)
        {
            notelist.RemoveAt(0);
        }
    }
    public List<int> StringToIntList(string inputString)
    {
        int numberLength = 1;
        List<int> numbersList = new List<int>();

        for (int i = 0; i < inputString.Length; i += numberLength)
        {
            string numberString = inputString.Substring(i, Mathf.Min(numberLength, inputString.Length - i));
            int number;
            if (int.TryParse(numberString, out number))
            {
                numbersList.Add(number);
            }
            else
            {
                Debug.LogWarning("Failed to parse: " + numberString);
            }
        }

        return numbersList;
    }
}
