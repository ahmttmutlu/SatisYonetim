namespace SatisYonetim.Models
{
    public class Durum
    {
        public int DurumId { get; set; }
        public int DurumSira { get; set; }
        public string DurumAdi { get; set; }
        public virtual ICollection<Teklif> Teklifler { get; set; } = new List<Teklif>();
    }
}
