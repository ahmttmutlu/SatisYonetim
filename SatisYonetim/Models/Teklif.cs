namespace SatisYonetim.Models
{
    public class Teklif
    {
        public int TeklifId { get; set; }
        public string TeklifAdi { get; set; }
        public string MusteriAdi { get; set; }
        public int Fiyat { get; set; }
        public int DurumId { get; set; }

        //public virtual ICollection<Durum> Durumlar { get; set; }= new List<Durum>();

    }
}
