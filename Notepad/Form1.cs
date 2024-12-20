﻿using System;
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

            this.saveFileDialog.Filter = this.openFileDialog.Filter = "Text Files | *.txt|Other Files (*.*)|*.*";
            this.saveFileDialog.FileName = "untitled.txt";

            this.fontDialog.MaxSize = 1024;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                DialogResult result = MessageBox.Show("Do You Want To Save Your Current File?", "Close Dialog", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    save();
                    return;
                }
                else if (result == DialogResult.Cancel) return;
            }

            textBox.Clear();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            clearText();
            path = null;
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
            if (textBox.Text == "")
            {
                this.Text = "Notepad";
                saved = true;
            }
            else
            {
                this.Text = "Notepad *";
                saved = false;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                clearText();
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
            this.Text = "Notepad";
            saved = true;
        }

        private void exitButton_Closing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Do You Want To Save Your Current File?", "Close Dialog", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes) save();
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else saved = true;
            }

            Application.Exit();
        }

        private void copycopyBtn_Click(object sender, EventArgs e)
        {
            selected = textBox.SelectedText;
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            string txt = textBox.Text;
            int start = textBox.SelectionStart;

            string beforeText = txt.Remove(start, txt.Length-start);
            string endText = txt.Remove(0, start);

            textBox.Text = beforeText + selected + endText;

            textBox.SelectionStart = start+selected.Length;
        }

        private void cutButton_Click(object sender, EventArgs e)
        {
            selected = textBox.SelectedText;
            string txt = textBox.Text;

            int start = textBox.SelectionStart;
            int count = textBox.SelectionLength;

            textBox.Text = txt.Remove(start, count);
        }

        private void colorBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            Color color = colorDialog.Color;
            selected = textBox.SelectedText;

            if (selected.Length == 0) textBox.ForeColor = color;
            else textBox.SelectionColor = color;
        }

        private void fontBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = fontDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            Font font = fontDialog.Font;
            selected = textBox.SelectedText;

            if (selected.Length == 0) textBox.Font = font;
            else textBox.SelectionFont = font;
        }

        private void allSelectBtn_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("a Basic C# Notepad\n© Copyright Mamanet LTD 2024-", "About");
        }

        private void soonBtn_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ctrl = (ToolStripMenuItem)sender;
            MessageBox.Show("Feature Coming Soon...", ctrl.Text.Trim('&'));
        }

        private void redoBtn_Click(object sender, EventArgs e)
        {
            textBox.Redo();
        }

        private void undoBtn_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("- How Does The Font & Color Button Work?\nIf anything is selected, it applies the change to that part, Otherwise applied it to the whole text", "FAQ");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Do You Want To Save Your Current File?", "Close Dialog", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes) save();
                else if (result == DialogResult.Cancel) return;
                else saved = true;
            }

            Application.Exit();
        }
    }
}