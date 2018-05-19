using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ForesterScriptTests {

	//[Test]
	//public void ForesterScriptTestsSimplePasses() {
	//	// Use the Assert class to test conditions.
	//}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ForesterScriptTestsWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        var Forester = new GameObject().AddComponent<ForesterScript>();
        var foresterScript = Forester.GetComponent<ForesterScript>();

        var foresterHut = new GameObject().AddComponent<ForestersHutScript>();
        foresterHut.Radius = int.MaxValue;

        var oog = new GameObject().AddComponent<ObjectOnGrid>();
        oog.placed = true;

        foresterHut.oog = oog;
        foresterScript.fhs = foresterHut;

        foresterScript.FindPlaceForTree2();



        //yield return null;

        Assert.IsNotNull(foresterScript.newTreeLocation);

		yield return null;
	}
}
