using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;

public class CodeTool : MonoBehaviour
{
    public static CodeTool Instance;

    private WebCamTexture camTexture;
    private Rect screenRect;

    public GameObject camHolder;

    public Text resultDisplay;

    private bool CameraOn = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string UserDataToCode(MainManager.UserData user)
    {
        string result = "";

        //HighScore conversion
        result += user.HighScore.ToString("X") + "|";

        //killedBosses conversion
        BitArray killedBosses = new BitArray(16, false);
        BitArray equipedWeapons = new BitArray(16, false);

        foreach (KeyValuePair<string, bool> pair in user.killedBosses)
        {
            if (pair.Key == "Hellicopter")
            {
                killedBosses[0] = true;
                equipedWeapons[0] = pair.Value;
            }

            if (pair.Key == "Car")
            {
                killedBosses[1] = true;
                equipedWeapons[1] = pair.Value;
            }

            if (pair.Key == "Tank")
            {
                killedBosses[2] = true;
                equipedWeapons[2] = pair.Value;
            }

            if (pair.Key == "AntiAir")
            {
                killedBosses[3] = true;
                equipedWeapons[3] = pair.Value;
            }

            if (pair.Key == "Jet")
            {
                killedBosses[4] = true;
                equipedWeapons[4] = pair.Value;
            }

            if (pair.Key == "Train")
            {
                killedBosses[5] = true;
                equipedWeapons[5] = pair.Value;
            }

            if (pair.Key == "Saucer")
            {
                killedBosses[6] = true;
                equipedWeapons[6] = pair.Value;
            }

            if (pair.Key == "Submarine")
            {
                killedBosses[7] = true;
                equipedWeapons[7] = pair.Value;
            }

            if (pair.Key == "Station")
            {
                killedBosses[8] = true;
                equipedWeapons[8] = pair.Value;
            }
        }

        byte[] data = new byte[2];
        killedBosses.CopyTo(data,0);
        result += System.BitConverter.ToString(data) + "|";

        //equiped weapons conversion
        equipedWeapons.CopyTo(data, 0);
        result += System.BitConverter.ToString(data);

        return result;
    }

    public MainManager.UserData CodeToUserData(string code)
    {
        // example code BE|01-00|00-00|
        // where highscore|killedBosses|equipedWeapons|
        MainManager.UserData result = new MainManager.UserData();

        string[] codeBlocks = code.Split('|');

        result.HighScore = int.Parse(codeBlocks[0], System.Globalization.NumberStyles.HexNumber);

        int blockNum = 1;
        foreach (string hex in codeBlocks[1].Split('-'))
        {
            int temp = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            byte[] bytes = System.BitConverter.GetBytes(temp);
            BitArray bits = new BitArray(bytes);
            
            if (blockNum == 1)
            {
                if (bits[0])
                {
                    result.confirmKill("Hellicopter");
                }

                if (bits[1])
                {
                    result.confirmKill("Car");
                }

                if (bits[2])
                {
                    result.confirmKill("Tank");
                }

                if (bits[3])
                {
                    result.confirmKill("AntiAir");
                }

                if (bits[4])
                {
                    result.confirmKill("Jet");
                }

                if (bits[5])
                {
                    result.confirmKill("Train");
                }

                if (bits[6])
                {
                    result.confirmKill("Saucer");
                }

                if (bits[7])
                {
                    result.confirmKill("Submarine");
                }

                blockNum++;
            }
            else 
            {
                if (bits[0])
                {
                    result.confirmKill("Station");
                }
            }
        }

        blockNum = 1;
        foreach (string hex in codeBlocks[2].Split('-'))
        {
            int temp = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            byte[] bytes = System.BitConverter.GetBytes(temp);
            BitArray bits = new BitArray(bytes);

            if (blockNum == 1)
            {
                if (bits[0])
                {
                    result.equipWeapon("Hellicopter");
                }

                if (bits[1])
                {
                    result.equipWeapon("Car");
                }

                if (bits[2])
                {
                    result.equipWeapon("Tank");
                }

                if (bits[3])
                {
                    result.equipWeapon("AntiAir");
                }

                if (bits[4])
                {
                    result.equipWeapon("Jet");
                }

                if (bits[5])
                {
                    result.equipWeapon("Train");
                }

                if (bits[6])
                {
                    result.equipWeapon("Saucer");
                }

                if (bits[7])
                {
                    result.equipWeapon("Submarine");
                }

                blockNum++;
            }
            else
            {
                if (bits[0])
                {
                    result.equipWeapon("Station");
                }
            }
        }

        return result;
    }

    
    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D CreateQRCode(MainManager.UserData user)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(UserDataToCode(user), encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public Texture2D CreateQRCode(string str)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(str, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public void ReadQRCode()
    {
        screenRect = camHolder.GetComponent<RectTransform>().rect;
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = (int)screenRect.height;
        camTexture.requestedWidth = (int)screenRect.width;
        
        if (camTexture != null)
        {
            CameraOn = true;
            camTexture.Play();
        }

    }

    void OnGUI()
    {
        if (CameraOn)
        {
            // drawing the camera on screen
            GUI.DrawTexture(new Rect(new Vector2(0,0),screenRect.size), camTexture, ScaleMode.ScaleToFit);
            // do the reading ? you might want to attempt to read less often than you draw on the screen for performance sake
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);
                if (result != null)
                {
                    resultDisplay.text = result.Text;
                    CameraOn = false;
                    camTexture.Stop();
                }
            }
            catch (Exception ex) { resultDisplay.text = ex.Message; }

            if (!camHolder.active)
            {
                CameraOn = false;
                camTexture.Stop();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
