using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pokemon_Shuffle_Secure_Value_Fix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string savegame
        {
            get { return savegamename1.Text; }
        }
        public string savegame2
        {
            get { return savegamename2.Text; }
        }
        
        private void BLKDTH_get_data_good()
        {
            OpenFileDialog openFD = new OpenFileDialog();
            //openFD.InitialDirectory = "c:\\";
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                savegamename2.Text = openFD.FileName;
            }
        }
        private void BLKDTH_get_data_corrupt()
        {
            OpenFileDialog openFD = new OpenFileDialog();
            //openFD.InitialDirectory = "c:\\";
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                savegamename1.Text = openFD.FileName;
            }
        }   

        private void BLKDTH_set_data()
        {
            FileStream savegame_fs = new FileStream(savegame2, FileMode.Open);
            BinaryReader savegame_br = new BinaryReader(savegame_fs);
            long length = savegame_fs.Length;
            savegame_br.BaseStream.Position = 0x2C;
            byte[] goodbytes = savegame_br.ReadBytes(8);
            
            System.IO.FileStream update_save_open = null;
            System.IO.BinaryWriter update_save_write = null;
            update_save_open = new System.IO.FileStream(savegame, System.IO.FileMode.OpenOrCreate);
            update_save_write = new System.IO.BinaryWriter(update_save_open);
            update_save_open.Position = Convert.ToInt64("2C", 16);
            update_save_write.Write(goodbytes);
            update_save_open.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLKDTH_get_data_corrupt();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BLKDTH_get_data_good();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BLKDTH_set_data();
            MessageBox.Show("Save should work now");
        }
    }
}
