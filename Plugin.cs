using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Reflection;
using System.Linq;

namespace ValheimPlugin
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.killerbob.valheimclock";
        public const string PluginName = "ValheimClock";
        public const string PluginVersion = "1.0.0";

        private ConfigEntry<bool> configEnabled;
        private ConfigEntry<float> configClockSize;

        private GameObject clockUI;
        private GameObject hourHand;
        private GameObject minuteHand;
        private Texture2D watchFaceTexture;

        private void Awake()
        {
            configEnabled = Config.Bind("General", "Enabled", true, "Enable the clock display");
            configClockSize = Config.Bind("General", "ClockSize", 100f, "Size of the clock in pixels");

            Logger.LogInfo($"{PluginName} v{PluginVersion} has loaded!");

            LoadWatchFace();
        }

        private void LoadWatchFace()
        {
            string pluginPath = Path.GetDirectoryName(Info.Location);
            string imagePath = Path.Combine(pluginPath, "clock.png");

            Logger.LogInfo($"Looking for clock.png at: {imagePath}");
            Logger.LogInfo($"File exists: {File.Exists(imagePath)}");

            if (File.Exists(imagePath))
            {
                try
                {
                    byte[] imageData = File.ReadAllBytes(imagePath);
                    Logger.LogInfo($"Read {imageData.Length} bytes from clock.png");
                    
                    // Create texture with alpha support
                    watchFaceTexture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                    watchFaceTexture.wrapMode = TextureWrapMode.Clamp;
                    watchFaceTexture.filterMode = FilterMode.Bilinear;
                    
                    // Try ImageConversion
                    var imageConversionType = System.Type.GetType("UnityEngine.ImageConversion, UnityEngine.ImageConversionModule");
                    if (imageConversionType != null)
                    {
                        Logger.LogInfo("ImageConversion type found!");
                        var loadMethod = imageConversionType.GetMethod("LoadImage", 
                            BindingFlags.Public | BindingFlags.Static,
                            null,
                            new[] { typeof(Texture2D), typeof(byte[]) },
                            null);
                        
                        if (loadMethod != null)
                        {
                            Logger.LogInfo("Calling ImageConversion.LoadImage...");
                            loadMethod.Invoke(null, new object[] { watchFaceTexture, imageData });
                            Logger.LogInfo($"clock.png loaded successfully! Size: {watchFaceTexture.width}x{watchFaceTexture.height}, Format: {watchFaceTexture.format}");
                            return;
                        }
                    }
                    
                    Logger.LogError("Could not find ImageConversion.LoadImage!");
                    CreateFallbackTexture();
                }
                catch (System.Exception ex)
                {
                    Logger.LogError($"Error loading clock.png: {ex.Message}");
                    Logger.LogError($"Stack trace: {ex.StackTrace}");
                    CreateFallbackTexture();
                }
            }
            else
            {
                Logger.LogError($"clock.png not found at: {imagePath}");
                CreateFallbackTexture();
            }
        }

        private void CreateFallbackTexture()
        {
            watchFaceTexture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            Color[] pixels = new Color[256 * 256];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Color(0.9f, 0.9f, 0.85f, 1f);
            }
            watchFaceTexture.SetPixels(pixels);
            watchFaceTexture.Apply();
            Logger.LogInfo("Using fallback texture for clock face");
        }

        private void CreateClockUI()
        {
            if (clockUI != null) return;

            GameObject canvasGO = new GameObject("ValheimClockCanvas");
            DontDestroyOnLoad(canvasGO);

            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            // Ingen CanvasScaler - använd pixel-perfect sizing istället
            canvasGO.AddComponent<GraphicRaycaster>();

            clockUI = new GameObject("ValheimClock");
            clockUI.transform.SetParent(canvas.transform, false);
            
            RectTransform clockRect = clockUI.AddComponent<RectTransform>();
            clockRect.anchorMin = new Vector2(0.5f, 1);
            clockRect.anchorMax = new Vector2(0.5f, 1);
            clockRect.pivot = new Vector2(0.5f, 1);
            clockRect.anchoredPosition = new Vector2(0, -20);
            
            // Använd bildstorlek direkt om textur finns
            if (watchFaceTexture != null)
            {
                clockRect.sizeDelta = new Vector2(watchFaceTexture.width, watchFaceTexture.height);
                Logger.LogInfo($"Clock size set to texture size: {watchFaceTexture.width}x{watchFaceTexture.height}");
            }
            else
            {
                clockRect.sizeDelta = new Vector2(configClockSize.Value, configClockSize.Value);
            }

            if (watchFaceTexture != null)
            {
                Image clockImage = clockUI.AddComponent<Image>();
                Sprite sprite = Sprite.Create(watchFaceTexture, 
                    new Rect(0, 0, watchFaceTexture.width, watchFaceTexture.height), 
                    new Vector2(0.5f, 0.5f), 
                    100f); // 100 pixels per unit för exakt pixelstorlek
                clockImage.sprite = sprite;
                clockImage.preserveAspect = true;
                
                // Enable transparency
                clockImage.material = null;
                clockImage.color = Color.white;
                
                Logger.LogInfo($"Clock image created with transparency support");
            }

            CreateHourHand();
            CreateMinuteHand();

            Logger.LogInfo("Clock UI created!");
        }

        private void CreateHourHand()
        {
            hourHand = new GameObject("HourHand");
            hourHand.transform.SetParent(clockUI.transform, false);

            RectTransform handRect = hourHand.AddComponent<RectTransform>();
            handRect.anchorMin = new Vector2(0.5f, 0.5f);
            handRect.anchorMax = new Vector2(0.5f, 0.5f);
            handRect.pivot = new Vector2(0.5f, 0);
            
            // Använd faktiska bildstorleken för beräkning
            float actualSize = watchFaceTexture != null ? watchFaceTexture.width : configClockSize.Value;
            float handWidth = Mathf.Max(2f, actualSize * 0.015f);
            float handLength = actualSize * 0.15f;
            handRect.sizeDelta = new Vector2(handWidth, handLength);
            handRect.anchoredPosition = Vector2.zero;

            Image handImage = hourHand.AddComponent<Image>();
            handImage.color = new Color(0.05f, 0.05f, 0.05f, 0.95f);
            
            Logger.LogInfo($"Hour hand created: {handWidth}px wide, {handLength}px long (based on {actualSize}px clock)");
        }

        private void CreateMinuteHand()
        {
            minuteHand = new GameObject("MinuteHand");
            minuteHand.transform.SetParent(clockUI.transform, false);

            RectTransform handRect = minuteHand.AddComponent<RectTransform>();
            handRect.anchorMin = new Vector2(0.5f, 0.5f);
            handRect.anchorMax = new Vector2(0.5f, 0.5f);
            handRect.pivot = new Vector2(0.5f, 0);
            
            // Använd faktiska bildstorleken för beräkning
            float actualSize = watchFaceTexture != null ? watchFaceTexture.width : configClockSize.Value;
            float handWidth = Mathf.Max(1f, actualSize * 0.01f);
            float handLength = actualSize * 0.25f;
            handRect.sizeDelta = new Vector2(handWidth, handLength);
            handRect.anchoredPosition = Vector2.zero;

            Image handImage = minuteHand.AddComponent<Image>();
            handImage.color = new Color(0.15f, 0.15f, 0.15f, 0.95f);
            
            Logger.LogInfo($"Minute hand created: {handWidth}px wide, {handLength}px long (based on {actualSize}px clock)");
        }

        private void Update()
        {
            if (!configEnabled.Value)
                return;

            // Skapa UI endast när vi är inne i spelet (Player finns)
            if (clockUI == null && watchFaceTexture != null && Player.m_localPlayer != null)
            {
                CreateClockUI();
            }

            if (clockUI == null || EnvMan.instance == null)
                return;

            float dayFraction = EnvMan.instance.GetDayFraction();
            float timeOfDay = dayFraction * 24f;
            
            float hours = timeOfDay % 12f;
            float minutes = (timeOfDay % 1f) * 60f;

            if (hourHand != null)
            {
                float hourAngle = (hours / 12f) * 360f;
                hourHand.transform.localRotation = Quaternion.Euler(0, 0, -hourAngle);
            }

            if (minuteHand != null)
            {
                float minuteAngle = (minutes / 60f) * 360f;
                minuteHand.transform.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
            }

            clockUI.SetActive(configEnabled.Value);
        }

        private void OnDestroy()
        {
            if (clockUI != null)
            {
                Destroy(clockUI.transform.parent.gameObject);
            }
        }
    }
}
