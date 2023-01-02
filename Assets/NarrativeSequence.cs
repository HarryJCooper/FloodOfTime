using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeSequence : MonoBehaviour
{
    public bool changeToMainUi, moreComingSoonUi;
    [SerializeField] private Transform player;
    public CheckIfNod checkIfNod;
    public bool airpodsConnected, qrCodeScanned = true;
    private bool hasPlayedWind = false;
    [SerializeField] AudioSource anna, annaFootstep, introSequence, isma, michael, windSource;
    [SerializeField] AudioClip[] annaClips, annaOverHereClips, annaFootstepClips, ismaClips, michaelClips;
    [SerializeField] AudioClip introSequenceClip, windClip;
    [SerializeField] RepeatAmbience[] seaAmbi, demolitionAmbi, thunderAmbi;
    [SerializeField] MovingAudioSource[] seaGullsSources, sirenSources;
    bool moveAnnaRight, moveAnnaLeft, moveAnnaForwards, moveAnnaBackwards, moveIsmaAndMichaelBackwards, hasNoddedOne, hasNoddedTwo;
    public bool hasPressedYes;
    [SerializeField] float distanceFromAnna;
    public float maxDistanceFromAnna = 2, annaWalkSpeed = 0.02f;
    public Text annaWalkSpeedText, maxDistanceFromAnnaText, distanceFromAnnaText, airpodsText;

    void MoveAnnaRight(){
        anna.transform.position = anna.transform.position + new Vector3(annaWalkSpeed, 0, 0);
    }

    void MoveAnnaLeft(){
        anna.transform.position = anna.transform.position + new Vector3(-annaWalkSpeed, 0, 0);
    }

    void MoveAnnaForwards(){
        anna.transform.position = anna.transform.position + new Vector3(0, 0, annaWalkSpeed);
    }

    void MoveAnnaBackwards(){
        anna.transform.position = anna.transform.position + new Vector3(0, 0, -annaWalkSpeed);
    }

    void MoveIsmaAndMichaelBackwards(){
        isma.transform.position = isma.transform.position + new Vector3(0, 0, -0.02f);
        michael.transform.position = michael.transform.position + new Vector3(0, 0, -0.02f);
    }

    void Update(){
        if (moveAnnaRight) MoveAnnaRight();
        if (moveAnnaLeft) MoveAnnaLeft();
        if (moveAnnaForwards) MoveAnnaForwards();
        if (moveAnnaBackwards) MoveAnnaBackwards();
        if (moveIsmaAndMichaelBackwards) MoveIsmaAndMichaelBackwards();
        if (airpodsConnected) airpodsText.text = ""; else airpodsText.text = "Connect Airpods";
        distanceFromAnna = Vector3.Distance(player.transform.position, anna.transform.position);
        distanceFromAnnaText.text = distanceFromAnna.ToString();
    }


    // ---VOICEOVER--- DONE
// ANNA 
// You made it! Have you got your airpods in? You’ll need them.
// [Instruction on what to do when you’ve done this]
// Maybe this should be text?
// They will help you hear me and find me.
// ---SOUNDS---
// --INTERACTION--
// Player stands at location and puts in airpods 
// Player [nods/tap text on screen] to continue
// Sets of footprints on the ground in the open space between the benches  
// START POINT
// Benches around open space to the left of the i360 viewing tower
// 50.8209676,-0.1495284
// --NOTES--
// Loop audio message til action is complete
////

    public IEnumerator WaitForNavisens(){
        yield return new WaitForSeconds(4f);
        StartCoroutine(YouMadeIt());
        // SET EVERYTHING TO FALSE FOR INTERACTIONS
        checkIfNod.canNod = false;
        checkIfNod.hasNodded = false;
    }

    public IEnumerator YouMadeIt(){
        yield return new WaitForSeconds(1f);
        anna.PlayOneShot(annaClips[0]);
        yield return new WaitForSeconds(annaClips[0].length);
        if (airpodsConnected) StartCoroutine(LookAtFeetTwo()); else StartCoroutine(YouMadeIt());
    }

// ---VOICEOVER--- DONE BUT NEED QR CODE
// Okay, now look for the footprints. Can you see some? Make sure you scan a pair of prints with your camera then stand in them. 
// ---SOUNDS---
// --INTERACTION--
// Player scans the footprints with their phone and then stands in them
// --NOTES--
// Loop audio message til action is complete 
////
    // IEnumerator LookAtYourFeet(){
    //     anna.PlayOneShot(annaClips[1]);
    //     yield return new WaitForSeconds(annaClips[1].length + 5f);
    //     // if (qrCodeScanned) StartCoroutine(LookAtFeetTwo()); else StartCoroutine(LookAtYourFeet());
    //     // INSTEAD EDITED TO JUST SAY 'LOOK AT FEET'
    //     StartCoroutine(LookAtFeetTwo());
    // }

// ---VOICEOVER---
// This is Voice not text
//  Look at your feet…Feel your feet rooting to the ground..roots stretching out deep into the earth…  Connecting ...connecting 
// ---SOUNDS---
// --INTERACTION--
// --NOTES--
// TEXT ON SCREEN  Look at your feet…Feel your feet rooting to the ground..roots stretching out deep into the earth…  Connecting ...connecting ////
// ---SOUNDS---
// Sound is triggered when these actions are complete. 
// Tree roots creaking and crunching through the earth as a hollow wind rises and swirls. This moves to sounds of nature, bees and birds (and the ocean?) before being broken up by digital interference. 
// We pass through many voices, sounds of talking, lots of conversations passing. (Keywords: Love, Earth,
// Planet, Breakup) maybe also the sound of nature.
// The conversations turn to radio recordings and news items (locust story, virus, global warming, oil tankers, Mars, moon, bees, sea, hottest beach days recorded heat, famine. It is not all gloom but we get a sense of what is looming.
// As the voices and sounds fade we hear a single voice appear through them.
// --INTERACTION--
// --NOTES--
// Location of the sound should make you feel it’s rising from below from the roots. Perhaps roots and earth have a bass boost? If possible, sound should feel like it rises up from the earth to head level…
// One of those recordings hidden is Marconi stating “I dream of making a device where all sound is recorded for all time.” slipped in without drawing too much attention. 
////
    IEnumerator LookAtFeetTwo(){
        // ADD IN A STEREO
        changeToMainUi = true;
        yield return new WaitForSeconds(2f);
        anna.PlayOneShot(annaClips[2], 0.7f);
        yield return new WaitForSeconds(annaClips[2].length -3f);
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence(){
        introSequence.PlayOneShot(introSequenceClip);
        yield return new WaitForSeconds(introSequenceClip.length + 5f);
        StartCoroutine(AnnaMovement());
    }

// ---VOICEOVER---
// Hello? Hello? I can hear you… I know you are there. You are here at the start. I’ll be here with you all the way... 
// Over here...(giggles)
// And here! (giggles)
// I can be anywhere 
// ---SOUNDS---
// Footsteps and Anna’s voice moving away  [to the left]
// [and then to the right]
// [her footsteps and voice move around you]
// --INTERACTION--
// Player here may move with Anna but without instruction is likely to remain in the footsteps
/////
    IEnumerator AnnaMovement(){
        foreach(RepeatAmbience ambi in seaAmbi){
            StartCoroutine(ambi.RepeatAmbienceClips());
        }
        foreach(RepeatAmbience ambi in demolitionAmbi){
            StartCoroutine(ambi.RepeatAmbienceClips());
        }
        anna.PlayOneShot(annaClips[3]);
        anna.transform.position = new Vector3(5f, 0f, 3.66f);
        yield return new WaitForSeconds(annaClips[3].length);
        anna.transform.position = new Vector3(-5f, 0f, 3.66f);
        yield return new WaitForSeconds(annaClips[4].length);
        anna.transform.position = new Vector3(0f, 0f, 10f);
        anna.PlayOneShot(annaClips[5]);
        yield return new WaitForSeconds(annaClips[5].length);
        anna.transform.position = new Vector3(0f, 0f, 3.66f);
        anna.PlayOneShot(annaClips[6]);
        moveAnnaForwards = true;
        yield return new WaitForSeconds(2f);
        moveAnnaForwards = false;
        StartCoroutine(FollowMyVoice());
    }

// ---VOICEOVER---
// You need to follow my voice and footsteps. Let’s try it. Follow me.
// ---SOUNDS---
// Anna’s voice is back with you in the footsteps. 
// --INTERACTION--
////

// ---VOICEOVER---
// ---SOUNDS---
// Anna’s footsteps move 

    IEnumerator FollowMyVoice(){
        anna.PlayOneShot(annaClips[7]);
        yield return new WaitForSeconds(annaClips[7].length);
        annaFootstep.PlayOneShot(annaFootstepClips[1]);
        moveAnnaForwards = true;
        moveAnnaLeft = true;
        yield return new WaitForSeconds(annaFootstepClips[1].length + 3f);
        moveAnnaForwards = false;
        moveAnnaLeft = false;
        StartCoroutine(CheckIfPlayerHasMovedLoop());
    }


// ---VOICEOVER---
    IEnumerator CheckIfPlayerHasMovedLoop(){
        if (distanceFromAnna > maxDistanceFromAnna){
            anna.PlayOneShot(annaOverHereClips[Random.Range(0, annaOverHereClips.Length)]); // player hasn't moved
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(CheckIfPlayerHasMovedLoop());
        } else {
            anna.PlayOneShot(annaClips[9]); // [If player has moved]
                                            // And now over here!
            yield return new WaitForSeconds(annaClips[9].length + 1f);
            StartCoroutine(MoveAnnaAgain()); 
        }
    }

    // ---SOUNDS---
// Anna’s footsteps move again.
// --INTERACTION--
// Player moves in the direction of Anna’s voice.
////

    IEnumerator MoveAnnaAgain(){
        annaFootstep.PlayOneShot(annaFootstepClips[1]);
        moveAnnaForwards = true;
        moveAnnaRight = true;
        yield return new WaitForSeconds(annaFootstepClips[1].length);
        moveAnnaForwards = false;
        moveAnnaRight = false;
        StartCoroutine(CheckIfPlayerHasMovedLoopTwo());
    }

    IEnumerator CheckIfPlayerHasMovedLoopTwo(){
        if (distanceFromAnna > maxDistanceFromAnna){
            anna.PlayOneShot(annaOverHereClips[Random.Range(0, annaOverHereClips.Length)]); // player hasn't moved
            yield return new WaitForSeconds(3f);
            StartCoroutine(CheckIfPlayerHasMovedLoopTwo());
        } else {
            anna.PlayOneShot(annaClips[9]);
            yield return new WaitForSeconds(annaClips[9].length);
            StartCoroutine(OkayGotIt()); // player has moved
        }
    }

    // ---VOICEOVER---
// Okay. Got it? You can nod your head to let me know you understand. Why don’t you try it now? 
// [If player nods]

    IEnumerator OkayGotIt(){
        if (!hasNoddedOne){
            anna.PlayOneShot(annaClips[10]);
            checkIfNod.canNod = true;
            checkIfNod.headDown = false;
            hasNoddedOne = true; // Okay. Got it? You can nod your head to let me know you understand. Why don’t you try it now? 
            yield return new WaitForSeconds(annaClips[10].length + 3f);    
        } else {
            anna.PlayOneShot(annaClips[11]);
            yield return new WaitForSeconds(annaClips[11].length + 4f);
        }
        if (checkIfNod.hasNodded || hasPressedYes){
            checkIfNod.hasNodded = false;
            checkIfNod.headDown = false;
            checkIfNod.canNod = false;
            StartCoroutine(NodAgain());
        } else {
            StartCoroutine(OkayGotIt());
        }
    }

    // Great. Now nod again to let me know it’s all working.
// [If player does nothing prompt] 
// Try nodding your head.
// [If player still does nothing after [6] seconds of silence] 
// Have you tried nodding? If you have and I just can’t [hear/feel/see?] it, you can press [‘Yes’] on your phone screen instead.
// ---SOUNDS---
// --INTERACTION--
// Player nods their head.
// Player nods again.
// If player is nodding their head or just not listening to the prompt, they can tap ‘yes’ on their screen to  I continue. I agree we need this
// --NOTES--
// Loop audio  message til action is complete. 
////



    IEnumerator NodAgain(){
        if (!hasNoddedTwo) {
            checkIfNod.hasNodded = false;
            checkIfNod.headDown = false;
            checkIfNod.canNod = true;
            anna.PlayOneShot(annaClips[12]);
            hasNoddedTwo = true;
            yield return new WaitForSeconds(annaClips[12].length + 2f);
        } else {
            anna.PlayOneShot(annaClips[11]);
            yield return new WaitForSeconds(annaClips[11].length + 2f);
        }
        if (checkIfNod.hasNodded || hasPressedYes){
            StartCoroutine(GreatNowFollowMe());
            checkIfNod.hasNodded = false;
            checkIfNod.canNod = false;
            checkIfNod.headDown = false;
        } else {
            StartCoroutine(NodAgain());
        }
    }

// ---VOICEOVER---
// [If player has nodded twice]
// Great. Now follow me…
// [If player has tapped ‘yes’ on their phone screen]
// Great. Now put your phone in your pocket and follow me.
// ---SOUNDS---
// --INTERACTION--
// I agree we need this
////

    IEnumerator GreatNowFollowMe(){
        anna.PlayOneShot(annaClips[13]);
        yield return new WaitForSeconds(annaClips[13].length);
        anna.PlayOneShot(annaFootstepClips[0]);
        moveAnnaForwards = true;
        yield return new WaitForSeconds(annaFootstepClips[0].length);
        moveAnnaForwards = false;
        StartCoroutine(NowWeCanHearAllYourTime());
        // PUTTING IN POCKET WILL CAUSE ISSUES AT THE MOMENT SO HAVE REMOVED
    }

// ---VOICEOVER---
// Now we can hear all your time ...Everything you said and did.. There was a man who said we would. 
// He dreamed of a device where all sound though all time could be found and heard… So you left us all your voices, your words...the sounds of cars, trains, bombs and rockets.. birds, the sea, the cities too..all your sounds like he hoped.. But we have silence here now, except for the weather and words of course..the world is quiet and we listen to you ..our ancestors . So I need your help see.. to help me find the lost pieces, pieces of love..Seems as things dies in our time the sounds dies.. 
// ---SOUNDS---
// Wind swirls in a circle around you then slips away into the distance in the east. (It should feel like it wraps around us, comes under us, through us, whistles up behind us and then slips away lingering in the distance. It should make us turn to follow the sound as it leaves and stop to listen for Anna.
// --INTERACTION--
////
    IEnumerator NowWeCanHearAllYourTime(){
        sirenSources[0].turnOn = true;
        anna.PlayOneShot(annaClips[14]);
        yield return new WaitForSeconds(annaClips[14].length);
        StartCoroutine(ICanFeelThatChill());
    }

// ---VOICEOVER---
// I can feel that chill..can you feel it..? 
// ---SOUNDS---
// --INTERACTION--
// [Player nods]
// --NOTES--
// Yes add nod////
////
    IEnumerator ICanFeelThatChill(){
        if (!hasPlayedWind){
            windSource.PlayOneShot(windClip);
            yield return new WaitForSeconds(2f);
            hasPlayedWind = true;
        }
        checkIfNod.canNod = true;
        anna.PlayOneShot(annaClips[15]);
        yield return new WaitForSeconds(annaClips[15].length + 3f); 
        // sirenSources[1].turnOn = true; - CUT BASED ON FEEDBACK!
        if (checkIfNod.hasNodded){
            checkIfNod.hasNodded = false;
            checkIfNod.headDown = false;
            checkIfNod.canNod = false;
            StartCoroutine(WeJustHadABurn());
        } else {
            StartCoroutine(ICanFeelThatChill()); // REFACTOR - replace this with Can You Hear Me?
        }
    }

// ---VOICEOVER---
// We just had a burn.. So hot I could not breathe..like a snake wrapping around my neck and chest..but today, today it’s cold, so cold. You can feel the wind reach up your legs and bite through your clothes.... sneaking through every gap tingling up your spine and freezing bones ..Makes you shake and shudder...A freeze stormer... 
// How does it feel there today? 
// ---SOUNDS---
// --INTERACTION--
    IEnumerator WeJustHadABurn(){
        anna.PlayOneShot(annaClips[16]);
        yield return new WaitForSeconds(annaClips[16].length);
        StartCoroutine(OkayWeBetterGetGoing());
    }

// ---VOICEOVER---
// Okay, we better get going, there’s not much time. // ANNA MOVES AWAY
// This is where we start // REFACTOR - have replaced this
// [MOVEMENT INTERACTION]
// ---SOUNDS---
// A crack and rumble of thunder. We hear cold sheeted hail. Anna’s teeth chatter.
// --INTERACTION--
// Player Follows
////
    IEnumerator OkayWeBetterGetGoing(){
        yield return new WaitForSeconds(3f);
        anna.PlayOneShot(annaClips[17]);
        foreach(RepeatAmbience ambi in thunderAmbi){
            StartCoroutine(ambi.RepeatAmbienceClips());
        }
        yield return new WaitForSeconds(annaClips[17].length);
        annaFootstep.PlayOneShot(annaFootstepClips[1]);
        moveAnnaLeft = true;
        moveAnnaForwards = true;
        yield return new WaitForSeconds(annaFootstepClips[1].length);
        moveAnnaLeft = false;
        moveAnnaForwards = false;
        StartCoroutine(CheckIfPlayerHasMovedLoopThree());
    }
 
    IEnumerator CheckIfPlayerHasMovedLoopThree(){
        if (distanceFromAnna > maxDistanceFromAnna){
            anna.PlayOneShot(annaOverHereClips[Random.Range(0, annaOverHereClips.Length)]); // [If player hasn’t moved]
                                            // Over here! 
            yield return new WaitForSeconds(annaClips[8].length + 2f);
            StartCoroutine(CheckIfPlayerHasMovedLoopThree());
            } else {
            StartCoroutine(TakeALook());
        }
    }

    // ---VOICEOVER---
// Take a look at what you see around you now..breath in slowly, deeply, breath out.. Close your eyes………listen. 
// ---SOUNDS---
// --INTERACTION--
////

    IEnumerator TakeALook(){
        seaGullsSources[0].turnOn = true;
        anna.PlayOneShot(annaClips[18]);
        foreach(RepeatAmbience ambi in thunderAmbi){
            ambi.StopCoroutine();
        }
        yield return new WaitForSeconds(annaClips[18].length);
        StartCoroutine(IsmaAndMichael());
    }

// ---VOICEOVER---
// ---SOUNDS---
// We hear the sound of the sea..seagulls. A pair of foot steps on the shingle. The footsteps stop .A female breath breathing in the air next to us. Then a sigh out as if in wonder…
// --INTERACTION--
// ////

// ---VOICEOVER---
// ISHA 
// 1  - Where did we meet?
// MICHAEL 
// 1 - Well....It’s .. 
// ISHA
// 2 Been a while? Mmmm I just love it here..... it was just there by that bouy out there. 2
// MICHAEL 
// 2 - I saw you looking out to sea so I sat .. 
// ISHA 
// 3 - MISSING - Next to me ...and .. 
// MICHAEL 
// 3 - We looked out together watching the sea swimmers. 
// ISHA 
// 4 - Mmm it’s beautiful....I feel.. I feel so free here....I feel hope here... despite everything. 
// MICHAEL 
// 4 - The sea’s about two metres higher I’d say. Soon it will be… 
// ISHA 
// 5 - As I said ..everything.. 
// MICHAEL 
// 5 - Look at the clouds, see how they run across the horizon. 
// ISHA
// 6 - Hmmm I always thought love was something in me….But. 
// MICHAEL 
// 6 - But.. 
// ISHA
// 7 - EXTRA LINE IN - Hold me. 
// ---SOUNDS---
// Sea and seagulls in the background 
// We hear them embrace and kiss.
// --INTERACTION--
////


    IEnumerator IsmaAndMichael(){

        isma.transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y, player.transform.position.z + 5);
        michael.transform.position = new Vector3(player.transform.position.x + 7, player.transform.position.y, player.transform.position.z + 8);
        moveIsmaAndMichaelBackwards = true;
        isma.PlayOneShot(ismaClips[0]);
        michael.PlayOneShot(michaelClips[0]);
        // ISMA AND MICHAEL FOOTSTEPS
        yield return new WaitForSeconds(ismaClips[0].length);
        moveIsmaAndMichaelBackwards = false;
        isma.PlayOneShot(ismaClips[1]);
        yield return new WaitForSeconds(ismaClips[1].length);
        michael.PlayOneShot(michaelClips[1]);
        yield return new WaitForSeconds(michaelClips[1].length);
        isma.PlayOneShot(ismaClips[2]);
        seaGullsSources[1].turnOn = true;
        yield return new WaitForSeconds(ismaClips[2].length);
        michael.PlayOneShot(michaelClips[2]);
        yield return new WaitForSeconds(michaelClips[2].length);
        isma.PlayOneShot(ismaClips[3]);
        yield return new WaitForSeconds(ismaClips[3].length);
        michael.PlayOneShot(michaelClips[3]);
        yield return new WaitForSeconds(michaelClips[3].length);
        isma.PlayOneShot(ismaClips[4]);
        yield return new WaitForSeconds(ismaClips[4].length);
        michael.PlayOneShot(michaelClips[4]);
        yield return new WaitForSeconds(michaelClips[4].length);
        isma.PlayOneShot(ismaClips[5]);
        yield return new WaitForSeconds(ismaClips[5].length);
        michael.PlayOneShot(michaelClips[5]);
        yield return new WaitForSeconds(michaelClips[5].length);
        isma.PlayOneShot(ismaClips[6]);
        yield return new WaitForSeconds(ismaClips[6].length);
        michael.PlayOneShot(michaelClips[6]);
        yield return new WaitForSeconds(michaelClips[6].length);
        isma.PlayOneShot(ismaClips[7]);
        yield return new WaitForSeconds(ismaClips[7].length);
        michael.PlayOneShot(michaelClips[7]);
        yield return new WaitForSeconds(michaelClips[7].length + 4f);
        StartCoroutine(YouCanOpenYourEyes());
    }

// ---VOICEOVER---
// ---SOUNDS---
// The sound of the scene fades away/crackles from interference and it returns to the ambience of Anna’s world. 
// --INTERACTION--
// ////

// ---VOICEOVER---
// ANNA
// You can open your eyes.. 
// Us children still hate kissing.. But.. what was she going to say? But.. but love?
// -
// ---SOUNDS---
// --INTERACTION--
// ////

    IEnumerator YouCanOpenYourEyes(){
        // ADD ADDITIONAL AUDIO
        anna.PlayOneShot(annaClips[19]);
        yield return new WaitForSeconds(annaClips[19].length);
        moreComingSoonUi = true;
    }
}
