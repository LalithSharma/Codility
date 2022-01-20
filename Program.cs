using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book_store
{
    static partial class Program
    {
        private static ListView myLisstview;
        private static TextBox searchText;
        private static Form myForm;
        private static List<Book_dic> dinary;
        private static bool selected;

        private static void populateData()
        {
            DataSet dsbooks = new DataSet();
            dsbooks.ReadXml(new System.IO.StringReader(Properties.Resources.XMLFile1));

            var booklist = dsbooks.Tables[0].AsEnumerable()
                .Select(DataRow => new Book_dic
                {
                    BookID = DataRow.Field<string>("id"),
                    Author = DataRow.Field<string>("author"),
                    Title = DataRow.Field<string>("title"),
                    Genre = DataRow.Field<string>("genre"),
                    Price = DataRow.Field<string>("price"),
                    Published_date = DataRow.Field<string>("publish_date"),
                    Description = DataRow.Field<string>("description"),

                }).ToList();

            dinary = booklist.ToList();

            foreach (Book_dic n in dinary)
            {
                myLisstview.Items.Add(new ListViewItem(new[] { n.BookID, n.Author, n.Title, n.Genre, n.Price, n.Published_date, n.Description }));
            }
        }

        private static void searchData(string searchFrom)
        {
            myLisstview.Items.Clear();
            foreach(Book_dic n in dinary)
            {
                if(n.BookID.ToLower().Contains(searchFrom.ToLower()) || n.Author.ToLower().Contains(searchFrom.ToLower()) || n.Title.ToLower().Contains(searchFrom.ToLower()) || n.Genre.ToLower().Contains(searchFrom.ToLower()) || n.Price.ToLower().Contains(searchFrom.ToLower()) || n.Description.ToLower().Contains(searchFrom.ToLower()))
                {
                    myLisstview.Items.Add(new ListViewItem(new[] { n.BookID, n.Author, n.Title, n.Genre, n.Price, n.Description }));
                }
            }
        }

        private static void setupEventHandler()
        {
            myLisstview.ItemSelectionChanged += myLisstview_ItemSelectionChanged;
            searchText.TextChanged += searchText_TextChanged;
        }

        static void searchText_TextChanged(object sender, EventArgs e)
       {
            searchData(searchText.Text);
        }

        static void myLisstview_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if(MessageBox.Show("Genre :" + myLisstview.SelectedItems[0].SubItems[3].Text + "\n" + "Title :" + myLisstview.SelectedItems[0].SubItems[2].Text + "\n" +
                    "Author :" + myLisstview.SelectedItems[0].SubItems[1].Text + "\n", "Do you like to borrow", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    myLisstview.SelectedItems[0].Remove();
                }
                else
                {

                }
                MessageBox.Show("success");
                selected = true;
            }
            else
            {
                selected = false;
            }
        }

        private static void createform()
        {
            myForm = new Form
            {
                AutoScaleDimensions = new System.Drawing.SizeF(1024, 1024),
                AutoScaleMode = AutoScaleMode.Font,
                Text = "Book Store: Codility",
                ClientSize = new System.Drawing.Size(920, 685),
                BackColor = Color.Salmon,
            };
            createComponents();
            populateData();
            setupEventHandler();
            Application.EnableVisualStyles();
            Application.Run(myForm);
        }

        public static void Main()
        {
            createform();
        }

    }

    static partial class Program
    {
        public static void createComponents()
        {
            Label searchLabel = new Label
            {
                Location = new System.Drawing.Point(57, 120),
                Size = new System.Drawing.Size(138, 19),
                Font = new System.Drawing.Font("segeo UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel),
                Text = "Browser your book here: "
            };
            searchText = new TextBox
            {
                Location = new System.Drawing.Point(246, 120),
                Size = new System.Drawing.Size(613, 23),
                TabIndex = 0,
            };

            myLisstview = new ListView { Location = new System.Drawing.Point(57,165),Size = new System.Drawing.Size(800,400),View = View.Details, FullRowSelect = true,Alignment= ListViewAlignment.SnapToGrid};

            myLisstview.Columns.Add("Book ID", 80);
            myLisstview.Columns.Add("Author", 200);
            myLisstview.Columns.Add("Title", 150);
            myLisstview.Columns.Add("Genre", 80);
            myLisstview.Columns.Add("Price", 50);
            myLisstview.Columns.Add("Published_date", 80);
            myLisstview.Columns.Add("Description", 200);

            myForm.Controls.Add(myLisstview);
            myForm.Controls.Add(searchText);
            myForm.Controls.Add(searchLabel);
        }
    }
}

