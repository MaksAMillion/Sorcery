using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;
	private AudioSource audioSource;
	private bool playOnce;

    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        
    }
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		playOnce = true;
	}
    void Update ()
    {
        if (sceneStarting)
        {
            StartScene();
        }
	}

    void FadeToClear()
    {
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
		
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (FadeImg.color.a <= 0.05f)
        {
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            sceneStarting = false;
        }
    }

    public IEnumerator EndSceneRoutine(int SceneNumber)
    {
        FadeImg.enabled = true;
		if (playOnce) {
            if (audioSource != null)
            {
                audioSource.Play();


            }
            playOnce = false;
		}
        do
        {
            FadeToBlack();

            if (FadeImg.color.a >= 0.95f)
            {
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        while (true);
    }

    public void EndScene(int SceneNumber)
    {
		
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }
}