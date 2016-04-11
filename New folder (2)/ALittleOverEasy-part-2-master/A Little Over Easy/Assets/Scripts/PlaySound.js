var soundToPlay:AudioClip;

function OnTriggerEnter () {
    GetComponent.<AudioSource>().PlayOneShot(soundToPlay);
}