using System;

namespace YSA
{
    class Program
    {
        static int ArakatmanDugumSayisi = 2;
        static int[,] Giris = {
                    {0,0,1,1},
                    {0,1,0,1},
                    
         };
        static int[] Beklenen = { 0, 1, 1, 1, };
        static int istenilenItr = 4;




        static double E = 0.0;
        static double Emax = 0.05;
        static double[,] GirisAgirlik = new double[Giris.GetLength(0), ArakatmanDugumSayisi];
        public static double[] AraKatmanAgirlik = new double[ArakatmanDugumSayisi];
        static double[] AraKatmancikis = new double[ArakatmanDugumSayisi];
        static double CikisKatmanCikis;
        static double AraHataSinyal;
        static double GirisHataSinyal;
        static int sayac1 = 0;
        static int sayac2 = 0;
        static int p = 0;
        static double Mu = 0.5;



        
        private static double[,] GirisKatmanAgirlikRandomOlustur() {
            Random rastgele = new Random();
            if (sayac1 == 0)
            {
                for (int i = 0; i < GirisAgirlik.GetLength(0); i++)
                {
                    for (int j = 0; j < GirisAgirlik.GetLength(1); j++)
                    {
                        GirisAgirlik[i, j] =Math.Round(rastgele.NextDouble(),5);
                    }
                }
               
                sayac1++;
                return GirisAgirlik;
                
            }
            else
            {
                return GirisAgirlik;
            }
        }
        private static double[] AraKatmanAgirlikRandomOlustur() {
            Random rastgele = new Random();
            if (sayac2 == 0)
            {
                    for (int j = 0; j < AraKatmanAgirlik.Length; j++)
                    {
                         AraKatmanAgirlik[j] =Math.Round(rastgele.NextDouble(),5);
                    }
                

                sayac2++;
                return AraKatmanAgirlik;

            }
            else
            {
                return AraKatmanAgirlik;
            }
        }
        private static void YazdirIkiBoyutlu(double[,] dizi) {
            for (int i = 0; i < dizi.GetLength(0); i++)
            {
                for (int j = 0; j < dizi.GetLength(1); j++)
                {
                    Console.Write(dizi[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
        private static void YazdirTekBoyutlu(double[] dizi) {
            for (int i = 0; i < dizi.Length; i++)
            {
                Console.Write(dizi[i]+" ");
            }
            Console.WriteLine();
        }
        private static double[] GirisKatmanAgirlikCarpim()
        {
            double toplam = 0.0;
           
            for (int i = 0; i < ArakatmanDugumSayisi; i++)
            {
                for (int j = 0; j <Giris.GetLength(0); j++)
                {
                    toplam += Giris[j, p] * GirisAgirlik[j,i];
                }
                toplam = (1 / (1 + Math.Pow(Math.E, (-toplam))));
                AraKatmancikis[i] =Math.Round(toplam,5);
                toplam = 0;
            } 
           

            return AraKatmancikis;
        }
        private static double CikisKatmanSonuc() {
            for (int i = 0; i < AraKatmancikis.Length; i++)
            {
                CikisKatmanCikis += AraKatmancikis[i] * AraKatmanAgirlik[i];
            }

            CikisKatmanCikis =Math.Round( (1 / (1 + Math.Pow(Math.E, (-CikisKatmanCikis)))),3);

            return CikisKatmanCikis;
        }
        private static void CycleHesaplama() {
            E = Math.Round((E + 0.5 * Math.Pow((CikisKatmanSonuc() - Beklenen[p]),2)),5);
        }
        private static double GirisKatmanHataSinyal() {
            
            GirisHataSinyal =Math.Round( ((Beklenen[p] - CikisKatmanSonuc()) * (1 - CikisKatmanSonuc()) * CikisKatmanSonuc()),5);
            return GirisHataSinyal;
                
        }
        private static double AraKatmanHataSinyal() {
           
            for (int i = 0; i < AraKatmancikis.Length; i++)
            {
                AraHataSinyal += Math.Round(AraKatmancikis[i]*(1-AraKatmancikis[i]) * AraKatmanAgirlik[i]* GirisKatmanHataSinyal(),3);
            }

             return AraHataSinyal;
        }
        private static void AraKatmanAgirlikGuncelle() {
            for (int i = 0; i < AraKatmanAgirlik.Length; i++)
            {
                AraKatmanAgirlik[i] =Math.Round( (AraKatmanAgirlik[i] +( Mu * AraKatmanHataSinyal() * AraKatmancikis[i])),5);
            }

        }
        private static void GirisKatmanAgirlikGuncelle() {
            for (int i = 0; i < GirisAgirlik.GetLength(1); i++)
            {
                for (int j = 0; j < GirisAgirlik.GetLength(0); j++)
                {
                    GirisAgirlik[j,i] = Math.Round((GirisAgirlik[j,i] + (Mu * GirisKatmanHataSinyal() * Giris[j,p])),5);
                }
            }
        }








        static void Main(string[] args)
        {
              Console.WriteLine("Giris Katman Agirlik: ");
              YazdirIkiBoyutlu(GirisKatmanAgirlikRandomOlustur());
              Console.WriteLine("Ara Katman Random Agirlik: ");
              YazdirTekBoyutlu(AraKatmanAgirlikRandomOlustur());
         

            for (int i = 0; i < Giris.GetLength(1); i++)
            {
             
       
             Console.WriteLine("--------------" + (i + 1) + ". iterasyon -----------------");
        
            Console.WriteLine("Ara Katman Cikis: ");
            YazdirTekBoyutlu(GirisKatmanAgirlikCarpim());
            Console.WriteLine("Cikis Katman Sonuc");
            Console.WriteLine(CikisKatmanSonuc());
            Console.WriteLine("Cycle: ");
            CycleHesaplama();
            Console.WriteLine(E);
            Console.WriteLine("Giris Katman Hata Sinyal");
            Console.WriteLine(GirisKatmanHataSinyal());
            Console.WriteLine("Ara Katman Hata Sinyal");
            Console.WriteLine(AraKatmanHataSinyal());
            AraKatmanAgirlikGuncelle();
            Console.WriteLine("Güncel Giris Katman Agirlik: ");
            YazdirIkiBoyutlu(GirisAgirlik);
            Console.WriteLine("Güncel Ara Katman Agirlik: ");
            YazdirTekBoyutlu(AraKatmanAgirlik);
            GirisKatmanAgirlikGuncelle();
                p++;
                if (E < Emax)
                {
                    break;
                }

                if (istenilenItr == (i))
                {
                    break;
                }

            }

            Console.ReadKey();

           
        }
    }
}
