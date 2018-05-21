using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test12 {
    private ForestersHutScript hut;

    [UnityTest]
    public IEnumerator TestPlaymode123WithEnumeratorPasses() {
        SetUp();

        for (int i = 0; i < 10; i++) {
            yield return new WaitForEndOfFrame();
            hut.AsignCitizens();
            if (GameObject.Find("Citizen(clone)").GetComponent<CitizenScript>().available) {
                yield break;
            }
        }

        Assert.Fail();
    }

    private void SetUp() {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("../../Prefabs/Citizen"));
        hut = MonoBehaviour.Instantiate(Resources.Load<GameObject>("../../Prefabs/ForestersHut")).GetComponent<ForestersHutScript>();

    }
}
