using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Yakovenko_072303_5
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db1.mdb;";
        // вариант 2
        //public  static  string  connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Workers.mdb;";
        // поле-ссылка на экземпляр класса OleDbConnection для соединения
        private OleDbConnection myConnection;
        public Form1()
        {
            InitializeComponent();
            //  создаем экземпляр класса OleDbConnection
            myConnection = new OleDbConnection(connectString);
            // открываем соединение с БД
            myConnection.Open();
        }

        private void Select1_Click(object sender, EventArgs e)
        {
            // обработчик события нажатия кнопки SELECT1 - элемент, выбранный в //listBox1, переписывается в textBox1

            string element = listBox1.SelectedItem.ToString();
            element = element.Substring(element.IndexOf(' '));   // Удалить id
            textBox1.Text = element;
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            // текст запроса
            string query = "SELECT Code, Group, Faculty, Namee, AvMark FROM Eksamens ORDER BY Code";
            // string query = "SELECT * FROM Eksamens";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command.ExecuteReader();
            // очищаем listBox1
            listBox1.Items.Clear();
            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                // выводим данные столбцов текущей строки в listBox1
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString());
            }
            // закрываем OleDbDataReader
            reader.Close();


        }

        private void Insert_Click(object sender, EventArgs e)
        {
            // Получить строку из textBox1
            string element = textBox1.Text;
            element = element.Trim();    //Удалить начальные и конечные пробелы
                                         //Выделить поля
            string[] words = element.Split(new char[] { ' ' });
            string Group = words[0];
            string Faculty = words[1];
            string Namee = words[2];
            string AvMark = words[3];



            // Сформировать запрос
            string query = "INSERT INTO Eksamens ( `Group`, Faculty, Namee, AvMark) VALUES (";
            string tmp = Group + ", ";
            query += tmp;

            tmp = "'" + Faculty + "', ";

            query += tmp;
            tmp = "'" + Namee + "', ";

            query += tmp;
            tmp = AvMark + ")";

            query += tmp;




            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // выполняем запрос к MS Access
            command.ExecuteNonQuery();
            // Отобразить новое состояние таблицы
            // текст запроса
            string query1 = "SELECT Code,  Group, Faculty, Namee, AvMark FROM Eksamens ORDER BY Code";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command1 = new OleDbCommand(query1, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command1.ExecuteReader();
            // очищаем listBox1
            listBox1.Items.Clear();
            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                // выводим данные столбцов текущей строки в listBox1
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString());
            }
            // закрываем OleDbDataReader
            reader.Close();

        }

        private void Update_Click(object sender, EventArgs e)
        {
            // Определить элемент, выделенный в списке listbox
            string element = listBox1.SelectedItem.ToString();
            string id = element.Remove(element.IndexOf(' '));   // Выделить id
                                                                // Получить строку из textBox1
            element = textBox1.Text;
            element = element.Trim();    //Удалить начальные и конечные пробелы
                                         //Выделить поля
            string[] words = element.Split(new char[] { ' ' });
            string Group = words[0];
            string Faculty = words[1];
            string Namee = words[2];
            string AvMark = words[3];

            // Сформировать запрос
            string query = "UPDATE Eksamens SET `Group` = ";
            string tmp = Group + ",";
            query += tmp;

            tmp = " Faculty = '" + Faculty + "',";

            query += tmp;
            tmp = " Namee = '" + Namee + "',";

            query += tmp;
            tmp = " AvMark = " + AvMark;

            query += tmp;
            tmp = " WHERE Code = " + id;

            query += tmp;

            Console.WriteLine(query);

            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // выполняем запрос к MS Access
            command.ExecuteNonQuery();
            // Отобразить новое состояние таблицы
            // текст запроса
            string query1 = "SELECT Code,  `Group`, Faculty, Namee, AvMark FROM Eksamens ORDER BY Code";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command1 = new OleDbCommand(query1, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command1.ExecuteReader();
            // очищаем listBox1
            listBox1.Items.Clear();
            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                // выводим данные столбцов текущей строки в listBox1
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString());
            }
            // закрываем OleDbDataReader
            reader.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            // Определить элемент, выделенный в списке listbox
            string element = listBox1.SelectedItem.ToString();
            element = element.Remove(element.IndexOf(' '));   // Выделить id
            string query = "DELETE FROM Eksamens WHERE Code = ";
            query += element;
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command = new OleDbCommand(query, myConnection);
            // выполняем запрос к MS Access
            command.ExecuteNonQuery();
            // Отобразить новое состояние таблицы
            string query1 = "SELECT Code,  Group, Faculty, Namee, AvMark FROM Eksamens ORDER BY Code";
            // создаем объект OleDbCommand для выполнения запроса к БД MS Access
            OleDbCommand command1 = new OleDbCommand(query1, myConnection);
            // получаем объект OleDbDataReader для чтения табличного результата запроса SELECT
            OleDbDataReader reader = command1.ExecuteReader();
            // очищаем listBox1
            listBox1.Items.Clear();
            // в цикле построчно читаем ответ от БД
            while (reader.Read())
            {
                // выводим данные столбцов текущей строки в listBox1
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + " " + reader[4].ToString() + " ");
            }
            // закрываем OleDbDataReader
            reader.Close();

        }
        // обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // закрываем соединение с БД
            myConnection.Close();
        }

    }
}
