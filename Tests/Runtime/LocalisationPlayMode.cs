using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;


public class LocalisationPlayMode
{
    private string _key = "test";
    private string _value = "NLTestValue";


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