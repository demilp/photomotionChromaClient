using Bypass.SimpleJSON;
using demilp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PhotomotionChromaClient
{
    public partial class ChromaCalibration : Form
    {
        public static ChromaCalibration instance;
        ChromaKey chroma;
        OpenFileDialog openFileDialog;
        public ChromaCalibration()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF";
            openFileDialog.FileOk += OpenFileDialog_FileOk;
            instance = this;
            chroma = new ChromaKey();
            chroma.Hue = MainForm.instance.chroma.Hue;
            chroma.HueTolerance = MainForm.instance.chroma.HueTolerance;
            chroma.Saturation = MainForm.instance.chroma.Saturation;
            chroma.SaturationTolerance = MainForm.instance.chroma.SaturationTolerance;
            chroma.Value = MainForm.instance.chroma.Value;
            chroma.ValueTolerance = MainForm.instance.chroma.ValueTolerance;
            numericUpDownHue.Value = (decimal)chroma.Hue;
            numericUpDownHueTolerance.Value = (decimal)chroma.HueTolerance;
            numericUpDownSaturation.Value = (decimal)chroma.Saturation;
            numericUpDownSaturationTolerance.Value = (decimal)chroma.SaturationTolerance;
            numericUpDownValue.Value = (decimal)chroma.Value;
            numericUpDownValueTolerance.Value = (decimal)chroma.ValueTolerance;
            trackBarHue.Value = (int)chroma.Hue;
            trackBarHueTolerance.Value = (int)chroma.HueTolerance;
            trackBarSaturation.Value = (int)chroma.Saturation;
            trackBarSaturationTolerance.Value = (int)chroma.SaturationTolerance;
            trackBarValue.Value = (int)chroma.Value;
            trackBarValueTolerance.Value = (int)chroma.ValueTolerance;

        }
        JSONNode config;

        Bitmap originalBitmap;


        private void ChromaCalibration_Load(object sender, EventArgs e)
        {
            //numericUpDownCamera.Maximum = MainForm.instance.cameraManager.GetCameras().Count();
            if (!File.Exists("chromaConfig.json"))
            {
                //File.Create("chromaConfig.json").Dispose();
                File.WriteAllText("chromaConfig.json", "{}");
            }
            config = JSON.Parse(File.ReadAllText("chromaConfig.json"));
            if (config["General"] != null)
            {
                numericUpDownHue.Value = (decimal)config["General"]["Hue"].AsFloat;
                numericUpDownHueTolerance.Value = (decimal)config["General"]["HueTolerance"].AsFloat;
                numericUpDownSaturation.Value = (decimal)config["General"]["Saturation"].AsFloat;
                numericUpDownSaturationTolerance.Value = (decimal)config["General"]["SaturationTolerance"].AsFloat;
                numericUpDownValue.Value = (decimal)config["General"]["Value"].AsFloat;
                numericUpDownValueTolerance.Value = (decimal)config["General"]["ValueTolerance"].AsFloat;
            }
            else
            {
                SaveConfiguration("General");
            }
            Refresh();
        }
        void UpdatePicture()
        {
            if (originalBitmap == null)
                return;
            Bitmap b = new Bitmap(originalBitmap);
            chroma.Chroma(b);
            pictureBox.Image = b;
        }
        void ShowConfiguration(string id)
        {
            if (config[id] != null)
            {
                numericUpDownHue.Value = (decimal)config[id]["Hue"].AsFloat;
                numericUpDownHueTolerance.Value = (decimal)config[id]["HueTolerance"].AsFloat;
                numericUpDownSaturation.Value = (decimal)config[id]["Saturation"].AsFloat;
                numericUpDownSaturationTolerance.Value = (decimal)config[id]["SaturationTolerance"].AsFloat;
                numericUpDownValue.Value = (decimal)config[id]["Value"].AsFloat;
                numericUpDownValueTolerance.Value = (decimal)config[id]["ValueTolerance"].AsFloat;
            }
        }
        private void ChromaCalibration_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void numericUpDownValueChanged(object sender, EventArgs e)
        {
            if (ch)
                return;
            ch = true;
            chroma.Hue = (float)numericUpDownHue.Value;
            chroma.HueTolerance = (float)numericUpDownHueTolerance.Value;
            chroma.Saturation = (float)numericUpDownSaturation.Value;
            chroma.SaturationTolerance = (float)numericUpDownSaturationTolerance.Value;
            chroma.Value = (float)numericUpDownValue.Value;
            chroma.ValueTolerance = (float)numericUpDownValueTolerance.Value;

            trackBarHue.Value = (int)numericUpDownHue.Value;
            trackBarHueTolerance.Value = (int)numericUpDownHueTolerance.Value;
            trackBarSaturation.Value = (int)numericUpDownSaturation.Value;
            trackBarSaturationTolerance.Value = (int)numericUpDownSaturationTolerance.Value;
            trackBarValue.Value = (int)numericUpDownValue.Value;
            trackBarValueTolerance.Value = (int)numericUpDownValueTolerance.Value;

            ch = false;
            UpdatePicture();
        }

        private void buttonSaveGeneral_Click(object sender, EventArgs e)
        {
            SaveConfiguration("General");
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            originalBitmap = (Bitmap)Image.FromFile(openFileDialog.FileName);
            labelImagePath.Text = openFileDialog.FileName;
            UpdatePicture();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ShowConfiguration(textBoxCameraId.Text);
        }
        void SaveConfiguration(string id)
        {
            config[id]["Hue"] = numericUpDownHue.Value.ToString();
            config[id]["HueTolerance"] = numericUpDownHueTolerance.Value.ToString();
            config[id]["Saturation"] = numericUpDownSaturation.Value.ToString();
            config[id]["SaturationTolerance"] = numericUpDownSaturationTolerance.Value.ToString();
            config[id]["Value"] = numericUpDownValue.Value.ToString();
            config[id]["ValueTolerance"] = numericUpDownValueTolerance.Value.ToString();
            File.WriteAllText("chromaConfig.json", config.ToString());
            MainForm.instance.chromaConfig = config;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxCameraId.Text != "")
                SaveConfiguration(textBoxCameraId.Text);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (ch)
                return;
            ch = true;
            chroma.Hue = (float)numericUpDownHue.Value;
            chroma.HueTolerance = (float)numericUpDownHueTolerance.Value;
            chroma.Saturation = (float)numericUpDownSaturation.Value;
            chroma.SaturationTolerance = (float)numericUpDownSaturationTolerance.Value;
            chroma.Value = (float)numericUpDownValue.Value;
            chroma.ValueTolerance = (float)numericUpDownValueTolerance.Value;

            numericUpDownHue.Value = trackBarHue.Value;
            numericUpDownHueTolerance.Value = trackBarHueTolerance.Value;
            numericUpDownSaturation.Value = trackBarSaturation.Value;
            numericUpDownSaturationTolerance.Value = trackBarSaturationTolerance.Value;
            numericUpDownValue.Value = trackBarValue.Value;
            numericUpDownValueTolerance.Value = trackBarValueTolerance.Value;


            ch = false;
            UpdatePicture();
        }
        bool ch = false;
        Color pixelColor;
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox.Image == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                pixelColor = GetColorAt(e.Location);
                panelColor.BackColor = pixelColor;

                double[] hsv1 = new double[3];
                ColorToHSV(pixelColor, hsv1);
                chroma.Hue = (float)hsv1[0];
                chroma.Saturation = (float)hsv1[1];
                chroma.Value = (float)hsv1[2];
                ch = true;
                numericUpDownHue.Value = (decimal)hsv1[0];
                numericUpDownSaturation.Value = (decimal)hsv1[1];
                numericUpDownValue.Value = (decimal)hsv1[2];
                trackBarHue.Value = (int)hsv1[0];
                trackBarSaturation.Value = (int)hsv1[1];
                trackBarValue.Value = (int)hsv1[2];
                ch = false;
                UpdatePicture();
            }


        }

        private Color GetColorAt(Point point)
        {
            float w = ((float)point.X / pictureBox.Width) * pictureBox.Image.Width;
            float h = ((float)point.Y / pictureBox.Height) * pictureBox.Image.Height;
            return originalBitmap.GetPixel((int)w, (int)h);
        }

        public void ColorToHSV(Color color, double[] hsv)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hsv[0] = color.GetHue();
            hsv[1] = ((max == 0) ? 0 : 1d - (1d * min / max)) * 100;
            hsv[2] = (max / 255d) * 100;
        }
    }
}