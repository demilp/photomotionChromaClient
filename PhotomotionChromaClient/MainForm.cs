using AForge.Video.FFMPEG;
using Bypass;
using Bypass.SimpleJSON;
using demilp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
        private int? fps;
        string videoMode;
        int? videoLoops;
        public bool processing = false;
        string calibrated;
        string originalPhotosPath;
        string outputPath;
        string backupPath;
        string absoluteBackupPath;
        Bitmap intro;
        Bitmap outro;
        int introOutroFrames;
        int minPhotos;
        int maxPhotos;
        int maxWaitPhotos;
        bool generateThumbs;
        int? initialPhoto;
        public MainForm()
        {
            InitializeComponent();
            instance = this;
            chroma = new ChromaKey();
            if (File.Exists("chromaConfig.json"))
            {
                chromaConfig = JSON.Parse(File.ReadAllText("chromaConfig.json"));
            }
            generateThumbs = bool.Parse(ConfigurationManager.AppSettings["generateThumbnails"]);
            if (ConfigurationManager.AppSettings["videoFPS"] == "")
                fps = null;
            else
                fps = int.Parse(ConfigurationManager.AppSettings["videoFPS"]);
            outputWidth = int.Parse(ConfigurationManager.AppSettings["videoWidth"]);
            outputHeight= int.Parse(ConfigurationManager.AppSettings["videoHeight"]);
            client = new BypassClient(ConfigurationManager.AppSettings["ip"], int.Parse(ConfigurationManager.AppSettings["port"]), ConfigurationManager.AppSettings["delimiter"], "photomotion", "tool");
            if (ConfigurationManager.AppSettings["videoLoopsCount"] == "")
                videoLoops = null;
            else
                videoLoops = int.Parse(ConfigurationManager.AppSettings["videoLoopsCount"]);
            videoMode = ConfigurationManager.AppSettings["videoMode"].ToLower();
            if (ConfigurationManager.AppSettings["initialPhoto"] == "")
                initialPhoto = null;
            else
                initialPhoto = int.Parse(ConfigurationManager.AppSettings["initialPhoto"]);
            calibrated = ConfigurationManager.AppSettings["pathPhotosSource"];
            outputPath = ConfigurationManager.AppSettings["pathVideoOutput"];
            backupPath = ConfigurationManager.AppSettings["pathPhotosBackup"];
            originalPhotosPath = ConfigurationManager.AppSettings["pathBackupNonCalibratedPhotos"];
            absoluteBackupPath = ConfigurationManager.AppSettings["absolutePathPhotosBackup"];
            introOutroFrames = int.Parse(ConfigurationManager.AppSettings["introOutroFrames"]);
            minPhotos = int.Parse(ConfigurationManager.AppSettings["minPhotos"]);
            maxPhotos = int.Parse(ConfigurationManager.AppSettings["maxPhotos"]);
            maxWaitPhotos = int.Parse(ConfigurationManager.AppSettings["maxWaitPhotos"]);
            initialPhoto = int.Parse(ConfigurationManager.AppSettings["initialPhoto"]);


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
        
        private void MainForm_Load(object sender, EventArgs ev)
        {
            try
            {
                intro = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["introImage"]);
            }
            catch (Exception e)
            {
                Log("Intro image path not valid");
            }
            try
            {
                outro = (Bitmap)Image.FromFile(ConfigurationManager.AppSettings["outroImage"]);
            }
            catch (Exception e)
            {
                Log("Outro image path not valid");
            }
            client.OnDataEvent += Client_OnDataEvent;

            if (!Directory.Exists(calibrated))
            {
                DialogResult res = MessageBox.Show("Calibrated photos folder doesn't exist. " + calibrated, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Log("Calibrated photos folder doesn't exist. " + backupPath);
            }
            if (!Directory.Exists(backupPath))
            {
                DialogResult res = MessageBox.Show("Back up folder doesn't exist. " + backupPath, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Log("Back up folder doesn't exist. " + backupPath);
            }
            if (!Directory.Exists(outputPath))
            {
                DialogResult res = MessageBox.Show("Output folder doesn't exist. " + outputPath, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Log("Output folder doesn't exist. " + outputPath);
            }
            if (!Directory.Exists(absoluteBackupPath))
            {
                DialogResult res = MessageBox.Show("AbsoluteBackupPath folder doesn't exist. " + absoluteBackupPath, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Log("AbsoluteBackupPath folder doesn't exist. " + absoluteBackupPath);
            }
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
                    //bool ok = ProcessImages(images2Process, videoFileName);
                    bool ok = ProcessImages(images2Process, "temp.mp4");
                    Encode2H264(videoFileName);
                    client.SendData("video|" + (ok ? "ok" : "error") + "|" + videoFileName, "photomotionListener");
                }
                else
                {
                    client.SendData("video|" + "error" + "|noImagesToProcess", "photomotionListener");
                }
                if(images2Process != null)
                {
                    for (int i = 0; i < images2Process.Length; i++)
                    {
                        images2Process[i].Dispose();
                    }
                }
                processing = false;
            }
        }
        private async void TakePhotos()
        {
            if (processing)
            {
                if (images2Process != null)
                {
                    for (int i = 0; i < images2Process.Length; i++)
                    {
                        images2Process[i].Dispose();
                    }
                }
                Log("Already processing another video. Canceling and starting new one");
            }
            videoFileName = null;
            images2Process = null;
            processing = true;
            DirectoryInfo di = new DirectoryInfo(calibrated);
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
                int tAcumulado = 0;
                int cantArch = 0;

                Log("Waiting 1 sec");
                System.Threading.Thread.Sleep(1000);
                Log("Ready");
                while (cantArch < maxPhotos && tAcumulado < maxWaitPhotos)
                {

                    int cantArch2 = Directory.GetFiles(calibrated, "*.jpg").Length;

                    int tIntervaloMs = 500;

                    Log("Files in folder: " + cantArch2 + ". Waiting " + tIntervaloMs + "ms");

                    if (cantArch2 > cantArch) tAcumulado = 0;

                    cantArch = cantArch2;
                    tAcumulado += tIntervaloMs;
                    System.Threading.Thread.Sleep(tIntervaloMs);
                }
                videoFileName = DateTime.Now.ToString("yyMMddHHmmss") + ".mp4";
                string fileNameWE = Path.GetFileNameWithoutExtension(videoFileName);
                string root = Path.Combine(backupPath, fileNameWE);
                Directory.CreateDirectory(root);
                string calibratedPhotos = Path.Combine(root, "calibrated");
                Directory.CreateDirectory(calibratedPhotos);
                string originalPhotos = Path.Combine(root, "original");
                Directory.CreateDirectory(originalPhotos);
                Log("Fetching photos from folder");
                string[] fileNames = Directory.GetFiles(calibrated, "*.jpg");
                if (fileNames.Length == 0)
                {
                    processing = false;
                    client.SendData("photos|" + "error", "photomotionListener");
                    Log("No photos in folder");
                    return;
                }
                else if(fileNames.Length < minPhotos)
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
                    MessageBox.Show("Photo has an incorrect file name", "Error", MessageBoxButtons.OK);
                    client.SendData("photos|" + "error", "photomotionListener");
                    processing = false;
                    return;
                }



                Log("Backing up calibrated photos");
                try
                {
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        File.Copy(fileNames[i], Path.Combine(calibratedPhotos, Path.GetFileName(fileNames[i])));
                    }
                }
                catch (Exception e)
                {
                    Log(e.Message);
                    client.SendData("photos|error", "photomotionListener");
                }
                Log("Backing up original photos");
                try
                {
                    string[] cd = Directory.GetFiles(originalPhotosPath);
                    for (int i = 0; i < cd.Length; i++)
                    {
                        File.Copy(cd[i], Path.Combine(originalPhotos, Path.GetFileName(fileNames[i])));
                        File.Delete(cd[i]);
                    }
                }
                catch (Exception e)
                {
                    Log(e.Message);
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
                                bg.Dispose();
                            }
                        }
                        images.Add(final);
                        Log("Photo: " + fileNames[i] + " " + final.Width+"x"+final.Height);
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                    }
                }
                
                
                string thumbsFolder = "";
                if (generateThumbs)
                {
                    thumbsFolder = Path.Combine(backupPath, fileNameWE, "thumbs");
                    Directory.CreateDirectory(thumbsFolder);
                    
                    Log("Generating thumbnails");
                    int stride = images.Count / 3;
                    for (int i = 0; i < 3; i++)
                    {
                        int j = i * stride;
                        //images[j % images.Count].Save(Path.Combine(thumbsFolder, i + ".jpg"), ImageFormat.Jpeg);
                        Bitmap bit = new Bitmap(images[j%images.Count], 256, (int)(256*((float)outputHeight/ (float)outputWidth)));
                        bit.Save(Path.Combine(thumbsFolder, i + ".jpg"), ImageFormat.Jpeg);
                        bit.Dispose();
                    }
                    
                }
                if (background != -1)
                {
                    string chromaFolder = Path.Combine(backupPath, fileNameWE, "chroma");
                    
                    try
                    {
                        Directory.CreateDirectory(chromaFolder);
                        Log("Backing up chroma photos");
                        File.WriteAllText(Path.Combine(chromaFolder, "background.txt"), background.ToString());
                        for (int i = 0; i < images.Count; i++)
                        {
                            images[i].Save(Path.Combine(chromaFolder, i + ".jpg"), ImageFormat.Jpeg);
                        }

                        client.SendData("photos|ok|"+videoMode+"|"+fps+"|"+Path.Combine(absoluteBackupPath, fileNameWE, "chroma"), "photomotionListener");
                    }
                    catch (Exception e)
                    {
                        Log(e.Message);
                        client.SendData("photos|error", "photomotionListener");
                    }
                }
                else
                {
                    client.SendData("photos|ok|" + videoMode + "|" + fps + "|" +Path.Combine(absoluteBackupPath, fileNameWE, "calibrated"), "photomotionListener");
                }


                images2Process = images.ToArray();
            });
        }
        Bitmap[] images2Process;
        string videoFileName;


        private void Encode2H264(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.CreateNoWindow = true;
            processInfo.FileName = "ffmpeg.exe";
            processInfo.Arguments = "-i temp.mp4 -c:v libx264 -r 25 \"" + Path.Combine(outputPath, fileName) + "\"";
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = Process.Start(processInfo);
            process.WaitForExit();
            Log("Video reencoded and renamed to " + fileName);
        }

        List<Bitmap> videoFrames = new List<Bitmap>(); 
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
                writer.Open(fileName, outputWidth, outputHeight, (int)fps, VideoCodec.MPEG4, 5000000);
                if (intro != null)
                {
                    for (int i = 0; i < introOutroFrames; i++)
                    {
                        Bitmap b = new Bitmap(intro, intro.Width, intro.Height);
                        writer.WriteVideoFrame(b);
                        videoFrames.Add(b);
                    }
                }
                for (int k = 0; k < videoLoops; k++)
                {
                    if (videoMode != "pingpong" || videoMode == "pingpong" && k % 2 == 0)
                    {
                        for (int i = 0; i < images.Length; i++)
                        {
                            Bitmap b = new Bitmap(images[i], outputWidth, outputHeight);
                            writer.WriteVideoFrame(b);
                            videoFrames.Add(b);
                        }
                    }
                    else
                    {
                        for (int i = images.Length - 1; i >= 0; i--)
                        {
                            Bitmap b = new Bitmap(images[i], outputWidth, outputHeight);
                            writer.WriteVideoFrame(b);
                            videoFrames.Add(b);
                        }
                    }
                }
                if (outro != null)
                {
                    for (int i = 0; i < introOutroFrames; i++)
                    {
                        Bitmap b = new Bitmap(outro, outro.Width, outro.Height);
                        writer.WriteVideoFrame(b);
                        videoFrames.Add(b);
                    }
                }
                writer.Close();
                Log("Video " + fileName + " processed");
                for (int i = 0; i < videoFrames.Count; i++)
                {
                    videoFrames[i].Dispose();
                }
                videoFrames.Clear();
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
