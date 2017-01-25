using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour {

	public float WaterfallArc;
    public float WaterfallLength;
    public float ForegroundVolume;
    public float BackgroundVolume;
    public bool WaterfallMain;
    public bool WaterfallMist;
    public bool SplashInner;
    public int SplashInnerDensity;
    public Vector3 SplashInnerScale;
    public Vector3 SplashInnerPosition;
    public bool SplashOuter;
    public int SplashOuterDensity;
    public Vector3 SplashOuterScale;
    public Vector3 SplashOuterPosition;
    private GameObject WaterfallMainObj;
    private GameObject WaterfallMistObj;
    private GameObject WaterfallStartMistObj;
    private GameObject WaterfallDripMistObj;
    private GameObject WaterfallSplashInnerObj;
	private GameObject WaterfallSplashOuterObj;
    private AudioSource WaterfallForegroundAS;
    private AudioSource WaterfallBackgroundAS;
    private ParticleSystem.MainModule WaterfallMainPS;
    private ParticleSystem.MainModule WaterfallMistPS;
    private ParticleSystem.MainModule SplashInnerPS;
    private ParticleSystem.MainModule SplashOuterPS;
    private ParticleSystem.VelocityOverLifetimeModule WaterfallMistVel;
    private ParticleSystem.VelocityOverLifetimeModule WaterfallMainVel;
    private ParticleSystem MistPS;

    // Use this for initialization
    void Start()
    {
        // Initialize Switches
        WaterfallMain = true;
        WaterfallMist = true;
        SplashInner = true;
        SplashOuter = true;

		// Default values for the waterfall transforms
        WaterfallArc = 0.84f;
        SplashInnerScale = new Vector3(1.39f, 1.39f, 0);
        SplashInnerPosition = new Vector3(0.6f, -1.06f, 0.76f);
        SplashOuterScale = new Vector3(1.19f, 1, 0);
        SplashOuterPosition = new Vector3(0.51f, -1.1f, 0.7f);
        SplashInnerDensity = 50;
        SplashOuterDensity = 50;
        WaterfallLength = 2.1f;
        ForegroundVolume = 3.0f;
        BackgroundVolume = 2.0f;

        // Map references to layers
        WaterfallMainObj = GameObject.Find("Waterfall Main");
        WaterfallMistObj = GameObject.Find("Waterfall Mist");
        WaterfallStartMistObj = GameObject.Find("Start Mist");
        WaterfallDripMistObj = GameObject.Find("Water Drops");
        WaterfallSplashInnerObj = GameObject.Find("Floor Mist Inner");
		WaterfallSplashOuterObj = GameObject.Find("Floor Mist Outer");

		// Map refrences to particle system's velocity
        WaterfallMistVel = GameObject.Find("Waterfall Mist").GetComponent<ParticleSystem>().velocityOverLifetime;
        WaterfallMainVel = GameObject.Find("Waterfall Main").GetComponent<ParticleSystem>().velocityOverLifetime;

		// Map refrences to particle systems
        SplashInnerPS = GameObject.Find("Floor Mist Inner").GetComponent<ParticleSystem>().main;
        SplashOuterPS = GameObject.Find("Floor Mist Outer").GetComponent<ParticleSystem>().main;
        WaterfallMainPS = GameObject.Find("Waterfall Main").GetComponent<ParticleSystem>().main;
        WaterfallMistPS = GameObject.Find("Waterfall Mist").GetComponent<ParticleSystem>().main;

        // Map references to audio sources
        WaterfallForegroundAS = GameObject.Find("Waterfall Foreground Audio").GetComponent<AudioSource>();
        WaterfallBackgroundAS = GameObject.Find("Waterfall Background Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        // Map waterfall Audio
        WaterfallForegroundAS.volume = ForegroundVolume;
        WaterfallBackgroundAS.volume = BackgroundVolume;


        // Map waterfall velocity
        WaterfallMistVel.x = new ParticleSystem.MinMaxCurve(10.0f, WaterfallArc);
		WaterfallMainVel.x = new ParticleSystem.MinMaxCurve(10.0f, WaterfallArc);

		// Map splash inner scale amd position
        WaterfallSplashInnerObj.transform.localScale = SplashInnerScale;
        WaterfallSplashInnerObj.transform.position = SplashInnerPosition;

        // Map splash inner scale amd position
        WaterfallSplashOuterObj.transform.localScale = SplashOuterScale;
        WaterfallSplashOuterObj.transform.position = SplashOuterPosition;


        // Map Splash density
        SplashInnerPS.maxParticles = SplashInnerDensity;
        SplashOuterPS.maxParticles = SplashOuterDensity;

        // Map Waterfall Length
        WaterfallMainPS.startLifetime = WaterfallLength;
        WaterfallMistPS.startLifetime = WaterfallLength;

        // Map the toggling of each layer's activeness
        toggleWaterfallLayer(WaterfallMistObj, WaterfallMist);
        toggleWaterfallLayer(WaterfallStartMistObj, WaterfallMist);
        toggleWaterfallLayer(WaterfallDripMistObj, WaterfallMist);
        toggleWaterfallLayer(WaterfallMainObj, WaterfallMain);
        toggleWaterfallLayer(WaterfallSplashInnerObj, SplashInner);
        toggleWaterfallLayer(WaterfallSplashOuterObj, SplashOuter);

    }

	// Switch to disable layers
    void toggleWaterfallLayer (GameObject WaterfallLayer, bool Mist)
    {
        switch (Mist)
        {
            case true:
                if (!WaterfallLayer.activeInHierarchy)
                {
                    WaterfallLayer.SetActive(true);
                }
                break;
            case false:
                if (WaterfallLayer.activeSelf)
                {
                    WaterfallLayer.SetActive(false);
                }
                break;
            default:
                WaterfallLayer.SetActive(true);
                break;
        }
    }

}
