using System.Collections;
using System.Collections.Generic;
using _mrstruijk.Components.Localisation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LocalisationPlayMode
{
    private string key = "test";
    private string value = "NLTestValue";

    [UnityTest]
    public IEnumerator LocalisationPlayModeWithEnumeratorPasses()
    {
        yield return null;
    }


    [TearDown]
    public void AfterEveryTest()
    {

    }
}
