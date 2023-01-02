using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using epoching.easy_debug_on_the_phone;
using UnityEngine.UI;
using epoching.easy_gui;

namespace epoching.easy_qr_code
{
    public class Read_qr_code : MonoBehaviour
    {
        public RawImage rawImageVideo;
        public UIBindings uIBindings;
        public Demo_control uIControl;
        private WebCamTexture camTexture;
        private bool isReading = false;

        void OnEnable(){
            StartCoroutine(this.StartWebcam());
        }

        private IEnumerator StartWebcam(){
            yield return new WaitForSeconds(0.11f);
            this.camTexture = new WebCamTexture();
            this.camTexture.requestedWidth = 720;
            this.camTexture.requestedHeight = 1280;
            this.camTexture.Play();

            if (Application.platform == RuntimePlatform.Android){
                this.rawImageVideo.rectTransform.sizeDelta = new Vector2(Screen.width * camTexture.width / (float)this.camTexture.height, Screen.width);
                this.rawImageVideo.rectTransform.rotation = Quaternion.Euler(0, 0, -90);
            } else if (Application.platform == RuntimePlatform.IPhonePlayer){
                this.rawImageVideo.rectTransform.sizeDelta = new Vector2(1080, 1080 * this.camTexture.width / (float)this.camTexture.height);
                this.rawImageVideo.rectTransform.localScale = new Vector3(-1, 1, 1);
                this.rawImageVideo.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
            } else {
                this.rawImageVideo.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelWidth * this.camTexture.height / (float)this.camTexture.width);
                this.rawImageVideo.rectTransform.localScale = new Vector3(-1, 1, 1);
            }

            this.rawImageVideo.texture = camTexture;
            this.isReading = true;
            yield return null;
        }


        void OnDisable(){
            if (this.camTexture != null){
                this.camTexture.Stop();
            }
        }

        private float intervalTime = 0.1f;
        private float timeStamp = 0;
        void Update(){
            if (this.isReading){
                this.timeStamp += Time.deltaTime;

                if (this.timeStamp > this.intervalTime){
                    this.timeStamp = 0;
                    try{
                        Debug.Log("reading");
                        IBarcodeReader barcodeReader = new BarcodeReader();
                        if (this.camTexture != null && this.camTexture.isPlaying && this.camTexture.isReadable){
                            // decode the current frame
                            var result = barcodeReader.Decode(this.camTexture.GetPixels32(), this.camTexture.width, this.camTexture.height);
                            if (result.Text == "Start Flood Of Time"){
                                uIControl.ChangeToConnectAirpodUi();
                                uIBindings.start();
                            }
                        }
                    }
                    catch (Exception ex){
                        Debug.LogWarning(ex.Message);
                        //Canvas_confirm_box.confirm_box
                        //(
                        //    "confirm box",
                        //    "error>>>" + ex.Message,
                        //    "cancel",
                        //    "OK",
                        //    true,
                        //    delegate ()
                        //    {
                        //        this.isReading = true;
                        //    },
                        //    delegate ()
                        //    {
                        //        this.isReading = true;
                        //    }
                        //);
                        //this.isReading = false;
                    }
                }
            }
        }
    }
}

