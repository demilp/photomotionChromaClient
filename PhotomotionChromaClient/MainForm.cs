using AForge.Video.FFMPEG;
using Bypass;
using Bypass.SimpleJSON;
using demilp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotomotionChromaClient
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public static MainForm instance;
        public JSONNode chromaConfig;
        BypassClient client;
        int background = -1;
        int outputWidth;
        int outputHeight;
        public ChromaKey chroma;
        string[] backgroundFolder;
        //public Camera.Canon.CanonFrameworkManager cameraManager;
        private int fps;
        string videoMode;
        int videoLoops;
        public bool processing = false;
        string outputPath;
        string backupPath;
        string netBackupPath;
        Bitmap intro;
        Bitmap outro;
        int introOutroFrames;
        int minPhotos;
        int maxPhotos;
        public MainForm()
        {
            InitializeComponent();
            instance = this;
            if (!Directory.Exists("camerasDump"))
            {
                Directory.CreateDirectory("camerasDump");
            }
            chroma = new ChromaKey();
            if (File.Exists("chromaConfig.json"))
            {
                chromaConfig = JSON.Parse(File.ReadAllText("chromaConfig.json"));
            }
            fps = int.Parse(ConfigurationManager.AppSettings["videoFPS"]);
            outputWidth = int.Parse(ConfigurationManager.AppSettings["videoWidth"]);
            outputHeight= int.Parse(ConfigurationManager.AppSettings["videoHeight"]);
            client = new BypassClient(ConfigurationManager.AppSettings["ip"], int.Parse(ConfigurationManager.AppSettings["port"]), ConfigurationManager.AppSettings["delimiter"], "photomotion", "tool");
            videoLoops = int.Parse(ConfigurationManager.AppSettings["videoLoopsCount"]);
            videoMode = ConfigurationManager.AppSettings["videoMode"].ToLower();
            outputPath = ConfigurationManager.AppSettings["pathVideoOutput"];
            backupPath = ConfigurationManager.AppSettings["pathPhotosBackup"];
            netBackupPath = ConfigurationManager.AppSettings["netPathPhotosBackup"];
            introOutroFrames = int.Parse(ConfigurationManager.AppSettings["introOutroFrames"]);
            minPhotos = int.Parse(ConfigurationManager.AppSettings["minPhotos"]);
            maxPhotos = int.Parse(ConfigurationManager.AppSettings["maxPhotos"]);
            try
            {
                intro = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["introImage"]);
            }
            catch (Exception e) { }
            try
            {
                outro = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["outroImage"]);
            }
            catch (Exception e) { }
            client.OnDataEvent += Client_OnDataEvent;
            if (!Directory.Exists(backupPath))
            {
                Log("Back up folder doesn't exist. " + backupPath);
            }
            if (!Directory.Exists(outputPath))
            {
                Log("Output folder doesn't exist. " + outputPath);
            }
            List<string> bf = new List<string>();
            int c = 0;
            string cc = "";
            do
            {
                cc = ConfigurationManager.AppSettings["backgroundFolder." + c];
                c++;
                if (cc != null)
                {
                    bf.Add(cc);
                }
                else { break; }
            } while (true);
            backgroundFolder = bf.ToArray();
        }

        private void Client_OnDataEvent(object sender, DataEventArgs e)
        {
            Log("Received: " + e.data);
            string[] s = e.data.Split('|');
            if (s.Length > 1)
            {
                if (s[0].ToLower() == "takevideo")
                {
                    if (!int.TryParse(s[1], out background))
                    {
                        return;
                    }
                    TakePhotos();
                }
            }
            else if (s[0] == "processvideo")
            {
                if (images2Process != null && videoFileName != null)
                {
                    bool ok = ProcessImages(images2Process, videoFileName);
                    client.SendData("video|" + (ok ? "ok" : "error") + "|" + videoFileName, "photomotionListener");
                }
                else
                {
                    client.SendData("video|" + "error" + "|noImagesToProcess", "photomotionListener");
                }                
                processing = false;
            }
        }
        private async void TakePhotos()
        {
            if (processing)
            {
                Log("Already processing another video. Canceling and starting new one");
            }
            videoFileName = null;
            images2Process = null;
            processing = true;
            DirectoryInfo di = new DirectoryInfo("camerasDump");
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception e) { Log(e.Message); }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }
                catch (Exception e) { Log(e.Message); }
            }
            if (ConfigurationManager.AppSettings["ipRelay"] != "")
            {
                Log("Taking photos via relay");
                try
                {
                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["ipRelay"] + ""))
                    {

                    }
                }
                catch (Exception e)
                {
                    Log(e.Message);
                }
            }
            else
            {
                
            }
            await Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(1000);
                if (Directory.GetFiles("camerasDump", "*.jpg").Length < maxPhotos)
                {
                    System.Threading.Thread.Sleep(1000);
                    if (Directory.GetFiles("camerasDump", "*.jpg").Length < maxPhotos)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                Log("Fetching photos from folder");
                string[] fileNames = Directory.GetFiles("camerasDump", "*.jpg");
                if (fileNames.Length == 0)
                {
                    processing = false;
                    client.SendData("photos|" + "error", "photomotionListener");
                    Log("No photos in folder");
                    return;
                }
                else if(fileNames.Length <= minPhotos)
                {
                    processing = false;
                    client.SendData("photos|" + "error", "photomotionListener");
                    Log(fileNames.Length+" photos in folder. Minimum is "+minPhotos);
                    return;
                }
                try
                {
                    fileNames = (from s in fileNames orderby int.Parse(Path.GetFileNameWithoutExtension(s)) select s).ToArray();
                }
                catch(Exception e)
                {
                    Log("Error sorting photos. "+e.Message);
                    client.SendData("photos|" + "error", "photomotionListener");
                    processing = false;
                    return;
                }
                List<Bitmap> images = new List<Bitmap>();
                for (int i = 0; i < fileNames.Length; i++)
                {
                    try
                    {
                        Bitmap b = new Bitmap(Image.FromFile(fileNames[i]));
                        Bitmap final = b;
                        if (background != -1)
                        {
                            if (File.Exists(Path.Combine(backgroundFolder[background], Path.GetFileName(fileNames[i]))))
                            {
                                SetChroma(Path.GetFileNameWithoutExtension(fileNames[i]));
                                chroma.Chroma(b);
                                Bitmap bg = new Bitmap(Image.FromFile(Path.Combine(backgroundFolder[background], Path.GetFileName(fileNames[i]))));
                                final = CombineBitmaps(bg, b);
                            }
                        }
                        images.Add(final);
                        Log("Photo: " + fileNames[i]);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }
                }
                videoFileName = DateTime.Now.ToString("yyMMddHHmmss") + ".mp4";                 
                
                string fileNameWE = Path.GetFileNameWithoutExtension(videoFileName);
                Directory.CreateDirectory(Path.Combine(backupPath, fileNameWE));
                string originalsFolder = Path.Combine(backupPath, fileNameWE);
                if (background != -1)
                {
                    originalsFolder = Path.Combine(backupPath, fileNameWE, "originals");
                    Directory.CreateDirectory(originalsFolder);
                    string chromaFolder = Path.Combine(backupPath, fileNameWE, "chroma");
                    Directory.CreateDirectory(chromaFolder);
                    Log("Backing up chroma photos");
                    try
                    {
                        File.WriteAllText(Path.Combine(chromaFolder, "background.txt"), background.ToString());
                        for (int i = 0; i < images.Count; i++)
                        {
                            images[i].Save(Path.Combine(chromaFolder, i + ".jpg"), ImageFormat.Jpeg);
                        }
                        client.SendData("photos|ok|"+videoMode+"|"+fps+"|"+Path.Combine(netBackupPath, fileNameWE, "chroma"), "photomotionListener");
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                        client.SendData("photos|error", "photomotionListener");
                    }
                }
                Log("Backing up original photos");
                try
                {
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        File.Copy(fileNames[i], Path.Combine(originalsFolder, Path.GetFileName(fileNames[i])));
                    }
                    if (background == -1)
                    {
                        client.SendData("photos|ok|" + Path.Combine(netBackupPath, fileNameWE), "photomotionListener");
                    }
                }
                catch (Exception e)
                {
                    Log(e.Message);
                    client.SendData("photos|error", "photomotionListener");
                }
                images2Process = images.ToArray();
            });
        }
        Bitmap[] images2Process;
        string videoFileName;



        private bool ProcessImages(Bitmap[] images, string fileName)
        {
            Log("Processing video");
            if (images == null || images.Length == 0)
            {
                Log("No images to process");
                return false;
            }
            try
            {
                VideoFileWriter writer = new VideoFileWriter();
                writer.Open(Path.Combine(outputPath, fileName), outputWidth, outputHeight, fps, VideoCodec.MPEG4, 5000000);
                if (intro != null)
                {
                    for (int i = 0; i < introOutroFrames; i++)
                    {
                        writer.WriteVideoFrame(new Bitmap(intro, intro.Width, intro.Height));
                    }
                }
                for (int k = 0; k < videoLoops; k++)
                {
                    if (videoMode != "pingpong" || videoMode == "pingpong" && k % 2 == 0)
                    {
                        for (int i = 0; i < images.Length; i++)
                        {
                            writer.WriteVideoFrame(new Bitmap(images[i], outputWidth, outputHeight));
                        }
                    }
                    else
                    {
                        for (int i = images.Length - 1; i >= 0; i--)
                        {
                            writer.WriteVideoFrame(new Bitmap(images[i], outputWidth, outputHeight));
                        }
                    }
                }
                if (outro != null)
                {
                    for (int i = 0; i < introOutroFrames; i++)
                    {
                        writer.WriteVideoFrame(new Bitmap(outro, outro.Width, outro.Height));
                    }
                }
                writer.Close();
                Log("Video " + fileName + " processed");
                return true;
            }
            catch (Exception e)
            {
                Log(e.Message);
                return false;
            }
        }

        private void buttonTakePhotos_Click(object sender, EventArgs e)
        {
            TakePhotos();
        }
        private void Log(string m)
        {
            this.Invoke((MethodInvoker)delegate {
                textBoxLog.Text = m + Environment.NewLine + textBoxLog.Text;
            });
            
        }
        private Bitmap CombineBitmaps(Bitmap bottom, Bitmap top)
        {
            System.Drawing.Bitmap finalImage = new System.Drawing.Bitmap(bottom.Width, bottom.Height);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
            {
                g.Clear(System.Drawing.Color.Black);
                g.DrawImage(bottom, new System.Drawing.Rectangle(0, 0, bottom.Width, bottom.Height));
                g.DrawImage(top, new System.Drawing.Rectangle(0, 0, top.Width, top.Height));
            }
            return finalImage;
        }

        private void buttonChromaCalibration_Click(object sender, EventArgs e)
        {
            if(ChromaCalibration.instance == null)
                new ChromaCalibration().Show();
        }
        void SetChroma(string id)
        {
            if (chromaConfig == null || chromaConfig["General"] == null)
            {
                chroma.Hue = 120;
                chroma.HueTolerance = 20;
                chroma.Saturation = 50;
                chroma.SaturationTolerance = 50;
                chroma.Value = 50;
                chroma.ValueTolerance = 50;
            }
            else
            {
                if (chromaConfig[id] == null)
                {
                    SetChroma("General");
                }
                else
                {
                    chroma.Hue = chromaConfig[id]["Hue"].AsFloat;
                    chroma.HueTolerance = chromaConfig[id]["HueTolerance"].AsFloat;
                    chroma.Saturation = chromaConfig[id]["Saturation"].AsFloat;
                    chroma.SaturationTolerance = chromaConfig[id]["SaturationTolerance"].AsFloat;
                    chroma.Value = chromaConfig[id]["Value"].AsFloat;
                    chroma.ValueTolerance = chromaConfig[id]["ValueTolerance"].AsFloat;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }
    }
}
