using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _intro;
    [SerializeField]
    private List<AudioClip> _swordSFX;
    [SerializeField]
    private AudioClip _cutSFX;
    [SerializeField]
    private AudioClip _groundSFX;
    [SerializeField]
    private AudioClip _bombSFX;
    [SerializeField]
    private AudioClip _powerupSFX;
    [SerializeField]
    private AudioClip _badPowerupSFX;
    [SerializeField]
    private AudioClip _FinishLineSFX;
    [SerializeField]
    private AudioSource _source;
   

    private void Start()
    {
        _source.clip = _intro;
        _source.Play();
    }

    public void OnSwordMove()
    {
        int random = Random.Range(0, _swordSFX.Count);
        _source.clip = _swordSFX[random];
        _source.Play();
    }

    public void OnCuttableHit()
    {
        _source.Stop();
        _source.clip = _cutSFX;
        _source.Play();
    }

    public void OnGroundHit()
    {
        _source.Stop();
        _source.clip = _groundSFX;
        _source.Play();
    }

    public void OnBombHit()
    {
        _source.Stop();
        _source.clip = _bombSFX;
        _source.Play();
    }

    public void OnPowerUpHit()
    {
        _source.Stop();
        _source.clip = _powerupSFX;
        _source.Play();
    }

    public void OnBadPowerUpHit()
    {

        _source.Stop();
        _source.clip = _badPowerupSFX;
        _source.Play();
    }

    public void OnFinishLineHit()
    {
        _source.Stop();
        _source.clip = _FinishLineSFX;
        _source.Play();
    }
}
