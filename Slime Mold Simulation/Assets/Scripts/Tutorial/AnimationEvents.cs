using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject spore2;

    [SerializeField] private RawImage display;
    [SerializeField] private VideoPlayer displayVideo;
    [SerializeField] private Animator fadeOutAnimation;

    public void ActivateSpore2()
    {
        spore2.SetActive(true);
    }

    public void DeActivateSpore2()
    {
        spore2.SetActive(false);
    }

    public void StartVideo()
    {
        displayVideo.frame = 0;
        displayVideo.Play();
    }

    public void ResetVideo()
    {
        displayVideo.frame = 0;
        displayVideo.Stop();
    }

    public void FadeToNextScene()
    {
        fadeOutAnimation.Play("FadeOut");
    }
}