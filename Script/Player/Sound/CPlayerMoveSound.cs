using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveCheack
{
    Grass,
    Rock
}
public class CPlayerMoveSound : CPlayerBase
{
    public PlayerMoveCheack _PlayerMoveCheack = PlayerMoveCheack.Grass;


    private void Start()
    {
    }
    private void Update()
    {        
    }
    public void SoundType(int type)
    {
        SoundManager.I.PlaySound(transform, (PlaySoundId)type);
    }
    public void MoveSoundPlay()
    {
        if (Mathf.Abs(CPlayerManager._instance._PlayerMove.fHorizontal) > 0.05f &&
            Mathf.Abs(CPlayerManager._instance._PlayerMove.fVertical) > 0.05f)
            return;

        if (_PlayerMoveCheack == PlayerMoveCheack.Rock)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_Stone);
            else
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_Stone);
        }
        else if (_PlayerMoveCheack == PlayerMoveCheack.Grass)
        {
            if (_PlayerManager._PlayerSwap._PlayerMode == PlayerMode.Shield)
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_Grass);
            else
                SoundManager.I.PlaySound(transform, PlaySoundId.Walk_Grass);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Rock;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
            _PlayerMoveCheack = PlayerMoveCheack.Grass;
        }
    }
}
