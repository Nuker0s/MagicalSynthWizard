using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    // Changed fields to properties
    public virtual string spellcode { get { return "404"; } }
    public virtual string spellname { get { return "debug"; } }

    public virtual void spellcast()
    {
        Debug.Log(spellname);
    }
}

public class Fireball : Spell
{
    // Override properties
    public override string spellcode { get { return "121"; } }
    public override string spellname { get { return "fireball"; } }
}