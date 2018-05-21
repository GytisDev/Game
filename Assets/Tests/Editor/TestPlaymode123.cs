using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestPlaymode123 {
    private GameObject hut;
	
	[UnityTest]
	public IEnumerator TestPlaymode123WithEnumeratorPasses() {
        SetUp();

        for (int i = 0; i < 10; i++) {
            yield return new WaitForEndOfFrame();
            if (GameObject.Find("Citizen(clone)").GetComponent<CitizenScript>().available) {
                yield break;
            }
        }

        Assert.Fail();
	}

    private void SetUp() {
        MonoBehaviour.Instantiate(Resources.Load<GameObject>("../../Prefabs/Citizen"));
        hut = MonoBehaviour.Instantiate(Resources.Load<GameObject>("../../Prefabs/ForestersHut"));

    }
}
