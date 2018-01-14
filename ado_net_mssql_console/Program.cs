using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ado_net_mssql_console
{
    /*
        Lépések:
        #1: Kell egy MSSQL adatbázis szervert telepíteni: https://www.youtube.com/watch?v=E_zFM7mzFUg Ne felejts el az "sa"-nak jelszót adni (és ne is felejtsd el a jelszót)
        #2: Telepíts egy SQL Server Managament Studio-t (lásd előző videó)
        #3: Az MSSQL servernek engedélyezd hogy távolról TCP-vel is elérhető legyen: https://www.youtube.com/watch?v=5UkHYNwUtCo (állítsd a portot is!)
        #4: Természetesen ha saját gépeden fut az SQL szerver és a programod is akkor is lehet hozzá TCP-n kapcsolódni mert a programodból kezdeményezett TCP kapcsolat a kernelben vissza fog "kanyarodni" az SQL szerverhez
        #5: Ellenőrizd az SQL Server Managament Studio-val a TCP kapcsolat működőképességét (lásd fenti videó) amíg azzal nem megy addig ne próbálkozz kódolni mert akkor nem fogod tudni hogy a kódban rontottál valamit vagy még nem tudsz kapcsolódni
        #6: Nem ártana később az adatbázis szervert másik gépre rakni (pl egy virtuális windows-ba) Tűzfalakat el ne felejts beállítani! (gyors javítás-> teljesen kikapcsolod)
        #7: Telepítsd a NorthWind minta adatbázist, ez egy gyakorló adatabázis Microsoft tanfolyami képzésekhez, jópár módja és leleőhelye van: https://www.youtube.com/results?search_query=mssql+nortwind
        #8: Ellenőrizd az SQL Server Managament Studio-val hogy vannak-e adatok a NortWind-ben

        #9: jöhet a kódolás! .NET környezetben az "ADO.NET" egy olyan környezet (https://en.wikipedia.org/wiki/ADO.NET) aminek a segítségével "egységesen" kapcsolódhatsz mindenféle adatbázishoz kb ugyanazzal a kóddal. Ehhez nem kell telepíteni semmit, a .NET framework része
        #10 a cs fájlod elején ezt kell beírnod: "using System.Data.SqlClient;" ezzel fogod a 
        #10: Kell egy (TCP) kapcsolat az adatbázishoz "SqlConnection" 
        #11: SQLCommand-al SQL szinten tudunk adatbázishoz hozzáférni        
        */

    class Program
    {
        const String ip = "127.0.0.1"; // Az SQL adatbázist kiszolgáló szerver IP címe, ha saját gép akkor "localhost" vagy "127.0.0.1"
        const String port = ",12345"; // Az SQL adatbázist kiszolgáló szerver IP port száma, az alapértelmezett port 1433 ezt nem kell megadni, ilyenkor ide ""-t kell írni
        const String instancename = "SQLEXPRESS"; // Az SQL szerver "példány neve" alapértelmezett telepítéskor ez "SQLEXPRESS"
        const String db = "NORTHWND"; // Az alapértelmezett adatbázis amiben dolgozunk
        const String user = "sa"; // az adatbázisbe kétféle módon lehet bejelentkezni, vagy Windows felhasználó szintű azonosítással, vagy az SQL szerver oldja meg, az "sa" az alapértelmezett "System Administrator" 
        const String pass = "a1b2c3d4"; // általam, telepítéskor megadott jelszó

        static void Main(string[] args)
        {
            using (SqlConnection c = new SqlConnection(@"Data Source=" + ip + port + "\\" + instancename + ";Initial Catalog=" + db + ";Persist Security Info=True;User ID=" + user + ";Password=" + pass))
            {
                c.Open();

                // lekérdezés
                using (SqlDataReader dr = new SqlCommand("SELECT * FROM Categories;", c).ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(dr["CategoryID"] + "-" + dr["CategoryName"] + "-" + dr["Description"]);
                        }
                    }
                }

                //Insert

                new SqlCommand("INSERT INTO Categories (CategoryName, Description) VALUES ('Próba Kategória','Próba kategória leírás');", c).ExecuteNonQuery();

                // lekérdezés

                Console.WriteLine("------------------------------------------------------------");

                using (SqlDataReader dr = new SqlCommand("SELECT * FROM Categories;", c).ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(dr["CategoryID"] + "-" + dr["CategoryName"] + "-" + dr["Description"]);
                        }
                    }
                }


                // Update

                new SqlCommand(@"
                    UPDATE Categories
                    SET
                        Description = '!!!'
                    WHERE
                        CategoryName = 'Próba kategória'",
                        c).ExecuteNonQuery();

                // lekérdezés

                Console.WriteLine("------------------------------------------------------------");

                using (SqlDataReader dr = new SqlCommand("SELECT * FROM Categories;", c).ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(dr["CategoryID"] + "-" + dr["CategoryName"] + "-" + dr["Description"]);
                        }
                    }
                }

                // Delete

                new SqlCommand(@"
                    DELETE FROM Categories
                    WHERE
                        CategoryName = 'Próba kategória'",
                        c).ExecuteNonQuery();
                // lekérdezés

                Console.WriteLine("------------------------------------------------------------");

                using (SqlDataReader dr = new SqlCommand("SELECT * FROM Categories;", c).ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(dr["CategoryID"] + "-" + dr["CategoryName"] + "-" + dr["Description"]);
                        }
                    }
                }
            }
            Console.ReadLine(); // várakozás enterre
        }
    }
}
