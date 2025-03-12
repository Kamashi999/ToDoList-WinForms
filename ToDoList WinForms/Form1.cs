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
        void InsertInfoCats()
        {
            if (Int32.TryParse(textBox3.Text, out int age) && textBox2.Text != null && textBox4.Text != null && textBox5.Text != null && richTextBox1.Text != null)
            {
                Cat cat = new Cat(textBox2.Text, age, textBox4.Text, textBox5.Text, richTextBox1.Text);
                string newItemCats = cat.ShowAnimalInfo();
                checkedListBox2.Items.Add(newItemCats);
                originalItemsCats.Add(checkedListBox2.Items.Count - 1, newItemCats); // Zapisanie oryginalnego tekstu
                Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number or fill all text!");
            }
        }
        void InsertInfoDogs()
        {
            if (Int32.TryParse(textBox3.Text, out int age) && textBox2.Text != null && textBox4.Text != null && textBox5.Text != null && richTextBox1.Text != null)
            {
                Dog dog = new Dog(textBox2.Text, age, textBox4.Text, textBox5.Text, richTextBox1.Text);
                string newItem = dog.ShowAnimalInfo();
                checkedListBox1.Items.Add(newItem);
                originalItems.Add(checkedListBox1.Items.Count - 1, newItem); // Zapisanie oryginalnego tekstu
                Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number or fill all text!");
            }
        }

        void InsertInfoDogs4()
        {
            if (Int32.TryParse(textBox3.Text, out int age) && textBox2.Text != null && textBox4.Text != null && textBox5.Text != null && richTextBox1.Text != null)
            {
                Dog dog = new Dog(textBox2.Text, age, textBox4.Text, textBox5.Text, richTextBox1.Text);
                string newItem = dog.ShowAnimalInfo();
                checkedListBox1.Items.Add(newItem);
                originalItems.Add(checkedListBox1.Items.Count - 1, newItem); // Zapisanie oryginalnego tekstu
                Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number or fill all text!");
            }
        }

        //void RefreshItems(string category, CheckedListBox checkedListBox, Dictionary<int, string> originalItems)
        //{
        //    for (int i = 0; i < checkedListBox.Items.Count; i++)
        //    {
        //        if (checkedListBox.GetItemChecked(i)) // Sprawdzamy, czy element jest zaznaczony
        //        {
        //            // Zapobiega wielokrotnemu dodawaniu znaku "✔️"
        //            if (!checkedListBox.Items[i].ToString().Contains("✔️"))
        //            {
        //                // Dodajemy ✔️ oraz datę do elementu
        //                checkedListBox.Items[i] = originalItems[i] + " ✔️ " + DateTime.Now;
        //            }
        //        }
        //        else
        //        {
        //            // Przywracamy oryginalny tekst, jeśli element nie jest zaznaczony
        //            if (originalItems.ContainsKey(i))
        //            {
        //                checkedListBox.Items[i] = originalItems[i];
        //            }
        //        }
        //    }
        //}


        //void RefreshCats()
        //{
        //    for (int i = 0; i < checkedListBox2.Items.Count; i++)
        //    {
        //        if (checkedListBox2.GetItemChecked(i))
        //        {
        //            if (!checkedListBox2.Items[i].ToString().Contains("✔️")) // Zapobiega wielokrotnemu dodawaniu
        //            {
        //                checkedListBox2.Items[i] = originalItemsCats[i] + " ✔️ " + DateTime.Now;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            if (originalItemsCats.ContainsKey(i))
        //            {
        //                checkedListBox2.Items[i] = originalItemsCats[i]; // Przywracanie oryginalnego tekstu
        //            }
        //        }
        //    }
        //}

        void RefreshDogs()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    if (!checkedListBox1.Items[i].ToString().Contains("✔️")) // Zapobiega wielokrotnemu dodawaniu
                    {
                        checkedListBox1.Items[i] = originalItems[i] + " ✔️ " + DateTime.Now;
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (originalItems.ContainsKey(i))
                    {
                        checkedListBox1.Items[i] = originalItems[i]; // Przywracanie oryginalnego tekstu
                    }
                }
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
                }
                else { MessageBox.Show("Please enter a valid number!"); }
            }
            else
            {
                MessageBox.Show("Please enter a valid number!");
            }
        }

        //void DeleteItemCats()
        //{
        //    string deleteIndexString = textBox1.Text;
        //    if (Int32.TryParse(deleteIndexString, out int index))
        //    {
        //        if (index > 0 && index < (checkedListBox2.Items.Count + 1))
        //        {
        //            checkedListBox2.Items.RemoveAt(index - 1);
        //            originalItemsCats.Remove(index - 1);
        //        }
        //        else { MessageBox.Show("Please enter a valid number!"); }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please enter a valid number!");
        //    }
        //}

        //void DeleteItemDogs()
        //{
        //    string deleteIndexString = textBox1.Text;
        //    if (Int32.TryParse(deleteIndexString, out int index))
        //    {
        //        if (index > 0 && index < (checkedListBox1.Items.Count + 1))
        //        {
        //            checkedListBox1.Items.RemoveAt(index - 1);
        //            originalItems.Remove(index - 1);
        //        }
        //        else { MessageBox.Show("Please enter a valid number!"); }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please enter a valid number!");
        //    }
        //}

        void Exit()
        {
            Application.Exit();
        }

        //void SaveCats()
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog
        //    {
        //        Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
        //        Title = "Zapisz plik"
        //    };

        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = saveFileDialog.FileName;

        //        List<string> lines = new List<string>();

        //        foreach (var item in checkedListBox2.Items)
        //        {
        //            // Sprawdź, czy element jest zaznaczony
        //            bool isChecked = checkedListBox2.GetItemChecked(checkedListBox2.Items.IndexOf(item));

        //            // Jeśli element zawiera znak "✔️", pomin go w zapisie
        //            if (!item.ToString().Contains("✔️"))
        //            {
        //                lines.Add($"{item}");  // Zapisz zadanie i jego status
        //            }
        //            else
        //            {
        //                string tempPath = "C:\\Users\\Sebastian\\Desktop\\Temp\\Temp.txt";
        //                using (StreamWriter sw = new StreamWriter(tempPath, true))
        //                {
        //                    sw.WriteLine(item.ToString());
        //                }
        //            }
        //        }

        //        File.WriteAllLines(filePath, lines);
        //    }
        //}

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

        void SaveDogs()
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

                foreach (var item in checkedListBox1.Items)
                {
                    // Sprawdź, czy element jest zaznaczony
                    bool isChecked = checkedListBox1.GetItemChecked(checkedListBox1.Items.IndexOf(item));

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

        //void OpenCats()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
        //        Title = "Wybierz plik do otwarcia"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = openFileDialog.FileName;
        //        string[] lines = File.ReadAllLines(filePath);

        //        checkedListBox2.Items.Clear();
        //        originalItemsCats.Clear();

        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            checkedListBox2.Items.Add(lines[i]);
        //            originalItemsCats[i] = lines[i]; // Aktualizacja słownika
        //        }
        //    }
        //}
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
        //void OpenDogs()
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog
        //    {
        //        Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*",
        //        Title = "Wybierz plik do otwarcia"
        //    };

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = openFileDialog.FileName;
        //        string[] lines = File.ReadAllLines(filePath);

        //        checkedListBox1.Items.Clear();
        //        originalItems.Clear();

        //        for (int i = 0; i < lines.Length; i++)
        //        {
        //            checkedListBox1.Items.Add(lines[i]);
        //            originalItems[i] = lines[i]; // Aktualizacja słownika
        //        }
        //    }
        //}

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
                InsertInfoDogs();
            }
            else
            {
                InsertInfoCats();
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
                SaveDogs();
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
            if (changeVisitors == true)
            {
                MessageBox.Show("Have you saved files?");
                Save("dogs", checkedListBox2);
                Exit();
            }
            else
            {
                MessageBox.Show("Have you saved files?");
                Save("cats", checkedListBox2);
                Exit();
            }
        }

        private void ChangeTheme_btn(object sender, EventArgs e)
        {
            ChangeTheme();
        }

        private void Refresh(Object sender, EventArgs e)
        {
            RefreshDogs();
            Refresh("cats", checkedListBox2, originalItemsCats);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            RefreshDogs();
            Refresh("cats", checkedListBox2, originalItemsCats);
        }
    }
}


