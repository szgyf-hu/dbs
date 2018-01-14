using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace sqlite_console
{
    class Program
    {

        static void Main(string[] args)
        {
            /*
             #0: https://www.youtube.com/watch?v=APVit-pynwQ             
             */

            //SQLiteConnection.CreateFile("test.sqlite3"); nem szükséges!
            using (SQLiteConnection c = new SQLiteConnection("Data Source=test.sqlite3;Version=3;"))
            {
                
                c.Open();

                // na okés, de honnan tudod hogy az adatbázis most jött létre, vagy már volt?
                // lekérdezzük az adatbázis információs adatbázisából hogy létezik-e a tábláink egyike:

                using (SQLiteCommand sc = c.CreateCommand())
                {
                    sc.CommandText="SELECT count(*) FROM sqlite_master WHERE type='table' AND name='enkicsitablam'";

                    if ((long)sc.ExecuteScalar() <= 0)
                    {
                        // nincs még ilyen tábla ezért létre kell hozni másrészt ha valamilyen adatokat inicializálni akarunk akkor it kell megtenni

                        Console.WriteLine("nincs");

                        sc.CommandText = "CREATE TABLE IF NOT EXISTS enkicsitablam (id INTEGER PRIMARY KEY AUTOINCREMENT, name nvarchar(200))";
                        sc.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("van");
                    }

                    sc.CommandText = "INSERT INTO enkicsitablam (name) VALUES (@param1)";
                    sc.Parameters.Add(new SQLiteParameter("@param1", "kukucs"));
                    sc.ExecuteNonQuery();

                    sc.CommandText = "INSERT INTO enkicsitablam (name) VALUES (\"a\"), (\"b\"), (\"c\")";
                    sc.ExecuteNonQuery();

                    sc.CommandText = "DELETE FROM enkicsitablam WHERE name = \"b\"";
                    sc.ExecuteNonQuery();

                    sc.CommandText = "SELECT * FROM enkicsitablam";
                    SQLiteDataReader sdr = sc.ExecuteReader();

                    while (sdr.Read())
                    {
                        Console.WriteLine(sdr["id"] + "-" + sdr["name"]);
                    }
                }              

            }

            Console.ReadLine();
        }
    }
}
