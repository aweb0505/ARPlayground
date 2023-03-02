using System;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace OpenCvSharp.ImageProcessing
{
    public class ARGrayScale : MonoBehaviour
    {
        public Camera cam;

        private RenderTexture targetTexture;

        private Texture2D destDefault = null;
        
        private UnityEngine.Rect _rect;

        private void Start()
        {
            cam = OVRExternalComposition.backgroundCamera;
            targetTexture = cam.targetTexture;
            destDefault = new Texture2D(targetTexture.width, targetTexture.height, TextureFormat.RGBA32, false);
            _rect = new UnityEngine.Rect(0, 0, targetTexture.width, targetTexture.height);
        }

        private void Update()
        {
            cam = OVRExternalComposition.backgroundCamera;
            targetTexture = cam.targetTexture;
            destDefault = new Texture2D(targetTexture.width, targetTexture.height, TextureFormat.RGBA32, false);
            _rect = new UnityEngine.Rect(0, 0, targetTexture.width, targetTexture.height);
            GreyScale();
        }

        private void GreyScale()
        {
            
            Texture2D texture2d = ToTexture2D(targetTexture);
            Mat mat = Unity.TextureToMat(texture2d);
            
            
            
            Mat grayMat = new Mat ();
            Cv2.CvtColor (mat, grayMat, ColorConversionCodes.BGR2GRAY); 
            
            Texture2D texture = Unity.MatToTexture(mat);
            texture.Apply();
            
            RawImage rawImage = gameObject.GetComponent<RawImage> ();
            rawImage.texture = texture;
            
            
        }

        private Texture2D dest;
        public bool testForMemLeak1 = false;
        public bool testForMemLeak2 = false;
        private Texture2D ToTexture2D(RenderTexture rTex)
        {
            //Texture2D dest = destDefault;
            //Texture2D dest = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false); //this line is a mem leak for some reason 
            if (testForMemLeak1)
            {
                /*if (destDefault == null)
                {
                    destDefault = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
                    Debug.LogWarning("null");
                }
                else
                {
                    //Debug.Log("not null");
                }*/
                dest = destDefault;

            }
            //Texture2D dest = Texture2D.blackTexture;
            
            if (testForMemLeak2)
            {
                //RenderTexture.active = rTex;
                Graphics.CopyTexture(rTex, dest);
                //dest.ReadPixels (_rect, 0, 0);
                //dest.Apply ();
                //RenderTexture.active = null;
                return dest;
            }

            return Texture2D.blackTexture;
        }
    }
}

