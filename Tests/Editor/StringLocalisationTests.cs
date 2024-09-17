using NUnit.Framework;
using SOSXR.Localiser;
using UnityEngine;


public class CSVLoaderTests
{
    private CSVLoader _loader;


    [SetUp]
    public void BeforeEveryTest()
    {
        _loader = new CSVLoader();
    }


    [Test]
    public void CanLoadCSVLocalisationFile()
    {
        Assert.IsNotNull(_loader.CSVFile);
    }


    [Test]
    public void CanGetDutchDictionaryValues()
    {
        var dict = _loader.GetDictionaryValues("nl");
        Assert.IsNotNull(dict);
    }


    [Test]
    public void CanGetEnglishDictionaryValues()
    {
        var dict = _loader.GetDictionaryValues("en");
        Assert.IsNotNull(dict);
    }


    [TearDown]
    public void AfterEveryTest()
    {
        _loader = null;
    }
}


public class StringLocalisationSystemTests
{
    private readonly string key = "test";
    private readonly string value = "NLTestValue";


    [SetUp]
    public void BeforeEveryTest()
    {
        Language.ChosenLanguage = Language.Lang.NL;
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
        var counter = 0;

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