using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*
 * Ebben a projektben inkább a "Data Source Configuration Wizard" használtam View / Other Windows / Data Sources menüpont, ott kitöltöd a kapcsolati adatokat 
 * úgy, ahogy órán gyakoroltuk, majd kijelölöd az összes táblát.
 * 
 * Figyeld meg hogy a varázslás után keletkezett egy "app.config" fájl ami az EXE mellé is oda másolódik, ezzel van megoldva hogy a kapcsolódási paraméterek konfigurálhatóak legyenek.
 * 
 * 
 * A továbbiak szinte huzigálással megoldható az egész : A kövi videó ugyan Access fájl-hoz való hozzáférést mutat meg, de ugyanez történik MSSQL szerverrel: https://www.youtube.com/watch?v=ZrrDNAyEKF4
 * 
 * lentebb a kódban látsz pár sort, ezt a varázsló kódgenerátor rakta bele és ezen kívül még több ezer c# sort mellé generált a projektnek hogy neked ne kelljen a technikai részletekkel foglalkozni
 * 
 * Erre mehetsz tovább például: https://msdn.microsoft.com/hu-hu/library/4esb49b4.aspx
 * 
 * Amikor tovább akarsz lépni és SQL scripteket végrehajtani, akkor a "tableadapter query configuration wizard" lesz a barátod https://www.youtube.com/watch?v=dBrAvAkCZR4
 * 
 * példának lásd lentebb a kódban
 * 
 */

namespace ado_net_mssql_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void categoriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.categoriesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.nORTHWNDDataSet);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nORTHWNDDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.nORTHWNDDataSet.Categories);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * ez is jó
             * this.nORTHWNDDataSet.Categories.AddCategoriesRow("Minta ketegória", "Minta ketegória leírás", null); // null -> nem adunk hozzá képet
             * this.tableAdapterManager.UpdateAll(this.nORTHWNDDataSet); // feltöltjük adatbázisba a változást
             * 
             * De varázslóval létrehoztam egy saját insertet
             */
            this.tableAdapterManager.CategoriesTableAdapter.EnKicsiInsertQuery_m("Minta ketegória", "Minta ketegória leírás", null);
            this.categoriesTableAdapter.Fill(this.nORTHWNDDataSet.Categories);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tableAdapterManager.CategoriesTableAdapter.MintakategoriakTorlese();
            this.categoriesTableAdapter.Fill(this.nORTHWNDDataSet.Categories);

        }
    }
}
