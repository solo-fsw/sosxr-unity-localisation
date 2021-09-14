using _mrstruijk.Localisation;
using NUnit.Framework;
using UnityEngine;


public class CSVLoaderTests
{
    private CSVLoader loader;

    [SetUp]
    public void BeforeEveryTest()
    {
        loader = new CSVLoader();
    }

    [Test]
    public void CanLoadCSVLocalisationFile()
    {
        Assert.IsNotNull(loader.CSVFile);
    }


    [Test]
    public void CanGetDutchDictionaryValues()
    {
        var dict = loader.GetDictionaryValues("nl");
        Assert.IsNotNull(dict);
    }


    [Test]
    public void CanGetEnglishDictionaryValues()
    {
        var dict = loader.GetDictionaryValues("en");
        Assert.IsNotNull(dict);
    }


    [TearDown]
    public void AfterEveryTest()
    {
        loader = null;
    }
}


public class StringLocalisationSystemTests
{
    private string key = "test";
    private string value = "NLTestValue";


    [SetUp]
    public void BeforeEveryTest()
    {
        Language.language = Language.Lang.NL;
    }


    [Test]
    public void CanAddToLocalisationSystem()
    {
        StringLocalisationSystem.Add(key, value);
        Assert.AreEqual(StringLocalisationSystem.GetLocalisedValue(key), value);
    }

    [Test]
    public void CanRemoveFromLocalisationSystem()
    {
        if (StringLocalisationSystem.GetLocalisedValue(key) == null)
        {
            StringLocalisationSystem.Add(key, value);
        }

        StringLocalisationSystem.Remove(key);

        Assert.IsNull(StringLocalisationSystem.GetLocalisedValue(key));
    }


    [Test]
    public void CanEditLocalisationSystem()
    {
        if (StringLocalisationSystem.GetLocalisedValue(key) == null)
        {
            StringLocalisationSystem.Add(key, "Not the real value");
        }

        StringLocalisationSystem.Replace(key, value);

        Assert.AreEqual(StringLocalisationSystem.GetLocalisedValue(key), value);
    }


    [TearDown]
    public void AfterEveryTest()
    {
        int counter = 0;

        while (StringLocalisationSystem.GetLocalisedValue(key) != null)
        {
            StringLocalisationSystem.Remove(key);
            counter++;

            if (counter > 10)
            {
                Debug.LogError("Was unable to remove al instances of keys. Check Localisation file manually!");
                break;
            }
        }
    }
}
