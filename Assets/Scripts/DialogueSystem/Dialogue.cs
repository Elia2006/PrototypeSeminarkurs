using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class Dialogue
{
    public string name;

    //[TextArea(3, 10)]
    public LocalizedString[] LocalizedSentences;
}
