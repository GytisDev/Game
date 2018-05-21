using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Test123 {

    public GameObject resource;

	[Test]
	public void Test123SimplePasses() {
        CitizenScript citizen = new CitizenScript();
        ForestersHutScript fhs = new ForestersHutScript();

        citizen.tag = "Citizen";
        fhs.AsignCitizens();

        Assert.That(!citizen.available);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator Test123WithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
