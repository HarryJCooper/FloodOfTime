using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using epoching.easy_gui;

namespace epoching.easy_qr_code
{
    public class Demo_control : MonoBehaviour
    {
        public AudioSource affirmativeSource;
        public AudioClip affirmativeClip;
        public static Demo_control instance;
        public GameObject backgroundUi;
        public GameObject startUi;
        public GameObject scanQrUi;
        public GameObject connectAirpodsUi;
        public GameObject mainUi;
        public GameObject underTheHoodUi;
        public GameObject moreComingSoonUi;
        public NarrativeSequence narrativeSequence;
        bool hasChangedToMainUi, hasChangedToMoreComingSoonUi;

        void Awake(){
            Demo_control.instance = this;
            this.ChangeToStartUi();
            backgroundUi.SetActive(true);
        }

        void Start(){
            new WebCamTexture(WebCamTexture.devices[0].name);
        }

        void Update(){
            if (narrativeSequence.changeToMainUi && !hasChangedToMainUi){
                hasChangedToMainUi = true;
                this.ChangeToMainUi();
            }
            if (narrativeSequence.moreComingSoonUi && !hasChangedToMoreComingSoonUi){
                hasChangedToMoreComingSoonUi = true;
                this.MoreComingSoonUi();
            }
        }

        public void ChangeToStartUi(){
            StartCoroutine(Canvas_grounp_fade.hide(this.scanQrUi));
            StartCoroutine(Canvas_grounp_fade.hide(this.connectAirpodsUi));
            StartCoroutine(Canvas_grounp_fade.hide(this.mainUi));
            StartCoroutine(Canvas_grounp_fade.hide(this.underTheHoodUi));
            StartCoroutine(Canvas_grounp_fade.hide(this.moreComingSoonUi));
            StartCoroutine(Canvas_grounp_fade.show(this.startUi));
        }

        public void ChangeToReadQrCodeUi(){
            affirmativeSource.PlayOneShot(affirmativeClip, 0.4f);
            StartCoroutine(Canvas_grounp_fade.hide(this.startUi));
            StartCoroutine(Canvas_grounp_fade.show(this.scanQrUi));
        }

        public void ChangeToConnectAirpodUi(){
            affirmativeSource.PlayOneShot(affirmativeClip, 0.4f);
            StartCoroutine(Canvas_grounp_fade.hide(this.scanQrUi));
            StartCoroutine(Canvas_grounp_fade.show(this.connectAirpodsUi));
        }

        public void ChangeToMainUi(){
            affirmativeSource.PlayOneShot(affirmativeClip, 0.4f);
            StartCoroutine(Canvas_grounp_fade.hide(this.connectAirpodsUi));
            StartCoroutine(Canvas_grounp_fade.show(this.mainUi));
        }

        public void LookUnderTheHoodUi(){
            StartCoroutine(Canvas_grounp_fade.hide(this.mainUi));
            StartCoroutine(Canvas_grounp_fade.hide(this.backgroundUi));
            StartCoroutine(Canvas_grounp_fade.show(this.underTheHoodUi));
        }

        public void ReturnToMainUi(){
            StartCoroutine(Canvas_grounp_fade.hide(this.underTheHoodUi));
            StartCoroutine(Canvas_grounp_fade.show(this.mainUi));
        }

        public void MoreComingSoonUi(){
            StartCoroutine(Canvas_grounp_fade.show(this.moreComingSoonUi));
        }
    }
}
