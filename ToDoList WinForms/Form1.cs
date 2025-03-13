using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ToDoList_WinForms
{

    public partial class Form1 : Form
    {
        private Dictionary<int, string> originalItems;
        private Dictionary<int, string> originalItemsCats;// Słownik do przechowywania oryginalnych treści
        bool changeTheme = false;
        bool changeVisitors = true;
        public Form1()
        {
            InitializeComponent();
            originalItems = new Dictionary<int, string>(); // Inicjalizacja słownika
            originalItemsCats = new Dictionary<int, string>();
            ApplyDarkTheme();
            CatsHide();
        }
        void CatsShow()
        {
            label7.Show();
            label8.Show();
            checkedListBox2.Show();
        }
        void CatsHide()
        {
            label7.Hide();
            label8.Hide();
            checkedListBox2.Hide();
        }

        void TextClear()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            richTextBox1.Clear();
        }

        void InsertInfo(string category)
        {
            if (Regex.IsMatch(textBox2.Text, @"^[a-zA-Z]+$") == true && Regex.IsMatch(textBox4.Text, @"^[a-zA-Z]+$") == true && (Regex.IsMatch(textBox5.Text, @"^[0-9]+$") == true && textBox5.TextLength == 9))
            {
                if (Int32.TryParse(textBox3.Text, out int age) && age > 0 && age < 15 && textBox2.Text != null && textBox4.Text != null && textBox5.Text != null && richTextBox1.Text != null)
                {
                  
                    if (category == "cat")
                    {
                        Cat cat = new Cat(textBox2.Text, age, textBox4.Text, textBox5.Text, richTextBox1.Text);
                        string newItemCats = cat.ShowAnimalInfo();
                        checkedListBox2.Items.Add(newItemCats);
                        originalItemsCats.Add(checkedListBox2.Items.Count - 1, newItemCats); // Zapisanie oryginalnego tekstu
                        Refresh();
                        TextClear();
                    }
                    else if (category == "dog")
                    {
                        Dog dog = new Dog(textBox2.Text, age, textBox4.Text, textBox5.Text, richTextBox1.Text);
                        string newItem = dog.ShowAnimalInfo();
                        checkedListBox1.Items.Add(newItem);
                        originalItems.Add(checkedListBox1.Items.Count - 1, newItem); // Zapisanie oryginalnego tekstu
                        Refresh();
                        TextClear();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid number or fill all text!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid name!");
            }
        }

        void Refresh(string category, CheckedListBox checkedListBox, Dictionary<int, string> array)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i))
                {
                    if (!checkedListBox.Items[i].ToString().Contains("✔️")) // Zapobiega wielokrotnemu dodawaniu
                    {
                        checkedListBox.Items[i] = array[i] + " ✔️ " + DateTime.Now;
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (array.ContainsKey(i))
                    {
                        checkedListBox.Items[i] = array[i]; // Przywracanie oryginalnego tekstu
                    }
                }
            }
        }

        void DeleteItem(string category, CheckedListBox checkedListBox, Dictionary<int, string> array)
        {
            string deleteIndexString = textBox1.Text;
            if (Int32.TryParse(deleteIndexString, out int index))
            {
                if (index > 0 && index < (checkedListBox.Items.Count + 1))
                {
                    checkedListBox.Items.RemoveAt(index - 1);
                    array.Remove(index - 1);
                    textBox1.Clear();
                }
                else { MessageBox.Show("Please enter a valid number!"); }
            }
            else
            {
                MessageBox.Show("Please enter a valid number!");
            }
        }

        void Exit()
        {
            DialogResult dr = MessageBox.Show("Have you saved your files?", "Warning!", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    Application.Exit();
                    break;
                case DialogResult.No:
                    if (changeVisitors == true)
                    {
                        Save("dogs", checkedListBox1);
                        if (checkedListBox2 != null) { Save("cats", checkedListBox2); }
                    }
                    else
                    {
                        Save("cats", checkedListBox2);
                        if (checkedListBox2 != null) { Save("dogs", checkedListBox1); }
                    }
                    break;
            }
        }
        void Save(string category, CheckedListBox checklistbox)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
                Title = "Zapisz plik"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                List<string> lines = new List<string>();

                foreach (var item in checklistbox.Items)
                {
                    // Sprawdź, czy element jest zaznaczony
                    bool isChecked = checklistbox.GetItemChecked(checklistbox.Items.IndexOf(item));

                    // Jeśli element zawiera znak "✔️", pomin go w zapisie
                    if (!item.ToString().Contains("✔️"))
                    {
                        lines.Add($"{item}");  // Zapisz zadanie i jego status
                    }
                    else
                    {
                        string tempPath = "C:\\Users\\Sebastian\\Desktop\\Temp\\Temp.txt";
                        using (StreamWriter sw = new StreamWriter(tempPath, true))
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }
                }

                File.WriteAllLines(filePath, lines);
            }
        }

        void Open(string category, CheckedListBox checkedListBox, Dictionary<int, string> array)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
                Title = "Wybierz plik do otwarcia"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath);

                checkedListBox.Items.Clear();
                array.Clear();

                for (int i = 0; i < lines.Length; i++)
                {
                    checkedListBox.Items.Add(lines[i]);
                    array[i] = lines[i]; // Aktualizacja słownika
                }
            }
        }
        void ChangeTheme()
        {
            if (changeTheme == true)
            {
                ApplyDarkTheme();
            }
            else
            {
                ApplyDefaultTheme();
            }

            changeTheme = !changeTheme;
        }

        private void ApplyDefaultTheme()
        {
            this.BackColor = System.Drawing.Color.White; // Tło formularza (domyślnie jasne)

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.BackColor = System.Drawing.Color.White;
                    control.ForeColor = System.Drawing.Color.Black;
                }
                else if (control is RichTextBox)
                {
                    control.BackColor = System.Drawing.Color.White;
                    control.ForeColor = System.Drawing.Color.Black;
                }
                else if (control is CheckBox)
                {
                    control.ForeColor = System.Drawing.Color.Black;
                    control.BackColor = System.Drawing.Color.FromArgb(31, 25, 24);
                }
                else if (control is Button)
                {
                    control.BackColor = System.Drawing.Color.LightGray;
                    control.ForeColor = System.Drawing.Color.Black;
                }
                else if (control is Label)
                {
                    control.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void ApplyDarkTheme()
        {
            // Tło formularza
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);

            // Ustawienia kontrolek
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
                    control.ForeColor = System.Drawing.Color.White;
                }
                else if (control is RichTextBox)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
                    control.ForeColor = System.Drawing.Color.White;

                    RichTextBox richTextBox = (RichTextBox)control;
                    richTextBox.SelectionColor = System.Drawing.Color.White;
                }
                else if (control is CheckBox)
                {
                    control.ForeColor = System.Drawing.Color.White;
                }
                else if (control is Button)
                {
                    control.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
                    control.ForeColor = System.Drawing.Color.White;
                }
                else if (control is Label)
                {
                    control.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        private void ChangeObject(object sender, EventArgs e)
        {
            changeVisitors = !changeVisitors;
            if (changeVisitors == true)
            {
                CatsHide();
                button7.Text = "Change to Cats";
            }
            else
            {
                CatsShow();
                button7.Text = "Change to Dogs";
            }
        }

        private void Apply_Btn(object sender, EventArgs e)
        {
            if (changeVisitors == true)
            {
                InsertInfo("dog");
            }
            else
            {
                InsertInfo("cat");
            }
        }

        private void Remove_btn(object sender, EventArgs e)
        {
            if (changeVisitors == true)
            {
                DeleteItem("cats", checkedListBox1, originalItems);
            }
            else
            {
                DeleteItem("cats", checkedListBox2, originalItemsCats);
            }
        }

        private void Save_btn(object sender, EventArgs e)
        {
            if (changeVisitors == true)
            {
                Save("dogs", checkedListBox1);
            }
            else
            {
                Save("cats", checkedListBox2);
            }
        }

        private void Load_btn(object sender, EventArgs e)
        {
            if (changeVisitors == true)
            {
                Open("dogs", checkedListBox1, originalItems);
            }
            else
            {
                Open("dogs", checkedListBox2, originalItemsCats);
            }
        }

        private void Exit_btn(object sender, EventArgs e)
        {
            Exit();
        }

        private void ChangeTheme_btn(object sender, EventArgs e)
        {
            ChangeTheme();
        }

        private void Refresh(Object sender, EventArgs e)
        {
            Refresh("dogs", checkedListBox1, originalItems);
            Refresh("cats", checkedListBox2, originalItemsCats);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Refresh("dogs", checkedListBox1, originalItems);
            Refresh("cats", checkedListBox2, originalItemsCats);
        }
    }
}


