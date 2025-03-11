using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDoList_WinForms
{
    public partial class Form1 : Form
    {
        private Dictionary<int, string> originalItems; // Słownik do przechowywania oryginalnych treści

        public Form1()
        {
            InitializeComponent();
            originalItems = new Dictionary<int, string>(); // Inicjalizacja słownika
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            string newItem = richTextBox1.Text;
            checkedListBox1.Items.Add(newItem);
            originalItems[checkedListBox1.Items.Count - 1] = newItem; // Zapisanie oryginalnego tekstu
            Refresh();
        }

        void Refresh()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (!checkedListBox1.Items[i].ToString().Contains("✔️")) // Zapobiega wielokrotnemu dodawaniu
                    {
                        checkedListBox1.Items[i] = originalItems[i] + " ✔️ " + DateTime.Now;
                    }
                }
                else
                {
                    checkedListBox1.Items[i] = originalItems[i]; // Przywracanie oryginalnego tekstu
                }
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
