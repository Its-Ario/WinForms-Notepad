using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
    public partial class mainForm : Form
    {
        bool saved = true;
        string path;

        string selected;
        public mainForm()
        {
            InitializeComponent();

            this.saveFileDialog.Filter = this.openFileDialog.Filter = "Text Files | *.txt";        
        }

        private void save()
        {
            if (path == null)
            {
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = saveFileDialog.FileName;
                }
                else return;
            }
            File.WriteAllText(path, textBox.Text);

            this.Text = "Notepad";
            saved = true;
        }

        private void clearText()
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Do You Want To Save Your Current File?", "Close Dialog", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    save();
                    return;
                }
            }

            textBox.Clear();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saved) return;
            save();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                File.WriteAllText(path, textBox.Text);
                this.Text = "Notepad";
                saved = true;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.Text = "Notepad *";
            saved = false;
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                clearText();
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Do You Want To Save Your Current File?", "Close Dialog", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) save();
                else saved = true;
            }

            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected = textBox.SelectedText;
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            string txt = textBox.Text;
            int start = textBox.SelectionStart;

            string beforeText = txt.Remove(start+1, txt.Length-start-1);
            string endText = txt.Remove(0, start);

            textBox.Text = beforeText + selected + endText;

            textBox.SelectionStart = start+1;
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            selected = textBox.SelectedText;
            string txt = textBox.Text;

            int start = textBox.SelectionStart;
            int count = textBox.SelectionLength;

            textBox.Text = txt.Remove(start, count);
        }
    }
}