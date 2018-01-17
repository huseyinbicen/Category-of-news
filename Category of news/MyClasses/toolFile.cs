using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Category_of_news.MyClasses
{
    public class toolFile
    {


        public toolFile()
        {

        }


        /// <summary>
        /// Data Teach klasöründe bulunan tüm txt dosyaları bir diziye atan metoddur
        /// </summary>
        /// <returns></returns>
        private static List<string> Method()
        {
            List<string> Haberler = new List<string>();
            String[] Kategoriler = HaberKategorileri();
            StreamReader sr;
            String yol = "Data Teach", metin = "";
            for (int i = 0; i < Kategoriler.Length; i++)
            {
                yol += "\\" + Kategoriler[i];
                for (int k = 0; k < 10; k++)// Buradaki 10: Bir kategori dosya içindeki makale sayısıdır.
                {
                    yol += "\\" + (k + 1) + ".txt";
                    //metin = BayAdlari[i] + " = " + (k+1);
                    sr = new StreamReader(yol, Encoding.Default, true);
                    metin += sr.ReadToEnd();
                    Haberler.Add(metin);
                    yol = "Data Teach\\" + Kategoriler[i];
                    metin = "";
                }
                yol = "Data Teach";
            }
            return Haberler;
        }



    
        /// <summary>
        /// Data Teach klasöründe bulunan kategorilerin klasör isimlerinin bir diziye atıyor..
        /// </summary>
        /// <returns></returns>
        private static string[] HaberKategorileri()
        {
            String[] Kategoriler;
            String Yol = @"Data Teach";

            if (!Directory.Exists(Yol)) // Dosya varmı diye bakar
            {
                MessageBox.Show("Girilen Yolda Dosya Bulunamadı..");
            }

            string[] directorie_Categori = Directory.GetDirectories(Yol); //Klasördeki klasörleri getirir
            Kategoriler = new string[directorie_Categori.Length];
            for (int i = 0; i < directorie_Categori.Length; i++)
            {
                Kategoriler[i] = Path.GetFileName(directorie_Categori[i]);
            }
            return Kategoriler;
        }



        public void DosyayaYaz(float[,] dizi)
        {
            StreamWriter dosya = new StreamWriter(@"C:\Users\Hüseyin\Desktop\Data Teach.txt");
            for (int i = 0; i < dizi.GetLength(0); i++)
            {
                for (int k = 0; k < dizi.GetLength(1); k++)
                {
                    dosya.Write(dizi[i, k].ToString("0.##"));
                    dosya.Write(" ; ");
                }
                dosya.WriteLine("");
            }
            dosya.Close();
            MessageBox.Show("Dosya masaüstüne yazıldı");
        }



        public String[] getMakale()
        {
            return Method().ToArray();
        }

        public String[] getKatetori()
        {
            return HaberKategorileri().ToArray();
        }


    }
}
