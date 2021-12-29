// 2021. október 22. 8:00 INFORMATIKAI ISMERETEK EMELT SZINTŰ GYAKORLATI VIZSGA
// 2. feladat: Életjáték szimulátor1
using System;

class EletjatekSzimulator
{
    private int[ , ] Matrix;    
    private int      OszlopokSzama;   
    private int      SorokSzama;      
   
    public EletjatekSzimulator(int sorok_szama, int oszlopok_szama)
    {
        this.OszlopokSzama = oszlopok_szama + 2;
        this.SorokSzama    = sorok_szama + 2;
        this.Matrix = new int[SorokSzama, OszlopokSzama];
        
        var rnd = new Random();
        for(int sor=1; sor < SorokSzama-1; sor++)
        {
            for(int oszlop=1; oszlop < OszlopokSzama-1; oszlop++)
            {  
                Matrix[sor, oszlop] = rnd.Next(0, 2);
            }
        }
    } // --- end of EletjatekSzimulator method ---

    private void Megjelenit() 
    {   
        for(int sor=0; sor<SorokSzama; sor++)
        {  
            for(int oszlop=0; oszlop<OszlopokSzama; oszlop++)
            {
                Console.SetCursorPosition(sor, oszlop);
                if(sor==0 || sor==SorokSzama-1 || oszlop==0 || oszlop == OszlopokSzama-1)
                {
                    Console.Write("X");
                }
                else
                {
                    if (Matrix[sor, oszlop] == 0)  Console.Write(" ");
                    else                   Console.Write("S");
                }
            }
        }
    } // --- end of Megjelenit method ---

    private int uj_cella_ertek(int sor, int oszlop)
    {   
        var sz =  Matrix[sor-1, oszlop-1] + Matrix[sor-1, oszlop+0] + Matrix[sor-1, oszlop+1]  
                + Matrix[sor+0, oszlop-1]                           + Matrix[sor+0, oszlop+1] 
                + Matrix[sor+1, oszlop-1] + Matrix[sor+1, oszlop+0] + Matrix[sor+1, oszlop+1];
        
        var letezik     = Matrix[sor, oszlop] == 1;
        var tuleli      = letezik && (sz == 2 || sz == 3);
        var uj_szuletik = !letezik && sz == 3;
        return tuleli || uj_szuletik ? 1 : 0;
    } //--- end of uj_cella_ertek method ---
    
    private void KovetkezoAllapot()
    {
        var ujmatrix = new int[SorokSzama, OszlopokSzama];
        for( int sor=1; sor < SorokSzama-1; sor++ )
        {
            for( int oszlop=1; oszlop < OszlopokSzama-1; oszlop++)
            {
                ujmatrix[sor, oszlop] = uj_cella_ertek(sor, oszlop);
            }
        }
        Array.Copy(ujmatrix, Matrix, ujmatrix.Length);
       
    } // --- end of KovetkezoAllapot method ---
    
    public void Run() 
    {
        //Console.Clear();
        Megjelenit();
        KovetkezoAllapot();
        System.Threading.Thread.Sleep(500);
    } // --- end of Run method ---

} // *** end of EletjatekSzimulator class ***

class Program 
{
    public static void Main (string[] args) 
    {
        var m = new EletjatekSzimulator(20, 20);
        Console.Clear();
        while(!Console.KeyAvailable)
        {
            m.Run();
        }
    } // --- end of method Main ---
} // *** end of Program class ***